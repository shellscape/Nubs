using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic.MyServices;

namespace Nubs {

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT {
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	public class WindowEventArgs : EventArgs {

		public IntPtr Handle { get; set; }

	}

	public class WindowBoundsEventArgs : WindowEventArgs {

		public RECT Rect { get; set; }
		public RECT LastRect { get; set; }

	}

	public class WindowManager {

		private Dictionary<IntPtr, RECT> _windows = new Dictionary<IntPtr, RECT>();
		private Rectangle _screen = SystemInformation.VirtualScreen;

		public event EventHandler<WindowEventArgs> WindowActivated;
		public event EventHandler<WindowEventArgs> WindowDestroyed;
		public event EventHandler<WindowEventArgs> WindowMoveSize;
		public event EventHandler<WindowBoundsEventArgs> WindowMoved;
		public event EventHandler<WindowBoundsEventArgs> WindowNeedsSumNubbin;

		#region .    Win32

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool EnumWindows(EnumWindowProc lpEnumFunc, IntPtr lParam);

		[DllImport("coredll.dll")]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder buf, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

		[DllImport("coredll.dll", SetLastError = true)]
		private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool IsWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool IsWindowVisible(IntPtr hWnd);

		/// <summary>
		/// Delegate for the EnumChildWindows method
		/// </summary>
		/// <param name="hWnd">Window handle</param>
		/// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
		/// <returns>True to continue enumerating, false to bail.</returns>
		public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

		public const int GW_OWNER = 4;

		public const int GWL_EXSTYLE = -20;
		public const int GWL_HWNDPARENT = -8;
		public const int GWL_STYLE = -16;
		public const int GWL_USERDATA = -21;

		public const int HCBT_MOVESIZE = 0;
		public const int HCBT_DESTROYWND = 4;
		public const int HCBT_ACTIVATE = 5;

		public const int WS_CHILD = 0x40000000;

		public const int WS_EX_APPWINDOW = 0x00040000;
		public const int WS_EX_TOOLWINDOW = 0x0080;

		#endregion

		public void Init() {

			EnumWindowProc callback = new EnumWindowProc(EnumProc);
			EnumWindows(callback, IntPtr.Zero);
		}

		private Boolean EnumProc(IntPtr hWnd, IntPtr lParam) {

			if (IsAppWindow(hWnd, false)) {
				RECT r = default(RECT);

				GetWindowRect(hWnd, out r);

				if (!_windows.ContainsKey(hWnd)) {
					_windows.Add(hWnd, r);
				}
				else {
					_windows[hWnd] = r;
				}
			}

			return true;
		}

		/// <summary>
		/// Determines whether the specified WindowHandle is real TaskWindow:
		/// That is, one that should show up on the TaskBar
		/// </summary>
		/// <param name="hWnd">The Window Handle</param>
		/// <param name="includeToolWindows">If set to <c>true</c> include Tool windows.</param>
		/// <returns>
		///	 <c>true</c> if the specified window is a TaskWindow; otherwise, <c>false</c>.
		/// </returns>
		private Boolean IsAppWindow(IntPtr hWnd, Boolean includeToolWindows) {

			bool result = true;

			if (IsWindow(hWnd) && IsWindowVisible(hWnd)) {
				uint exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

				if ((exStyle & WS_EX_APPWINDOW) != 0) {
					result = true;
				}
				else {
					uint style = GetWindowLong(hWnd, GWL_STYLE);
					Boolean isParent = GetWindowLong(hWnd, GWL_HWNDPARENT) != 0;
					Boolean isOwner = GetWindow(hWnd, GW_OWNER) != IntPtr.Zero;

					// if it's visible and doesn't have a parent or owner ... it's PROBABLY an app

					if (!isParent && !isOwner && IsWindowVisible(hWnd) && (style & WS_CHILD) == 0) {

						// If it is an WS_EX_APPWINDOW force a pass?
						if ((exStyle & WS_EX_TOOLWINDOW) != 0 && (!includeToolWindows)) {
							result = false;
						}
						else {
							result = true;
						}
					}
					else {
						result = false;
					}
				}

				if (result) {
					StringBuilder sb = new StringBuilder(255);

					GetClassName(hWnd, sb, 255);

					String className = sb.ToString();

					if (className.Equals("WindowsScreensaverClass") || className.Equals("tooltips_class32")) {
						result = false;
					}
				}
			}
			else {
				result = false;
			}

			// i can't recall what this was for, but I do remember that this is some sort of hidden window.
			if (GetWindowLong(hWnd, GWL_USERDATA) == 1229407553) {
				result = false;
			}

			return result;
		}

		private Boolean ValidEdge(Rectangle WindowRect) {

			int[] numbers = new int[4];
			int leftDiff = 0;
			int rightDiff = 0;
			int topDiff = 0;
			int bottomDiff = 0;

			if (WindowRect.Left < _screen.Left) {
				leftDiff = Math.Abs(_screen.Left - WindowRect.Left);
			}

			if (WindowRect.Right > _screen.Right) {
				rightDiff = WindowRect.Right - _screen.Right;
			}

			if (WindowRect.Top < _screen.Top) {
				topDiff = Math.Abs(_screen.Top - WindowRect.Top);
			}

			if (WindowRect.Bottom > _screen.Bottom) {
				bottomDiff = WindowRect.Bottom - _screen.Bottom;
			}

			numbers[0] = leftDiff;
			numbers[1] = rightDiff;
			numbers[2] = topDiff;
			numbers[3] = bottomDiff;

			Array.Sort(numbers);

			if (numbers[3] == leftDiff) {
				if (!Config.Current.EdgeLeft) {
					return false;
				}
				if (Math.Abs(leftDiff) < Config.Current.NubDistance) {
					return false;
				}
			}
			else if (numbers[3] == rightDiff) {
				if (!Config.Current.EdgeRight) {
					return false;
				}
				if (Math.Abs(rightDiff) < Config.Current.NubDistance) {
					return false;
				}
			}
			else if (numbers[3] == topDiff) {
				if (!Config.Current.EdgeTop) {
					return false;
				}
				if (Math.Abs(topDiff) < Config.Current.NubDistance) {
					return false;
				}
			}
			else if (numbers[3] == bottomDiff) {
				if (!Config.Current.EdgeBottom) {
					return false;
				}
				if (Math.Abs(bottomDiff) < Config.Current.NubDistance) {
					return false;
				}
			}

			return true;

		}
		
		private void OnWindowActivated(IntPtr hWnd) {
			if (WindowActivated != null) {
				WindowActivated(this, new WindowEventArgs() { Handle = hWnd });
			}
		}

		private void OnWindowDestroyed(IntPtr hWnd) {
			if (WindowDestroyed != null) {
				WindowDestroyed(this, new WindowEventArgs() { Handle = hWnd });
			}
		}

		private void OnWindowMoveSize(IntPtr hWnd) {

			if (WindowMoveSize != null) {
				WindowMoveSize(this, new WindowEventArgs() { Handle = hWnd });
			}

			if (!IsAppWindow(hWnd, false)) {
				return;
			}

			RECT r = default(RECT);

			GetWindowRect(hWnd, out r);

			if (r.Left < _screen.Left | r.Top < _screen.Top | r.Right > _screen.Right | r.Bottom > _screen.Bottom) {

				Rectangle windowRect = new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);

				if (!ValidEdge(windowRect)) {
					return;
				}

				if (WindowNeedsSumNubbin != null) {
					RECT lastRect = r;

					if (_windows.ContainsKey(hWnd)) {
						lastRect = _windows[hWnd];
					}

					WindowNeedsSumNubbin(this, new WindowBoundsEventArgs() { Handle = hWnd, Rect = r, LastRect = lastRect });
				}
			}
			else {

				if (!((r.Left < _screen.Left | r.Left > _screen.Right) | (r.Top < _screen.Top | r.Top > _screen.Bottom))) {
					if (!_windows.ContainsKey(hWnd)) {
						_windows.Add(hWnd, r);
					}
					else {
						_windows[hWnd] = r;
					}

					if (WindowMoved != null) {
						WindowMoved(this, new WindowBoundsEventArgs() { Handle = hWnd, Rect = r });
					}
				}
			}
		}

		public void HandleDisplayChange() {
			_screen = SystemInformation.VirtualScreen;
		}

		public void HandleHookMessage(IntPtr hWnd, IntPtr lParam) {

			int param = lParam.ToInt32();

			if (param == HCBT_ACTIVATE) {
				OnWindowActivated(hWnd);
			}
			else if (param == HCBT_DESTROYWND) {
				OnWindowDestroyed(hWnd);
			}
			else if (param == HCBT_MOVESIZE) {
				OnWindowMoveSize(hWnd);
			}

		}

	}
}

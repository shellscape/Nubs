using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Nubs.Themes;

namespace Nubs.Forms {

	public partial class Nub : Form {

		private Rectangle _lastWindowRect = new Rectangle();
		private Boolean _loading = true;

		#region .    Win32

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

		const int HWND_TOPMOST = -1;
		const int HWND_NOTOPMOST = -2;

		const int SWP_NOMOVE = 0x0002;
		const int SWP_NOSIZE = 0x0001;

		const int WA_CLICKACTIVE = 2;

		const int WM_ACTIVATE = 0x0006;
		const int WM_DISPLAYCHANGE = 0x007E;
		const int WM_MOVING = 0x0216;
		const int WM_SIZING = 0x0214;

		const int WS_EX_LAYERED = 0x00080000;
		const int WS_EX_TOOLWINDOW = 0x0080;

		[StructLayoutAttribute(LayoutKind.Sequential)]
		private struct WM_MOVINGRECT {
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		static ushort LOWORD(uint value) {
			return (ushort)(value & 0xFFFF);
		}

		#endregion

		public Nub() : this(false) { }

		public Nub(Boolean recover) {
			InitializeComponent();
		}

		protected override CreateParams CreateParams {
			get {
				CreateParams @params = base.CreateParams;

				@params.ExStyle = WS_EX_LAYERED | WS_EX_TOOLWINDOW;

				return @params;
			}
		}

		public String WindowText {
			get {
				return String.Empty;
			}
		}

		public WindowEdge WindowEdge { get; set; }

		public IntPtr WindowHandle { get; set; }

		public Rectangle WindowRect {
			get { return _lastWindowRect; }
			set {
				_lastWindowRect = value;

				// TODO
				//  Nub.WindowLastRect = new Point(StoredRect.Left, StoredRect.Top);

				//  Xml.XmlNode WindowNode = _XmlMan.XmlDoc.CreateNode(Xml.XmlNodeType.Element, "window", string.Empty);
				//  Xml.XmlNode HandleNode = _XmlMan.XmlDoc.CreateNode(Xml.XmlNodeType.Element, "handle", string.Empty);
				//  Xml.XmlNode CoordsNode = _XmlMan.XmlDoc.CreateNode(Xml.XmlNodeType.Element, "coords", string.Empty);

				//  HandleNode.InnerText = hWnd.ToInt32.ToString;
				//  CoordsNode.InnerText = string.Concat(Nub.WindowLastRect.X, ",", Nub.WindowLastRect.Y);
				//  WindowNode.AppendChild(HandleNode);
				//  WindowNode.AppendChild(CoordsNode);

				//  Xml.XmlNode Node = _XmlMan.Node("nubs/recover");
				//  Node.AppendChild(WindowNode);

				//  _XmlMan.Save();
			}
		}

		public Rectangle LastWindowRect { get; set; }

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);

			if (Config.Current.NubsTopmost) {
				SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, (SWP_NOMOVE | SWP_NOSIZE));
			}

			Resize();
			Position();

			_loading = false;
		}

		protected override void OnClosing(CancelEventArgs e) {

			// TODO - dispose of any resources

			// TODO
			//ArrayList Windows = _XmlMan.List("nubs/recover/window", true);

			//foreach (Xml.XmlNode Node in Windows) {
			//  if (Node.SelectSingleNode("handle").InnerText == StringHandle) {
			//    Node.ParentNode.RemoveChild(Node);

			//    _XmlMan.Save();

			//    Node = null;

			//    break; // TODO: might not be correct. Was : Exit For

			//  }

			//}			

			base.OnClosing(e);
		}

		protected override void WndProc(ref Message m) {

			WM_MOVINGRECT lParam = default(WM_MOVINGRECT);
			
			switch (m.Msg) {
				case WM_ACTIVATE:
					if (LOWORD((uint)m.WParam) == WA_CLICKACTIVE) {
						//_TriggerMouseSequence = true;
					}

					break;

				case WM_SIZING:

					if (_loading) {
						break;
					}

					lParam = (WM_MOVINGRECT)m.GetLParam(typeof(WM_MOVINGRECT));

					switch (this.WindowEdge) {
						case Nubs.WindowEdge.Top:
							lParam.Top = WindowManager.Screen.Top;
							lParam.Bottom = Theme.Current.Horizontal.Middle.Height;
							break;

						case Nubs.WindowEdge.Bottom:
							lParam.Bottom = lParam.Top + Theme.Current.Horizontal.Middle.Height;
							break;

						case Nubs.WindowEdge.Left:
						case Nubs.WindowEdge.Right:
							lParam.Right = lParam.Left + Theme.Current.Horizontal.Middle.Width;

							break;
					}

					Marshal.StructureToPtr(lParam, m.LParam, true);
					break;

				case WM_MOVING:

					Rectangle screen = WindowManager.Screen;

					//_TriggerMouseSequence = false;

					lParam = (WM_MOVINGRECT)m.GetLParam(typeof(WM_MOVINGRECT));

					switch (this.WindowEdge) {
						case Nubs.WindowEdge.Top:
							lParam.Top = screen.Top;
							lParam.Bottom = Theme.Current.Horizontal.Middle.Height;
							break;

						case Nubs.WindowEdge.Right:
							lParam.Left = screen.Right - Width;
							lParam.Right = lParam.Left + Width;
							break;

						case Nubs.WindowEdge.Bottom:
							lParam.Top = screen.Bottom - Height;
							lParam.Bottom = lParam.Top + Height;
							break;

						case Nubs.WindowEdge.Left:
							lParam.Left = screen.Left;
							lParam.Right = screen.Left + screen.Width;
							break;

					}

					Marshal.StructureToPtr(lParam, m.LParam, true);

					break;
			}

			base.WndProc(ref m);
		}

		#region .    Menu Events

		private void _MenuItemShow_Click(object sender, EventArgs e) {

			_MenuItemShow.Visible = false;
			_MenuItemHide.Visible = true;

			ToggleTarget();
		}

		private void _MenuItemHide_Click(object sender, EventArgs e) {

			_MenuItemShow.Visible = true;
			_MenuItemHide.Visible = false;

			ToggleTarget();
		}

		private void _MenuItemClose_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion

		private void ToggleTarget() {

		}

		private void Position() {

			switch (WindowEdge) {
				case Nubs.WindowEdge.Top:
					this.Top = WindowManager.Screen.Top;
					break;
				case Nubs.WindowEdge.Right:
					this.Left = WindowManager.Screen.Right - this.Width;
					break;
				case Nubs.WindowEdge.Bottom:
					this.Top = WindowManager.Screen.Bottom - this.Height;
					break;
				case Nubs.WindowEdge.Left:
					this.Left = WindowManager.Screen.Left;
					break;
			}

		}

		private void Resize() {
			this.Size = Utilities.ThemeHelper.CalculateSize(this.WindowText, this.WindowEdge);
		}

		public void Renub(Rectangle windowRect) {
			// TODO reshow this form
		}
	}
}

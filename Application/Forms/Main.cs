using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Shellscape.Utilities;

namespace Nubs.Forms {
	public partial class Main : Form {

		private WindowManager _windowManager = new WindowManager();

		private Bitmap _buffer = null;

		private static Dictionary<IntPtr, Forms.Nub> _nubs = new Dictionary<IntPtr, Forms.Nub>();

		#region .    Win32    

		[DllImport("NubHooks.dll")]
		private static extern bool InstallShellHook(IntPtr hWnd, int uintMsg);

		[DllImport("NubHooks.dll")]
		private static extern bool UninstallShellHook(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "RegisterWindowMessageA")]
		private static extern Int32 RegisterWindowMessage(string lpString);

		private const int WM_DISPLAYCHANGE = 0x007E;

		private int NUB_CBTHOOK;

		#endregion

		public Main() {
			InitializeComponent();

			this.Icon = ResourceHelper.GetIcon("nubs.ico");
			this.Location = new Point(-10000, -10000);
			this.Text = AssemblyMeta.Title;

			// TODO
			//RestoreWindows();

			_windowManager.Init();

			NUB_CBTHOOK = RegisterWindowMessage("NUB_CBTHOOK");

			InstallShellHook(this.Handle, NUB_CBTHOOK);
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);

			Shellscape.CodePackShim.AllowTaskbarWindowMessagesThroughUIPI();
			
		}

		protected override void WndProc(ref System.Windows.Forms.Message m) {
			base.WndProc(ref m);

			if(m.Msg == WM_DISPLAYCHANGE) {
				_windowManager.HandleDisplayChange();
			}
		 else if(m.Msg == NUB_CBTHOOK){
			 _windowManager.HandleHookMessage(m.LParam, m.WParam);
			}

		}

		private void window_Activated(object sender, WindowEventArgs e) {
			if (_nubs.ContainsKey(e.Handle)) {
			//  Console.WriteLine("Contains");
			//  frmNub Nub = _Nubs(hWnd);

			//  if (!Nub.WindowVisible) {
			//    Nub.RestoreWindow();
			//  }
			}
		}

		private void window_Destroyed(object sender, WindowEventArgs e) {
			if (_nubs.ContainsKey(e.Handle)) {
				Nub nub = _nubs[e.Handle];
				nub.Close();
			}
		}

		private void window_Moved(object sender, WindowBoundsEventArgs e) {
			if (_nubs.ContainsKey(e.Handle)) {
				Nub nub = _nubs[e.Handle];

				nub.LastWindowRect = e.LastRect.ToRectangle();

			}
		}

		private void window_NeedsSumNubbin(object sender, WindowBoundsEventArgs e) {

			Nub nub = null;
			Rectangle windowRect = e.Rect.ToRectangle();

			if (_nubs.ContainsKey(e.Handle)) {

				nub = _nubs[e.Handle];
				nub.Renub(windowRect);

				return;
			}

			nub = new Nub() {
				WindowEdge = e.Edge,
				WindowHandle = e.Handle,
				WindowRect = windowRect,
				LastWindowRect = e.LastRect.ToRectangle()
			};
			
			_nubs.Add(e.Handle, nub);

			nub.FormClosing += nub_FormClosing;
			nub.Show();
		}

		private void nub_FormClosing(object sender, FormClosingEventArgs e) {

			Nub nub = sender as Nub;
			
			_nubs.Remove(nub.WindowHandle);

		}

		
		
	}
}

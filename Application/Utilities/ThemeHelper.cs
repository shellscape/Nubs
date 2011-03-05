using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Nubs.Forms;
using Nubs.Themes;

namespace Nubs.Utilities {
	internal static class ThemeHelper {

		#region .    Win32

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteDC(IntPtr hdc);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("coredll.dll", EntryPoint = "ReleaseDC", SetLastError = true)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINTAPI pptDst, ref ST_SIZE psize, IntPtr hdcSrc, ref POINTAPI pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, int dwFlags);

		[StructLayout(LayoutKind.Sequential)]
		public struct BLENDFUNCTION {
			public byte BlendOp;
			public byte BlendFlags;
			public byte SourceConstantAlpha;
			public byte AlphaFormat;

			public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format) {
				BlendOp = op;
				BlendFlags = flags;
				SourceConstantAlpha = alpha;
				AlphaFormat = format;
			}
		}

		public const int AC_SRC_ALPHA = 0x01;
		public const int AC_SRC_OVER = 0x00;
		public const int ULW_ALPHA = 2;

		public struct POINTAPI {
			public int x;
			public int y;
		}

		public struct ST_SIZE {
			public int vSize;
			public int hSize;
		}


		#endregion

		//private void b_Extract_Click(object sender, EventArgs e) {
		//  SevenZipExtractor.SetLibraryPath(@"C:\Program Files\7-Zip\7z.dll");
		//  string fileName = tb_ExtractArchive.Text;
		//  string directory = tb_ExtractDirectory.Text;
		//  var extr = new SevenZipExtractor(fileName);
		//  pb_ExtractWork.Maximum = (int)extr.FilesCount;
		//  extr.Extracting += new EventHandler<ProgressEventArgs>(extr_Extracting);
		//  extr.FileExtractionStarted += new EventHandler<FileInfoEventArgs>(extr_FileExtractionStarted);
		//  extr.FileExists += new EventHandler<FileOverwriteEventArgs>(extr_FileExists);
		//  extr.ExtractionFinished += new EventHandler<EventArgs>(extr_ExtractionFinished);
		//  extr.BeginExtractArchive(directory);
		//}

		//void extr_ExtractionFinished(object sender, EventArgs e) {
		//  pb_ExtractWork.Style = ProgressBarStyle.Blocks;
		//  pb_ExtractProgress.Value = 0;
		//  pb_ExtractWork.Value = 0;
		//  l_ExtractProgress.Text = "Finished";
		//  (sender as SevenZipExtractor).Dispose();
		//}

		public static Size CalculateSize(String windowText, WindowEdge edge) {

			Size size = new Size();
			ThemeImageLayout layout = null;
			SizeF textSize = new SizeF();

			using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) {
				// add an extra wide character to make sure there is room enough.
				textSize = g.MeasureString(String.Concat(windowText, "W"), Theme.Current.Font);
			}

			if(edge == WindowEdge.Left || edge == WindowEdge.Right){
				layout = Theme.Current.Vertical;

				size.Width = layout.Middle.Width;
				size.Height = layout.Top.Height + (int)textSize.Width + layout.Bottom.Height;
			}
			else{
				layout = Theme.Current.Horizontal;

				size.Height = layout.Middle.Height;
				size.Width = layout.Top.Width + (int)textSize.Width + layout.Bottom.Width;
			}

			return size;
		}

		public static void Draw(Nub nub, Graphics g) {

			SizeF textSize = g.MeasureString(String.Concat(nub.WindowText, "W"), Theme.Current.Font);

		}

		public virtual void UpdateWindow(Nub nub, Bitmap buffer) {

			if (buffer == null || buffer.PixelFormat == 0) {
				return;
			}

			IntPtr dcMemory = default(IntPtr);
			dcMemory = CreateCompatibleDC(IntPtr.Zero);

			// calculate the new window position/size based on the bitmap size
			POINTAPI ptWindowScreenPosition = default(POINTAPI);
			ptWindowScreenPosition.x = nub.Left;
			ptWindowScreenPosition.y = nub.Top;

			ST_SIZE szWindow = default(ST_SIZE);
			szWindow.hSize = nub.Height;
			szWindow.vSize = nub.Width;

			// setup the blend function
			BLENDFUNCTION blendPixelFunction = new BLENDFUNCTION() {
				BlendOp = AC_SRC_OVER,
				BlendFlags = 0,
				SourceConstantAlpha = 255,
				AlphaFormat = AC_SRC_ALPHA
			};

			POINTAPI ptSrc = default(POINTAPI);
			// start point of the copy from dcMemory to dcScreen
			ptSrc.x = 0;
			ptSrc.y = 0;

			//This is where anything needs to get drawn to be shown or updated on the window
			using (Graphics g = Graphics.FromImage(buffer)) {
				Draw(nub, g);
			}

			// perform the alpha blend
			IntPtr useBmp = IntPtr.Zero;
			useBmp = buffer.GetHbitmap(Color.FromArgb(0));
			IntPtr bmpOld = IntPtr.Zero;
			bmpOld = SelectObject(dcMemory, useBmp);

			try {
				UpdateLayeredWindow(
					nub.Handle,
					IntPtr.Zero,
					ref ptWindowScreenPosition,
					ref szWindow,
					dcMemory,
					ref ptSrc,
					0,
					ref blendPixelFunction,
					ULW_ALPHA
				);

			}
			catch (System.ObjectDisposedException) { }
			catch (Exception) { }

			SelectObject(dcMemory, bmpOld);
			DeleteObject(useBmp);
			DeleteObject(bmpOld);
			ReleaseDC(IntPtr.Zero, dcMemory);
			DeleteDC(dcMemory);

			dcMemory = IntPtr.Zero;
			bmpOld = IntPtr.Zero;
			useBmp = IntPtr.Zero;
		}
	}
}

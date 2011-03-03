using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nubs.Utilities {
	internal class ThemeHelper {

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

	}
}

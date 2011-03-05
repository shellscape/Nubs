using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs.Themes {

	[DataContract(Name="theme")]
	public class Theme {

		public static Theme Current { get; set; }

		[DataMember(Name="name")]
		public String Name { get; set; }

		[DataMember(Name="author")]
		public String Author { get; set; }

		[DataMember(Name = "version")]
		public String Version { get; set; }

		[DataMember(Name = "date")]
		public String Date { get; set; }

		[DataMember(Name = "images")]
		public ThemeImages Images { get; set; }

		[DataMember(Name = "text")]
		public ThemeLayout TextLayout { get; set; }

		[DataMember(Name = "icon")]
		public ThemeLayout IconLayout { get; set; }

		[DataMember(Name = "horizontal")]
		public ThemeImageLayout Horizontal { get; set; }

		[DataMember(Name = "vertical")]
		public ThemeImageLayout Vertical { get; set; }

		[DataMember(Name="font")]
		private ThemeFont ThemeFont { get; set; }

		// non-serialized members

		public Font Font { get; set; }
		public Color FontColor { get; set; }

		public void Init() {
			// init font
			this.Font = new Font(ThemeFont.Name, ThemeFont.Size);
			this.FontColor = ColorTranslator.FromHtml(ThemeFont.Color);

			// init bitmaps
			// TODO
		}

	}

}

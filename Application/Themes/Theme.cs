using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs.Themes {

	internal class Theme {

		public String Name { get; set; }
		public String Author { get; set; }
		public String Version { get; set; }
		public String Date { get; set; }

		public ThemeImages Images { get; set; }
		public ThemeLayout Text { get; set; }
		public ThemeLayout Horizontal { get; set; }
		public ThemeLayout Vertical { get; set; }

	}

}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs.Themes {
	
	[DataContract(Name="images")]
	internal class ThemeImages {

		[DataMember(Name="top")]
		private String ImageTop { get; set; }

		[DataMember(Name = "right")]
		private String ImageRight { get; set; }

		[DataMember(Name = "bottom")]
		private String ImageBottom { get; set; }

		[DataMember(Name = "left")]
		private String ImageLeft { get; set; }

		// non-serialized members

		public Bitmap Top { get; set; }
		public Bitmap Right { get; set; }
		public Bitmap Bottom { get; set; }
		public Bitmap Left { get; set; }

	}
}

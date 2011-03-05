using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs.Themes {
	
	[DataContract(Name="layoutrect")]
	internal class ThemeLayoutRectangle {

		[DataMember(Name="top")]
		public int Top { get; set; }

		[DataMember(Name = "left")]
		public int Left { get; set; }

		[DataMember(Name = "maxwidth")]
		public int MaxWidth { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

	}
}

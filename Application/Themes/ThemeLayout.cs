using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs.Themes {

	[DataContract(Name = "layout")]
	internal class ThemeLayout {

		[DataMember(Name = "top")]
		public ThemeLayoutRectangle Top { get; set; }

		[DataMember(Name = "right")]
		public ThemeLayoutRectangle Right { get; set; }

		[DataMember(Name = "bottom")]
		public ThemeLayoutRectangle Bottom { get; set; }

		[DataMember(Name = "left")]
		public ThemeLayoutRectangle Left { get; set; }

	}
}

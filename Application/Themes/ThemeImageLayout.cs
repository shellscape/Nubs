using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs.Themes {

	[DataContract(Name="imagelayout")]
	internal class ThemeImageLayout {

		[DataMember(Name="top")]
		public Rectangle Top { get; set; }

		[DataMember(Name = "middle")]
		public Rectangle Middle { get; set; }

		[DataMember(Name = "bottom")]
		public Rectangle Bottom { get; set; }

	}
}

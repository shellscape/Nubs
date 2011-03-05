using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs.Themes {
	
	[DataContract(Name="font")]
	internal class ThemeFont {

		[DataMember(Name = "name")]
		public String Name { get; set; }

		[DataMember(Name = "size")]
		public int Size { get; set; }

		[DataMember(Name = "color")]
		public String Color { get; set; }

	}
}

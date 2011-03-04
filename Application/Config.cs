using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nubs {

	[DataContract(Name = "config")]
	internal class Config : Shellscape.Configuration.Config {

		public Config() : base() {  }

		protected override String ApplicationName { get { return Nubs.Resources.WindowTitle; } }

		protected override void SetDefaults(){

		}

		public static Config Current { get; set; }

		public Boolean EdgeTop { get; set; }
		public Boolean EdgeRight { get; set; }
		public Boolean EdgeBottom { get; set; }
		public Boolean EdgeLeft { get; set; }

		public Boolean CheckForUpdates { get; set; }
		public Boolean HideFromTaskbar { get; set; }
		public Boolean HideNumberIcon { get; set; }
		public Boolean FadeWindows { get; set; }
		public Boolean LoadOnStartup { get; set; }
		public Boolean NubsOnTop { get; set; }
		public Boolean ShowWindowFocus { get; set; }

		public int NubDistance { get; set; }
	}
}

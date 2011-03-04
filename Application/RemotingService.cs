using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nubs {
	public class RemotingService : Shellscape.RemotingService<ProgramArguments> {

		[Shellscape.RemoteServiceMethod(ProgramArguments.Settings)]
		public void ShowSettings() {

		}

		[Shellscape.RemoteServiceMethod(ProgramArguments.About)]
		public void ShowAbout() {

		}

	}
}

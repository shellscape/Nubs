using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Nubs {
	static class Program {

		private static Shellscape.Program _program;
		private static RemotingService _remoting;

		[STAThread]
		static void Main(String[] arguments) {

			if (_program == null) {

				_remoting = new RemotingService();
			
				_program = new Shellscape.Program(){
					RemotingServiceType = typeof(RemotingService)
				};

				_program.MainInstanceStarted += program_MainInstanceStarted;
				_program.RemoteCall += program_RemoteCall;
			}

			_program.Run<Forms.Main>(arguments);

		}

		private static void program_MainInstanceStarted(object sender, EventArgs e) {
			Shellscape.Configuration.Config.Init<Config>();
		}

		private static void program_RemoteCall(object sender, Shellscape.RemoteCallEventArgs e) {
			_remoting.Execute(e.Arguments);
		}
	}
}

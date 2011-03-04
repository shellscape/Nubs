using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Nubs {
	static class Program {

		private static Shellscape.Program _program;

		[STAThread]
		static void Main(String[] arguments) {

			if (_program == null) {
				_program = new Shellscape.Program();
				_program.MainInstanceStarted += program_MainInstanceStarted;
				_program.RemoteCall += program_RemoteCall;
			}

			_program.Run<Forms.Main>(arguments);

		}

		private static void program_MainInstanceStarted(object sender, EventArgs e) {

		}

		private static void program_RemoteCall(object sender, EventArgs e) {

		}
	}
}

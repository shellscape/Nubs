using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nubs.Forms {
	public partial class Nub : Form {

		public Nub() {
			InitializeComponent();
		}

		public Nub(Boolean recover) {

		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
		}

		protected override void OnClosing(CancelEventArgs e) {
			
			// dispose of any resources
			
			base.OnClosing(e);
		}

		#region .    Menu Events

		private void _MenuItemShow_Click(object sender, EventArgs e) {

			_MenuItemShow.Visible = false;
			_MenuItemHide.Visible = true;

			ToggleTarget();
		}

		private void _MenuItemHide_Click(object sender, EventArgs e) {

			_MenuItemShow.Visible = true;
			_MenuItemHide.Visible = false;

			ToggleTarget();
		}

		private void _MenuItemClose_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion

		private void ToggleTarget() {

		}
	}
}

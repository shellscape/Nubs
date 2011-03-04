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

		private Rectangle _lastWindowRect = new Rectangle();

		public Nub() {
			InitializeComponent();
		}

		public Nub(Boolean recover) {

		}

		public IntPtr WindowHandle { get; set; }
		public Rectangle WindowRect {
			get { return _lastWindowRect; }
			set {
				_lastWindowRect = value;

				// TODO
				//  Nub.WindowLastRect = new Point(StoredRect.Left, StoredRect.Top);

				//  Xml.XmlNode WindowNode = _XmlMan.XmlDoc.CreateNode(Xml.XmlNodeType.Element, "window", string.Empty);
				//  Xml.XmlNode HandleNode = _XmlMan.XmlDoc.CreateNode(Xml.XmlNodeType.Element, "handle", string.Empty);
				//  Xml.XmlNode CoordsNode = _XmlMan.XmlDoc.CreateNode(Xml.XmlNodeType.Element, "coords", string.Empty);

				//  HandleNode.InnerText = hWnd.ToInt32.ToString;
				//  CoordsNode.InnerText = string.Concat(Nub.WindowLastRect.X, ",", Nub.WindowLastRect.Y);
				//  WindowNode.AppendChild(HandleNode);
				//  WindowNode.AppendChild(CoordsNode);

				//  Xml.XmlNode Node = _XmlMan.Node("nubs/recover");
				//  Node.AppendChild(WindowNode);

				//  _XmlMan.Save();
			}
		}

		public Rectangle LastWindowRect { get; set; }

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
		}

		protected override void OnClosing(CancelEventArgs e) {
			
			// TODO - dispose of any resources

			// TODO
			//ArrayList Windows = _XmlMan.List("nubs/recover/window", true);

			//foreach (Xml.XmlNode Node in Windows) {
			//  if (Node.SelectSingleNode("handle").InnerText == StringHandle) {
			//    Node.ParentNode.RemoveChild(Node);

			//    _XmlMan.Save();

			//    Node = null;

			//    break; // TODO: might not be correct. Was : Exit For

			//  }

			//}			

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

		public void Renub(Rectangle windowRect) {

		}
	}
}

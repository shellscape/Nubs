namespace Nubs.Forms {
	partial class Nub {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this._Tooltip = new System.Windows.Forms.ToolTip(this.components);
			this._Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._MenuItemShow = new System.Windows.Forms.ToolStripMenuItem();
			this._MenuItemHide = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this._MenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this._Menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// _Tooltip
			// 
			this._Tooltip.ShowAlways = true;
			this._Tooltip.ToolTipTitle = "Nubs! Tab";
			// 
			// _Menu
			// 
			this._Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._MenuItemShow,
            this._MenuItemHide,
            this.ToolStripMenuItem2,
            this._MenuItemClose});
			this._Menu.Name = "ContextMenuStrip1";
			this._Menu.Size = new System.Drawing.Size(153, 98);
			// 
			// _MenuItemShow
			// 
			this._MenuItemShow.Name = "_MenuItemShow";
			this._MenuItemShow.Size = new System.Drawing.Size(152, 22);
			this._MenuItemShow.Text = "Show Window";
			this._MenuItemShow.Click += new System.EventHandler(this._MenuItemShow_Click);
			// 
			// _MenuItemHide
			// 
			this._MenuItemHide.Name = "_MenuItemHide";
			this._MenuItemHide.Size = new System.Drawing.Size(152, 22);
			this._MenuItemHide.Text = "Hide Window";
			this._MenuItemHide.Visible = false;
			this._MenuItemHide.Click += new System.EventHandler(this._MenuItemHide_Click);
			// 
			// ToolStripMenuItem2
			// 
			this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
			this.ToolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// _MenuItemClose
			// 
			this._MenuItemClose.Name = "_MenuItemClose";
			this._MenuItemClose.Size = new System.Drawing.Size(152, 22);
			this._MenuItemClose.Text = "Close Nub";
			this._MenuItemClose.Click += new System.EventHandler(this._MenuItemClose_Click);
			// 
			// Nub
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 30);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Nub";
			this.Text = "Nub";
			this._Menu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.ToolTip _Tooltip;
		internal System.Windows.Forms.ContextMenuStrip _Menu;
		internal System.Windows.Forms.ToolStripMenuItem _MenuItemShow;
		internal System.Windows.Forms.ToolStripMenuItem _MenuItemHide;
		internal System.Windows.Forms.ToolStripSeparator ToolStripMenuItem2;
		internal System.Windows.Forms.ToolStripMenuItem _MenuItemClose;
	}
}
using System.ComponentModel;

namespace SanderSade.EpubPreviewer.App
{
	partial class ConfigForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btDeleteTemporary = new System.Windows.Forms.Button();
			this.lbLink = new System.Windows.Forms.Label();
			this.SuspendLayout();
			//
			// label2
			//
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Calibri", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(10);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(10);
			this.label2.Size = new System.Drawing.Size(652, 60);
			this.label2.TabIndex = 1;
			this.label2.Text = ".epub previewer";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			// label1
			//
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 60);
			this.label1.Margin = new System.Windows.Forms.Padding(10);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(10);
			this.label1.Size = new System.Drawing.Size(652, 198);
			this.label1.TabIndex = 2;
			this.label1.Text = resources.GetString("label1.Text");
			//
			// btDeleteTemporary
			//
			this.btDeleteTemporary.Dock = System.Windows.Forms.DockStyle.Top;
			this.btDeleteTemporary.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btDeleteTemporary.Location = new System.Drawing.Point(0, 258);
			this.btDeleteTemporary.Margin = new System.Windows.Forms.Padding(20);
			this.btDeleteTemporary.Name = "btDeleteTemporary";
			this.btDeleteTemporary.Size = new System.Drawing.Size(652, 23);
			this.btDeleteTemporary.TabIndex = 3;
			this.btDeleteTemporary.Text = "Delete all temporary EpubPreviewer files";
			this.btDeleteTemporary.UseVisualStyleBackColor = true;
			this.btDeleteTemporary.Click += new System.EventHandler(this.btDeleteTemporary_Click);
			//
			// lbLink
			//
			this.lbLink.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbLink.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lbLink.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbLink.ForeColor = System.Drawing.Color.Maroon;
			this.lbLink.Location = new System.Drawing.Point(0, 321);
			this.lbLink.Name = "lbLink";
			this.lbLink.Size = new System.Drawing.Size(652, 25);
			this.lbLink.TabIndex = 4;
			this.lbLink.Text = "https://github.com/SanderSade/EpubPreviewer";
			this.lbLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbLink.Click += new System.EventHandler(this.lbLink_Click);
			//
			// ConfigForm
			//
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 346);
			this.Controls.Add(this.lbLink);
			this.Controls.Add(this.btDeleteTemporary);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ConfigForm";
			this.Text = "Epub Previewer";
			this.TopMost = true;
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ConfigForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ConfigForm_DragEnter);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btDeleteTemporary;
		private System.Windows.Forms.Label lbLink;
	}
}


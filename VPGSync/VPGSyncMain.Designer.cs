namespace VPGSync
{
    partial class VPGSyncMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VPGSyncMain));
            this.cmdSync = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurrentTask = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblGoogleContacts = new System.Windows.Forms.Label();
            this.lblToBeDeleted = new System.Windows.Forms.Label();
            this.lblToBeUpdated = new System.Windows.Forms.Label();
            this.lblToBeCreated = new System.Windows.Forms.Label();
            this.lblVpContacts = new System.Windows.Forms.Label();
            this.lblInitials = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSync
            // 
            this.cmdSync.Location = new System.Drawing.Point(331, 186);
            this.cmdSync.Name = "cmdSync";
            this.cmdSync.Size = new System.Drawing.Size(75, 23);
            this.cmdSync.TabIndex = 0;
            this.cmdSync.Text = "Sync";
            this.cmdSync.UseVisualStyleBackColor = true;
            this.cmdSync.Click += new System.EventHandler(this.cmdSync_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(418, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Checked = true;
            this.aboutToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCurrentTask);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lblGoogleContacts);
            this.groupBox1.Controls.Add(this.lblToBeDeleted);
            this.groupBox1.Controls.Add(this.lblToBeUpdated);
            this.groupBox1.Controls.Add(this.lblToBeCreated);
            this.groupBox1.Controls.Add(this.lblVpContacts);
            this.groupBox1.Controls.Add(this.lblInitials);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 138);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sync Status";
            // 
            // lblCurrentTask
            // 
            this.lblCurrentTask.AutoSize = true;
            this.lblCurrentTask.Location = new System.Drawing.Point(168, 36);
            this.lblCurrentTask.Name = "lblCurrentTask";
            this.lblCurrentTask.Size = new System.Drawing.Size(67, 13);
            this.lblCurrentTask.TabIndex = 3;
            this.lblCurrentTask.Text = "Not Running";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(266, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(114, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lblGoogleContacts
            // 
            this.lblGoogleContacts.AutoSize = true;
            this.lblGoogleContacts.Location = new System.Drawing.Point(168, 63);
            this.lblGoogleContacts.Name = "lblGoogleContacts";
            this.lblGoogleContacts.Size = new System.Drawing.Size(13, 13);
            this.lblGoogleContacts.TabIndex = 4;
            this.lblGoogleContacts.Text = "0";
            // 
            // lblToBeDeleted
            // 
            this.lblToBeDeleted.AutoSize = true;
            this.lblToBeDeleted.Location = new System.Drawing.Point(168, 106);
            this.lblToBeDeleted.Name = "lblToBeDeleted";
            this.lblToBeDeleted.Size = new System.Drawing.Size(13, 13);
            this.lblToBeDeleted.TabIndex = 3;
            this.lblToBeDeleted.Text = "0";
            // 
            // lblToBeUpdated
            // 
            this.lblToBeUpdated.AutoSize = true;
            this.lblToBeUpdated.Location = new System.Drawing.Point(168, 92);
            this.lblToBeUpdated.Name = "lblToBeUpdated";
            this.lblToBeUpdated.Size = new System.Drawing.Size(13, 13);
            this.lblToBeUpdated.TabIndex = 3;
            this.lblToBeUpdated.Text = "0";
            // 
            // lblToBeCreated
            // 
            this.lblToBeCreated.AutoSize = true;
            this.lblToBeCreated.Location = new System.Drawing.Point(168, 78);
            this.lblToBeCreated.Name = "lblToBeCreated";
            this.lblToBeCreated.Size = new System.Drawing.Size(13, 13);
            this.lblToBeCreated.TabIndex = 3;
            this.lblToBeCreated.Text = "0";
            // 
            // lblVpContacts
            // 
            this.lblVpContacts.AutoSize = true;
            this.lblVpContacts.Location = new System.Drawing.Point(168, 50);
            this.lblVpContacts.Name = "lblVpContacts";
            this.lblVpContacts.Size = new System.Drawing.Size(13, 13);
            this.lblVpContacts.TabIndex = 3;
            this.lblVpContacts.Text = "0";
            // 
            // lblInitials
            // 
            this.lblInitials.AutoSize = true;
            this.lblInitials.Location = new System.Drawing.Point(168, 23);
            this.lblInitials.Name = "lblInitials";
            this.lblInitials.Size = new System.Drawing.Size(53, 13);
            this.lblInitials.TabIndex = 3;
            this.lblInitials.Text = "Unknown";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(76, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "To be deleted:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "To be created:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "VP Contacts found:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "To be updated:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Existing Google Contacts";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Vestas Initials:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Task:";
            // 
            // VPGSyncMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 215);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdSync);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "VPGSyncMain";
            this.Text = "VP -> Google Sync (VPGSync)";
            this.Load += new System.EventHandler(this.VPGSyncMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSync;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblToBeDeleted;
        private System.Windows.Forms.Label lblToBeUpdated;
        private System.Windows.Forms.Label lblToBeCreated;
        private System.Windows.Forms.Label lblVpContacts;
        private System.Windows.Forms.Label lblCurrentTask;
        private System.Windows.Forms.Label lblGoogleContacts;
        private System.Windows.Forms.Label lblInitials;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}


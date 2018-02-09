namespace VPGSync
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.button1 = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linklblGitHub = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(479, 319);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Controls.Add(this.label6);
            this.cmdOK.Controls.Add(this.label3);
            this.cmdOK.Controls.Add(this.label2);
            this.cmdOK.Controls.Add(this.pictureBox1);
            this.cmdOK.Controls.Add(this.label5);
            this.cmdOK.Controls.Add(this.label4);
            this.cmdOK.Controls.Add(this.label1);
            this.cmdOK.Controls.Add(this.linklblGitHub);
            this.cmdOK.Location = new System.Drawing.Point(13, 13);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(541, 300);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.TabStop = false;
            this.cmdOK.Text = "About VPGSync";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "8th Feb. 2018";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "VPGSync 0.1 BETA";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(354, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 142);
            this.label5.TabIndex = 1;
            this.label5.Text = "Stuff \"working\":\r\n\r\n* Person and Dept. Sync\r\n* Following fields:\r\n    - Initials\r" +
    "\n    - Name\r\n    - Work & Private Email\r\n    - Work & Private Phone\r\n    - Depar" +
    "tment\r\n    - Title";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(185, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 129);
            this.label4.TabIndex = 1;
            this.label4.Text = "Stuff \"not\" working:\r\n\r\n* Sync of Sites in VP\r\n* Recursive sync of departments\r\n*" +
    " Re-occouring Sync\r\n* Sync of images";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(185, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 129);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // linklblGitHub
            // 
            this.linklblGitHub.AutoSize = true;
            this.linklblGitHub.Location = new System.Drawing.Point(6, 284);
            this.linklblGitHub.Name = "linklblGitHub";
            this.linklblGitHub.Size = new System.Drawing.Size(189, 13);
            this.linklblGitHub.TabIndex = 0;
            this.linklblGitHub.TabStop = true;
            this.linklblGitHub.Text = "https://github.com/hyberdk/VPGSync";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "License: GPLv3";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 354);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "About";
            this.Text = "About";
            this.cmdOK.ResumeLayout(false);
            this.cmdOK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox cmdOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linklblGitHub;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
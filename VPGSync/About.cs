using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPGSync
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            // Place in local code
            Assembly ass = Assembly.GetExecutingAssembly();
            string version;
            if (ass != null)
            {
                FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(ass.Location);

                lblVersion.Text = String.Format("{0} {1:0}.{2:0} BETA",
                              FVI.ProductName,
                              FVI.FileMajorPart.ToString(),
                              FVI.FileMinorPart.ToString());
                
            }
            else
            {
                lblVersion.Text = "UNKNOWN";
            }
        }

        private void linklblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/hyberdk/VPGSync");
        }
    }
}

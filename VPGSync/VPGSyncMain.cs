using Google.GData.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPGSync
{
    public partial class VPGSyncMain : Form
    {
        public VPGSyncMain()
        {
            InitializeComponent();
        }

        private void cmdSync_Click(object sender, EventArgs e)
        {

            if (!InitialChecks())
            {
                lblCurrentTask.Text = "Initial checks failed, stopping sync";
                return;
            }
            cmdSync.Enabled = false;
            Task task = SyncronizeAsync();

            return;
        }

        private bool InitialChecks()
        {
            try
            {
                lblCurrentTask.Text = "Initial checks..";

                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                var result = ping.Send("vp-db.vestas.net");
                if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    ShowErrorBox("Could not ping vp-db.vestas.net, are you connected on VPN or in a office?", "");
                    return false;
                }
            }
            catch (System.Net.NetworkInformation.PingException ex)
            {
                ShowErrorBox("Could not lookup DNS: vp-db.vestas.net, are you connected to the Vestas network?", "");
                return false;
            }
            catch (Exception ex)
            {
                ShowErrorBox("Initial checks failed", ex.Message);
                return false;
            }

            return true;
        }

        async void Sync()
        {
            await SyncronizeAsync();
        }

        Task SyncronizeAsync()
        {
            Sync sync = new Sync();
            sync.ProgressUpdate += Sync_ProgressUpdate;

            return Task.Run(() => sync.DoSync());
        }

        private void ShowErrorBox(string text, string techtext)
        {
            
            if (!string.IsNullOrEmpty(techtext)) text = text + "\n Tech description \n\n" + techtext;

            MessageBox.Show(text, "Sorry, Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Sync_ProgressUpdate(ProgressUpdater update)
        {

            if (update.HasError)
            {
                ShowErrorBox(update.Error, update.ErrorTech);
            }

            lblCurrentTask.Invoke(new UpdateTextCallback(UpdateCurrentTask),
                new object[] {update.CurrentTask});

            lblToBeCreated.Invoke(new UpdateTextCallback(this.UpdateToBeCreated),
                new object[] { update.ToBeCreated.ToString() });

            lblToBeDeleted.Invoke(new UpdateTextCallback(this.UpdateToBeDeleted),
                new object[] { update.ToBeDeleted.ToString() });

            lblToBeUpdated.Invoke(new UpdateTextCallback(this.UpdateToBeUpdated),
                new object[] { update.ToBeUpdated.ToString() });

            lblVpContacts.Invoke(new UpdateTextCallback(this.UpdateVpContacts),
                new object[] { update.VPContacts.ToString() });

            lblGoogleContacts.Invoke(new UpdateTextCallback(this.UpdateGoogleContacts),
                new object[] { update.GoogleContacts.ToString() });

            cmdSync.Invoke(new UpdateBoolCallback(this.UpdateGoogleContacts),
                new object[] { update.IsRunning });

        }

        public delegate void UpdateTextCallback(string text);
        public delegate void UpdateBoolCallback(bool value);
        private void UpdateCurrentTask(string text) { lblCurrentTask.Text = text; }
        private void UpdateToBeCreated(string text) { lblToBeCreated.Text = text; }
        private void UpdateToBeDeleted(string text) { lblToBeDeleted.Text = text; }
        private void UpdateToBeUpdated(string text) { lblToBeUpdated.Text = text; }
        private void UpdateVpContacts(string text) { lblVpContacts.Text = text; }
        private void UpdateGoogleContacts(string text) { lblGoogleContacts.Text = text; }
        private void UpdateGoogleContacts(bool value) { cmdSync.Enabled = !value; }

        private void VPGSyncMain_Load(object sender, EventArgs e)
        {
            lblInitials.Text = Environment.UserName;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

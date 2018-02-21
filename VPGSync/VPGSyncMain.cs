using Google.GData.Extensions;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPGSync
{
    public partial class VPGSyncMain : Form
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        int _nextSync = 99999;


        public VPGSyncMain()
        {
            InitializeComponent();
            _notifyIcon = new NotifyIcon();
            this.components = new System.ComponentModel.Container();
            _notifyIcon.Icon = new Icon("icon.ico");
            _notifyIcon.DoubleClick += new System.EventHandler(this._notifyIcon_DoubleClick);
            LoadSettings();
            SetupGUI();
        }

        private void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.WindowState = FormWindowState.Maximized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
                
            this.Activate();
        }

        private void cmdSync_Click(object sender, EventArgs e)
        {
            logger.Info("Starting manual sync");
            StartSync();
        }

        private void StartSync()
        {
            if (!cmdSync.Enabled) return;

            if (!InitialChecks())
            {
                lblCurrentTask.Text = "Initial checks failed, stopping sync";
                return;
            }
            cmdSync.Enabled = false;
            Task task = SyncronizeAsync();
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

        private void VPGSyncMain_Resize(object sender, EventArgs e)
        {
            CheckSystemTray();
        }

        private void CheckSystemTray()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                _notifyIcon.Visible = true;
                //_notifyIcon.BalloonTipText = "TEST";
                //_notifyIcon.ShowBalloonTip(500);
                this.Hide();


                //this._notifyIcon.BalloonTipText = "[Balloon Text when Minimized]";
                //this._notifyIcon.BalloonTipTitle = "[Balloon Title when Minimized]";
                //this._notifyIcon.Text = "[Message shown when hovering over tray icon]";
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                _notifyIcon.Visible = false;
            }
        }

        private void tmrSync_Tick(object sender, EventArgs e)
        {

            if (chkAutoSync.Enabled)
            {
                _nextSync--;
                lblNextSyncIn.Text = "Next sync in (minutes): " + _nextSync;
            }

            if (chkAutoSync.Enabled && _nextSync == 0 && cmdSync.Enabled)
            {
                //do sync
                _nextSync = (int)numSyncInterval.Value;
                StartSync();
                return;
            }
        }


        public void SaveConfig()
        {
            Properties.Settings.Default.AutoSyncEnabled = chkAutoSync.Checked;
            Properties.Settings.Default.AutoSyncInterval = (int) numSyncInterval.Value;
            Properties.Settings.Default.StartMinimized = chkStartMinimized.Checked;
            Properties.Settings.Default.Save();
        }

        private void SetupGUI()
        {
            tmrSync.Enabled = chkAutoSync.Checked;
            lblSyncIntervalLabel.Enabled = chkAutoSync.Checked;
            lblNextSyncIn.Enabled = chkAutoSync.Checked;
            numSyncInterval.Enabled = chkAutoSync.Checked;
        }

        private void LoadSettings()
        {
            chkAutoSync.Checked = Properties.Settings.Default.AutoSyncEnabled;
            numSyncInterval.Value = Properties.Settings.Default.AutoSyncInterval;
            chkStartMinimized.Checked = Properties.Settings.Default.StartMinimized;
            _nextSync = Properties.Settings.Default.AutoSyncInterval;
            lblNextSyncIn.Text = "Next sync in (minutes): " + _nextSync;

            if (Properties.Settings.Default.StartMinimized)
            {
                WindowState = FormWindowState.Minimized;
                CheckSystemTray();
            }
        }


        private void chkAutoSync_MouseClick(object sender, MouseEventArgs e)
        {
            SetupGUI();
            SaveConfig();
        }

        private void numSyncInterval_MouseClick(object sender, MouseEventArgs e)
        {
            SaveConfig();
            _nextSync = Properties.Settings.Default.AutoSyncInterval;
            lblNextSyncIn.Text = "Next sync in (minutes): " + _nextSync;
        }

        private void chkStartMinimized_MouseClick(object sender, MouseEventArgs e)
        {
            SaveConfig();
        }

        private void cmdOpenLog_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("logs\\"))
            {
                Directory.CreateDirectory("logs");
            }
            
            Process.Start("logs\\");
        }
    }
}

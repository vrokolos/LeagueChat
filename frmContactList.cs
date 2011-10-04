using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using jabber.client;
using System.Net;
using System.IO;
using LolChatWin.Properties;


namespace LolChatWin
{
    public partial class frmContactList : Form
    {
        public frmContactList()
        {
            InitializeComponent();
        }

        JabberManager jm = new JabberManager();
        public Dictionary<User, frmMessage> msgWindows = new Dictionary<User, frmMessage>();
        Dictionary<string, ListViewGroup> grps = new Dictionary<string, ListViewGroup>();
        private bool Started = false;
        private int getServer()
        {
            if (radioButton1.Checked)
            {
                return 0;
            }
            else if (radioButton2.Checked)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private void setServer(int value)
        {
            if (value == 0)
            {
                radioButton1.Checked = true;
            }
            else if (value == 1)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton3.Checked = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.Username = txtUsername.Text;
            Settings.Default.Password = JabberManager.EncryptString(JabberManager.ToSecureString(txtPassword.Text));

            Settings.Default.Server = getServer();
            Settings.Default.Save();
            Connect();

        }

        void jm_OnMessage(User From, string message, DateTime Date)
        {
            ShowWindow(From);
            msgWindows[From].gotMessage(Date, message);
        }

        private void ShowWindow(User From)
        {
            if (msgWindows.ContainsKey(From))
            {
                msgWindows[From].Show();
                msgWindows[From].Activate();
            }
            else
            {
                frmMessage newWindow = new frmMessage();
                newWindow.Images = imgChamps;
                newWindow.StartPosition = FormStartPosition.Manual;
                newWindow.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 100 + msgWindows.Count * 30, SystemInformation.VirtualScreen.Height / 2 - 100 + msgWindows.Count * 30);
                newWindow.theUser = From;
                newWindow.Closed += new frmMessage.CloseHandler(newWindow_Closed);
                newWindow.jm = jm;
                msgWindows.Add(From, newWindow);
                newWindow.Show();
            }
        }

        void newWindow_Closed(User e)
        {
            if (msgWindows.ContainsKey(e))
            {
                msgWindows.Remove(e);
            }
        }

        void jm_UserChanged(User e)
        {
                UpdateNode(e);
        }

        private void UpdateNode(User u)
        {
            if ( (!u.isOnline) || (u.status == null))
            { 
                if (lstBuddies.Items.ContainsKey(u.JID))
                {
                    lstBuddies.Items.RemoveByKey(u.JID);

                    if (jm.theUsers.Where(p => p.Group == u.Group).Count() == 1)
                    {
                        grps.Remove(u.Group);
                    }
                } 
            }
            else
            {
                ListViewItem k;
                if (!lstBuddies.Items.ContainsKey(u.JID))
                {
                    k = lstBuddies.Items.Add(u.JID, u.Nickname, u.State);
                    k.SubItems.Add("");
                    k.SubItems.Add("");
                    k.SubItems.Add("");
                    k.UseItemStyleForSubItems = false;

                }
                else
                {
                    k = lstBuddies.Items[u.JID];
                }

                k.SubItems[0].Text = u.Nickname;

                k.SubItems[0].Font = new Font(k.SubItems[0].Font.FontFamily, 9, FontStyle.Bold);
                if (u.State == "outOfGame")
                {
                    k.SubItems[0].ForeColor = Color.Green;
                }
                else
                {
                    k.SubItems[0].ForeColor = Color.OrangeRed;
                }
                if (u.State == "")
                    k.SubItems[1].Text = "";
                else
                {

                    k.SubItems[1].Text = (u.State + " for " + u.duration + " mins");
                }
                 

                k.SubItems[2].Text = ("Normal: " + u.wins + " wins " + u.leaves + " leaves");

                if (u.rankedRating != "0")
                    k.SubItems[3].Text = ("Ranked: " + u.rankedRating + " rating " + u.rankedWins + " wins " + u.rankedLosses + " losses");

                k.ImageKey = u.State;
                if (!grps.ContainsKey(u.Group))
                {
                    grps.Add(u.Group, lstBuddies.Groups.Add(u.Group, u.Group));
                    ((ListViewGroupSorter)lstBuddies).SortGroups(true); 
                }
                k.Group = grps[u.Group];
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                ShowInTaskbar = false;
            else
                ShowInTaskbar = true;

    
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ShowWindow(jm.users[lstBuddies.SelectedItems[0].Name]);
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            btnDisconnect.Text = "Disconnecting...";
            jm.Disconnect();
        }

        private void frmContactList_Load(object sender, EventArgs e)
        {
            Settings.Default.Reload();

            if (Settings.Default.Top > 0)
            {
                Location = new Point( Settings.Default.Left, Settings.Default.Top);
            }
            Started = true;
            
            foreach (string file in Directory.GetFiles(Path.Combine( Path.GetDirectoryName( Application.ExecutablePath ) ,"Champs")))
            {
                imgChamps.Images.Add(Path.GetFileNameWithoutExtension( file), Image.FromFile(file));
            }
            lstBuddies.Columns.Add("Nickname");
            lstBuddies.Columns.Add("duration");
            lstBuddies.Columns.Add("rankedRating");
            lstBuddies.Columns.Add("State");
            jm.UserChanged += new JabberManager.UserChangedHandler(jm_UserChanged);
            jm.OnMessage += new JabberManager.MsgHandler(jm_OnMessage);
            jm.OnConnect += new JabberManager.ConnectedHandler(jm_OnConnect);
            jm.OnDisconnect += new JabberManager.ConnectedHandler(jm_OnDisconnect);
            jm.OnError += new JabberManager.ErrorHandler(jm_OnError);
            txtUsername.Text = Settings.Default.Username;
            txtPassword.Text = JabberManager.ToInsecureString(JabberManager.DecryptString(Settings.Default.Password));
            setServer(Settings.Default.Server);
            if ((Settings.Default.Username != "") && (Settings.Default.Password != ""))
            {
                Connect();
            }
            else
            {
                pblConnect.Visible = true;
                btnDisconnect.Text = "Connect";
            }

        }

        void jm_OnError(string error)
        {
            MessageBox.Show(error,"Error");
        }

        void jm_OnDisconnect()
        {
            pblConnect.Visible = true;
            btnDisconnect.Text = "Connect";
        }

        void jm_OnConnect()
        {
            pblConnect.Visible = false;
            btnDisconnect.Text = "Disconnect";
        }

        private void Connect()
        {
            btnDisconnect.Text = "Connecting...";
            try
            {
                jm.Initialize(txtUsername.Text, txtPassword.Text, getServer(), this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmContactList_Move(object sender, EventArgs e)
        {
            if (Started && (WindowState == FormWindowState.Normal))
            {
                Settings.Default.Top = Location.Y;
                Settings.Default.Left = Location.X;
                Settings.Default.Save();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://vrokolos.github.com/LeagueChat/");
        }

    }
    

}

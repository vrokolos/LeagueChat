using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace LolChatWin
{
    public partial class frmMessage : Form
    {
        public JabberManager jm;
        public ImageList Images;
        private User _theUser;
        public User theUser {
            get
            {
                return _theUser;
            }
            set
            {
                _theUser = value;
                Text = theUser.Nickname;

                if (Images.Images.ContainsKey(theUser.State))
                {
                    Icon = Icon.FromHandle(((Bitmap)Images.Images[theUser.State]).GetHicon());
                }
            }
        }
        public frmMessage()
        {
            InitializeComponent();
        }

        public delegate void CloseHandler(User e);
        public event CloseHandler Closed;

        public void gotMessage(DateTime date, string msg)
        {
            txtLog.Text += msg + Environment.NewLine;
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void frmMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Closed != null)
            {
                Closed(theUser);
            }
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (txtMessage.Text != "")
            {
                jm.SendMessage(txtMessage.Text, theUser);
                txtLog.Text += ">> " + txtMessage.Text + Environment.NewLine;
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
                txtMessage.Text = "";
            }
        }

        private void frmMessage_Activated(object sender, EventArgs e)
        {
            txtMessage.Focus();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            TaskbarManager.Instance.SetApplicationIdForSpecificWindow(Handle, Guid.NewGuid().ToString());
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }
    }
}

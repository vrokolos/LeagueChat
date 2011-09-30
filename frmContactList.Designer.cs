namespace LolChatWin
{
    partial class frmContactList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContactList));
            this.imgChamps = new System.Windows.Forms.ImageList(this.components);
            this.pblConnect = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.trayBuddies = new System.Windows.Forms.NotifyIcon(this.components);
            this.lstBuddies = new System.Windows.Forms.ListView();
            this.tsBuddies = new System.Windows.Forms.ToolStrip();
            this.btnDisconnect = new System.Windows.Forms.ToolStripButton();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.pblConnect.SuspendLayout();
            this.tsBuddies.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgChamps
            // 
            this.imgChamps.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgChamps.ImageSize = new System.Drawing.Size(60, 60);
            this.imgChamps.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pblConnect
            // 
            this.pblConnect.Controls.Add(this.radioButton3);
            this.pblConnect.Controls.Add(this.radioButton2);
            this.pblConnect.Controls.Add(this.radioButton1);
            this.pblConnect.Controls.Add(this.lblPassword);
            this.pblConnect.Controls.Add(this.txtPassword);
            this.pblConnect.Controls.Add(this.lblUsername);
            this.pblConnect.Controls.Add(this.txtUsername);
            this.pblConnect.Controls.Add(this.btnConnect);
            this.pblConnect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pblConnect.Location = new System.Drawing.Point(0, 483);
            this.pblConnect.Name = "pblConnect";
            this.pblConnect.Size = new System.Drawing.Size(321, 125);
            this.pblConnect.TabIndex = 10;
            this.pblConnect.Visible = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(221, 64);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(83, 17);
            this.radioButton3.TabIndex = 15;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Europe East";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(121, 64);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(87, 17);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Europe West";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 64);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(92, 17);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "North America";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(16, 41);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(82, 38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(209, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(14, 15);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 8;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(82, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(209, 20);
            this.txtUsername.TabIndex = 7;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(121, 87);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 26);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // trayBuddies
            // 
            this.trayBuddies.Icon = ((System.Drawing.Icon)(resources.GetObject("trayBuddies.Icon")));
            this.trayBuddies.Visible = true;
            this.trayBuddies.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // lstBuddies
            // 
            this.lstBuddies.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstBuddies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBuddies.FullRowSelect = true;
            this.lstBuddies.GridLines = true;
            this.lstBuddies.HideSelection = false;
            this.lstBuddies.LargeImageList = this.imgChamps;
            this.lstBuddies.Location = new System.Drawing.Point(0, 0);
            this.lstBuddies.MultiSelect = false;
            this.lstBuddies.Name = "lstBuddies";
            this.lstBuddies.Size = new System.Drawing.Size(321, 483);
            this.lstBuddies.SmallImageList = this.imgChamps;
            this.lstBuddies.TabIndex = 9;
            this.lstBuddies.TileSize = new System.Drawing.Size(300, 64);
            this.lstBuddies.UseCompatibleStateImageBehavior = false;
            this.lstBuddies.View = System.Windows.Forms.View.Tile;
            this.lstBuddies.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // tsBuddies
            // 
            this.tsBuddies.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsBuddies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDisconnect,
            this.btnAbout});
            this.tsBuddies.Location = new System.Drawing.Point(0, 608);
            this.tsBuddies.Name = "tsBuddies";
            this.tsBuddies.Size = new System.Drawing.Size(321, 25);
            this.tsBuddies.TabIndex = 11;
            this.tsBuddies.Text = "toolStrip1";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("btnDisconnect.Image")));
            this.btnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(86, 22);
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(60, 22);
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // frmContactList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 633);
            this.Controls.Add(this.lstBuddies);
            this.Controls.Add(this.pblConnect);
            this.Controls.Add(this.tsBuddies);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmContactList";
            this.Text = "LolChat";
            this.Load += new System.EventHandler(this.frmContactList_Load);
            this.Move += new System.EventHandler(this.frmContactList_Move);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.pblConnect.ResumeLayout(false);
            this.pblConnect.PerformLayout();
            this.tsBuddies.ResumeLayout(false);
            this.tsBuddies.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstBuddies;
        private System.Windows.Forms.ImageList imgChamps;
        private System.Windows.Forms.Panel pblConnect;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.NotifyIcon trayBuddies;
        private System.Windows.Forms.ToolStrip tsBuddies;
        private System.Windows.Forms.ToolStripButton btnDisconnect;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}


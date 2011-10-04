using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jabber.client;
using System.ComponentModel;
using System.Security;
using System.Security.Cryptography;
using System.Timers;

namespace LolChatWin
{
    public class JabberManager
    {
        public Dictionary<string, User> users = new Dictionary<string, User>();
        public BindingList<User> theUsers = new BindingList<User>();
        JabberClient c = new JabberClient();
        RosterManager k = new RosterManager();

        public delegate void UserChangedHandler(User e);
        public event UserChangedHandler UserChanged;
        public delegate void MsgHandler(User From, string message, DateTime Date);
        public event MsgHandler OnMessage;
        public delegate void ErrorHandler(string error);
        public event ErrorHandler OnError;

        public delegate void ConnectedHandler();
        public event ConnectedHandler OnConnect;
        public event ConnectedHandler OnDisconnect;

        Timer durationTimer = new Timer(30000); 
        public JabberManager()
        {
            c.OnPresence += new PresenceHandler(c_OnPresence);
            c.OnInvalidCertificate += new System.Net.Security.RemoteCertificateValidationCallback(c_OnInvalidCertificate);
            k.OnRosterItem += new RosterItemHandler(k_OnRosterItem);
            k.OnRosterEnd += new bedrock.ObjectHandler(k_OnRosterEnd);
            c.OnMessage += new MessageHandler(c_OnMessage);
            c.OnDisconnect += new bedrock.ObjectHandler(c_OnDisconnect);
            c.OnAuthError += new jabber.protocol.ProtocolHandler(c_OnAuthError);
            c.OnError += new bedrock.ExceptionHandler(c_OnError);
            durationTimer.Elapsed += new ElapsedEventHandler(durationTimer_Elapsed);
            durationTimer.Start();
        }

        void c_OnError(object sender, Exception ex)
        {
            if (OnError != null)
            OnError(ex.Message);

        }

        void durationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var user in theUsers)
            {

                if (UserChanged != null)
                {
                    if (c.InvokeControl != null)
                    {
                        c.InvokeControl.Invoke(UserChanged,new object[]{ user });
                    }

                }
            }
        }
        public void Disconnect()
        {
            c.Close(true);
        }

        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");

        public static string EncryptString(System.Security.SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        public void Initialize(string username, string password, int server, ISynchronizeInvoke si )
        {

            c.InvokeControl = si;
            c.User = username;
            c.Password = "AIR_" + password;
            c.Port = 5223;
            c.SSL = true;
            c.AutoRoster = true;
            c.AutoLogin = true;
            c.AutoPresence = true;
            
            if (server == 0 )
            {
                c.NetworkHost = "chat.na.lol.riotgames.com";
            }
            else if (server == 1)
            {
                c.NetworkHost = "chat.eu.lol.riotgames.com";
            }
            else
            {
                c.NetworkHost = "chat.eun1.lol.riotgames.com";
            }
            c.Server = "pvp.net";
            k.Stream = c;

            users.Clear();
            theUsers.Clear();

            c.Connect();
        }

        bool c_OnInvalidCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        void c_OnAuthError(object sender, System.Xml.XmlElement rp)
        {
            if (OnError != null) OnError("Wrong username or password");
            if (OnDisconnect != null)
            {
                OnDisconnect();
            }
        }

        void c_OnDisconnect(object sender)
        {
            if (OnDisconnect != null)
            {
                OnDisconnect();
            }
        }

        void k_OnRosterEnd(object sender)
        {

            if (OnConnect != null)
            {
                OnConnect();
            }
        }

        void c_OnMessage(object sender, jabber.protocol.client.Message msg)
        {
            if ((OnMessage != null) && (!msg.Body.StartsWith("<body>")))
            {
                if (users.ContainsKey(msg.From.User))
                {
                    OnMessage(users[msg.From.User], msg.Body, DateTime.Now);
                }
            }
        }
        void k_OnRosterItem(object sender, jabber.protocol.iq.Item ri)
        {

            if (users.ContainsKey(ri.JID.User))
            {
                if (ri.Subscription == jabber.protocol.iq.Subscription.remove)
                {
                    users[ri.JID.User].status = null;
                    
                    if (UserChanged != null)
                        UserChanged(users[ri.JID.User]);
                    users.Remove(ri.JID.User);

                }
                else
                {
                    User us = users[ri.JID.User];
                    us.Nickname = ri.Nickname;
                    us.Group = ri.GetGroups().First().GroupName;
                    us.item = ri;
                }
            }
            else
            {
                User us = new User(ri.JID.User, ri.Nickname, ri.GetGroups().First().GroupName, "", ri);
                users.Add(ri.JID.User, us);
            }
        }

        public void SendMessage(string text, User u)
        {
            c.Message(u.item.JID, text);
        }

        void c_OnPresence(object sender, jabber.protocol.client.Presence pres)
        {
            if (users.ContainsKey(pres.From.User))
            {
                users[pres.From.User].status = pres.Status;
            }
            else
            {
                User us = new User(pres.From.User, "", "", pres.Status, null);
                users.Add(pres.From.User, us);
                theUsers.Add(us);
            }
            if (users[pres.From.User].State == "offline")
            {
                users[pres.From.User].isOnline = false;
                if (theUsers.Contains(users[pres.From.User]))
                    theUsers.Remove(users[pres.From.User]);

            }
            else
            {
                users[pres.From.User].isOnline = (users[pres.From.User].Nickname != "") ;
                if (!theUsers.Contains(users[pres.From.User]))
                    theUsers.Add(users[pres.From.User]);
            }
            if (theUsers.Contains(users[pres.From.User]))
            {
                if (UserChanged != null)
                UserChanged(users[pres.From.User]);
            }
        } 

    }
}

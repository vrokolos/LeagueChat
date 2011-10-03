using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace LolChatWin
{

    public class User : INotifyPropertyChanged
    {

        private string _profileIcon;
        private string _statusMsg;
        private string _level;
        private string _wins;
        private string _leaves;
        private string _queueType;
        private string _rankedWins;
        private string _rankedLosses;
        private string _rankedRating;
        private string _tier;
        private string _skinname;
        private string _gameStatus;
        private string _timeStamp;

        private string _JID;
        private string _Nickname;
        private string _Group;

        public bool isOnline;
        public jabber.protocol.iq.Item item;
        private string _status;
        public WebClient c = new WebClient();
        public event PropertyChangedEventHandler PropertyChanged;

        public User(string JID, string Nickname, string Group, string status, jabber.protocol.iq.Item _item)
        {
            item = _item;
            _JID = JID;
            _Nickname = Nickname;
            _Group = Group;
            _status = status;
        }

        public System.Drawing.Image Image;
        public string State
        {
            get
            {
                if (gameStatus == "outOfGame")
                {
                    return "outOfGame";
                }
                else if (gameStatus == "inQueue")
                {
                    return "inQueue";
                }
                else if ((gameStatus != null) && (skinname != null))
                {
                    return skinname.Replace(".", "").Replace(" ", "-").Replace("`", "").Replace("'", "").ToLower();
                }
                else if (status == null) return "";
                else if (Nickname == "") return "offline";
                else return "outOfGame";
            }
        }
        public string Nickname { get { return _Nickname; } set { _Nickname = value; this.NotifyPropertyChanged("Nickname"); } }
        public string profileIcon { get { return _profileIcon; } set { _profileIcon = value; this.NotifyPropertyChanged("profileIcon"); } }
        public string statusMsg { get { return _statusMsg; } set { _statusMsg = value; this.NotifyPropertyChanged("statusMsg"); } }
        public string level { get { return _level; } set { _level = value; this.NotifyPropertyChanged("level"); } }
        public string wins { get { return _wins; } set { _wins = value; this.NotifyPropertyChanged("wins"); } }
        public string leaves { get { return _leaves; } set { _leaves = value; this.NotifyPropertyChanged("leaves"); } }
        public string queueType { get { return _queueType; } set { _queueType = value; this.NotifyPropertyChanged("queueType"); } }
        public string rankedWins { get { return _rankedWins; } set { _rankedWins = value; this.NotifyPropertyChanged("rankedWins"); } }
        public string rankedLosses { get { return _rankedLosses; } set { _rankedLosses = value; this.NotifyPropertyChanged("rankedLosses"); } }
        public string rankedRating { get { return _rankedRating; } set { _rankedRating = value; this.NotifyPropertyChanged("rankedRating"); } }
        public string tier { get { return _tier; } set { _tier = value; this.NotifyPropertyChanged("tier"); } }
        public string skinname { get { return _skinname; } set { _skinname = value; this.NotifyPropertyChanged("skinname"); } }
        public string gameStatus { get { return _gameStatus; } set { _gameStatus = value; this.NotifyPropertyChanged("gameStatus"); } }
        public string timeStamp { get { return _timeStamp; } set { _timeStamp = value; this.NotifyPropertyChanged("timeStamp"); } }
        public string JID { get { return _JID; } set { _JID = value; } }
        public void CalcDuration()
        {
            this.NotifyPropertyChanged("duration");
        }

        public string duration
        {
            get
            {
                if (timeStamp == null)
                    return "0";
                else
                {
                    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    DateTime parsed = origin.AddSeconds(double.Parse(_timeStamp) / 1000);
                    var k =(Math.Round((DateTime.UtcNow - parsed).TotalMinutes) + 1);
                    if (k < 0) { k = 0; }
                    return k.ToString();
                }
            }
        }

        public string Group
        {
            get { return _Group; }
            set
            {
                _Group = value;
                this.NotifyPropertyChanged("Group");
            }
        }
        Regex statreg = new Regex("<(\\w*)>(.*?)</");
        public string status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    if (_status != null)
                    {
                        foreach (Match m in statreg.Matches(_status))
                        {
                            string thename = m.Groups[1].Value;
                            string thevalue = m.Groups[2].Value;
                            switch (thename)
                            {
                                case "profileIcon": profileIcon = thevalue; break;
                                case "statusMsg": statusMsg = thevalue; break;
                                case "level": level = thevalue; break;
                                case "wins": wins = thevalue; break;
                                case "leaves": leaves = thevalue; break;
                                case "queueType ": queueType = thevalue; break;
                                case "rankedWins": rankedWins = thevalue; break;
                                case "rankedLosses": rankedLosses = thevalue; break;
                                case "rankedRating": rankedRating = thevalue; break;
                                case "tier": tier = thevalue; break;
                                case "skinname": skinname = thevalue; break;
                                case "gameStatus": gameStatus = thevalue; break;
                                case "timeStamp": timeStamp = thevalue; break;

                            }
                        }

                        isOnline = (Nickname != "");
                    }
                    else
                    {

                        isOnline = false;
                    }
                    this.NotifyPropertyChanged("status");
                    this.NotifyPropertyChanged("profileImageUrl");
                    this.NotifyPropertyChanged("duration");
                    this.NotifyPropertyChanged("State");
                }
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

}

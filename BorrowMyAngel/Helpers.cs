using System;
using System.Linq;
using System.Collections.Generic;

using Firebase;
using Firebase.Auth;
using Firebase.Database;

namespace BorrowMyAngel
{
    [Serializable()]
    public class MessageData : Java.Lang.Object
    {
        public string data;
        public String timestamp;
        public string uID;

        public MessageData() 
        {

        }

        public MessageData(string data, DateTime timestamp, string uID)
        {
            this.data = data;
            this.timestamp = timestamp.ToShortTimeString();
            this.uID = uID;
        }
    }

    [Serializable]
    public class ChatSession : Java.Lang.Object
    {
        FirebaseAuth auth;

        private static Random random = new Random();
        public List<MessageData> data { get; set; }
        public List<String> users { get; set; }
        public String chatID { get; set; }

        public ChatSession()
        {

        }

        public ChatSession(List<MessageData> data, List<String> users)
        {
            this.data = data;
            this.users = users; 
        }

        public ChatSession(List<MessageData> data)
        {
            this.data = data;
        }

        public ChatSession(List<String> users, String id)
        {
            this.users = users;
        }

        public ChatSession(List<String> users)
        {
            this.users = users;
        }

        public void GenerateChat()
        {
            auth = FirebaseAuth.GetInstance(Auth.app);
            FirebaseDatabase db = FirebaseDatabase.GetInstance(Auth.app, "https://borrowmyangel-9cb04.firebaseio.com/");
            this.chatID = "chatSessions/" + new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 32)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            db.GetReference(this.chatID + "/users/client").SetValue(auth.Uid);
        }

        public void FetchGeneratedChat(String chatID)
        {
            auth = FirebaseAuth.GetInstance(Auth.app);
            FirebaseDatabase db = FirebaseDatabase.GetInstance(Auth.app, "https://borrowmyangel-9cb04.firebaseio.com/");
            this.chatID = "chatSessions/" + chatID;
            db.GetReference(this.chatID + "/users/angel").SetValue(auth.Uid);
            db.GoOnline();
        }
    }
}


using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Provider;
using Firebase.Iid;
using Firebase;

namespace BorrowMyAngel
{
    [Activity(Label = "ChatRoomActivity")]
    public class ChatRoomActivity : ListActivity, Android.Gms.Tasks.IOnCompleteListener
    {
        RelativeLayout activity_chat_room;
        FirebaseAuth auth;
        ChatSession session = new ChatSession();
        private ListView lstChat;
        String status = "";
        private Timer demoTimer;
        private int totalRuns = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChatRoom);

            // Create your application here
            activity_chat_room = FindViewById<RelativeLayout>(Resource.Id.chatRoom);
            Button send = FindViewById<Button>(Resource.Id.send);
            EditText edtChat = FindViewById<EditText>(Resource.Id.input);
            lstChat = FindViewById<ListView>(Resource.Id.list);
            StartMessages();
        }

        public void StartMessages()
        {
            if (Auth.app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId(Auth.ApplicationID)
                    .SetApiKey(Auth.APIKey)
                    .SetProjectId(Auth.ProjectID)
                    .Build();
                Auth.app = FirebaseApp.InitializeApp(this, options);
            }
            auth = FirebaseAuth.GetInstance(Auth.app);
            auth.SignOut();
            status = "SignIn";

            if (!Auth.isDemo){
                auth.SignInAnonymously().AddOnCompleteListener(this);
            } else {
                auth.SignInAnonymously();
                StartDemo();
            }


        }

        public void OnComplete(Task task)
        {
            if (status == "AddMessage")
            {
                if (task.IsSuccessful)
                {
                    Toast.MakeText(ApplicationContext, "SUCCESS!!!!!!!!!!!!", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(ApplicationContext, task.Exception.Message, ToastLength.Short).Show();
                }
            }
            else if (status == "SignIn")
            {
                if (task.IsSuccessful)  //, new List<String> {"test", "test"}
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "Logged In", ToastLength.Short);
                    FirebaseDatabase db = FirebaseDatabase.GetInstance(Auth.app, "https://borrowmyangel-9cb04.firebaseio.com/");
                    DatabaseReference dbRef = db.GetReference("test");
                    session.GenerateChat();
                }
                else
                {
                    Toast.MakeText(ApplicationContext, task.Exception.Message, ToastLength.Short).Show();
                }
            }

        }

        public void StartDemo()
        {
            Toast.MakeText(ApplicationContext, "You will now be connected with your angel....", ToastLength.Long).Show();
            session.GenerateChat();

            session.data.Add(new MessageData("Hey, I need someone to talk to.", DateTime.Now, "You"));
            session.data.Add(new MessageData("Sure, that's what I'm here for. What's up?", DateTime.Now, "Angel"));
            session.data.Add(new MessageData("Well ever since my mom died, my brother deems depressed.", DateTime.Now, "You"));
            session.data.Add(new MessageData("How so?", DateTime.Now, "Angel"));
            session.data.Add(new MessageData("He never comes out of his room anymore. We used to do things all the time.", DateTime.Now, "You"));
            DisplayChatMessage();

            demoTimer = new Timer(5000);
            demoTimer.Enabled = true;
            demoTimer.Elapsed += (object s, ElapsedEventArgs w) =>
            {
                RunOnUiThread(() =>
                {
                    Toast.MakeText(ApplicationContext, totalRuns.ToString(), ToastLength.Short).Show();
                    switch (totalRuns)
                    {
                        case 0:
                            totalRuns++;
                            session.data.Add(new MessageData("Hey, I need someone to talk to.", DateTime.Now, "You"));
                            DisplayChatMessage();
                            break;
                        case 1:
                            totalRuns++;
                            session.data.Add(new MessageData("Sure, that's what I'm here for. What's up?", DateTime.Now, "Angel"));
                            DisplayChatMessage();
                            break;
                        case 2:
                            totalRuns++;
                            session.data.Add(new MessageData("Well ever since my mom died, my brother deems depressed.", DateTime.Now, "You"));
                            DisplayChatMessage();
                            break;
                        case 3:
                            totalRuns++;
                            session.data.Add(new MessageData("How so?", DateTime.Now, "Angel"));
                            DisplayChatMessage();
                            break;
                        case 4:
                            totalRuns++;
                            session.data.Add(new MessageData("He never comes out of his room anymore. We used to do things all the time.", DateTime.Now, "You"));
                            DisplayChatMessage();
                            break;
                        case 5:
                            demoTimer.Stop();
                            demoTimer.Dispose();
                            break;
                    }
                });
            };
            demoTimer.Start();
        }

        private void DisplayChatMessage()
        {
            ListViewAdapter adapter = new ListViewAdapter(this, session.data);
            lstChat.Adapter = adapter;
            adapter.NotifyDataSetChanged();
        }
    }
}

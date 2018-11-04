
using System;
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
using Firebase.Provider;
using Firebase.Iid;
using Firebase;

namespace BorrowMyAngel
{
    [Activity(Label = "ChatRoomActivity")]
    public class ChatRoomActivity : Activity, Android.Gms.Tasks.IOnCompleteListener
    {
        RelativeLayout activity_chat_room;
        FirebaseAuth auth;
        String status = "";
        JavaDictionary data;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChatRoom);

            // Create your application here
            activity_chat_room = FindViewById<RelativeLayout>(Resource.Id.chatRoom);

            JavaDictionary dictionary = new JavaDictionary
            {
                { "users", new List<String> { "HELLO A", "I MADE IT" } }
            };
            AddMessage(dictionary);
        }

        private async void ShowNewMessage()
        {
        }

        public void AddMessage(JavaDictionary data)
        {
            if (Auth.app == null){
                var options = new FirebaseOptions.Builder()
               .SetApplicationId(Auth.ApplicationID)
               .SetApiKey(Auth.APIKey)
               .Build();
                Auth.app = FirebaseApp.InitializeApp(this, options);
            }
            auth = FirebaseAuth.GetInstance(Auth.app);
            auth.SignOut();
            status = "SignIn";
            this.data = data;
            auth.SignInAnonymously().AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        { 
            if(status == "AddMessage"){
                if (task.IsSuccessful)
                {
                    Snackbar snackbar = Snackbar.Make(activity_chat_room, "Message Sent!", Snackbar.LengthShort);
                    snackbar.Show();
                }
                else
                {
                    Snackbar snackbar = Snackbar.Make(activity_chat_room, "Failed to Send Message", Snackbar.LengthShort);
                    snackbar.Show();
                }
            } else if(status == "SignIn"){
                if (task.IsSuccessful)
                {
                    Snackbar snackbar = Snackbar.Make(activity_chat_room, "Signed In!", Snackbar.LengthShort);
                    snackbar.Show();

                }
                else
                {
                    Snackbar snackbar = Snackbar.Make(activity_chat_room, "Failed to Sign In", Snackbar.LengthShort);
                    snackbar.Show();
                }
            }

        }
    }
}

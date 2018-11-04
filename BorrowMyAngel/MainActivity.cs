
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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
    [Activity(Label = "Borrow My Angel", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        FirebaseAuth auth;
        String status = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            Button chat = FindViewById<Button>(Resource.Id.btnChat);

            button.Click += Login_Click;
            chat.Click += Chat_Click;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            var secondIntent = new Intent(this, typeof(LoginActivity));
            StartActivity(secondIntent);
        }

        private void Chat_Click(object sender, EventArgs e)
        { 
            var secondIntent = new Intent(this, typeof(ChatRoomActivity));
            StartActivity(secondIntent);
        }
    }
}

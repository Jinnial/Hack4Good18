using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace BorrowMyAngel
{
    [Activity(Label = "Borrow My Angel", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);

            loginButton.Click += Login_Click;

            Button signupButton = FindViewById<Button>(Resource.Id.signupButton);

            signupButton.Click += SignUp_Click;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }
    }
}


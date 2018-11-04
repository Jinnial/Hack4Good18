using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace BorrowMyAngel
{
    [Activity(Label = "Borrow My Angel", Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.loginButton);
            button.Click += Login_Click;

            Button guestButton = FindViewById<Button>(Resource.Id.guestButton);
            guestButton.Click += Guest_Click;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        private void Guest_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(GuestDashboardActivity));
            StartActivity(intent);
        }
    }
}


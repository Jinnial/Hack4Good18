using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace BorrowMyAngel
{
    [Activity(Label = "Borrow My Angel", MainLauncher = false, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            //button.Click += Login_Click;
        }

        //private void Login_Click(object sender, EventArgs e)
        //{
        //    var secondIntent = new Intent(this, typeof(LoginActivity));
        //    StartActivity(secondIntent);
        //}
    }
}


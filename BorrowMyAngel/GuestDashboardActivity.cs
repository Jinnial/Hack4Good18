using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BorrowMyAngel
{
    [Activity(Label = "GuestDashboardActivity")]
    public class GuestDashboardActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);

            Button startCall = FindViewById<Button>(Resource.Id.startCall);
            startCall.Click += StartCall_Click;
        }


        private void StartCall_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AudioCallActivity));
            StartActivity(intent);


        }
    }
}
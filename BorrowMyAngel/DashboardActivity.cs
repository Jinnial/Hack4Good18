using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using System;


namespace BorrowMyAngel
{
    [Activity(Label = "Dashboard", Theme = "@style/AppTheme")]
    public class DashBoardActivity : AppCompatActivity
    {
        string id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);

            // get the ID
            id = Intent.GetStringExtra("id") ?? string.Empty;

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
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
using static Android.Views.View;

namespace BorrowMyAngel
{
    [Activity(Label = "Dashboard", Theme = "@style/AppTheme")]
    public class DashBoardActivity : AppCompatActivity
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
            // var intent = new Intent(this, typeof(AudioCallActivity));
            //StartActivity(intent);

            var uri = Android.Net.Uri.Parse("https://static.vidyo.io/4.1.24.15/connector/VidyoConnector.html?host=prod.vidyo.io&token=" + Auth.vidToken + "&resourceId=SomeRoom");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

    }
}
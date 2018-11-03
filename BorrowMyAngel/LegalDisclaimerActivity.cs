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
    [Activity(Label = "LegalDisclaimerActivity")]
    public class LegalDisclaimerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LegalDisclaimer);

            Button agreeButton = FindViewById<Button>(Resource.Id.agreeButton);
            agreeButton.Click += delegate
            {
                //take the user over to the authenticated dashboard
                var activity = new Intent(this, typeof(DashBoardActivity));
                StartActivity(activity);
            };
        }
    }
}
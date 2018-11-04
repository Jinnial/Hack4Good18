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
using Android.Util;
using Android.Support.V7.App;
using System.Threading.Tasks;

namespace BorrowMyAngel {
    [Activity(Label="Borrow My Angel", MainLauncher = true, Theme="@style/AppTheme.Splash", NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X: " + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            // Create your application here
            Log.Debug(TAG, "OnCreate for Splash Activity");
        }

        protected override void OnResume()
        {
            base.OnResume();
                Log.Debug(TAG, "Performing some startup work...");
                //Task.Delay(5000); //delay while we show the splash screen
                Task startupWork = new Task(() => { DoStartup(); });
            //Log.Debug(TAG, "Done waiting");
            
                startupWork.Start();
            
        }
        async void DoStartup() {
            Log.Debug(TAG, "Waiting done, launching main activity");
            await Task.Delay (3000);
            Log.Debug(TAG, "Startup work is finished - starting SignUpActivity.");
            StartActivity(new Intent(Application.Context, typeof (SignUpActivity)));
            //OverridePendingTransition(0, Resource.Animation.splash_fade);
        }

        public override void OnBackPressed() { }
    }
}
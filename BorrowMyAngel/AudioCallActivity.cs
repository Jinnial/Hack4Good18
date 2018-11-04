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
    [Activity(Label = "AudioCallActivity")]
    public class AudioCallActivity : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AudioCall);


            try
            {
                // Get our buttons from the layout resource and attach an event to each button 
                Button connectButton = FindViewById<Button>(Resource.Id.connectButton);
                Button disconnectButton = FindViewById<Button>(Resource.Id.disconnectButton);

                // Delegates when the buttons are clicked

                connectButton.Click += delegate
                {
                    
                };
                disconnectButton.Click += delegate
                {
                    
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        
    }
}
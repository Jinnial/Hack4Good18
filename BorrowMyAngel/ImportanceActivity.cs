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

namespace BorrowMyAngel {
    [Activity(Label = "Borrow My Angel")]
    public class ImportanceActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Importance);
            Button btnConnect = FindViewById<Button>(Resource.Id.btn_connect);
            btnConnect.Click += Connect_Click;
        }
        private void Connect_Click(object sender, EventArgs e) {
            //TODO: Link to call
            //StartActivity(new Android.Content.Intent(this, typeof(SignUpActivity)));

        }
    }
}
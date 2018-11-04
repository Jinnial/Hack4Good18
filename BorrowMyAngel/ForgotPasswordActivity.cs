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
using Firebase.Auth;

namespace BorrowMyAngel
{
    [Activity(Label = "ForgotPasswordActivity")]
    public class ForgotPasswordActivity : Activity
    {
        EditText input_email;
        Button btnResetPas;
        TextView btnBack;
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ForgotPassword);

            base.OnCreate(savedInstanceState);
            //Init Firebase  
            auth = FirebaseAuth.GetInstance(LoginActivity.app);
            //Views  
            input_email = FindViewById<EditText>(Resource.Id.forget_email);
            btnResetPas = FindViewById<Button>(Resource.Id.forget_btn_reset);
            btnResetPas.Click += ResetPassword;
        }

        private void ResetPassword(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(SignUpActivity)));
        }

        
    }
}
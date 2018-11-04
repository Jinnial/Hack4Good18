using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using static Android.Views.View;
using System;
using Newtonsoft.Json.Linq;
using Amazon.Util;

namespace BorrowMyAngel
{
    [Activity(Label = "SignUp", Theme = "@style/AppTheme")]
    public class SignUpActivity : Activity, IOnClickListener, IOnCompleteListener
    {
        Button btnSignup;
        TextView btnLogin, btnForgetPass;
        EditText input_email, input_password;
        RelativeLayout activity_sign_up;
        FirebaseAuth auth;
        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.signup_btn_login)
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
               
            }
            else
            if (v.Id == Resource.Id.signup_btn_forget_password)
            {
                StartActivity(new Intent(this, typeof(ForgetPasswordActivity)));
                
            }
            else
            if (v.Id == Resource.Id.signup_btn_register)
            {
                SignUpUser(input_email.Text, input_password.Text);
            }
        }
        private void SignUpUser(string email, string password)
        {
            auth.CreateUserWithEmailAndPassword(email, password).AddOnCompleteListener(this, this);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here  
            SetContentView(Resource.Layout.SignUp);
            //Init Firebase  
            auth = FirebaseAuth.GetInstance(LoginActivity.app);
            //Views  
            btnSignup = FindViewById<Button>(Resource.Id.signup_btn_register);
            btnLogin = FindViewById<TextView>(Resource.Id.signup_btn_login);
            btnForgetPass = FindViewById<TextView>(Resource.Id.signup_btn_forget_password);
            input_email = FindViewById<EditText>(Resource.Id.signup_email);
            input_password = FindViewById<EditText>(Resource.Id.signup_password);
            activity_sign_up = FindViewById<RelativeLayout>(Resource.Id.activity_sign_up);
            btnLogin.SetOnClickListener(this);
            btnSignup.SetOnClickListener(this);
            btnForgetPass.SetOnClickListener(this);
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                //get the current user now that they are authenticated
                FirebaseUser user = auth.CurrentUser;

                //insert into database
                WriteRecord(user);

                Toast.MakeText(this, "Registration Successful", ToastLength.Long).Show();
                Intent signup = new Intent(this, typeof(UserProfileActivity));
                signup.PutExtra("email",input_email.Text);
                StartActivity(signup);
                Finish();
            }
            else
            {
                Toast.MakeText(this, "Registration Failed", ToastLength.Long).Show();

            }
        }

        public async void WriteRecord(FirebaseUser user)
        {
            //point the lambda call to the right location endpoint
            Amazon.RegionEndpoint region = Amazon.RegionEndpoint.USEast1;

            //credentials
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(Auth.AWSKey, Auth.AWSSecret);

            try
            {
                //setup the lambda client with the credential and endpoint information
                Amazon.Lambda.AmazonLambdaClient client = new Amazon.Lambda.AmazonLambdaClient(awsCredentials, region);

                JObject myUser = new JObject();
                myUser["id"] = user.Uid;
                myUser["email"] = user.Email;

                //payload has to be encapsulated in double quotes, thus the strange escape sequences here
                var invokeRequest = new Amazon.Lambda.Model.InvokeRequest { FunctionName = "WriteLogin", InvocationType = "RequestResponse", PayloadStream = AWSSDKUtils.GenerateMemoryStreamFromString(myUser.ToString()), };

                //going to put this task on another thread so the UI doesn't lock up
                System.Threading.Tasks.Task<Amazon.Lambda.Model.InvokeResponse> responseTask = client.InvokeAsync(invokeRequest);

                await responseTask.ContinueWith((response) =>
                {
                    string statusText = "";
                    string informationalText = "Internal error: Unhandled branch point"; // Should always be set in the next "if"

                    if (response.IsCanceled) { statusText = "Cancelled"; informationalText = ""; }
                    else if (response.IsFaulted)
                    {
                        statusText = "Faulted";
                        informationalText = response.Exception.Message;
                        foreach (var exception in response.Exception.InnerExceptions)
                        {
                            informationalText += "\n" + exception.Message;
                        }
                    }
                    else if (response.IsCompleted)
                    {
                        statusText = "Finished";
                        var responseReader = new System.IO.StreamReader(response.Result.Payload);
                        informationalText = responseReader.ReadToEnd();

                        //take the user over to the profile layout so we can collect the rest of the information
                        var activity = new Intent(this, typeof(UserProfileActivity));
                        activity.PutExtra("id", user.Uid);
                        activity.PutExtra("email", user.Email);
                        StartActivity(activity);
                    }

                    // This continuation is not run on the main UI thread, so need to set up
                    // another task perform the UI changes on the correct thread.
                    RunOnUiThread(() =>
                    {
                        Console.WriteLine(statusText);
                        Console.WriteLine(informationalText);

                   });
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }


}
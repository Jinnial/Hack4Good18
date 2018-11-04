using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace BorrowMyAngel
{
    [Activity(Theme = "@style/AppTheme")]
    public class UserProfileActivity : Activity
    {
        RadioButton maleButton;
        RadioButton femButton;
        RadioButton nbButton;

        private string gender = "";
        string id;
        string email;
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                switch (value)
                {
                    case "Female":
                        gender = "f";
                        break;
                    case "Male":
                        gender = "m";
                        break;
                    case "Non-Binary":
                        gender = "n";
                        break;
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the layout resource
            SetContentView(Resource.Layout.UserProfile);

            //get the ID
            //id = Intent.GetStringExtra("id") ?? string.Empty;
            id = Intent.GetStringExtra("id") ?? string.Empty;

            //display the currently active user email
            email = Intent.GetStringExtra("email") ?? string.Empty;
            TextView emailView = FindViewById<TextView>(Resource.Id.emailView);
            emailView.Text = email;

            maleButton = FindViewById<RadioButton>(Resource.Id.maleButton);
            maleButton.Click += RadioButtonClick;

            femButton = FindViewById<RadioButton>(Resource.Id.femaleButton);
            femButton.Click += RadioButtonClick;

            nbButton = FindViewById<RadioButton>(Resource.Id.nbButton);
            nbButton.Click += RadioButtonClick;

            //get the submit button
            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);
            submitButton.Click += Submit_Click;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            string firstname;
            string nick;
            int userAge = 0;

            EditText firstName = FindViewById<EditText>(Resource.Id.firstNameBox);
            firstname = firstName.Text;

            EditText nickName = FindViewById<EditText>(Resource.Id.nicknameBox);
            nick = nickName.Text;

            EditText age = FindViewById<EditText>(Resource.Id.ageBox);
            if(age.Text.Trim() != "") {
                userAge = Convert.ToInt32(age.Text);
            }

            WriteRecord(firstname, nick, userAge, Gender);
        }

        private void RadioButtonClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            string rbText = rb.Text;
            Gender = rb.Text;
        }

        public async void WriteRecord(string fname, string nick, int age, string gender)
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
                myUser["id"] = id;
                myUser["email"] = email;
                myUser["name"] = fname;
                myUser["nickname"] = nick;
                myUser["age"] = age;
                //myUser["gender"] = Gender;
                myUser["gender"] = gender;

                //payload has to be encapsulated in double quotes, thus the strange escape sequences here
                var invokeRequest = new Amazon.Lambda.Model.InvokeRequest { FunctionName = "WriteUserProfile", InvocationType = "RequestResponse", PayloadStream = AWSSDKUtils.GenerateMemoryStreamFromString(myUser.ToString()), };

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

                        //take the user over to the legal disclaimer step
                        var activity = new Intent(this, typeof(LegalDisclaimerActivity));
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
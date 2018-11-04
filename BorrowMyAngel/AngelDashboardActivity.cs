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
    [Activity(Label = "AngelDashboardActivity", MainLauncher = true)]
    public class AngelDashboardActivity : Activity
    {
        //hard coded ID for testing
        string id = "FhVcVCpVoqW6mWtch6d3dIxoQoI2";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AngelDashboard);

            //get the ID of the currently logged in Angel


            //update status
            CheckStatus();

            Button abutton = FindViewById<Button>(Resource.Id.availableButton);
            abutton.Click += GoAvailable_Click;

            Button ubutton = FindViewById<Button>(Resource.Id.unavailableButton);
            ubutton.Click += GoUnavailable_Click;

            Button dbutton = FindViewById<Button>(Resource.Id.dndButton);
            dbutton.Click += GoDND_Click;
        }

        private void GoAvailable_Click(object sender, EventArgs e)
        {
            //get the current timestamp
            DateTime timestamp = DateTime.Now;

            //set status to 1 = Available
            int status = 1;

            WriteRecord(timestamp, status);

        }

        private void GoUnavailable_Click(object sender, EventArgs e)
        {
            //get the current timestamp
            DateTime timestamp = DateTime.Now;

            //set status 2 = Unavailable
            int status = 2;

            WriteRecord(timestamp, status);
        }

        private void GoDND_Click(object sender, EventArgs e)
        {
            //get the current timestamp
            DateTime timestamp = DateTime.Now;

            //set status 3 = DND
            int status = 3;

            WriteRecord(timestamp, status);
        }

        public async void WriteRecord(DateTime timestamp, int status)
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
                myUser["timestamp"] = timestamp;
                myUser["status"] = status;

                //payload has to be encapsulated in double quotes, thus the strange escape sequences here
                var invokeRequest = new Amazon.Lambda.Model.InvokeRequest { FunctionName = "AngelChangeStatus", InvocationType = "RequestResponse", PayloadStream = AWSSDKUtils.GenerateMemoryStreamFromString(myUser.ToString()), };

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
                    }

                    // This continuation is not run on the main UI thread, so need to set up
                    // another task perform the UI changes on the correct thread.
                    RunOnUiThread(() =>
                    {
                        Console.WriteLine(statusText);
                        Console.WriteLine(informationalText);
                        CheckStatus();

                    });
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public async void CheckStatus()
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


                //payload has to be encapsulated in double quotes, thus the strange escape sequences here
                var invokeRequest = new Amazon.Lambda.Model.InvokeRequest { FunctionName = "CheckAngelStatus", InvocationType = "RequestResponse", PayloadStream = AWSSDKUtils.GenerateMemoryStreamFromString(myUser.ToString()), };

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

                        //update the status text
                        TextView status = FindViewById<TextView>(Resource.Id.statusView);
                        status.Text = "Current Status: ";
                        switch(informationalText)
                        {
                            case "1":
                                status.Text += "Available";
                                break;
                            case "2":
                                status.Text += "Unavailable";
                                break;
                            case "3":
                                status.Text += "Do Not Disturb";
                                break;
                        }

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
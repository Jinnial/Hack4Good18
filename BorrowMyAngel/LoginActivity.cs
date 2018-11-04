using Android.App;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Auth;
using System;
using static Android.Views.View;
using Android.Views;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Android.Content;

namespace BorrowMyAngel
{
    [Activity(Label = "XamarinFirebaseAuth", Theme = "@style/AppTheme")]
    public class LoginActivity : Activity, IOnCompleteListener
    {
        Button btnLogin;
        TextView btnAngelSignUp;
        TextView btnPrivacy;
        ImageButton btnAngel;
        EditText input_email, input_password;
        TextView btnSignUp, btnForgetPassword;
        RelativeLayout activity_main;
        public static FirebaseApp app;
        FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Login);
            //Init Auth  
            InitFirebaseAuth();
            //Views  
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            input_email = FindViewById<EditText>(Resource.Id.login_email);
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            btnForgetPassword = FindViewById<TextView>(Resource.Id.login_btn_forget_password);
            activity_main = FindViewById<RelativeLayout>(Resource.Id.activity_main);
            btnAngelSignUp = FindViewById<TextView>(Resource.Id.become_angel);
            btnPrivacy = FindViewById<TextView>(Resource.Id.privacy_policy);
            btnAngel = FindViewById<ImageButton>(Resource.Id.emergency);
            btnSignUp.Click += SignUp_Click;
            btnLogin.Click += Login_Click;
            btnForgetPassword.Click += Forgot_Click;
            btnAngel.Click += Angel_Click;
            btnPrivacy.Click += Privacy_Click;
            btnAngelSignUp.Click += AngelSignUp_Click;
        }
        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
               .SetApplicationId(Auth.ApplicationID)
               .SetApiKey(Auth.APIKey)
               .Build();
            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
            auth = FirebaseAuth.GetInstance(app);
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(this, typeof(SignUpActivity)));
          
        }

        private void AngelSignUp_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("http://otcwebdev.net/borrowmyangel/angel-signup.php");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);

        }

        private void Privacy_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("http://otcwebdev.net/borrowmyangel/index.php");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);

        }

        private void Login_Click(object sender, EventArgs e)
        {
            LoginUser(input_email.Text, input_password.Text);
        }

        private void Forgot_Click(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(this, typeof(ForgotPasswordActivity)));
           
        }

        private void Angel_Click(object sender, EventArgs e) {
            StartActivity(new Android.Content.Intent(this, typeof(GuestDashboardActivity)));
            
        }


        private void LoginUser(string email, string password)
        {
            auth.SignInWithEmailAndPassword(email, password).AddOnCompleteListener(this);
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                //get their uID
                FirebaseUser user = auth.CurrentUser;
                string id = user.Uid;

                //find out which app role they have so we send them to the right dashboard
                //currently hard-coded for demo
                string angelId = "z31cp6TuFvVTAppIcrrSpJLi6AJ2";

                if(angelId == "z31cp6TuFvVTAppIcrrSpJLi6AJ2")
                {
                    //if they are an angel
                    var intent = new Intent(this, typeof(AngelDashboardActivity));
                    intent.PutExtra("id", id);
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    //if they are a user 
                    var intent = new Intent(ApplicationContext, typeof(DashBoardActivity));
                    intent.PutExtra("id", id);
                    StartActivity(intent);
                    Finish();
                }
                

                
            }
            else
            {
                Toast.MakeText(this, "Login Failed", ToastLength.Long).Show();
            }
        }

  
    }
}
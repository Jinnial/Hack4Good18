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
    public class UserAccount {
        private string userID;
        private string email;
        private string password;

        private string fName;
        private string nickname;
        private int age;
        private char gender;
        private string city;
        private string state;

        public UserAccount(string userID, string email, string password) {
            this.userID = userID;
            this.email = email;
            this.password = password;
        }

        public string UserID { get => userID; set => userID = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string FName { get => fName; set => fName = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int Age { get => age; set => age = value; }
        public char Gender { get => gender; set => gender = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
    }
}
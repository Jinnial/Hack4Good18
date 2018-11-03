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
    class AngelAccount : UserAccount{
        private string lName;
        private string mName;
        private string address;
        private int phone;
        private List<string> references;

        private string reasons;
        private string crisisExperience;
        private string volunteerExperience;
        private bool backgroundCheckPermission;
        private string convictions;

        public AngelAccount(string userID, string email, string password, string lName, string mName, string address, int phone, List<string> references, string reasons, string crisisExperience, string volunteerExperience, bool backgroundCheckPermission, string convictions) :
            base(userID, email, password){
            this.lName = lName;
            this.mName = mName;
            this.address = address;
            this.phone = phone;
            this.references = references;
            this.reasons = reasons;
            this.crisisExperience = crisisExperience;
            this.volunteerExperience = volunteerExperience;
            this.backgroundCheckPermission = backgroundCheckPermission;
            this.convictions = convictions;
        }
    }
}
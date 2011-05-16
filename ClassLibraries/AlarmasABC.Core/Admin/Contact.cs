using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class Contact
    {

        public IList<Contact> _dropDownList = new List<Contact>();

        public Contact()
        {
            /// Default Constructor
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }     


        private string _Email;
        /// <summary>
        /// Set/Get Company Email address
        /// </summary>

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _department;

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

     

    }
}

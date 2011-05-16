using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class Company
    {

        public IList<Company> _dropDownList=new List<Company>();

        public Company()
        { 
            /// Default Constructor
        }

        public Company(int value, string name)
        {
            _comID = value; _CompanyName = name;
        }
        
        public int Value
        {
            get { return _comID; }
            set { _comID = value; }
        }

        
        public string Name
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        
        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private string _CompanyName;

        /// <summary>
        /// Set/Get Company Name
        /// </summary>

        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        private string _ContactPerson;
        /// <summary>
        ///  Set/Get Company contact person name
        /// </summary>

        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        private string _Address;
        /// <summary>
        /// Set/Get Company Address
        /// </summary>

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Phone;
        /// <summary>
        /// Set/Get Company Phone Number
        /// </summary>

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _WebSite;
        /// <summary>
        /// Set/Get Company Website
        /// </summary>

        public string WebSite
        {
            get { return _WebSite; }
            set { _WebSite = value; }
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

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private bool _isActive;
        /// <summary>
        /// Set/Get Company status Whether company is active or not
        /// </summary>

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

    }
}

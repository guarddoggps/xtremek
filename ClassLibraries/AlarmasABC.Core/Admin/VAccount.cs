using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class VAccount
    {
        public IList<VAccount> _userDropDown = new List<VAccount>();
        public VAccount()
        {
            // Default Constructor
        }



        //public VAccount(int value, string name)
        //{
        //    _uID = value; _login = name;
        //}

        #region private variables and properties

        private int _uID;
        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }

        private string _fName;

        public string FName
        {
            get { return _fName; }
            set { _fName = value; }
        }

        private string _lName;

        public string LName
        {
            get { return _lName; }
            set { _lName = value; }
        }

        private string _ini;

        public string Ini
        {
            get { return _ini; }
            set { _ini = value; }
        }

        private string _cName;

        public string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        private string _sAddress;

        public string SAddress
        {
            get { return _sAddress; }
            set { _sAddress = value; }
        }

        private string _apt;

        public string Apt
        {
            get { return _apt; }
            set { _apt = value; }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _state;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _zip;

        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        private string _country;

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _hPhone;

        public string HPhone
        {
            get { return _hPhone; }
            set { _hPhone = value; }
        }

        private string _oPhone;

        public string OPhone
        {
            get { return _oPhone; }
            set { _oPhone = value; }
        }
        private string _cPhone;

        public string CPhone
        {
            get { return _cPhone; }
            set { _cPhone = value; }
        }

        private DateTime _dOB;

        public DateTime DOB
        {
            get { return _dOB; }
            set { _dOB = value; }
        }


        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _securityQuestion;
        public string SecurityQuestion
        {
            get { return _securityQuestion; }
            set { _securityQuestion = value; }
        }

        private string _securityAnswer;
        public string SecurityAnswer
        {
            get { return _securityAnswer; }
            set { _securityAnswer = value; }
        }





        #endregion

        #region User DropDownList

        //public int Value
        //{
        //    get { return _uID; }
        //    set { _uID = value; }
        //}

        //public string Name
        //{
        //    get { return _login; }
        //    set { _login = value; }
        //}

        #endregion
    }
}


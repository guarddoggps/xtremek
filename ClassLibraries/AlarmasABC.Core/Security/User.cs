using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Security
{
    public class User
    {
        public IList<User> _list = new List<User>();
        public User()
        { 
            // Default Constructor
        }

        

        public User(int value,string name)
        {
            _uID = value; _login = name;
        }

        #region private variables and properties
       
            private int _uID;
            public int UID
            {
                get { return _uID; }
                set { _uID = value; }
            }

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _groupID;
            public int GroupID
            {
                get { return _groupID; }
                set { _groupID = value; }
            }

            private string _login;
            public string Login
            {
                get { return _login; }
                set { _login = value; }
            }

            private string _userName;
            public string UserName
            {
                get { return _userName; }
                set { _userName = value; }
            }

            private string _Password;
            public string Password
            {
                get { return _Password; }
                set { _Password = value; }
            }

            private bool _isActive;
            public bool IsActive
            {
                get { return _isActive; }
                set { _isActive = value; }
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

            private int _RoleID;
            public int RoleID
            {
                get { return _RoleID; }
                set { _RoleID = value; }
            }

            private float _timeZone;
            public float TimeZone
            {
                get { return _timeZone; }
                set { _timeZone = value; }
            }

            private bool _IsLogIn;
            public bool IsLogIn
            {
                get { return _IsLogIn; }
                set { _IsLogIn = value; }
            }

        #endregion

        #region User DropDownList

            public int Value
            {
                get { return _uID; }
                set { _uID = value; }
            }

            public string Name
            {
                get { return _login; }
                set { _login = value; }
            }

        #endregion
    }
}

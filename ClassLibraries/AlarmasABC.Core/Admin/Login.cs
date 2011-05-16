using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class Login
    {

        /*
         * UID,COMID,USERNAME,ROLEID
         * @COMPANYNAME		VARCHAR(100),
	@LOGINNAME			VARCHAR(50),
	@PASSWORD			VARCHAR(100)
         * */

        private int _uID;
        /// <summary>
        /// Set/Get User ID
        /// </summary>
        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }

        private int _comID;
        /// <summary>
        /// Get/Set Company ID
        /// </summary>
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private string _comName;
        public string ComName
        {
            get { return _comName; }
            set { _comName = value; }
        }

        private string _loginName;
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        private string _passWord;
        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        private string _userName;
        /// <summary>
        /// Get/Set User Name
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private int _roleID;
        /// <summary>
        /// Get/Set User Role ID
        /// </summary>
        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }
    }
}

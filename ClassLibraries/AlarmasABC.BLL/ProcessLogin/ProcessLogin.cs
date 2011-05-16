using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessLogin
{
    public class ProcessLogin:IAlopekBusinessLogic
    {
        

        private Login _userLogin;
        public Login UserLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void invoke()
        {
            LogInUser();
        }

        private void LogInUser()
        {
            try
            {
                UserLogin _login = new UserLogin();
                _login.LoginObj = this.UserLogin;
                _login.Login();
                this.Ds = _login.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception(" BLL:: ProcessLogin :: "+ex.Message);
            }
            
        }
    }
}

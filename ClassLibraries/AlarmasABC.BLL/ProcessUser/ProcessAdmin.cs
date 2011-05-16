using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessUser
{
    public class ProcessAdmin:IAlopekBusinessLogic
    {
        #region Private variables and Properties

            private string _loginName;
            public string LoginName
            {
                get { return _loginName; }
                set { _loginName = value; }
            }

            private string _password;
            public string Password
            {
                get { return _password; }
                set { _password = value; }
            }

            private DataSet _ds;
            public DataSet Ds
            {
                get { return _ds; }
                set { _ds = value; }
            }

        #endregion

            public void invoke()
            {

                AdminLogin _admin = new AdminLogin();
                
                try
                {
                    _admin.LoginName = this.LoginName;
                    _admin.Password = this.Password;
                    _admin.AdminLogIn();
                    this.Ds = _admin.Ds;
                }
                catch (Exception ex)
                {
                    throw new Exception("BLL::ProcessAdmin::" + ex.Message);
                }
                finally
                {
                    _admin = null;
                }
            }

    }
}

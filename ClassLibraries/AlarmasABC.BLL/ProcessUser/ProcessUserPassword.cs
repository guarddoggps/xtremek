using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessUser
{
    public class ProcessUserPassword:IAlopekBusinessLogic
    {
        #region Private variables and Properties

        private string _loginName;
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        private int _securityQ;
        public int SecurityQ
        {
            get { return _securityQ; }
            set { _securityQ = value; }
        }

        private string _securityA;
        public string SecurityA
        {
            get { return _securityA; }
            set { _securityA = value; }
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
            PasswordRetrive _pass = new PasswordRetrive();

            try
            {
                _pass.LoginName = this.LoginName;
                _pass.SecurityQ = this.SecurityQ;
                _pass.SecurityA = this.SecurityA;
                _pass.GetPassword();
                this.Ds = _pass.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUserPassword::" + ex.Message);
            }
            finally
            {
                _pass = null;
            }
        }        
    }
}

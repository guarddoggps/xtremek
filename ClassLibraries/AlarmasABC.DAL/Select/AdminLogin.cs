using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Select
{
    public class AdminLogin:DataAccessBase
    {
        public AdminLogin()
        {
				Command = @"SELECT * FROM tblUser WHERE login = :login AND password = :password" +
            		  	  @" AND roleID != 2 AND coalesce(isDelete,'0') != '1'";
        }

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

        public void AdminLogIn()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);

            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("login",     NpgsqlTypes.NpgsqlDbType.Varchar, 100,    ParameterDirection.Input,   this.LoginName),
                                        DataBaseHelper.MakeParam("password",   NpgsqlTypes.NpgsqlDbType.Varchar, 150,    ParameterDirection.Input,   this.Password)
                                     };
            return _params;
        }
    }
}

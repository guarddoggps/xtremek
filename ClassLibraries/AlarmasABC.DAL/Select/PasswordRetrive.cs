using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;


namespace AlarmasABC.DAL.Select
{
    public class PasswordRetrive:DataAccessBase
    {
        public PasswordRetrive()
        {
            Command = "SELECT password FROM tblUser WHERE login = :login AND" +
					  " securityQuestion = :securityQ AND securityAnswer = :securityA;";
        }

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

        public void GetPassword()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);

            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::Password Retrive::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("login",   NpgsqlTypes.NpgsqlDbType.Varchar,     100,    ParameterDirection.Input, this.LoginName),
                                        DataBaseHelper.MakeParam("securityQ", NpgsqlTypes.NpgsqlDbType.Varchar,          4,    ParameterDirection.Input, this.SecurityQ),
                                        DataBaseHelper.MakeParam("securityA",  NpgsqlTypes.NpgsqlDbType.Varchar,   200,    ParameterDirection.Input, this.SecurityA)
                                     };
            return _params;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class UserLogin:DataAccessBase
    {
        public UserLogin()
        {
            Command = @"SELECT uID,comID,userName,roleID FROM tblUser" +
                      @" WHERE comID=(SELECT comID FROM tblCompany" +
                      @" WHERE companyName = :companyName) AND login = :loginName"+
                      @" AND password = :password AND isActive='1';";
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private Login _loginObj;
        public Login LoginObj
        {
            get { return _loginObj; }
            set { _loginObj = value; }
        }

		public void Login()
		{                        
        	DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);   
            try
            {
				this._ds = _db.Run(base.ConnectionString, returnParams());
            }
            catch (Exception ex)
			{
                throw new Exception(" DAL:: UserLogin:: " + ex.Message);
            }
            finally
			{
            	_db = null;
            }
        }


       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                      DataBaseHelper.MakeParam("companyName",         NpgsqlTypes.NpgsqlDbType.Varchar,     100,        ParameterDirection.Input,       this._loginObj.ComName),
                                      DataBaseHelper.MakeParam("loginName",            NpgsqlTypes.NpgsqlDbType.Varchar,     50,         ParameterDirection.Input,       this._loginObj.LoginName),
                                      DataBaseHelper.MakeParam("password",             NpgsqlTypes.NpgsqlDbType.Varchar,     100,        ParameterDirection.Input,       this._loginObj.PassWord)
                                     };

            return _params;
        }
    }
}

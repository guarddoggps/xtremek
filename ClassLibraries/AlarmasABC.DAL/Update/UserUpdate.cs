using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Update
{
    public class UserUpdate:DataAccessBase
    {
        public UserUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_USER.ToString();
        }

        private User _userObj;
        public User UserObj
        {
            get { return _userObj; }
            set { _userObj = value; }
        }

        public void updateUser()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, returnUpdateParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::UpdateUser():: " + ex.Message);
            }
        }

      
       private NpgsqlParameter[] returnUpdateParam()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@uID",       NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this._userObj.UID),
                                        DataBaseHelper.MakeParam("@groupID",    NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this._userObj.GroupID),
                                        DataBaseHelper.MakeParam("@password",  NpgsqlTypes.NpgsqlDbType.Varchar,     100,    ParameterDirection.Input,   this._userObj.Password),
                                        DataBaseHelper.MakeParam("@userName",   NpgsqlTypes.NpgsqlDbType.Varchar,     80,     ParameterDirection.Input,   this._userObj.UserName),
                                        DataBaseHelper.MakeParam("@email",      NpgsqlTypes.NpgsqlDbType.Varchar,     70,     ParameterDirection.Input,   this._userObj.Email),
                                        DataBaseHelper.MakeParam("@securityQuestion",  NpgsqlTypes.NpgsqlDbType.Integer,     4,    ParameterDirection.Input,   this._userObj.SecurityQuestion),
                                        DataBaseHelper.MakeParam("@securityAnswer",   NpgsqlTypes.NpgsqlDbType.Varchar,          200,      ParameterDirection.Input,   this._userObj.SecurityAnswer),
                                        DataBaseHelper.MakeParam("@schemeID",   NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this._userObj.SecurityScheme),
                                        DataBaseHelper.MakeParam("@isActive",  NpgsqlTypes.NpgsqlDbType.Boolean,          1,      ParameterDirection.Input,   this._userObj.IsActive)
                                     };

            return _params;
        }
    }
}

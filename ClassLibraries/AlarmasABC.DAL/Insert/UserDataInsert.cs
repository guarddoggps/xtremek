using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;


namespace AlarmasABC.DAL.Insert
{
    public class UserDataInsert:DataAccessBase
    {

        public UserDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_USER.ToString();
        }

        private User _userObj;
        public User UserObj
        {
            get { return _userObj; }
            set { _userObj = value; }
        }

        public void addUser()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);
                _ds = _db.Run(base.ConnectionString, returnParams());

                UserObj.UID = int.Parse(_ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(" UserDataInsert:: Add User() " + ex.Message.ToString());
            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = {
                                        DataBaseHelper.MakeParam("@userName",      NpgsqlTypes.NpgsqlDbType.Varchar,     80,     ParameterDirection.Input,       this._userObj.UserName),
                                        DataBaseHelper.MakeParam("@login",          NpgsqlTypes.NpgsqlDbType.Varchar,     40,     ParameterDirection.Input,       this._userObj.Login),
                                        DataBaseHelper.MakeParam("@comID",         NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,       this._userObj.ComID),
                                        DataBaseHelper.MakeParam("@groupID",        NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,       this._userObj.GroupID),
                                        DataBaseHelper.MakeParam("@password",          NpgsqlTypes.NpgsqlDbType.Varchar,     150,    ParameterDirection.Input,       this._userObj.Password),
                                        DataBaseHelper.MakeParam("@email",          NpgsqlTypes.NpgsqlDbType.Varchar,     80,     ParameterDirection.Input,       this._userObj.Email),
                                        DataBaseHelper.MakeParam("@isActive",      NpgsqlTypes.NpgsqlDbType.Boolean,          1,      ParameterDirection.Input,       this._userObj.IsActive),
                                        DataBaseHelper.MakeParam("@securityScheme", NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,       this._userObj.SecurityScheme),
                                        DataBaseHelper.MakeParam("@securityQuestion",NpgsqlTypes.NpgsqlDbType.Integer,    4,    ParameterDirection.Input, this._userObj.SecurityQuestion),
                                        DataBaseHelper.MakeParam("@securityAnswer", NpgsqlTypes.NpgsqlDbType.Varchar,     200,    ParameterDirection.Input,       this._userObj.SecurityAnswer)
                                     
                                     };
            return _params;
        }
    }
    
}

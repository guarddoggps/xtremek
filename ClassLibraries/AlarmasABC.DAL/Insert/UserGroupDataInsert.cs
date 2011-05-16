using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Insert
{
    public class UserGroupDataInsert:DataAccessBase
    {
        public UserGroupDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_USER_GROUP.ToString();
        }

        private UserGroup _userGroup;
        public UserGroup UserGroup
        {
            get { return _userGroup; }
            set { _userGroup = value; }
        }

        public void addUserGroup()
        {
            makeInsertParams _insParam = new makeInsertParams(this._userGroup);
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString,_insParam._params1);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally 
            {
                _insParam = null;
            }
        }
    }

    class makeInsertParams
    {
        private UserGroup _userGroup;

        public makeInsertParams(UserGroup _userGroup)
        {
            this._userGroup = _userGroup;
            build();
        }

       private NpgsqlParameter[] _params;
        public NpgsqlParameter[] _params1
        {
            get { return _params; }
            set { _params = value; }
        }

         public void build()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@groupName", NpgsqlTypes.NpgsqlDbType.Varchar,     100,    ParameterDirection.Input,   this._userGroup.GroupName),
                                        DataBaseHelper.MakeParam("@comID",     NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this._userGroup.ComID)
                                    };
            this._params = _param;
        }
    }
}

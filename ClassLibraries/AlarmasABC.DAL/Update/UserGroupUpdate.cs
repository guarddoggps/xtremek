using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Update
{
    public class UserGroupUpdate:DataAccessBase
    {
        public UserGroupUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_USER_GROUP.ToString();
        }

        private UserGroup _usergroup;
        public UserGroup Usergroup
        {
            get { return _usergroup; }
            set { _usergroup = value; }
        }

        public void deleteGroup()
        {
            makeUpdateParam _updateParam = new makeUpdateParam(this._usergroup);
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _updateParam._params1);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            finally 
            {
                _updateParam = null;
            }
        }
        
    }

    class makeUpdateParam
    {
        private UserGroup _userGroup;

        public makeUpdateParam(UserGroup _userGroup)
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

        private void build()
        {
            try
            {
                NpgsqlParameter[] _param = { 
                                            DataBaseHelper.MakeParam("@groupID",   NpgsqlTypes.NpgsqlDbType.Integer,      4,      ParameterDirection.Input,       this._userGroup.GroupID),
                                            DataBaseHelper.MakeParam("@groupName", NpgsqlTypes.NpgsqlDbType.Varchar, 100,    ParameterDirection.Input,       this._userGroup.GroupName)                                            
                                        };
                this._params1 = _param;
            }
            catch (Exception ex)
            { 
            
            }

        }


    }
}

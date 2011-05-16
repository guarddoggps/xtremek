using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
   public class UserGroupDelete:DataAccessBase
    {
       public UserGroupDelete()
       {
          Command = StoredProcedure.Name.SP_DELETE_USER_GROUP.ToString();
       }

       private UserGroup _userGroup;
       public UserGroup UserGroup
       {
           get { return _userGroup; }
           set { _userGroup = value; }
       }


       public void deleteUserGroup()
       {
           makeDeleteParam _delParam = new makeDeleteParam(this._userGroup);
           try
           {
               DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
               _db.Run(base.ConnectionString, _delParam._params1);
           }
           catch (Exception )
           {
           }
           finally
           {
               _delParam = null;
           }
       }

    }

   class makeDeleteParam
   {
       public makeDeleteParam(UserGroup _userGroup)
       {
           this._userGroup = _userGroup;
           build();
       }

       private UserGroup _userGroup;
      private NpgsqlParameter[] _params;
       public NpgsqlParameter[] _params1
       {
           get { return _params; }
           set { _params = value; }
       }


       private void build()
       {
           NpgsqlParameter[] _param = { 
                                    DataBaseHelper.MakeParam("@groupID",   NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._userGroup.GroupID)
                                   };

           this._params1 = _param;
       }


   }
}

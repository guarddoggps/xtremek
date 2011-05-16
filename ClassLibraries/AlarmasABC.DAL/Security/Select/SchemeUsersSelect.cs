using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Security.Select
{
    public class SchemeUsersSelect:DataAccessBase
    {
        public SchemeUsersSelect()
       {
          Command = "SELECT COUNT(*) AS userCount FROM tblUserWiseScheme WHERE schemeID = :schemeID";
       }

       #region instance variable and propertirs
       private int _ID;

       public int ID
       {
           get { return _ID; }
           set { _ID = value; }
       }

       #endregion

       private DataSet _ds;

       public DataSet Ds
       {
           get { return _ds; }
           set { _ds = value; }
       }

       public void selectSchemeUsers()
       {
           try
           {
               DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
               this._ds = _db.Run(base.ConnectionString, returnParams());
           }
           catch (Exception ex)
           {
               ex.Message.ToString();
           }
           finally
           {

           }
       }

      private NpgsqlParameter[] returnParams()
       {
           NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("schemeID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._ID)
                                     };
           return _params;
       }

    }
}

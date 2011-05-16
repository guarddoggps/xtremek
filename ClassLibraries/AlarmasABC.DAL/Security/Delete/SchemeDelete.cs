using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Security.Delete
{
    public class SchemeDelete:DataAccessBase
    {

        public SchemeDelete()
       {
          Command = StoredProcedure.Name.SP_DELETE_SCHEME.ToString();
       }

       #region instance variable and propertirs
       private int _ID;

       public int ID
       {
           get { return _ID; }
           set { _ID = value; }
       }
       private int _comID;

       public int ComID
       {
           get { return _comID; }
           set { _comID = value; }
       }
       #endregion

       private DataSet _ds;

       public DataSet Ds
       {
           get { return _ds; }
           set { _ds = value; }
       }

       public void DeleteScheme()
       {
           try
           {
               DataBaseHelper _db =new DataBaseHelper(Command, CommandType.StoredProcedure);
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
                                        DataBaseHelper.MakeParam("@SCHEMEID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._ID),
                                        DataBaseHelper.MakeParam("@COMID",  NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._comID)
                                     };
           return _params;
       }


    }
}

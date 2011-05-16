using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Security.Select
{
    public class SchemeUnitsSelect:DataAccessBase
    {

        public SchemeUnitsSelect()
       {
          Command = "SELECT count(*) AS unitCount FROM tblUnits WHERE unitID IN" +
				    " (SELECT unitID FROM tblUnitUserWise WHERE uID IN (SELECT userID" +
					" FROM tblUserWiseScheme WHERE schemeID = :schemeID)) AND coalesce(isDelete,'0') != '1';";
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

       public void selectSchemeUnits()
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

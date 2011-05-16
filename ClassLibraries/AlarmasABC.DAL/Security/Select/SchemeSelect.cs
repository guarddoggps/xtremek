using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Security.Select
{
   public class SchemeSelect:DataAccessBase
    {
       	public SchemeSelect()
       	{
			Command = "SELECT id,schemeName,defaultScheme FROM tblSecurityScheme" +
					  " WHERE comID = :comID ORDER BY schemeName ASC;";
       	}

       #region instance variable and propertirs
       private int _ID;

       public int ID
       {
           get { return _ID; }
           set { _ID = value; }
       }
       private string _schemeName;

       public string SchemeName
       {
           get { return _schemeName; }
           set { _schemeName = value; }
       }
       private int _comID;

       public int ComID
       {
           get { return _comID; }
           set { _comID = value; }
       }
       private bool defaultScheme;

       public bool DefaultScheme
       {
           get { return defaultScheme; }
           set { defaultScheme = value; }
       }
       #endregion

       private DataSet _ds;

       public DataSet Ds
       {
           get { return _ds; }
           set { _ds = value; }
       }

       public void selectScheme()
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
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._comID)
                                     };
           return _params;
       }
    }
}

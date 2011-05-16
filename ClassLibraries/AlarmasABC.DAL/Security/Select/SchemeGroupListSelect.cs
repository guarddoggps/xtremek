using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Security;

namespace AlarmasABC.DAL.Security.Select
{
    public class SchemeGroupListSelect:DataAccessBase
    {

    	public SchemeGroupListSelect()
       	{
			Command = "SELECT groupID,groupName FROM tblGroup WHERE comID = :comID" +
          			  " AND coalesce(isDelete,'0') != '1' ORDER BY groupName ASC;";
       	}

       #region instance variable and propertirs

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

       public void LoadGroupList(IList<Group> _GLists)
       {
           NpgsqlDataReader _dr = null;
           DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
           try
           {
               _dr = _db.ExecuteReader(returnParam());

               // _GLists.Add(new UserGroup(0,));

               while (_dr.Read())
               {
                   _GLists.Add(new Group(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
               }
           }
           catch (Exception ex)
           {
               throw new Exception(" :: " + ex.Message);
           }
           finally
           {
               if (_dr != null)
               {
                   _db = null;
                   _dr = null;
                   _GLists = null;
               }

           }
       }

      private NpgsqlParameter[] returnParam()
       {
           NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._comID)
                                     };
           return _params;
       }


    }
}

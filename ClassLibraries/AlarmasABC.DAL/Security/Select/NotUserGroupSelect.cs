using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Security.Select
{
    public class NotUserGroupSelect:DataAccessBase
    {

        public NotUserGroupSelect(string _schemeID,string _comID)
        {
            this.SchemeID = _schemeID;
            this.ComID = _comID;
			Command = "SELECT * FROM tblGroup WHERE groupID NOT IN (SELECT groupID" +
					  " FROM tblUser WHERE uID IN (SELECT userID FROM tblUserWiseScheme" +
            		  " WHERE schemeID = :schemeID AND comID = :comID) AND comID = :comID)" +
					  " AND coalesce(isDelete,'0') != '1' AND comID = :comID ORDER BY groupName ASC;";
        }

        private string _schemeID;

        public string SchemeID
        {
            get { return _schemeID; }
            set { _schemeID = value; }
        }

        private string _comID;
        public string ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        public DataSet getNotUserGroup()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);                
                _ds= _db.Run(base.ConnectionString,returnParam());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
            return _ds;
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("schemeID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.SchemeID),
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                    };

            return _param;
        }
    }
}

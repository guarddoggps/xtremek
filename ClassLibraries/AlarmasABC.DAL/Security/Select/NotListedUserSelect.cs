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
    public class NotListedUserSelect:DataAccessBase
    {

        public NotListedUserSelect(string _schemeID, string _comID)
        {
            this.SchemeID = _schemeID;
            this.ComID = _comID;
		    Command = "SELECT uID,login FROM tblUser WHERE uID NOT IN (SELECT userID" +
					  " FROM tblUserWiseScheme WHERE schemeID = :schemeID) AND comID = :comID" +
            		  " AND coalesce(isDelete,'0') != '1' ORDER BY login ASC;";

        }

        private string _comID;

        public string ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private string _schemeID;

        public string SchemeID
        {
            get { return _schemeID; }
            set { _schemeID = value; }
        }

        public DataSet getNotListedUser()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);                
                _ds= _db.Run(base.ConnectionString,returnParam());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Select
{
    public class SelectEventList:DataAccessBase
    {
        public SelectEventList()
        {
            Command = "SELECT DISTINCT alertType,alertMessage,(SELECT unitName FROM tblUnits" + 
                      " WHERE tblUnits.unitID = al.unitID) AS unitName," + 
                      " (SELECT alertTime FROM tblAlert alrt WHERE alrt.unitID = al.unitID" + 
                      " AND comID = :comID ORDER BY alertTime DESC LIMIT 1) AS alertTime FROM tblAlert al" +
                      " WHERE unitID IN (SELECT unitID FROM tblUnitUserWise WHERE uID = :userID)" +
                      " AND comID = :comID AND to_char(abstime(alertTime), 'MM/DD/YYYY') = to_char('now'::timestamp, 'MM/DD/YYYY');";
        }

        #region Private variables and properties

        private int _userID;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }
        #endregion

        public void GetEvents()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);

            try
            {
                this.Ds = _db.Run(base.ConnectionString, GetParams());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] GetParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("userID",    NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UserID),
                                        DataBaseHelper.MakeParam("comID",      NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                     };
            return _params;
        }
    }
}

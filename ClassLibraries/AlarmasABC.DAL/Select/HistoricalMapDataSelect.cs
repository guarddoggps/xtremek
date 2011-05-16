using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Tracking;


namespace AlarmasABC.DAL.Select
{
    public class HistoricalMapDataSelect:DataAccessBase
    {
        public HistoricalMapDataSelect()
        {
			Command = "SELECT DISTINCT unitName,lat,long,deviceID,CAST(coalesce(velocity,'0') * 0.621 AS INT) AS velocity," + 
					  "recTime,recTimeRevised AS recDateTime,distance,iconName FROM unitRecords WHERE deviceID = :deviceID" + 
					  " AND recTimeRevised::date = :historyDate AND comID = :comID ORDER BY recTime DESC;";
        }

        #region Private variables and Properties

            private int _deviceID;
            public int DeviceID
            {
                get { return _deviceID; }
                set { _deviceID = value; }
            }

            private string _date;
            public string Date
            {
                get { return _date; }
                set { _date = value; }
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

        public void GetHistoricalData()
        {
			
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
			
            try
            {
				this._ds = _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::GetHistoricalMapData:: " + ex.Message);
            }
            finally
            {
                _db = null;
            }

        }
		
       private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("historyDate", NpgsqlTypes.NpgsqlDbType.Varchar, 20, ParameterDirection.Input,   this.Date),
                                        DataBaseHelper.MakeParam("deviceID",  NpgsqlTypes.NpgsqlDbType.Integer,      4,  ParameterDirection.Input,   this.DeviceID),
                                        DataBaseHelper.MakeParam("comID",   NpgsqlTypes.NpgsqlDbType.Integer,      4,  ParameterDirection.Input,   this.ComID)
                                     };
            return _params;
        }
    }
}

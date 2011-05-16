using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Tracking;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class BreadCrumbsDataSelect:DataAccessBase
    {
        public BreadCrumbsDataSelect()
		{
        }

        #region Private Variables and Properties

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _deviceID;
            public int DeviceID
            {
                get { return _deviceID; }
                set { _deviceID = value; }
            }

            private long _recTime;
            public long RecTime
            {
                get { return _recTime; }
                set { _recTime = value; }
            }

            private IList<MapData> _mapdata;
            public IList<MapData> Mapdata
            {
                get { return _mapdata; }
                set { _mapdata = value; }
            }

        #endregion

        public void SelectData1()
        {
        	Command = "SELECT unitName,lat,long,deviceID,CAST(velocity * 0.621 AS int) AS velocity," +
					  "recTime,recTimeRevised,iconName FROM unitRecords WHERE deviceID = :deviceID" + 
					  " AND comID = :comID AND recTime BETWEEN :recTime	AND (SELECT MAX(recTime) FROM tblGPRS" +
					  " WHERE deviceID = :deviceID AND msgType != 4) ORDER BY recTime DESC LIMIT 25;";
			
			DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
           	NpgsqlDataReader _dr = null;

            try
            {
                _dr = _db.ExecuteReader(ReturnParams1());
                while(_dr.Read())
                {
                    this._mapdata.Add(new MapData( _dr[0].ToString(), decimal.Parse(_dr[1].ToString()), decimal.Parse(_dr[2].ToString()),
                        _dr[3].ToString(),_dr[4].ToString(),_dr[5].ToString(),_dr[6].ToString(),_dr[7].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::SelectData1::"+ex.Message);
            }
            finally
            {
                _dr = null;
            }
        }

        public void SelectData2()
		{
        	Command = "SELECT unitName,lat,long,deviceID,CAST(velocity * 0.621 AS int) AS velocity," + 
					  "recTime,recTimeRevised,iconName FROM unitRecords WHERE recTime = (SELECT MAX(recTime)" + 
					  " FROM unitRecords WHERE deviceID = :deviceID AND comID = :comID AND msgType != 4) AND comID = :comID" +
					  " AND deviceID = :deviceID;";
			
			DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            NpgsqlDataReader _dr = null;

            try
            {
                _dr = _db.ExecuteReader(ReturnParams2());

                while (_dr.Read())
                {
                    this.Mapdata.Add(new MapData( _dr[0].ToString(), decimal.Parse(_dr[1].ToString()), decimal.Parse(_dr[2].ToString()),
                        _dr[3].ToString(), _dr[4].ToString(), _dr[5].ToString(), _dr[6].ToString(),_dr[7].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::SelectData1::" + ex.Message);
            }
            finally
			{
            	_db = null;
                _dr = null;
            }
        }

       private NpgsqlParameter[] ReturnParams1()
        {
       
            NpgsqlParameter[] _params = { 
                                     DataBaseHelper.MakeParam("deviceID", NpgsqlTypes.NpgsqlDbType.Integer,      4,  ParameterDirection.Input,   this.DeviceID),
                                     DataBaseHelper.MakeParam("recTime",  NpgsqlTypes.NpgsqlDbType.Bigint,   12, ParameterDirection.Input,   this.RecTime),
                                     DataBaseHelper.MakeParam("comID",     NpgsqlTypes.NpgsqlDbType.Integer,      4,  ParameterDirection.Input,   this.ComID)
                                     };
            return _params;
        }

        private NpgsqlParameter[] ReturnParams2()
        {
 
            NpgsqlParameter[] _params = { 
                                     DataBaseHelper.MakeParam("deviceID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.DeviceID),
                                     DataBaseHelper.MakeParam("comID",     NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                     };
            return _params;
        }
    }
}

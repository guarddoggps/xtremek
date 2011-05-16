
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmasABC.Core.Tracking;
using Npgsql;

namespace AlarmasABC.DAL.Select
{
	public class AlertSelect:DataAccessBase
	{
		public AlertSelect()
		{
			Command = "SELECT alertType,alertMessage FROM tblAlert where alertTime = :alertTime::timestamp AND" + 
					   " unitID = :unitID AND comID = :comID";
		}
		
        private int _unitID;
        public int UnitID
        {
        	get { return _unitID; }
        	set { _unitID = value; }
        }
		
        private int _comID;
        public int ComID
        {
        	get { return _comID; }
            set { _comID = value; }
        }
		
        private string _alertTime;
        public string AlertTime
        {
        	get { return _alertTime; }
            set { _alertTime = value; }
        }
		
        private AlertData _alert;
        public AlertData Alert
        {
        	get { return _alert; }
            set { _alert = value; }
        }
		
		public void GetAlert()
		{
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
			try 
			{
				DataSet _ds;
				_ds = _db.Run(base.ConnectionString, ReturnParams());
				
			 	_alert.AlertTime = _alertTime;
				
				if (_ds.Tables[0].Rows.Count > 0) {
					_alert.AlertType = _ds.Tables[0].Rows[0]["alertType"].ToString();
					_alert.AlertMessage =_ds.Tables[0].Rows[0]["alertMessage"].ToString();
				} else {
					_alert.AlertType = "None";
					_alert.AlertMessage = "";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("AlarmasABC.DAL.AlertSelect: " + ex.Message);
			}
			finally
			{
				_db = null;
			}
		}
		
       	private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                              			DataBaseHelper.MakeParam("alertTime", NpgsqlTypes.NpgsqlDbType.Timestamp, 20, ParameterDirection.Input,  DateTime.Parse(this.AlertTime)),
                                        DataBaseHelper.MakeParam("unitID",  NpgsqlTypes.NpgsqlDbType.Integer,      4,  ParameterDirection.Input,  this.UnitID),
                                        DataBaseHelper.MakeParam("comID",   NpgsqlTypes.NpgsqlDbType.Integer,      4,  ParameterDirection.Input,  this.ComID)
                                     };
            return _params;
        }
	}
}

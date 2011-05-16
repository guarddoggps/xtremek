
using System;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessMapData
{
	public class ProcessAlerts
	{
		public ProcessAlerts()
		{
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
			AlertData data = new AlertData();
			AlertSelect alertSelect = new AlertSelect();
			alertSelect.UnitID = _unitID;
			alertSelect.ComID = _comID;
			alertSelect.AlertTime = _alertTime;
			alertSelect.Alert = data;
			alertSelect.GetAlert();
			_alert = alertSelect.Alert;
		}
		
	}
}

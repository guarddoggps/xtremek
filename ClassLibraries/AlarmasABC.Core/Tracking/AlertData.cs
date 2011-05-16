using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Tracking
{
	public class AlertData
	{
		public AlertData()
		{
		}
		
		private string _alertTime;
		public string AlertTime
		{
			get { return _alertTime; }
			set { _alertTime = value; }
		}
			
		private string _alertType;
		public string AlertType
		{
			get { return _alertType; }
			set { _alertType = value; }
		}
		
		private string _alertMessage;
		public string AlertMessage
		{
			get { return _alertMessage; }
			set { _alertMessage = value; }
		}
	}
}

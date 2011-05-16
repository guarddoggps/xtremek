using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Tracking
{
   public class RulesData
    {
       public IList<RulesData> _dropDownList =new List<RulesData>();

       public RulesData()
       {
       }

        public RulesData(int value, string name)
        {
            _rulesID = value; _rulesName = name;
        }
        
        public int Value
        {
            get { return _rulesID; }
            set { _rulesID = value; }
        }

        
        public string Name
        {
            get { return _rulesName; }
            set { _rulesName = value; }
        }
        private int _rulesID;

        public int RulesID
        {
            get { return _rulesID; }
            set { _rulesID = value; }
        }

        private string _rulesName;

        public string RulesName
        {
            get { return _rulesName; }
            set { _rulesName = value; }
        }

        private int _unitID;

        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private string _rulesValue;

        public string RulesValue
        {
            get { return _rulesValue; }
            set { _rulesValue = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
		
		private string _speedPhoneNum;
		
		public string SpeedingPhoneNum
		{
			get { return _speedPhoneNum; }
			set { _speedPhoneNum = value; }
		}
		
		private bool _isSms;
		
		public bool IsSMS
		{
			get { return _isSms; }
			set { _isSms = value; }
		}

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private string _rulesOperator;

        public string RulesOperator
        {
            get { return _rulesOperator; }
            set { _rulesOperator = value; }
        }

        private string _rulesOperatorName;

        public string RulesOperatorName
        {
            get { return _rulesOperatorName; }
            set { _rulesOperatorName = value; }
        }

        private int _uID;

        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }

        private int _geoID;

        public int GeoID
        {
            get { return _geoID; }
            set { _geoID = value; }
        }
    }
}

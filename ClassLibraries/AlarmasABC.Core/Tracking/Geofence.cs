using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AlarmasABC.Core.Tracking
{
    public class Geofence
    {
         public IList<Geofence> _dropDownList =new List<Geofence>();

         public Geofence()
         {
         }

        public Geofence(int value, string name)
        {
            _id = value; _name = name;
        }
        
        public int Value
        {
            get { return _id; }
            set { _id = value; }
        }

       
        
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private int _userID;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _userRole;
        public int UserRole
        {
            get { return _userRole; }
            set { _userRole = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private decimal _centerLat;
        public decimal CenterLat
        {
            get { return _centerLat; }
            set { _centerLat = value; }
        }

        private decimal _centerLong;
        public decimal CenterLong
        {
            get { return _centerLong; }
            set { _centerLong = value; }
        }

        private decimal _radius;
        public decimal Radius
        {
            get { return _radius; }
            set { _radius = value; }
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

        private int _unitID;

        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        private int _unitGroupID;

        public int UnitGroupID
        {
            get { return _unitGroupID; }
            set { _unitGroupID = value; }
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class Units
    {

        public IList<Units> _dropDownList = new List<Units>();
        public IList<Units> _ListItems=new List<Units>();
        public Units()
        {
                
        }
        public Units(int Value,string Name)
        {
            _unitID = Value; _unitName = Name;
        }

        public int Value
        {
            get { return _unitID; }
            set { _unitID = value; }
        }
        public string Name
        {
            get { return _unitName; }
            set { _unitName = value; }
        }
        #region instance variable and Properties

        private int _GroupID;

        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
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

        private string _unitName;

        public string UnitName
        {
            get { return _unitName; }
            set { _unitName = value; }
        }

        private int _typeID;

        public int TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }

        private int _deviceID;

        public int DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        private string _LicenseID;

        public string LicenseID
        {
            get { return _LicenseID; }
            set { _LicenseID = value; }
        }

        private string _DriverName;

        public string DriverName
        {
            get { return _DriverName; }
            set { _DriverName = value; }
        }

        private string _iconName;

        public string IconName
        {
            get { return _iconName; }
            set { _iconName = value; }
        }

        private string _VIN;

        public string VIN
        {
            get { return _VIN; }
            set { _VIN = value; }
        }

        private int _modelID;

        public int ModelID
        {
            get { return _modelID; }
            set { _modelID = value; }
        }

        private DateTime _Unitpurchasedate;

        public DateTime Unitpurchasedate
        {
            get { return _Unitpurchasedate; }
            set { _Unitpurchasedate = value; }
        }

        private string _unitColor;

        public string UnitColor
        {
            get { return _unitColor; }
            set { _unitColor = value; }
        }

        private string _keyCode;

        public string KeyCode
        {
            get { return _keyCode; }
            set { _keyCode = value; }
        }

        private int _unitfueltype;

        public int Unitfueltype
        {
            get { return _unitfueltype; }
            set { _unitfueltype = value; }
        }

        private string _DevicePurchaseLocation;

        public string DevicePurchaseLocation
        {
            get { return _DevicePurchaseLocation; }
            set { _DevicePurchaseLocation = value; }
        }

        private DateTime _DevicePurchaseDate;

        public DateTime DevicePurchaseDate
        {
            get { return _DevicePurchaseDate; }
            set { _DevicePurchaseDate = value; }
        }
       

        private int _unitcost;

        public int Unitcost
        {
            get { return _unitcost; }
            set { _unitcost = value; }
        }

        private string _levelArmor;

        public string LevelArmor
        {
            get { return _levelArmor; }
            set { _levelArmor = value; }
        }

        private string _wtint;

        public string Wtint
        {
            get { return _wtint; }
            set { _wtint = value; }
        }

        private string _package;

        public string Package
        {
            get { return _package; }
            set { _package = value; }
        }

        private string _counterIED;

        public string CounterIED
        {
            get { return _counterIED; }
            set { _counterIED = value; }
        }

        private int _patternID;

        public int PatternID
        {
            get { return _patternID; }
            set { _patternID = value; }
        }

        private string _pMaintainance;

        public string PMaintainance
        {
            get { return _pMaintainance; }
            set { _pMaintainance = value; }
        }

        private bool _isDelete;

        public bool IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        private bool _isActivePattern;

        public bool IsActivePattern
        {
            get { return _isActivePattern; }
            set { _isActivePattern = value; }
        }

        private string _otherInfo;

        public string OtherInfo
        {
            get { return _otherInfo; }
            set { _otherInfo = value; }
        }

        #endregion
    }
}

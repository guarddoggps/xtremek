using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class UnitType
    {
        public IList<UnitType> _dropDownList = new List<UnitType>();

        public UnitType()
        {
            /// Default Constructor
        }

        public UnitType(int typeID, string typeName)
        {
            this._typeID = typeID;
            this._typeName = typeName;
        }

        #region Instance variable and properties

        private int _typeID;

        public int TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }

        private string _typeName;

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private bool _isDelete;

        public bool IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        private string iconName;

        public string IconName
        {
            get { return iconName; }
            set { iconName = value; }
        }

        #endregion

        #region for dropdown list

            public int Value
            {
                get { return _typeID; }
                set { _typeID = value; }
            }

            public string Name
            {
                get { return _typeName; }
                set { _typeName = value; }
            }

        #endregion

    }
}
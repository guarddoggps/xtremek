using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class UserGroup
    {
        public IList<UserGroup> _dropDownList = new List<UserGroup>();

        public UserGroup()
        { 
            // Default Constructor
        }

        public UserGroup(int value, string name)
        {
            _groupID = value; _groupName = name;
        }

        #region Private variables and Properties
        
            private int _groupID;
            public int GroupID
            {
                get { return _groupID; }
                set { _groupID = value; }
            }

            private string _groupName;
            public string GroupName
            {
                get { return _groupName; }
                set { _groupName = value; }
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

            private int _UnitID;

            public int UnitID
            {
                get { return _UnitID; }
                set { _UnitID = value; }
            }

        #endregion

        #region  for dropdownlist

            public int Value
            {
                get { return _groupID; }
                set { _groupID = value; }
            }

            public string Name
            {
                get { return _groupName; }
                set { _groupName = value; }
            }

        #endregion
    }
}

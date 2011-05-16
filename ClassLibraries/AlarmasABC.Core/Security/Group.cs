using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Security
{
    public class Group
    {
        public IList<Group> _List = new List<Group>();

        public Group()
        { 
            // Default Constructor
        }

        public Group(int value, string name)
        {
            _grpID = value; _grpName = name;
        }

        #region Private variables and Properties
        
            private int _grpID;
            public int GrpID
            {
                get { return _grpID; }
                set { _grpID = value; }
            }

            private string _grpName;
            public string GrpName
            {
                get { return _grpName; }
                set { _grpName = value; }
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

        #region  for ListBox

            public int Value
            {
                get { return _grpID; }
                set { _grpID = value; }
            }

            public string Name
            {
                get { return _grpName; }
                set { _grpName = value; }
            }

        #endregion
    }
}

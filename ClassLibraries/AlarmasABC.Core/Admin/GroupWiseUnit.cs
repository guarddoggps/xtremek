using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Admin
{
    public class GroupWiseUnit
    {

        #region instance variable and Properties
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

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
        #endregion

    }
}


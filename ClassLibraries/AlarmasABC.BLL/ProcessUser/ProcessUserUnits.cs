using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.DAL.Delete;

namespace AlarmasABC.BLL.ProcessUser
{
    public class ProsessUserUnits
    {

        private int _userID;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public void DeleteUnits()
        {
            UnitUserWiseDelete _del = new UnitUserWiseDelete();
            _del.UserID = this.UserID;
            _del.invoke();
        }
    }
}

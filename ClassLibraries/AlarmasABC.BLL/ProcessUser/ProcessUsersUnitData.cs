using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.DAL.Insert;

namespace AlarmasABC.BLL.ProcessUser
{
    public class ProcessUsersUnitData:IAlopekBusinessLogic
    {
        public ProcessUsersUnitData()
        {
        }

        #region Private Variables and Properties

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private int _unitID;
        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        #endregion

        public void invoke()
        {
            UsersUnitDataInsert _usersUnit = new UsersUnitDataInsert();
            try
            {
                _usersUnit.ComID = this.ComID;
                _usersUnit.UnitID = this.UnitID;
                _usersUnit.addUnits();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUsersUnit::Invoke" + ex.Message);
            }
            finally
            {
                _usersUnit = null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;

namespace AlarmasABC.BLL.ProcessUser
{
    public class ProcessUserGroupUnits:IAlopekBusinessLogic
    {
        private InvokeOperations.operations _mode;

        public ProcessUserGroupUnits(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }

        #region Private Variables and properties

            private int _groupID;
            public int GroupID
            {
                get { return _groupID; }
                set { _groupID = value; }
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

        #endregion


        public void invoke()
        {
             switch (this._mode)
             {
                 case InvokeOperations.operations.INSERT:
                     addGroupWiseUnitData();
                     break;
                 case InvokeOperations.operations.SELECT:
                     break;
                 case InvokeOperations.operations.UPDATE:

                     break;
                 case InvokeOperations.operations.DELETE:
                     UnitGroupDelete();
                     break;
                 default:
                     break;
             }
         }

        private void addGroupWiseUnitData()
        {
            UserGroupWiseUnitDataInsert _UserGroupUnit = new UserGroupWiseUnitDataInsert();
            try
            {
                _UserGroupUnit.ComID = this.ComID;
                _UserGroupUnit.GroupID = this.GroupID;
                _UserGroupUnit.UserID = this.UserID;
                _UserGroupUnit.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::Invoke::" + ex.Message);
            }
            finally
            {
                _UserGroupUnit = null;
            }
        }

        private void UnitGroupDelete()
        {
            try
            {
                UnitUserWiseDelete _del = new UnitUserWiseDelete();
                _del.UserID = this.UserID;
                _del.ComID = this.ComID;
                _del.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::Delete::" + ex.Message);
            }
            finally
            { 
            
            }
        }
             

    }
}

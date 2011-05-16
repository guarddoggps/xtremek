using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Update;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.BLL.ProcessUnitUserWise
{
    public class ProcessUnitUserWise:IAlopekBusinessLogic
    {
        private InvokeOperations.operations _mode;
        public ProcessUnitUserWise()
        {
 
        }
        public ProcessUnitUserWise(InvokeOperations.operations _mode)
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

        private int _unitID;

        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }


        #endregion


        public void invoke()
        {
            switch (this._mode)
            {
                case InvokeOperations.operations.INSERT:
                    insertUnitUserWise();
                    break;
                case InvokeOperations.operations.SELECT:
                    break;
                case InvokeOperations.operations.UPDATE:
                    updateUnitUserWise();
                    break;
                case InvokeOperations.operations.DELETE:
                    deleteUnitUserWise();
                    break;
                default:
                    break;
            }
        }

        private void insertUnitUserWise()
        {
            UnitUserWiseInsert _unitIns = new UnitUserWiseInsert
            ();
            try
            {
                _unitIns.UnitID = this.UnitID;
                _unitIns.UserID = this.UserID;
                _unitIns.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUnitUserWiseInsert::Invoke" + ex.Message);
            }
            finally
            {
                _unitIns = null;
            }
        }

        private void updateUnitUserWise()
        {
            UnitUserWiseUpdate _unitUpdate = new UnitUserWiseUpdate
            ();
            try
            {
                _unitUpdate.UserID = this.UserID;
                _unitUpdate.GroupID = this.GroupID;
                _unitUpdate.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUserUnitWiseUpdate::Invoke" + ex.Message);
            }
            finally
            {
                _unitUpdate = null;
            }

        }

        private void deleteUnitUserWise()
        {
            UnitUserWiseDelete _userUnitWise = new UnitUserWiseDelete();
            try
            {
                _userUnitWise.UserID = this.UserID;
                _userUnitWise.invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUserUnitWiseDelete::Invoke" + ex.Message);
            }
            finally
            {
                _userUnitWise = null;
            }
 
        }
    }
}

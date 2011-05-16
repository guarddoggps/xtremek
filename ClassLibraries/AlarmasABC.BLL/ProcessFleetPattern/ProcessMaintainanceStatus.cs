using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Security;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;

using AlarmasABC.DAL.Update;
using AlarmasABC.DAL.Security.Select;
//using AlarmasABC.DAL.Security.Update;
using AlarmasABC.DAL.Security.Delete;

using AlarmasABC.DAL.Fleet.Select;
using AlarmasABC.DAL.Fleet.Insert;
using AlarmasABC.DAL.Fleet.Update;
using AlarmasABC.DAL.Fleet.Delete;
using AlarmasABC.Core.Fleet;


namespace AlarmasABC.BLL.ProcessFleetPattern
{
    public class ProcessMaintainanceStatus : IAlopekBusinessLogic
    {
        #region Variables 
        private int _patternID;

        public int PatternID
        {
            get { return _patternID; }
            set { _patternID = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private int _deviceID;

        public int DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        

        #endregion

        #region Methods
        public ProcessMaintainanceStatus()
        {
        }

        public void invoke()
        {
            //getSchemeInfo();
        }

        public DataSet getUnitCount()
        {
            DataSet _ds = new DataSet();
            try
            {
                UnitCountSelect _unitSelect = new UnitCountSelect();
                _unitSelect.ComID = this.ComID.ToString();
                _ds = _unitSelect.getUnitCount();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }

        public void SlelectStatus()
        {
            MaintainanceStatusSelect _StatusSelect = new MaintainanceStatusSelect();
            try
            {
                _StatusSelect.ComID = this._comID;
                _StatusSelect.PatternID = this._patternID;
                _StatusSelect.selectMaintainanceStatus();
                this._ds = _StatusSelect.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _StatusSelect = null;
            }
        }

        public void SelectSuppPatern()
        {
            SuppPaternSelect _suppPatern = new SuppPaternSelect();
            try
            {
                _suppPatern.PatternID = this._patternID;
                _suppPatern.selectSuppPatern();
                this._ds = _suppPatern.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _suppPatern = null;
            }
        }

        public void SelectGPRS()
        {
            GPRSSelect _GPRSsel = new GPRSSelect();
            try
            {
                _GPRSsel.DeviceID = this._deviceID;
                _GPRSsel.StartDate = this._startDate;
                _GPRSsel.selectGPRS();
                this._ds = _GPRSsel.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _GPRSsel = null;
            }
        }

        #endregion
        

    }
}

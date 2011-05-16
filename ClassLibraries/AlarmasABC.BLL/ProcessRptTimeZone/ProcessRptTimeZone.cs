using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.ProcessRptTimeZone
{
    public class ProcessRptTimeZone:IAlopekBusinessLogic
    {
        private InvokeOperations.operations _mode;
        public ProcessRptTimeZone(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }

        private DataSet _ds;

        /// <summary>
        ///  Set/Get Dataset
        /// </summary>
        
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private int _tzID;

        public int TzID
        {
            get { return _tzID; }
            set { _tzID = value; }
        }
        private float _tzValue;

        public float TzValue
        {
            get { return _tzValue; }
            set { _tzValue = value; }
        }
        private string _rptLocation;

        public string RptLocation
        {
            get { return _rptLocation; }
            set { _rptLocation = value; }
        }

        public void invoke()
        {
            switch (this._mode)
            { 
                case InvokeOperations.operations.INSERT:
                    InsertTimeZone();
                    break;
                case InvokeOperations.operations.SELECT:
                    SelectTimeZone();
                    break;
                case InvokeOperations.operations.UPDATE:
                    UpdateTimeZone();
                    break;
                case InvokeOperations.operations.DELETE:
                    DeleteTimeZone();
                    break;
                default:
                    break;
            }
        }

        private void InsertTimeZone()
        {

            RptTimeZoneInsert _insTime = new RptTimeZoneInsert();
            try
            {
                _insTime.RptLocation = this._rptLocation;
                _insTime.TzValue = this._tzValue;
                _insTime.insertRptTimeZone();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _insTime = null;
            }
        }

        private void SelectTimeZone()
        {
            RptTimeZoneSelect _selectTime = new RptTimeZoneSelect();
            try
            {
                _selectTime.selectRptTimeZone();
                this._ds = _selectTime.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _selectTime = null;
            }
        }

        private void UpdateTimeZone()
        {
            RptTimeZoneUpdate _updateTime = new RptTimeZoneUpdate();
            try
            {
                _updateTime.RptLocation = this._rptLocation;
                _updateTime.TzValue = this._tzValue;
                _updateTime.TzID = this._tzID;
                _updateTime.updateRptTimeZone();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _updateTime = null;
            }
        }
        private void DeleteTimeZone()
        {
            RptTimeZoneDelete _deleteTime = new RptTimeZoneDelete();
            try
            {
                _deleteTime.TzID = this._tzID;
                _deleteTime.deleteRptTimeZone();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _deleteTime = null;
            }
        }


       
    }
}

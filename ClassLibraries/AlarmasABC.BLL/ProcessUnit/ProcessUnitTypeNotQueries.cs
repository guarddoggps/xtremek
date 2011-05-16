using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.DAL.Delete;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;

namespace AlarmasABC.BLL.ProcessUnit
{
    public class ProcessUnitTypeNotQueries:IAlopekBusinessLogic
    {
        public InvokeOperations.operations mode; 

        public ProcessUnitTypeNotQueries(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }
        public ProcessUnitTypeNotQueries()
        {
            
        }
        

        private UnitType _unitType;
        public UnitType UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
        }

        private int _unitID;
        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void invoke()
        {
            switch (this.mode)
            {
                case InvokeOperations.operations.INSERT:
                    AddUnitType();
                    break;
                case InvokeOperations.operations.UPDATE:
                    UpdateUnitType();
                    break;
                case InvokeOperations.operations.DELETE:
                    DeleteUnits();
                    break;
                default: break;
                   
            }
        }

        private void AddUnitType()
        {
            UnitTypeDataInsert data = new UnitTypeDataInsert();
            try
            {
                data.UnitType = this._unitType;
                data.AddUnitType();
            }
            catch (Exception ex)
            {
                throw new Exception("Add UintType BLL Error::" + ex.Message);
            }
            finally
            {
                data = null;
            }
        }

        private void DeleteUnits()
        {
            DeleteUnit _unit = new DeleteUnit();
            try
            {
                _unit.UnitID = this.UnitID;
                _unit.DelUnit();
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessUnitTypeNotQueries::DeleteUnits(): " + ex.Message);
            }
            finally
            {
                _unit = null;
            }
        }

        private void UpdateUnitType()
        { 
        }

        public void disableUnit()
        {
            DeleteUnit _del = new DeleteUnit("Disable");

            try
            {
                _del.UnitID = this.UnitID;
                _del.UpdateUnitStatus();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::DisablUnit::" + ex.Message);
            }
            finally
            {
                _del = null;
            }
        }

        public void enableUnit()
        {
            DeleteUnit _ena= new DeleteUnit("Enable");

            try
            {
                _ena.UnitID = this.UnitID;
                _ena.UpdateUnitStatus();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::EnableUnit::" + ex.Message);
            }
            finally
            {
                _ena = null;
            }
        }
    }
}

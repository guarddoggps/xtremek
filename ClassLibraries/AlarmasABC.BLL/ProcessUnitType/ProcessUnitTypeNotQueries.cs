
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.ProcessUnitType
{
    public class ProcessUnitTypeNotQueries : IAlopekBusinessLogic
    {
        public InvokeOperations.operations mode;

        public ProcessUnitTypeNotQueries(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }

        private UnitType _unitType;

        public UnitType UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
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
                    DeleteUnitType();
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
                throw new Exception("Add UnitType BLL Error::" + ex.Message);
            }
            finally
            {
                data = null;
            }
        }

        private void UpdateUnitType()
        {
            UnitTypeUpdate _uTypeUpdate = new UnitTypeUpdate();

            try
            {
                _uTypeUpdate.UnitType = this._unitType;
                _uTypeUpdate.updateUnitType();
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                _uTypeUpdate = null;
            }
        }

        private void DeleteUnitType()
        {
                UnitTypeDelete _typeDel = new UnitTypeDelete();
                try
                {
                    _typeDel.UnitType = this._unitType;
                    _typeDel.DeleteUnitType();
                }
                catch (Exception ex)
                {
                    throw new Exception(" :: " + ex.Message);
                }
                finally
                {
                    _typeDel = null;
                }
            }
        }




    
}

    


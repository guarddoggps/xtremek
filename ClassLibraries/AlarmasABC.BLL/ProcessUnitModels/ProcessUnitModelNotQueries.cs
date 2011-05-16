using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.ProcessUnitModels
{
   public class ProcessUnitModelNotQueries : IAlopekBusinessLogic
    {
        public InvokeOperations.operations mode;

        public ProcessUnitModelNotQueries(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }


        private UnitModel _unitModel;
        public UnitModel UnitModel
        {
            get { return _unitModel; }
            set { _unitModel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void invoke()
        {
            switch (this.mode)
            {
                case InvokeOperations.operations.INSERT:
                    AddUnitModel();
                    break;
                case InvokeOperations.operations.UPDATE:
                    UpdateUnitModel();
                    break;
                case InvokeOperations.operations.DELETE:
                    DeleteUnitModel();
                    break;
                default: break;

            }
        }

        private void AddUnitModel()
        {
            UnitModelDataInsert data = new UnitModelDataInsert();
            try
            {
                data.UnitModel = this._unitModel;
                data.AddUnitModel();
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

        private void UpdateUnitModel()
        {
            UnitModelUpdate _uModelUpdate = new UnitModelUpdate();
            
            try
            {
                _uModelUpdate.UnitModel = this._unitModel;
                _uModelUpdate.updateUnitModel();
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                _uModelUpdate = null;
            }
        }

        private void DeleteUnitModel()
        {
            UnitModelDelete _modelDel = new UnitModelDelete();
            try
            {
                _modelDel.UnitModel = this._unitModel;
                _modelDel.DeleteUnitModel();
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                _modelDel = null;
            }
        }
    }

        

    
}

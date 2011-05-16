using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;

namespace AlarmasABC.BLL.ProcessUnitModel
{
    class ProcessUnitModelNotQueries : IAlopekBusinessLogic
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
                    UpdateUnitType();
                    break;
                case InvokeOperations.operations.DELETE:
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

        private void UpdateUnitType()
        { 
        }
     
    }
}

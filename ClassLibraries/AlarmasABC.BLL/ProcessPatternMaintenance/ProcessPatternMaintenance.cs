using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Fleet.Update;

namespace AlarmasABC.BLL.ProcessPatternMaintenance
{
    public class ProcessPatternMaintenance:IAlopekBusinessLogic
    {
        public ProcessPatternMaintenance(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }

        private InvokeOperations.operations _mode;

        private int _unitID;

        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        public void invoke()
        {
            switch (this._mode)
            { 
                case InvokeOperations.operations.SELECT:
                
                    SelectMaintainance();
                    break;
                
                case InvokeOperations.operations.UPDATE:
                    
                    UpdateMaintainance();
                    break;
                
                default:
                    break;

            }
        }

        private void SelectMaintainance()
        { 
        
        }

        private void UpdateMaintainance()
        {
            PatternMaintenaceUpdate _maintainanceUpdate=new PatternMaintenaceUpdate();

            try
            {
                _maintainanceUpdate.UnitID=this.UnitID;
                _maintainanceUpdate.UpdateMaintenace();
            }
            catch (Exception ex)
            { 
				throw new Exception("ProcessPatternMaintenance::UpdateMaintainance(): " + ex.Message);
            
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.ProcessRulesData
{
    public class ProcessSpeedingNotQueries:IAlopekBusinessLogic
    {
        public InvokeOperations.operations mode; 

        public ProcessSpeedingNotQueries(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }
        

        private RulesData _rulesInfo;

public RulesData RulesInfo
{
  get { return _rulesInfo; }
  set { _rulesInfo = value; }
}

        /// <summary>
        /// 
        /// </summary>
        public void invoke()
        {
            switch (this.mode)
            {
                case InvokeOperations.operations.INSERT:
                    AssignRules();
                    break;
                case InvokeOperations.operations.UPDATE:
                    UpdateRules();
                    break;
                case InvokeOperations.operations.DELETE:
                    break;
                default: break;
                   
            }
        }

        private void AssignRules()
        {
            SpeedingDataInsert _speedingInsert = new SpeedingDataInsert();
            try
            {
                _speedingInsert.RulesInfo = this._rulesInfo;
                _speedingInsert.AssignRules();
            }
            catch (Exception ex)
            {
                throw new Exception("Assign Rules BLL Error::" + ex.Message);
            }
            finally
            {
                _speedingInsert = null;
            }
        }

        private void UpdateRules()
        {
            SpeedingRulesUpdate _speedingUpdate = new SpeedingRulesUpdate();
            try
            {
                _speedingUpdate.RulesInfo = this._rulesInfo;
                _speedingUpdate.UpdateRules();
            }
            catch (Exception ex)
            {
                throw new Exception("Rules Update BLL Error::" + ex.Message);
            }
            finally
            {
                _speedingUpdate = null;
            }
        }
        
    
}
}

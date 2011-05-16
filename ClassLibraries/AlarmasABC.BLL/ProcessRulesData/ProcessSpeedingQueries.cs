using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.DAL.Select;
using System.Data;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.BLL.ProcessRulesData
{
   public class ProcessSpeedingQueries
    {
         private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }


        private RulesData _rulesInfo;

        public RulesData RulesInfo
        {
            get { return _rulesInfo; }
            set { _rulesInfo = value; }
        }

        
        public void invoke()
        {
            try
            {
                SpeedingRulesSelect _sRules = new SpeedingRulesSelect();

                _sRules.RulesInfo = this.RulesInfo;
                _sRules.selectSpeedingRules();
                this._ds = _sRules.Ds;
              
             }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {

            }
        }

        /// <summary>
        /// load dropdownlist 
        /// </summary>
        /// <param name="ddl">System.Web.UI.WebControls.DropDownList ddl</param>
       
        /// <summary>
        /// load dropdownlist 
        /// </summary>
        /// <param name="ddl">System.Web.UI.WebControls.DropDownList ddl</param>
       

    }

    
}

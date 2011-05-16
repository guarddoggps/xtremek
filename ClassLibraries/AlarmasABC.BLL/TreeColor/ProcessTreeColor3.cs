using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.TreeColor
{
    public class ProcessTreeColor3 : IAlopekBusinessLogic
    {
        private RulesData _rules;

        public RulesData Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public ProcessTreeColor3()
        { 
        }

        public void invoke()
        {
            try
            {
                TreeColor3 _TreeColor3 = new TreeColor3(this.Rules);
                _TreeColor3.getTreeColor3();
                this._ds = _TreeColor3.Ds;

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
        }

    }
}

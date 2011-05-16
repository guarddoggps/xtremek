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
    public class ProcessTreeColor1 : IAlopekBusinessLogic
    {
        private Units _units;

        public Units Units
        {
            get { return _units; }
            set { _units = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public ProcessTreeColor1()
        { 
        }

        public void invoke()
        {
            try
            {
                TreeColor1 _TreeColor1 = new TreeColor1(this.Units);
                _TreeColor1.getTreeColor1();
                this._ds = _TreeColor1.Ds;

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
        }

    }
}

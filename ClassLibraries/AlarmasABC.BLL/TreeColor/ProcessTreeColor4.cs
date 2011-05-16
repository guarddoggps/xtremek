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
    public class ProcessTreeColor4 : IAlopekBusinessLogic
    {
       

        private Geofence _geofence;

        public Geofence Geofence
        {
            get { return _geofence; }
            set { _geofence = value; }
        }
            

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public ProcessTreeColor4()
        { 
        }

        public void invoke()
        {
            try
            {
                TreeColor4 _TreeColor4 = new TreeColor4(this.Geofence);
                _TreeColor4.getTreeColor4();
                this._ds = _TreeColor4.Ds;

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
        }

    }
}

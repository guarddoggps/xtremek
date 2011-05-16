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

namespace AlarmasABC.BLL.ErrorReports
{
    public class ProcessErrorReport : IAlopekBusinessLogic
    {
       

        private ErrorReport _errorReport;

        public ErrorReport ErrorReport
        {
            get { return _errorReport; }
            set { _errorReport = value; }
        }

        

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public ProcessErrorReport()
        { 
        }

        public void invoke()
        {
            try
            {
                ErrorReportSelect _errorteport = new ErrorReportSelect(this.ErrorReport);
                _errorteport.getErrorReport();
                this._ds = _errorteport.Ds;

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
        }

    }
}

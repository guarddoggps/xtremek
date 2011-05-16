using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessPermission
{
    public class ProcessSchemePermission
    {
        public ProcessSchemePermission()
        {
        }

        private User _uObj;

        public User UObj
        {
            get { return _uObj; }
            set { _uObj = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void GetSchemePermission()
        {
            try
            {
                SelectSchemePermission ssP = new SelectSchemePermission();

                ssP.UserObj = _uObj;

                ssP.GetSchemePermission();

                _ds = ssP.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessSchemePermission::GetSchemePermission(): " + ex.Message);
            }
            finally
            {

            }
        }

        public void GetModule()
        {
            try
            {
                SelectSchemePermission ssP = new SelectSchemePermission("module");
                

                ssP.GetModule();

                _ds = ssP.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessSchemePermission::GetModule(): " + ex.Message);
            }
            finally
            {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessUserGroup
{
    public class ProcessGroupCompanyWise:IAlopekBusinessLogic
    {
        public ProcessGroupCompanyWise()
        {

        }

        #region Private Variables and Properties

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        #endregion

        public void invoke()
        {
            UserGroupCompanyWiseSelect _UserGroup = new UserGroupCompanyWiseSelect();

            try
            {
                _UserGroup.ComID = this.ComID;
                _UserGroup.SelectGroup();
                this.Ds = _UserGroup.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::"+ex.Message.ToString());
            }
            finally
            {
                _UserGroup = null;
            }


        }
    }
}

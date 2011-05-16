using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessTreeData
{
    public class ProcessTreeData:IAlopekBusinessLogic
    {
        public ProcessTreeData()
        {

        }

        #region Private variables an properties

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _uID;
            public int UID
            {
                get { return _uID; }
                set { _uID = value; }
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
            TreeDataSelect _tree = new TreeDataSelect();
            try
            {
                _tree.ComID = this.ComID;
                _tree.UID = this.UID;
                _tree.SelectData();
                this.Ds = _tree.Ds;
            }

            catch (Exception ex)
            {
                throw new Exception(" BLL:: ProcessTreeData:: Invoke :: " + ex.Message);
            }

            finally
            {
                _tree = null;
            }
        }
    }
}

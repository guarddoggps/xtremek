using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;


namespace AlarmasABC.BLL.ProcessTreeData
{
    public class ProcessUnitCount:IAlopekBusinessLogic
    {
        public ProcessUnitCount()
        { 
        }

        #region Private Variables and Properties

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

            private int _typeID;
            public int TypeID
            {
                get { return _typeID; }
                set { _typeID = value; }
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
            UnitCountSelect _unitCount = new UnitCountSelect();
            try
            {
                _unitCount.ComID = this.ComID;
                _unitCount.UID = this.UID;
                _unitCount.TypeID = this.TypeID;
                _unitCount.GetUnitCount();
                this.Ds = _unitCount.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessUnitCount::Invoke:: " + ex.Message);
            }
            finally
            {
                _unitCount = null;
            }
        }
    }
}

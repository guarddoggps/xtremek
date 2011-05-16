using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Security;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;

using AlarmasABC.DAL.Update;
using AlarmasABC.DAL.Security.Select;
//using AlarmasABC.DAL.Security.Update;
using AlarmasABC.DAL.Security.Delete;

using AlarmasABC.DAL.Fleet.Select;
using AlarmasABC.DAL.Fleet.Insert;
using AlarmasABC.DAL.Fleet.Update;
using AlarmasABC.DAL.Fleet.Delete;
using AlarmasABC.Core.Fleet;


namespace AlarmasABC.BLL.ProcessSupplies
{
    public class ProcessSupplies : IAlopekBusinessLogic
    {
        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private Supplies _supplies;

        public Supplies Supplies1
        {
            get { return _supplies; }
            set { _supplies = value; }
        }

        #region Methords
        public ProcessSupplies()
        {
        }

        public void invoke()
        {
            //getSchemeInfo();
        }

     

        public DataSet getSupplies()
        {
            DataSet _ds = new DataSet();
            try
            {
                SuppliesSelect _suppliesSelect = new SuppliesSelect(this.ComID);
                _ds = _suppliesSelect.getSupplies();
                
            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }

        public int addSupplies()
        {
            int _i;
            try
            {
                SuppliesInsert _insertSupplies = new SuppliesInsert(this.Supplies1);
                _insertSupplies.Supplies = this.Supplies1;
                _i = _insertSupplies.insertSupplies();
            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }

        public void updateSupplies()
        {
            try
            {
                SuppliesUpdate _insertSupplies = new SuppliesUpdate(this.Supplies1);
                _insertSupplies.Supplies = this.Supplies1;
                _insertSupplies.UpdateSupplies();
            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
           
        }
        public void deleteSupplies()
        {
            try
            {
                SuppliesDelete _deleteSupplies = new SuppliesDelete(this.Supplies1);
                _deleteSupplies.Supplies = this.Supplies1;
                _deleteSupplies.deleteSupplies();
            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }

        } 

        #endregion
        

    }
}

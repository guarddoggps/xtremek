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


namespace AlarmasABC.BLL.ProcessFleetPattern
{
    public class ProcessCreatePattern : IAlopekBusinessLogic
    {
        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        private SuppliesPerPattern _suppliesPerPattern;

        public SuppliesPerPattern SuppliesPerPattern
        {
            get { return _suppliesPerPattern; }
            set { _suppliesPerPattern = value; }
        }
        private Pattern _pattern;

        public Pattern Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        private Units _unit;

        public Units Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        #region Methords
        public ProcessCreatePattern()
        {
        }

        public void invoke()
        {
            //getSchemeInfo();
        }

        public DataSet getUnit()
        {
            DataSet _ds = new DataSet();
            try
            {
                UnitSelect _unitSelect = new UnitSelect(this.ComID);
                _ds = _unitSelect.getUnit();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }

        public DataSet addPattern()
        {
            DataSet _ds = new DataSet();
            try
            {
                PatternInsert _insertPattern = new PatternInsert(this.Pattern);
                _ds = _insertPattern.insertPattern();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }


        public int addSuppliesPerPattern()
        {
            int _i;
            try
            {
                SuppliesPerPatternInsert _insertSuppliesPerPattern = new SuppliesPerPatternInsert(this.SuppliesPerPattern);
                _i = _insertSuppliesPerPattern.insertSuppliesPerPattern();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }
        public int updateUnit()
        {
            int _i;
            try
            {
                UnitUpdate _unitUpdate = new UnitUpdate(this.Unit);
                _i = _unitUpdate.updateUnit();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }

        #endregion
        

    }
}

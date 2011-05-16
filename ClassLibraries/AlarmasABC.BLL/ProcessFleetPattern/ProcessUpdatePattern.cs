using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Fleet.Select;
using AlarmasABC.DAL.Fleet.Insert;
using AlarmasABC.DAL.Fleet.Delete;
using AlarmasABC.DAL.Fleet.Update;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Fleet;


namespace AlarmasABC.BLL.ProcessFleetPattern
{
    public class ProcessUpdatePattern:IAlopekBusinessLogic
    {

        public InvokeOperations.operations mode;

        public ProcessUpdatePattern()
        {
            ///Default Constructor
        }

        public ProcessUpdatePattern(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private int _PatternID;

        public int PatternID
        {
            get { return _PatternID; }
            set { _PatternID = value; }
        }

        private string _Operation;

        public string Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

        private int _UnitId;

        public int UnitId
        {
            get { return _UnitId; }
            set { _UnitId = value; }
        }

        private Pattern _pattern;

        public Pattern Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        private SuppliesPerPattern _suppliesPerPattern;

        public SuppliesPerPattern SuppliesPerPattern
        {
            get { return _suppliesPerPattern; }
            set { _suppliesPerPattern = value; }
        }

        private Units _unit;

        public Units Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void invoke()
        {
            switch (this.mode)
            {
                case InvokeOperations.operations.SELECT:
                    selectUnitPerPattern();
                    break;
                case InvokeOperations.operations.INSERT:

                    break;
                case InvokeOperations.operations.UPDATE:

                    break;
                case InvokeOperations.operations.DELETE:

                    break;
                default: break;

            }
        }

        private void selectUnitPerPattern()
        {
            UnitPerPatternSelect _UnitPerPat = new UnitPerPatternSelect();
            _UnitPerPat.PatternID = this._PatternID;
            _UnitPerPat.selectUnitPerPattern();
            this._ds = _UnitPerPat.Ds;            
        }

        public void selectSuppliesPerPattern()
        {
            SupplyPerPatternSelect _SupllyPatSelect = new SupplyPerPatternSelect();
            _SupllyPatSelect.PatternID = this._PatternID;
            _SupllyPatSelect.selectSuppliesPerPattern();
            this._ds = _SupllyPatSelect.Ds;
        }

        public void LoadListedUnits(System.Web.UI.WebControls.ListBox LstBox)
        {
            ListedUnitsSelect _ListedUnits = new ListedUnitsSelect();
            Units _unit = new Units();
            try
            {
                _ListedUnits.PatternID = this._PatternID;
                _ListedUnits.ComID = this._comID;
                _ListedUnits.LoadListedUnitsForSupply(_unit._ListItems);

                LstBox.DataSource = _unit._ListItems;
                LstBox.DataTextField = "Name";
                LstBox.DataValueField = "Value";
                LstBox.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void LoadNotListedUnits(System.Web.UI.WebControls.ListBox LstBox)
        {
            NotListedUnitsSelect _notListedUnits = new NotListedUnitsSelect();
            Units _unit = new Units();
            try
            {
                _notListedUnits.PatternID = this._PatternID;
                _notListedUnits.ComID = this._comID;
                _notListedUnits.LoadNotListedUnitsForSupply(_unit._ListItems);

                LstBox.DataSource = _unit._ListItems;
                LstBox.DataTextField = "Name";
                LstBox.DataValueField = "Value";
                LstBox.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        public int updatePatterin()
        {
            int _i;
            try
            {
                PatternUpdate _updatePattern = new PatternUpdate(this.Pattern);
                _i = _updatePattern.UpdatePattern();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }

        public void deletePatterin()
        {

            try
            {
                SuppliesPerPatternDelete _deletePattern = new SuppliesPerPatternDelete(this.SuppliesPerPattern);
                _deletePattern.deleteSupplies();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }

        }
      

        public void deleteUnitPerpattern()
        {
            UnitsPerPatternDelete _unitDel = new UnitsPerPatternDelete();
            try
            {
                _unitDel.PatternID = this._PatternID;
                _unitDel.deleteUnitPerPattern();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void UpdateUnitPerPattern()
        {
            UnitsPerPatternUpdate _UnitUpdate = new UnitsPerPatternUpdate();
            try
            {
                _UnitUpdate.PatternID = this._PatternID;
                _UnitUpdate.UnitID = this._UnitId;
                _UnitUpdate.Operation = this.Operation;
                _UnitUpdate.updateUnitPerPattern();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}

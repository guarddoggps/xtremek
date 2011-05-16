using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Tracking;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.BLL.ProcessRulesData
{
    public class ProcessRulesData:IAlopekBusinessLogic
    {
        private InvokeOperations.operations _mode;
        public ProcessRulesData(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }
        public ProcessRulesData()
        {
            
        }

        #region Private variables and Properties
        
            private int _comID;

            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private RulesData _rulesObj;
            public RulesData RulesObj
            {
                get { return _rulesObj; }
                set { _rulesObj = value; }
            }

            private DataSet _ds;
            public DataSet Ds
            {
                get { return _ds; }
                set { _ds = value; }
            }

        #endregion

            private RulesData _rulesData;

            public RulesData RulesData
            {
                get { return _rulesData; }
                set { _rulesData = value; }
            }

        public void invoke()
        {
            switch (this._mode)
            {
                case InvokeOperations.operations.INSERT:
                    
                        AddRules();
                    
                    break;
                case InvokeOperations.operations.SELECT:
                    SelectRules();
                    break;
                default:
                    break;
            }
        }

        private void AddRules()
        {
            RulesDataInsert _rulesInsert = new RulesDataInsert();
            try
            {
                _rulesInsert.RulesObj = this.RulesObj;
                _rulesInsert.AddRulesData();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessRulesData::AddRules::" + ex.Message);
            }
            finally
            {
                _rulesInsert = null;
            }
        }
        private void SelectRules()
        {
            RulesDataSelect _rulesSelect = new RulesDataSelect();

            try
            {
                _rulesSelect.ComID = this.ComID;
                _rulesSelect.GetRulesData();
                this.Ds = _rulesSelect.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("ProcessSelect::BLL::RulesSelect"+ex.Message);
            }
            finally
            {
                _rulesSelect = null;
            }
        }
        public void GetUnits()
        {
            RulesDataSelect _unitSelect = new RulesDataSelect("Units");


            try
            {
                //_unitSelect.ComID = this.ComID;
                _unitSelect.RulesData = this._rulesData;
                _unitSelect.GetUnit();
                this.Ds = _unitSelect.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("ProcessSelect::BLL::UnitSelect" + ex.Message);
            }
            finally
            {
                _unitSelect = null;
            }
        }

        public void GetAssignedUnits()
        {
            RulesDataSelect _unitSelect = new RulesDataSelect("AssignedUnits");


            try
            {
                //_unitSelect.ComID = this.ComID;
                _unitSelect.RulesData = this._rulesData;
                _unitSelect.GetAssignedUnits();
                this.Ds = _unitSelect.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("ProcessSelect::BLL::UnitSelect" + ex.Message);
            }
            finally
            {
                _unitSelect = null;
            }
        }

        public void GetUnits3()
        {
            RulesDataSelect _unitSelect = new RulesDataSelect("User3");


            try
            {
                
                _unitSelect.RulesData = this._rulesData;
                _unitSelect.GetUnit3();
                this.Ds = _unitSelect.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("ProcessSelect::BLL::UnitSelect" + ex.Message);
            }
            finally
            {
                _unitSelect = null;
            }
        }
        public void UnitRuleInfo()
        {
            RulesDataSelect _unitSelect = new RulesDataSelect("unitInfo");


            try
            {
                
                _unitSelect.RulesData = this._rulesData;
                _unitSelect.UnitRuleInfo();
                this.Ds = _unitSelect.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("ProcessSelect::BLL::UnitSelect" + ex.Message);
            }
            finally
            {
                _unitSelect = null;
            }
        }
        

        public void AssignRules()
        {
            RulesDataInsert _rulesInsert = new RulesDataInsert("AssignRules");
            try
            {
                _rulesInsert.RulesObj = this._rulesData;
                _rulesInsert.AssignRules();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessRulesData::AssignRules::" + ex.Message);
            }
            finally
            {
                _rulesInsert = null;
            }
        }

        public void CancelRules()
        {
            RulesDataDelete _rulesDel = new RulesDataDelete ();
            try
            {
                _rulesDel .RulesObj = this._rulesData;
                _rulesDel.CancelRules();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessRulesData::AssignRules::" + ex.Message);
            }
            finally
            {
                _rulesDel = null;
            }
        }

        public void UpdateRules()
        {
            RulesDataUpdate _rulesUp = new RulesDataUpdate();
            try
            {
                _rulesUp.RulesObj = this._rulesData;
                _rulesUp.UpdateRules();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessRulesData::AssignRules::" + ex.Message);
            }
            finally
            {
                _rulesUp = null;
            }
        }


        /// <summary>
        /// load dropdownlist 
        /// </summary>
        /// <param name="ddl">System.Web.UI.WebControls.DropDownList ddl</param>
        public static void fillDropDownItems(System.Web.UI.WebControls.DropDownList ddl, string comID, string type)
        {
            try
            {


                RulesData _r = new RulesData();
                Geofence _g = new Geofence();
                Units _u = new Units();
                

                if (type == "rules")
                {
                    new RulesDataSelect().RulesDropDownList(_r._dropDownList, comID);

                    ddl.DataSource = _r._dropDownList;

                    ddl.DataTextField = "Name";
                    ddl.DataValueField = "Value";
                    ddl.DataBind();
                }
                else if ( type == "geo")
                {
                    new RulesDataSelect("geo").GeoDropDownList(_g._dropDownList, comID);
                    ddl.DataSource = _g._dropDownList;

                    ddl.DataTextField = "Name";
                    ddl.DataValueField = "Value";
                    ddl.DataBind();
                }

                else if (type == "units")
                {
                    new RulesDataSelect("units").UnitsDropDownList(_u._dropDownList, comID);
                    ddl.DataSource = _u._dropDownList;

                    ddl.DataTextField = "Name";
                    ddl.DataValueField = "Value";
                    ddl.DataBind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ddl = null;
            }
        }

       
    }
}

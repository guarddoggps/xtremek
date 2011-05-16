using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmasABC.DAL.Select;
using AlarmasABC.Core.Admin;



namespace AlarmasABC.BLL.ProcessUnitType
{
    public class ProcessUnitTypeQueries : IAlopekBusinessLogic
    {
        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void invoke()
        {
            try
            {
                UnitTypeSelect _unitType = new UnitTypeSelect();
                _unitType.selectUnitType();
                this._ds = _unitType.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessUnitTypeQueries::invoke(): " + ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// load dropdownlist 
        /// </summary>
        /// <param name="ddl">System.Web.UI.WebControls.DropDownList ddl</param>
        public static void fillDropDownItems(System.Web.UI.WebControls.DropDownList ddl,UnitType _unitype)
        {
            try
            {
                UnitType _c = new UnitType();
                _c = _unitype;
                new UnitTypeSelect(_c).UnitTypeDropDownList(_c._dropDownList);

                ddl.DataSource = _c._dropDownList;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Value";
                ddl.DataBind();

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

        /// <summary>
        /// load dropdownlist 
        /// </summary>
        /// <param name="ddl">System.Web.UI.WebControls.DropDownList ddl</param>
        public static void fillListBoxItems(System.Web.UI.WebControls.ListBox _lst, UnitType _unitype)
        {
            try
            {            

                UnitType _c = new UnitType();
                _c = _unitype;
                new UnitTypeSelect(_c).UnitTypeListBox(_c._dropDownList);

                _lst.DataSource = _c._dropDownList;
                _lst.DataTextField = "Name";
                _lst.DataValueField = "Value";
                _lst.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _lst = null;
                _unitype = null;
            }
        }

    }

}


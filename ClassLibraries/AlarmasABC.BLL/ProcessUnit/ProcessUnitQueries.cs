using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessUnit
{
    public class ProcessUnitQueries
    {

        

        public static void fillDropDownList(System.Web.UI.WebControls.DropDownList _ddl, UnitType _unitype)
        {
            try
            {


                UnitType _unit = new UnitType();
                _unit = _unitype;
                new UnitTypeSelect(_unit).UnitTypeDropDownList(_unit._dropDownList);

                _ddl.DataSource = _unit._dropDownList;
                _ddl.DataTextField = "Name";
                _ddl.DataValueField = "Value";
                _ddl.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _ddl = null;
            }
        }

        public static void fillListBox(System.Web.UI.WebControls.ListBox _lst, int _comID)
        {
            try
            {


                Units _unit=new Units();

                UnitInfoSelect _unitSelect = new UnitInfoSelect();
                _unitSelect.ComID = _comID;
                _unitSelect.UnitListBoxItems(_unit._ListItems);

                _lst.DataSource = _unit._ListItems;
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
            }
        }

    }
}

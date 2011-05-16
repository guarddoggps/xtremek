using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmasABC.DAL.Select;
using AlarmasABC.Core.Admin;


namespace AlarmasABC.BLL.ProcessUnitModels
{
    public class ProcessUnitModelQueries : IAlopekBusinessLogic
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
                    UnitModelSelect _unitModel = new UnitModelSelect();
                    _unitModel.selectUnitModel();
                    this._ds = _unitModel.Ds;
                }
                catch (Exception ex)
                {
					throw new Exception("ProcessUnitModelQueries::invoke(): " + ex.Message);
                }
                finally
                {

                }
            }

            /// <summary>
            /// load dropdownlist 
            /// </summary>
            /// <param name="ddl">System.Web.UI.WebControls.DropDownList ddl</param>
            public static void fillDropDownItems(System.Web.UI.WebControls.DropDownList ddl)
            {
                try
                {
                    //if (Branch.dropDownItems.Count < 1)
                    //{
                    //    new BranchSelectDataByUser().getBranchDropDownItemsByUser(-1);
                    //}

                    Company _c = new Company();
                    new CompanySelect().CompanyDropDownList(_c._dropDownList);

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

        }
    
}

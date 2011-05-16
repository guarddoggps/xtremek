using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmasABC.DAL.Select;
using AlarmasABC.Core.Admin;


namespace AlarmasABC.BLL.ProcessCompany
{
   public class ProcessCompanyQueries:IAlopekBusinessLogic
    {
		public ProcessCompanyQueries()
		{
			_comID = -1;
		}
		
		public ProcessCompanyQueries(int comID)
		{
			_comID = comID;
		}
			
       	private DataSet _ds;

        public DataSet Ds
        {
          get { return _ds; }
          set { _ds = value; }
        }
		
		private int _comID;
		
        public void invoke()
        {
            try
            {
				CompanySelect _company;
				if (_comID == -1) {
                	_company = new CompanySelect();
				} else {
                	_company = new CompanySelect(_comID);
				}
					
                _company.selectCompany();
                this._ds = _company.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessCompanyQueries::invoke(): " + ex.Message);
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

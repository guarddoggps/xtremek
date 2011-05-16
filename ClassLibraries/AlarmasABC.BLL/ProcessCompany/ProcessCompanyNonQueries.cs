using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Update;
using AlarmasABC.DAL.Delete;

namespace AlarmasABC.BLL.ProcessCompany
{
    public class ProcessCompanyNonQueries : IAlopekBusinessLogic
    {
        public InvokeOperations.operations _mode;

        public ProcessCompanyNonQueries(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }

        private Company _company;
        /// <summary>
        /// Set/Get Company object
        /// </summary>

        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public void invoke()
        {
            switch (this._mode)
            { 
                case InvokeOperations.operations.INSERT:
                    addCompanyInfo();
                    break;
                case InvokeOperations.operations.UPDATE:
                    updateCompanyInfo();
                    break;
                case InvokeOperations.operations.SELECT:
                    break;
                case InvokeOperations.operations.DELETE:
                    deleteCompanyInfo();
                    break;
                default:
                    break;
            }
        }

        private void addCompanyInfo()
        {
            CompanyDataInsert _cpInsert = new CompanyDataInsert();

            try
            {
                _cpInsert.Company = this._company;
                _cpInsert.addCompanyinfo();
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }

            finally
            {
                _cpInsert = null;
            }
        }

        private void updateCompanyInfo()
        {
            CompanyUpdate _companyUpdate = new CompanyUpdate();
            try
            {
                _companyUpdate.Company = this._company;
                _companyUpdate.updateCompanyInfo();
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally 
            {
                _companyUpdate = null;
            }
        }

        private void deleteCompanyInfo()
        {
            CompanyDelete _comDel = new CompanyDelete();
            try
            {
                _comDel.Company = this._company;
                _comDel.deleteCompany();
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                _comDel = null;
            }
        }
    }
}

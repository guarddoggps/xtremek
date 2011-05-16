using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Delete;
using AlarmasABC.Core;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.BLL.ProcessIcon
{
    public class ProcessIconNonQueries:IAlopekBusinessLogic
    {
        public InvokeOperations.operations mode;
        public ProcessIconNonQueries(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }

        private IconSetup _IconSetup;
        public IconSetup IconSetup
        {
            get { return _IconSetup; }
            set { _IconSetup = value; }
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
                case InvokeOperations.operations.INSERT:
                    AddIconSetup();
                    break;
                case InvokeOperations.operations.UPDATE:
                    UpdateIconSetup();
                    break;
                case InvokeOperations.operations.DELETE:
                    DeleteIconSetup();
                    break;
                case InvokeOperations.operations.SELECT:
                    SelectIconSetup();
                    break;
                default: break;
                    
            }
        }

        private void AddIconSetup()
        {
            IconSetupDataInsert data = new IconSetupDataInsert();
            try
            {
                data.IconSetup = this._IconSetup;
                data.AddIconSetup();
            }
            catch (Exception ex)
            {
                throw new Exception("Add IconSetup BLL ERROR :: " + ex.Message);
            }
            finally
            {
                data = null;
            }
        }
        private void SelectIconSetup()
        {
            try
            {
                IconSelect _IconSelect = new IconSelect();
                _IconSelect.selectIcon();
                this._ds = _IconSelect.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessIconNonQueries::SelectIconSetup(): " + ex.Message);
            }
            finally
            {

            }
        }
        private void UpdateIconSetup()
        {
 
        }
        private void DeleteIconSetup()
        {
           
            IconDelete _IconDelete = new IconDelete();
            try
            {
                _IconDelete.IconSetup = this._IconSetup;
                _IconDelete.DeleteIcon();
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                _IconDelete = null;
            }
        }

    }
}

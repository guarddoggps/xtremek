using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Update;
using AlarmasABC.DAL.Delete;

namespace AlarmasABC.BLL.ProcessContactInfo
{
    public class ProcessContact : IAlopekBusinessLogic
    {
        public InvokeOperations.operations _mode;

        public ProcessContact(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }

       private Contact _conInfo;

        public Contact ConInfo
        {
          get { return _conInfo; }
          set { _conInfo = value; }
        }

        public void invoke()
        {
            switch (this._mode)
            {
                case InvokeOperations.operations.INSERT:
                    addContactInfo();
                    break;
                case InvokeOperations.operations.UPDATE:
                    
                    break;
                case InvokeOperations.operations.SELECT:
                    break;
                case InvokeOperations.operations.DELETE:
                    
                    break;
                default:
                    break;
            }
        }

        private void addContactInfo()
        {
            ContactDataInsert _cpInsert = new ContactDataInsert();

            try
            {
                _cpInsert.Contact = this._conInfo;
                _cpInsert.addContactInfo();
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
    }
}

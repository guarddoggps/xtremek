using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.DAL.Insert;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.BLL.ProcessCompany
{
    public class ProcessViperAccount
    {
        private VAccount _vAccount;

        public VAccount VAccount
        {
            get { return _vAccount; }
            set { _vAccount = value; }
        }

        public void AddViperAccount()
        {
            try
            {

                ViperAccountInsert vaI = new ViperAccountInsert();
                vaI.VAccount = this._vAccount;
                vaI.AddViperAccount();
            }
            catch (Exception ex)
            {
				throw new Exception("ProcessViperAccount::GetActiveImageInfo(): " + ex.Message);
            }
            finally
            {
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.DAL.Delete;

namespace AlarmasABC.BLL.ProcessMapData
{
    public class ProcessGprsData:IAlopekBusinessLogic
    {

        #region Private Variables and Properties

        private string _startDate;
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private string _endDate;
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        #endregion

        public ProcessGprsData()
        {         
        }

        public void invoke()
        {
            DeleteGprsData _data = new DeleteGprsData();

            try
            {
                _data.StartDate = this.StartDate;
                _data.EndDate=this.EndDate;
                _data.DeleteData();
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::ProcessGprsData::" + ex.Message);
            }
            finally
            {
                _data = null;
            }
        }

    }
}

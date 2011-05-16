using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.BLL.ProcessMapData
{
    public class ProcessHistoricalMapData:IAlopekBusinessLogic
    {
        public ProcessHistoricalMapData()
        {
        }

        #region Private Variables and Properties

                private int _deviceID;
                public int DeviceID
                {
                    get { return _deviceID; }
                    set { _deviceID = value; }
                }

                private string _date;
                public string Date
                {
                    get { return _date; }
                    set { _date = value; }
                }

                private int _comID;
                public int ComID
                {
                    get { return _comID; }
                    set { _comID = value; }
                }

				private DataSet _ds;
				public DataSet Ds
				{
					get { return _ds; }
					set { _ds = value; }
				}

        #endregion


        public void invoke()
        {
            HistoricalMapDataSelect _MapData = new HistoricalMapDataSelect();

            try
            {
                _MapData.Date = this.Date;
                _MapData.DeviceID = this.DeviceID;
                _MapData.ComID = this.ComID;
                _MapData.GetHistoricalData();
                this.Ds = _MapData.Ds;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL::Invoke:: " + ex.Message);
            }
            finally
            {
                _MapData = null;
            }
        }
    }
}

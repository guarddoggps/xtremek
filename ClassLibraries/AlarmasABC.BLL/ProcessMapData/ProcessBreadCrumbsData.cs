using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessMapData
{
    public class ProcessBreadCrumbsData
    {

        #region Private Variables and Properties

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _deviceID;
            public int DeviceID
            {
                get { return _deviceID; }
                set { _deviceID = value; }
            }

            private long _recTime;
            public long RecTime
            {
                get { return _recTime; }
                set { _recTime = value; }
            }

            private IList<MapData> _mapData;
            public IList<MapData> MapData
            {
                get { return _mapData; }
                set { _mapData = value; }
            }

        #endregion

            public void GetData1()
            {
                BreadCrumbsDataSelect _breadCrumbsData = new BreadCrumbsDataSelect();

                try
                {
                    MapData _mapData = new MapData();
                    _breadCrumbsData.ComID = this.ComID;
                    _breadCrumbsData.DeviceID = this.DeviceID;
                    _breadCrumbsData.RecTime = this.RecTime;
                    _breadCrumbsData.Mapdata = _mapData._mapData;
                    _breadCrumbsData.SelectData1();

                    this.MapData = _breadCrumbsData.Mapdata;
                }
                catch (Exception ex)
                {
					throw new Exception("ProcessBreadCrumbsData::GetData1(): " + ex.Message);
                }
                finally
                {
                    _breadCrumbsData = null;
                }
            }

            public void GetData2()
            {
                BreadCrumbsDataSelect _breadCrumbsData = new BreadCrumbsDataSelect();

                try
                {
                    MapData _mapData = new MapData();
                    _breadCrumbsData.ComID = this.ComID;
                    _breadCrumbsData.DeviceID = this.DeviceID;
                    _breadCrumbsData.Mapdata = _mapData._mapData;
                    _breadCrumbsData.SelectData2();

                    this.MapData = _breadCrumbsData.Mapdata;
                }
                catch (Exception ex)
                {
					throw new Exception("ProcessBreadCrumbsData::GetData2(): " + ex.Message);
                }
                finally
                {
                    _breadCrumbsData = null;
                }
            }
    }
}

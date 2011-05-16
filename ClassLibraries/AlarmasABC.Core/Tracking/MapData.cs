using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmasABC.Core.Tracking
{
    public class MapData
    {

        public IList<MapData> _mapData = new List<MapData>();

        public MapData()
        { 
            ///default Constructor
        }

        public MapData(string unitName, decimal latitude, decimal longitude, string deviceID, string velocity,
		               string recTime, string recTimeRevised, string iconName)
        {
            this._unitName = unitName;
            this._latitude = latitude;
            this._longitude = longitude;
            this._deviceID = deviceID;
            this._velocity = velocity;
            this._recTime = recTime;
            this._recTimeRevised = recTimeRevised;
            this._iconName = iconName;
        }

        #region private variable and properties

               
                private string _unitName;
                public string UnitName
                {
                    get { return _unitName; }
                    set { _unitName = value; }
                }

                private decimal _latitude;
                public decimal Latitude
                {
                    get { return _latitude; }
                    set { _latitude = value; }
                }

                private string _deviceID;
                public string DeviceID
                {
                  get { return _deviceID; }
                  set { _deviceID = value; }
                }

                private decimal _longitude;
                public decimal Longitude
                {
                    get { return _longitude; }
                    set { _longitude = value; }
                }

                private string _velocity;
                public string Velocity
                {
                    get { return _velocity; }
                    set { _velocity = value; }
                }

                private string _recTime;
                public string RecTime
                {
                    get { return _recTime; }
                    set { _recTime = value; }
                }

                private string _recTimeRevised;
                public string RecTimeRevised
                {
                    get { return _recTimeRevised; }
                    set { _recTimeRevised = value; }
                }

                private string _iconName;
                public string IconName
                {
                    get { return _iconName; }
                    set { _iconName = value; }
                }

        #endregion
    }
}

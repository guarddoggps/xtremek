using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;using Npgsql;

using AlarmasABC.DAL;

/// <summary>
/// Return True if the current position is outside the assigned Geofence else return false
/// </summary>
/// 
namespace AlarmasABC.Utilities
{
    public class chkGeofence
    {
        public chkGeofence()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool isViolate(string unitID, string comID, double lat, double lng, string geofenceID)
        {
            double _distance;
            double _radius = 0;
            double _Lat1 = 0, _Lng1 = 0, _Lat2 = 0, _Lng2 = 0;
            string _strSQL = "";
            _strSQL = "select centerLat,centerLng,radius from tblGeofence where comID=" + comID + " and ID=" + geofenceID + "";

            try
            {
                DataBaseHelper _db = new DataBaseHelper();
                DataSet _ds = new DataSet();
                _ds = _db.Run(_strSQL);

                if (_ds.Tables[0].Rows.Count > 0)
                {
                    _Lat1 = Convert.ToDouble(_ds.Tables[0].Rows[0]["centerLat"].ToString());
                    _Lng1 = double.Parse(_ds.Tables[0].Rows[0]["centerLng"].ToString());
                    _radius = double.Parse(_ds.Tables[0].Rows[0]["radius"].ToString());
                    _Lat2 = lat;
                    _Lng2 = lng;

                    _distance = DistanceCalculator.CalcDistance(_Lat1, _Lng1, _Lat2, _Lng2);
                    if (_distance > _radius)
                    {
                        return true;
                    }
                }
				return true;


            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }

}
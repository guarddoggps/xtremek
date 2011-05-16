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
using System.Xml.Linq;

/// <summary>
/// Summary description for DistanceCalculator
/// </summary> 
/// 

namespace AlarmasABC.Utilities
{
    public class DistanceCalculator
    {
        public DistanceCalculator()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public const double EarthRadiusInMiles = 3956.0;// Earth's Radius in Miles
        public const double EarthRadiusInKilometers = 6367.0; //Earth's Radius in Kilometers

        public static double ToRadian(double val) { return val * (Math.PI / 180); }

        public static double DiffRadian(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }

        /// <summary>
        /// Calculate the distance between two geocodes. Defaults to using Miles.
        /// </summary>
        public static double CalcDistance(double lat1, double lng1, double lat2, double lng2)
        {
            return CalcDistance(lat1, lng1, lat2, lng2, GeoCodeCalcMeasurement.Miles);
        }

        /// <summary>
        /// Calculate the distance between two geocodes.
        /// </summary>
        public static double CalcDistance(double lat1, double lng1, double lat2, double lng2, GeoCodeCalcMeasurement m)
        {
            double radius = DistanceCalculator.EarthRadiusInKilometers;
			
            if (m == GeoCodeCalcMeasurement.Miles) 
			{ 
				radius = DistanceCalculator.EarthRadiusInMiles; 
			}
			
            return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * Math.Pow(Math.Sin((DiffRadian(lng1, lng2)) / 2.0), 2.0)))));
        }
    }
    public enum GeoCodeCalcMeasurement : int
    {
        Miles = 0,
        Kilometers = 1
    }

}
       

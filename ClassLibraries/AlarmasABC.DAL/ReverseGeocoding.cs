using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Misc
{
	public class ReverseGeocoding:DataAccessBase
	{
		public ReverseGeocoding(string lat, string lon)
		{
			this.latitude = lat;
			this.longitude = lon;
			Command = "findNearestAddress";
		}
		
		//private double radius;
		
		private string latitude;
		private string longitude;
		
		private string postalCode = "";
		public string PostalCode
		{
			get { return postalCode; }
			set { postalCode = value; }
		}
		
		private string city = "";
		public string City
		{
			get { return city; }
			set { city = value; }
		}
		
		private string county = "";
		public string County
		{
			get { return county; }
			set { county = value; }
		}
		
		private string state = "";
		public string State
		{
			get { return state; }
			set { state = value; }
		}
		
		private string country = "";
		public string Country
		{
			get { return country; }
			set { country = value; }
		}
		
		
		public void GetReverseGeocoding()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);
			DataSet ds = new DataSet();
            try
            {
				// Use the reverse geocoding database connection string. VERY IMPORTANT so
				// Reverse Geocoding will work!
                ds = _db.Run(base.RGConnectionString, ReturnParams());
				
				if (ds.Tables[0].Rows.Count > 0) {
					// Get the postal code
					postalCode = ds.Tables[0].Rows[0]["postalCode"].ToString();
					// Get the city
					city = ds.Tables[0].Rows[0]["placeName"].ToString();
					// Get the county
					county = ds.Tables[0].Rows[0]["adminName2"].ToString();
					// Get the state
					state = ds.Tables[0].Rows[0]["adminName1"].ToString();
					// Get the country
					country = ds.Tables[0].Rows[0]["countryCode"].ToString();
				}
            }
            catch (Exception ex)
            {
				throw new Exception(" AlarmasABC.DAL.ReverseGeocoding:: " + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }
		
       	private NpgsqlParameter[] ReturnParams()
        {
        	NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@latitude",     NpgsqlTypes.NpgsqlDbType.Double, 25,    ParameterDirection.Input,   this.latitude),
                                        DataBaseHelper.MakeParam("@longitude",   NpgsqlTypes.NpgsqlDbType.Double, 25,    ParameterDirection.Input,   this.longitude)
                                     };
            return _params;
        }
	}
}

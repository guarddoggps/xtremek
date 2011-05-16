using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Tracking;
using Npgsql;
using System.Data;

namespace AlarmasABC.DAL.Select
{
    public class GeofenceDataSelect : DataAccessBase
    {
        public GeofenceDataSelect()
        {
            Command = @"SELECT * FROM tblGeofence WHERE comID = :comID AND id = :id;" +
					  @"SELECT * FROM  tblUnitWiseRules WHERE unitID = :unitID;";
        }

        private Geofence _geoData;

        public Geofence GeoData
        {
            get { return _geoData; }
            set { _geoData = value; }
        }

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectGeofenceData()
        {



            makeGeoParam _mp = new makeGeoParam(this._geoData);

                try
                {
                    DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                   this._ds =  _db.Run(base.ConnectionString, _mp._params1);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _mp = null;
                }

            }
        }

        class makeGeoParam
        {

             private Geofence _geoData;

            public Geofence GeoData
            {
                get { return _geoData; }
                set { _geoData = value; }
            }
            
           private NpgsqlParameter[] _params;

            public NpgsqlParameter[] _params1
            {
                get { return _params; }
                set { _params = value; }
            }


            public makeGeoParam(Geofence _geoinfo)
            {
                this._geoData = _geoinfo;
                build();
            }

            private void build()
            {
                NpgsqlParameter[] _param = { 
                                    
                                        DataBaseHelper.MakeParam("comID",NpgsqlTypes.NpgsqlDbType.Integer, 4, ParameterDirection.Input,this._geoData.ComID ),
                                        DataBaseHelper.MakeParam("id", NpgsqlTypes.NpgsqlDbType.Integer, 4, ParameterDirection.Input,this._geoData.Id ),
                                        DataBaseHelper.MakeParam("unitID", NpgsqlTypes.NpgsqlDbType.Integer, 4, ParameterDirection.Input,this._geoData.UnitID )
                                                                              
                                    };
                this._params1 = _param;
            }
        
    
    }
}

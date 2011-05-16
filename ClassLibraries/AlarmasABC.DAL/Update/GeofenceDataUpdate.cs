using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Tracking;
using Npgsql;
using System.Data;

namespace AlarmasABC.DAL.Update
{
   
    public class GeofenceDataUpdate : DataAccessBase
    {
        public GeofenceDataUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_GEOFENCE_UNITWISE.ToString();
        }

        private Geofence _geoData;

        public Geofence GeoData
        {
            get { return _geoData; }
            set { _geoData = value; }
        }

        public void UpdateGeofence()
        {
            GeofenceUpdateParams prm = new GeofenceUpdateParams(this._geoData);
            try
            {
                DataBaseHelper db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                db.Run(base.ConnectionString, prm.Parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Data Access Error:: " + ex.Message);
            }
            finally
            {
                prm = null;
            }
        }
    }

    class GeofenceUpdateParams
    {
       private NpgsqlParameter[] _parameters;
        public NpgsqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
        Geofence _geoData;

        private RulesData _rulesInfo;
        public GeofenceUpdateParams(Geofence _geoinfo)
        {
            this._geoData = _geoinfo;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] parameters = { 
          
              

                    DataBaseHelper.MakeParam("@geofenceID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._geoData.Id),
                    DataBaseHelper.MakeParam("@comID",NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._geoData.ComID),
                    DataBaseHelper.MakeParam("@unitID",NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._geoData.UnitID),
                    DataBaseHelper.MakeParam("@name",NpgsqlTypes.NpgsqlDbType.Varchar,70,ParameterDirection.Input,this._geoData.Name),
                    DataBaseHelper.MakeParam("@email",NpgsqlTypes.NpgsqlDbType.Varchar,200,ParameterDirection.Input,this._geoData.Email),
                    DataBaseHelper.MakeParam("@phoneNumber",NpgsqlTypes.NpgsqlDbType.Varchar,200,ParameterDirection.Input,this._geoData.SpeedingPhoneNum),
                    DataBaseHelper.MakeParam("@isSMS",NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input,this._geoData.IsSMS),
                    DataBaseHelper.MakeParam("@centerLat",NpgsqlTypes.NpgsqlDbType.Numeric,25,ParameterDirection.Input,this._geoData.CenterLat),
                    DataBaseHelper.MakeParam("@centerLng",NpgsqlTypes.NpgsqlDbType.Numeric,25,ParameterDirection.Input,this._geoData.CenterLong),
                    DataBaseHelper.MakeParam("@radius",NpgsqlTypes.NpgsqlDbType.Numeric,10,ParameterDirection.Input,this._geoData.Radius),
                    DataBaseHelper.MakeParam("@isActive", NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input,this._geoData.IsActive)
                
                                        };
            this._parameters = parameters;
        }
    }
}

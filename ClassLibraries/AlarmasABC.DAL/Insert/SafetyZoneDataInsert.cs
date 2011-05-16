using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Tracking;
using Npgsql;
using System.Data;

namespace AlarmasABC.DAL.Insert
{
    public class SafetyZoneDataInsert: DataAccessBase
    {
        public SafetyZoneDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_GEOFENCE_UNITWISE.ToString();
        }

        private Geofence _geoInfo;

        public Geofence GeoInfo
        {
            get { return _geoInfo; }
            set { _geoInfo = value; }
        }
       

        public void SaveSafetyData()
        {
            SafetyInsertParams prm = new SafetyInsertParams(this._geoInfo);
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

    class SafetyInsertParams
    {
       private NpgsqlParameter[] _parameters;
        public NpgsqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private Geofence _geoInfo;
        public SafetyInsertParams(Geofence _geoInfos)
        {
            this._geoInfo = _geoInfos;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] parameters = { 
       
                DataBaseHelper.MakeParam("@comID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._geoInfo.ComID),
                DataBaseHelper.MakeParam("@unitID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._geoInfo.UnitID),
                DataBaseHelper.MakeParam("@name", NpgsqlTypes.NpgsqlDbType.Varchar,50,ParameterDirection.Input,this._geoInfo.Name),
                DataBaseHelper.MakeParam("@email", NpgsqlTypes.NpgsqlDbType.Varchar,200,ParameterDirection.Input,this._geoInfo.Email),
                DataBaseHelper.MakeParam("@phoneNumber", NpgsqlTypes.NpgsqlDbType.Varchar,200,ParameterDirection.Input,this._geoInfo.SpeedingPhoneNum),
                DataBaseHelper.MakeParam("@isSMS", NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input,this._geoInfo.IsSMS),
                DataBaseHelper.MakeParam("@centerLat", NpgsqlTypes.NpgsqlDbType.Numeric,25,ParameterDirection.Input,this._geoInfo.CenterLat),
                DataBaseHelper.MakeParam("@centerLng", NpgsqlTypes.NpgsqlDbType.Numeric,25,ParameterDirection.Input,this._geoInfo.CenterLong),
                DataBaseHelper.MakeParam("@radius", NpgsqlTypes.NpgsqlDbType.Numeric,10,ParameterDirection.Input,this._geoInfo.Radius),
                DataBaseHelper.MakeParam("@isActive", NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input,this._geoInfo.IsActive)
                                        };
            this._parameters = parameters;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Delete
{
    public class DeleteSafetyZone:DataAccessBase
    {
        public DeleteSafetyZone()
        {
           Command = StoredProcedure.Name.SP_DELETE_UNIT_GEOFENCE.ToString();
        }

        private Geofence _gf;
        public Geofence Gf
        {
            get { return _gf; }
            set { _gf = value; }
        }

        public void DeleteZone()
        {
            DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, getParams());
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Safety Zone::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] getParams()
        {
            NpgsqlParameter[] _params = { 
                                     
                                     DataBaseHelper.MakeParam("@unitID",       NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.Gf.UnitID),
                                     DataBaseHelper.MakeParam("@geofenceID",    NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.Gf.Id)
                                     //DataBaseHelper.MakeParam("@COMID",         NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.Gf.ComID)
                                     };

            return _params;
        }
    }
}

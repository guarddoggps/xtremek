using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class TreeColor4:DataAccessBase
    {

        public TreeColor4(Geofence _geofence)
        {
			this.Geofence = _geofence;
            Command = @"SELECT centerLat,centerLng,radius FROM tblGeofence" +
					  @" WHERE comID = :comID and id = :id;";
        }

        private Geofence _geofence;

        public Geofence Geofence
        {
            get { return _geofence; }
            set { _geofence = value; }
        }
            

       
        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void getTreeColor4()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString,returnParam());             
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._geofence.ComID),
                                        DataBaseHelper.MakeParam("id", NpgsqlTypes.NpgsqlDbType.Bigint,  4,  ParameterDirection.Input,   this._geofence.Id)
                                    };

            return _param;
        }
    }
}

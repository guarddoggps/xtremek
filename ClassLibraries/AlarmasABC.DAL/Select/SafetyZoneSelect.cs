using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Select
{
    public class SafetyZoneSelect:DataAccessBase
    {
        public SafetyZoneSelect()
        {
			Command = @"SELECT geofenceID,(SELECT name FROM tblGeofence WHERE id = geofenceID)" +
					  @" AS name,coalesce(isActive,'0') AS isActive FROM  tblUnitWiseGeofence" +
            		  @" WHERE comID = :comID AND unitID = :unitID;";
        }

        #region Private Variables and Properties

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _unitID;
            public int UnitID
            {
                get { return _unitID; }
                set { _unitID = value; }
            }

            private DataSet _ds;
            public DataSet Ds
            {
                get { return _ds; }
                set { _ds = value; }
            }

        #endregion

        public void GetZoneList()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::SafetyZoneList::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }
       private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _Params = { 
                                        DataBaseHelper.MakeParam("unitID",   NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UnitID),
                                        DataBaseHelper.MakeParam("comID",      NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                     };

            return _Params;
        }
    }
}

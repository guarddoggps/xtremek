using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class TreeColor2:DataAccessBase
    {

        public TreeColor2(Units _unit)
        {
			this.Units = _unit;
			
			Command = @"SELECT coalescerulesID, '0') AS rulesID FROM tblUnitWiseRules WHERE unitID = :unitID AND isActive='1';" +

					  @"SELECT coalesce(geofenceID, '0') AS geofenceID FROM tblUnitWiseRules" +
					  @" WHERE unitID = :unitID and isGeofenceActive = '1';";
					

        }

        private Units _units;

        public Units Units
        {
            get { return _units; }
            set { _units = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void getTreeColor2()
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
                                        DataBaseHelper.MakeParam("unitID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._units.UnitID)
                                    };

            return _param;
        }
    }
}

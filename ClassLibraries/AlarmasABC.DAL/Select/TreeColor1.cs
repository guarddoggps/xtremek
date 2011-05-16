using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class TreeColor1:DataAccessBase
    {

        public TreeColor1(Units _unit)
        {
			this.Units = _unit;
			
            Command = @"SELECT DISTINCT CAST(velocity*0.621 as int) AS velocity,recTimeRevised,recTime,lat,long FROM tblGPRS" +
					  @" WHERE recTime = (SELECT MAX(recTime) FROM tblGPRS" +
					  @" WHERE deviceID = (SELECTdeviceID FROM tblUnits" +
					  @" WHERE unitID = :unitID)) AND deviceID = (SELECT deviceID FROM tblUnits WHERE unitID = :unitID);";
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

        public void getTreeColor1()
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

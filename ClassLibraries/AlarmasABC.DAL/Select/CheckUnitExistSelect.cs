using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class CheckUnitExistSelect:DataAccessBase
    {

        public CheckUnitExistSelect(Units _Units)
        {
            this.Units = _Units;
			Command = @"SELECT deviceID FROM tblUnits WHERE comID = :comID AND" +
					  @" (deviceID = coalesce(:unitID,0) OR unitName = :unitName) AND" +
					  @" coalesce(isDelete,'0') != '1';";
        }

        public CheckUnitExistSelect(Units _Units, string up)
        {
            this.Units = _Units;
           Command = StoredProcedure.Name.SP_CHECK_INFO_FOR_UPDATE.ToString();
        }
        
        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private Units _units;

        public Units Units
        {
            get { return _units; }
            set { _units = value; }
        }

        public void selectUnitExist()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString, returnParams());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("unitID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._units.UnitID),
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._units.ComID),
                                        DataBaseHelper.MakeParam("unitName",  NpgsqlTypes.NpgsqlDbType.Varchar,4,ParameterDirection.Input,this._units.UnitName)
                                     };
            return _params;
        }

    }
}

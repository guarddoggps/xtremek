using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Insert;

namespace AlarmasABC.DAL.Insert
{
    public class UnitsInfoInsert:DataAccessBase
    {
        public UnitsInfoInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_UNIT.ToString();
        }

        private Units _Units;

        public Units Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        public void addUnitInfo()
        {
            makeInsertParamforUnitData _insParam = new makeInsertParamforUnitData(this._Units);
            DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
            try
            {
                _db.Run(base.ConnectionString, _insParam._params2);
            }
            catch (Exception ex)
            {
                throw new Exception(" UnitDataInsert:: addUnitInfo() " + ex.Message.ToString());
            }
            finally
            {
                _insParam = null;
                _db = null;
            }
        }

    }

    class makeInsertParamforUnitData
    {
        private Units _Unit;
        public makeInsertParamforUnitData(Units _Unit)
        {
            this._Unit = _Unit;
            build();
        }

       private NpgsqlParameter[] _params;

        public NpgsqlParameter[] _params2
        {
            get { return _params; }
            set { _params = value; }
        }

        private void build()
        {
            NpgsqlParameter[] _param = {
								   DataBaseHelper.MakeParam("@unitID", NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.UnitID),
								   DataBaseHelper.MakeParam("@unitName", NpgsqlTypes.NpgsqlDbType.Varchar,50,ParameterDirection.Input,this._Unit.UnitName),
								   DataBaseHelper.MakeParam("@licenseID", NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input,this._Unit.LicenseID),
								   DataBaseHelper.MakeParam("@unitCategory",NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.TypeID),
								   DataBaseHelper.MakeParam("@iconName",NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input,this._Unit.IconName),
								   DataBaseHelper.MakeParam("@comID",NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.ComID),
								   DataBaseHelper.MakeParam("@patternID",NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.PatternID),
								   DataBaseHelper.MakeParam("@otherInfo",NpgsqlTypes.NpgsqlDbType.Varchar,200,   ParameterDirection.Input,this._Unit.OtherInfo)
			};
            
            this._params2 = _param;
        }


    }
}

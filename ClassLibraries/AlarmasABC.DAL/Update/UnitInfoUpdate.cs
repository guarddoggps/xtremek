using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.DAL.Update
{
    public class UnitsInfoUpdate:DataAccessBase
    {
        public UnitsInfoUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_VEHICLE.ToString();
        }

        private Units _Units;

        public Units Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        public void UpdateUnitInfo()
        {
            makeUpdateParamforUnitData _insParam = new makeUpdateParamforUnitData(this._Units);
            DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
            try
            {
                _db.Run(base.ConnectionString, _insParam._params2);
            }
            catch (Exception ex)
            {
                throw new Exception(" UnitDataUpdate:: UpdateUnitInfo() " + ex.Message.ToString());
            }
            finally
            {
                _insParam = null;
                _db = null;
            }
        }

    }

    class makeUpdateParamforUnitData
    {
        private Units _Unit;
        public makeUpdateParamforUnitData(Units _Unit)
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
   DataBaseHelper.MakeParam("@unitName",NpgsqlTypes.NpgsqlDbType.Varchar,50,ParameterDirection.Input,this._Unit.UnitName),
   DataBaseHelper.MakeParam("@licenseID",NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input,this._Unit.LicenseID),
   /*DataBaseHelper.MakeParam("@driverName",NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input),
   DataBaseHelper.MakeParam("@vin",NpgsqlTypes.NpgsqlDbType.Varchar,30,   ParameterDirection.Input,),
   DataBaseHelper.MakeParam("@modelID", NpgsqlTypes.NpgsqlDbType.Bigint,6,   ParameterDirection.Input,null),
   DataBaseHelper.MakeParam("@unitpDate", NpgsqlTypes.NpgsqlDbType.Timestamp,30,   ParameterDirection.Input,null),
   DataBaseHelper.MakeParam("@levelArmor",NpgsqlTypes.NpgsqlDbType.Varchar,10,   ParameterDirection.Input,),
   DataBaseHelper.MakeParam("@wtint",NpgsqlTypes.NpgsqlDbType.Varchar,10,   ParameterDirection.Input,),
   DataBaseHelper.MakeParam("@commonPackage",NpgsqlTypes.NpgsqlDbType.Varchar,6,   ParameterDirection.Input,),
   DataBaseHelper.MakeParam("@unitColor",NpgsqlTypes.NpgsqlDbType.Varchar,20,   ParameterDirection.Input,),
   DataBaseHelper.MakeParam("@keyCode",NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input,),
   DataBaseHelper.MakeParam("@fuelType",NpgsqlTypes.NpgsqlDbType.Bigint,6,   ParameterDirection.Input,null),
   DataBaseHelper.MakeParam("@purchaseLocation",NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input,),
   DataBaseHelper.MakeParam("@purchaseDate",NpgsqlTypes.NpgsqlDbType.Timestamp,30,   ParameterDirection.Input,null),
   DataBaseHelper.MakeParam("@unitCost", NpgsqlTypes.NpgsqlDbType.Money,15,   ParameterDirection.Input,null),*/
   DataBaseHelper.MakeParam("@unitCategory",NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.TypeID),
   //DataBaseHelper.MakeParam("@id",NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input,),
   //DataBaseHelper.MakeParam("@msgSetup",NpgsqlTypes.NpgsqlDbType.Bigint,6,   ParameterDirection.Input,1),
   DataBaseHelper.MakeParam("@iconName",NpgsqlTypes.NpgsqlDbType.Varchar,50,   ParameterDirection.Input,this._Unit.IconName),
   DataBaseHelper.MakeParam("@otherInfo",NpgsqlTypes.NpgsqlDbType.Varchar,200,   ParameterDirection.Input,this._Unit.OtherInfo),
   DataBaseHelper.MakeParam("@patternID",NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.PatternID)
                                    };
            this._params2 = _param;
        }


    }
}

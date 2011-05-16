using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.DAL.Update;

namespace AlarmasABC.DAL.Update
{
   public class UnitUserInfoUpdate:DataAccessBase
    {

        public UnitUserInfoUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_UNIT_USER.ToString();
        }

        private Units _Units;

        public Units Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        public void updateUnitUserInfo()
        {
            makeParamforUnitUserData _insParam = new makeParamforUnitUserData(this._Units);
            DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
            try
            {
                _db.Run(base.ConnectionString, _insParam._params1);
            }
            catch (Exception ex)
            {
                throw new Exception(" UnitUserInfoUpdate:: updateUnitUserInfo() " + ex.Message.ToString());
            }
            finally
            {
                _insParam = null;
                _db = null;
            }
        }

    }

    class makeParamforUnitUserData
    {
        private Units _Unit;
        public makeParamforUnitUserData(Units _Unit)
        {
            this._Unit = _Unit;
            build();
        }

       private NpgsqlParameter[] _params;

        public NpgsqlParameter[] _params1
        {
            get { return _params; }
            set { _params = value; }
        }

      

        private void build()
        {
            NpgsqlParameter[] _param = {
   DataBaseHelper.MakeParam("@groupID", NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.GroupID),
   DataBaseHelper.MakeParam("@unitID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._Unit.UnitID),
   DataBaseHelper.MakeParam("@comID",NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,this._Unit.ComID)
                                     };
            this._params1 = _param;
        }


    }
}

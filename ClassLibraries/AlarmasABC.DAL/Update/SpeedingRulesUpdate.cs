using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Tracking;
using Npgsql;
using System.Data;

namespace AlarmasABC.DAL.Update
{
    public class SpeedingRulesUpdate : DataAccessBase
    {
        public SpeedingRulesUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_SPEEDING_DATA.ToString();
        }

        private RulesData _rulesInfo;

        public RulesData RulesInfo
        {
            get { return _rulesInfo; }
            set { _rulesInfo = value; }
        }

        public void UpdateRules()
        {
            SpeedingUpdateParams prm = new SpeedingUpdateParams(this._rulesInfo);
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

    class SpeedingUpdateParams
    {
       private NpgsqlParameter[] _parameters;
        public NpgsqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private RulesData _rulesInfo;
        public SpeedingUpdateParams(RulesData _rules)
        {
            this._rulesInfo = _rules;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] parameters = { 
          
                DataBaseHelper.MakeParam("@email",	NpgsqlTypes.NpgsqlDbType.Varchar, 100,ParameterDirection.Input,	this._rulesInfo.Email),                                        
                DataBaseHelper.MakeParam("@phoneNumber",		NpgsqlTypes.NpgsqlDbType.Varchar, 200,ParameterDirection.Input,this._rulesInfo.SpeedingPhoneNum	), 
                DataBaseHelper.MakeParam("@description",		NpgsqlTypes.NpgsqlDbType.Varchar, 200,ParameterDirection.Input, this._rulesInfo.Message),
                DataBaseHelper.MakeParam("@rulesValue",   NpgsqlTypes.NpgsqlDbType.Varchar, 20 ,ParameterDirection.Input, this._rulesInfo.RulesValue),
                DataBaseHelper.MakeParam("@status",	NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input,this._rulesInfo.IsActive),
                DataBaseHelper.MakeParam("@isSMS",	NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input,this._rulesInfo.IsSMS),
                DataBaseHelper.MakeParam("@rulesID",  NpgsqlTypes.NpgsqlDbType.Integer,     4,ParameterDirection.Input,	this._rulesInfo.RulesID),                                                        
                DataBaseHelper.MakeParam("@unitID",   NpgsqlTypes.NpgsqlDbType.Integer,     4,ParameterDirection.Input,	this._rulesInfo.UnitID)
                
                                        };
            this._parameters = parameters;
        }
    }
}

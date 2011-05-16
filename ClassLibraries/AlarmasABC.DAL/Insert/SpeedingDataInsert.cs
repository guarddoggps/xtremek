using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Tracking;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class SpeedingDataInsert : DataAccessBase
    {
        public SpeedingDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_SPEEDING_DATA.ToString();
        }

        private RulesData _rulesInfo;

        public RulesData RulesInfo
        {
            get { return _rulesInfo; }
            set { _rulesInfo = value; }
        }

        public void AssignRules()
        {
            SpeedingInsertParams prm = new SpeedingInsertParams(this._rulesInfo);
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

    class SpeedingInsertParams
    {
       private NpgsqlParameter[] _parameters;
        public NpgsqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private RulesData _rulesInfo;
        public SpeedingInsertParams(RulesData _rules)
        {
            this._rulesInfo = _rules;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] parameters = { 
          
                DataBaseHelper.MakeParam("@unitID",  NpgsqlTypes.NpgsqlDbType.Integer,     4,ParameterDirection.Input,	this._rulesInfo.UnitID),                                    
                DataBaseHelper.MakeParam("@email",	NpgsqlTypes.NpgsqlDbType.Varchar, 100,ParameterDirection.Input,	this._rulesInfo.Email.ToString()),                             
                DataBaseHelper.MakeParam("@phoneNumber",	NpgsqlTypes.NpgsqlDbType.Varchar, 10,ParameterDirection.Input,	this._rulesInfo.SpeedingPhoneNum),               
                DataBaseHelper.MakeParam("@description",		NpgsqlTypes.NpgsqlDbType.Varchar, 200,ParameterDirection.Input, this._rulesInfo.Message.ToString()),
                DataBaseHelper.MakeParam("@isActive",	NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input, true),
                DataBaseHelper.MakeParam("@rulesName", NpgsqlTypes.NpgsqlDbType.Varchar,50,ParameterDirection.Input, this._rulesInfo.RulesName ),
                DataBaseHelper.MakeParam("@rulesForID",   NpgsqlTypes.NpgsqlDbType.Integer,    4, ParameterDirection.Input, 1),
                DataBaseHelper.MakeParam("@isSMS",	NpgsqlTypes.NpgsqlDbType.Boolean,1,ParameterDirection.Input, this._rulesInfo.IsSMS),
                DataBaseHelper.MakeParam("@rulesOperator",   NpgsqlTypes.NpgsqlDbType.Varchar, 2 ,ParameterDirection.Input,"<" ),
                DataBaseHelper.MakeParam("@rulesOperatorName",   NpgsqlTypes.NpgsqlDbType.Varchar, 50  ,ParameterDirection.Input, "Less Than" ),
                DataBaseHelper.MakeParam("@rulesValue",   NpgsqlTypes.NpgsqlDbType.Varchar, 20 ,ParameterDirection.Input, this._rulesInfo.RulesValue.ToString()),
                DataBaseHelper.MakeParam("@comID",   NpgsqlTypes.NpgsqlDbType.Integer, 4  ,ParameterDirection.Input, this._rulesInfo.ComID)
                                        };
            this._parameters = parameters;
        }
    }
}

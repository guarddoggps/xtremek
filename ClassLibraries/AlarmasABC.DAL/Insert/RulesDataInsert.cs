using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Insert
{
    public class RulesDataInsert:DataAccessBase
    {
        public RulesDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_RULES.ToString();
        }
        public RulesDataInsert(string type)
        {
            Command = StoredProcedure.Name.SP_INSERT_RULES_UNITWISE.ToString();
        }

        private RulesData _rulesObj;

        public RulesData RulesObj
        {
            get { return _rulesObj; }
            set { _rulesObj = value; }
        }

        public void AddRulesData()
        {
            DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString,ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::RulesDataInsert::"+ex.Message.ToString());
            }

            finally
            {
                _db=null;
            }
        }

        public void AssignRules()
        {

            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, ReturnParamsAssignRules());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::RulesAssign::" + ex.Message.ToString());
            }

            finally
            {
                _db = null;
            }

        }

       private NpgsqlParameter[] ReturnParamsAssignRules()
        {
            NpgsqlParameter[] _params = {
                                        DataBaseHelper.MakeParam("@unitID", NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.RulesObj.UnitID),
                                        DataBaseHelper.MakeParam("@rulesID", NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.RulesObj.RulesID),
                                        DataBaseHelper.MakeParam("@geofenceID", NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.RulesObj.GeoID),
                                        DataBaseHelper.MakeParam("@email",NpgsqlTypes.NpgsqlDbType.Varchar,     100,     ParameterDirection.Input,   this.RulesObj.Email),
                                        DataBaseHelper.MakeParam("@subject", NpgsqlTypes.NpgsqlDbType.Varchar,     200,     ParameterDirection.Input,   this.RulesObj.Message),
                                        DataBaseHelper.MakeParam("@description",  NpgsqlTypes.NpgsqlDbType.Varchar,     200,     ParameterDirection.Input,   this.RulesObj.Message),
                                        DataBaseHelper.MakeParam("@isActive", NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.RulesObj.IsActive)
                                     };

            return _params;
        }
        private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@rulesForID",         NpgsqlTypes.NpgsqlDbType.Varchar,     20,     ParameterDirection.Input,   this.RulesObj.RulesID),
                                        DataBaseHelper.MakeParam("@rulesOperator",      NpgsqlTypes.NpgsqlDbType.Varchar,     20,     ParameterDirection.Input,   this.RulesObj.RulesOperator),
                                        DataBaseHelper.MakeParam("@rulesOperatorName",  NpgsqlTypes.NpgsqlDbType.Varchar,     30,     ParameterDirection.Input,   this.RulesObj.RulesOperatorName),
                                        DataBaseHelper.MakeParam("@rulesValue",         NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.RulesObj.RulesValue),
                                        DataBaseHelper.MakeParam("@comID",              NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.RulesObj.ComID)
                                     };

            return _params;

        }
    }
}

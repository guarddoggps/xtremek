using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Update
{
    public class RulesDataUpdate:DataAccessBase
    {
        public RulesDataUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_RULES_DATA.ToString();
        }

        RulesData rulesObj;

        public RulesData RulesObj
        {
            get { return rulesObj; }
            set { rulesObj = value; }
        }

        public void UpdateRules()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, returnUpdateParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::UpdateRules():: " + ex.Message);
            }
        }

      
       private NpgsqlParameter[] returnUpdateParam()
        {
            NpgsqlParameter[] _params = { 
                                         
        
        DataBaseHelper.MakeParam("@unitID",       NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.rulesObj.UnitID),
        DataBaseHelper.MakeParam("@rulesID",        NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.rulesObj.RulesID),
        DataBaseHelper.MakeParam("@geofenceID",    NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Input,   this.rulesObj.GeoID),
        DataBaseHelper.MakeParam("@email",     NpgsqlTypes.NpgsqlDbType.Varchar,    100,     ParameterDirection.Input,   this.rulesObj.Email),
        DataBaseHelper.MakeParam("@subject",  NpgsqlTypes.NpgsqlDbType.Varchar,     200,    ParameterDirection.Input,   this.rulesObj.Message),
        DataBaseHelper.MakeParam("@description",   NpgsqlTypes.NpgsqlDbType.Varchar,     200,    ParameterDirection.Input,   this.rulesObj.Message),
        DataBaseHelper.MakeParam("@isActive",  NpgsqlTypes.NpgsqlDbType.Boolean,          1,      ParameterDirection.Input,   this.rulesObj.IsActive)
                                     };

            return _params;
        }
    }
}

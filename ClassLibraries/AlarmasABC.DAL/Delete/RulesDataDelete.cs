using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Tracking;
using AlarmasABC.Core;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    public class RulesDataDelete : DataAccessBase
    {

        public RulesDataDelete()
        {
           Command = StoredProcedure.Name.SP_DELETE_RULES_DATA.ToString();
        }

        RulesData _rulesObj;

        public RulesData RulesObj
        {
            get { return _rulesObj; }
            set { _rulesObj = value; }
        }

        public void CancelRules()
        {
            makeRulesDeleteParam _mip = new makeRulesDeleteParam(this._rulesObj);
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _mip.Param);
            }
            catch (Exception ex)
            {
                throw new Exception(":: " + ex.Message);
            }

            finally
            {
                _mip = null;
            }
        }
    }

   class makeRulesDeleteParam
   {
       private RulesData _rulesData;

       public makeRulesDeleteParam(RulesData rules)
       {
           this._rulesData = rules;
           build();
       }
      private NpgsqlParameter[] _param;

       public NpgsqlParameter[] Param
       {
           get { return _param; }
           set { _param = value; }
       }
       public void build()
       {
           NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@unitID", NpgsqlTypes.NpgsqlDbType.Integer, 4, ParameterDirection.Input, _rulesData.UnitID)
                                     };

           this.Param = _params;
       }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class TreeColor3:DataAccessBase
    {

        public TreeColor3(RulesData _rules)
        {
			this.Rules = _rules;
			Command = @"SELECT frulesFor,r.rulesOperator,r.rulesValue FROM tblRules r INNER JOIN" +
					  @" tblRulesFor f ON f.rulesForID=r.rulesForID WHERE rulesID = :rulesID;";

        }

        private RulesData _rules;

        public RulesData Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }

       
        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void getTreeColor3()
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
                                        DataBaseHelper.MakeParam("rulesID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._rules.RulesID)
                                    };

            return _param;
        }
    }
}

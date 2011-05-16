﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Tracking;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class SpeedingRulesSelect : DataAccessBase
    {
            private DataSet _ds;

            public DataSet Ds
            {
                get { return _ds; }
                set { _ds = value; }
            }

            private RulesData _rulesInfo;

            public RulesData RulesInfo
            {
                get { return _rulesInfo; }
                set { _rulesInfo = value; }
            }

           
            public SpeedingRulesSelect()
			{
				Command = @"SELECT * FROM tblUnitWiseRules JOIN tblRules ON" + 
            		          @" (tblRules.rulesID = tblUnitWiseRules.rulesID)" +
						  @" WHERE tblRules.comID = :comID AND unitID = :unitID;";
            }

            public void selectSpeedingRules()
            {
                
                
                makeSpeedingParam _mp = new makeSpeedingParam(this._rulesInfo);

                try
                {
                    DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
					this._ds =  _db.Run(base.ConnectionString, _mp._params1);   
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _mp = null;
                }

            }
        }

        class makeSpeedingParam
       {

           private RulesData _rulesInfo;

           public RulesData RulesInfo
           {
               get { return _rulesInfo; }
               set { _rulesInfo = value; }
           }


          private NpgsqlParameter[] _params;

           public NpgsqlParameter[] _params1
           {
               get { return _params; }
               set { _params = value; }
           }


            public makeSpeedingParam(RulesData _rules)
            {
                this._rulesInfo = _rules;
                build();
            }

            private void build()
            {
                NpgsqlParameter[] _param = { 
                                    
                                        DataBaseHelper.MakeParam("comID",NpgsqlTypes.NpgsqlDbType.Integer,         4,   ParameterDirection.Input,_rulesInfo.ComID ),
                                         DataBaseHelper.MakeParam("unitID", NpgsqlTypes.NpgsqlDbType.Integer,         4,   ParameterDirection.Input,_rulesInfo.UnitID )
                                                                              
                                    };
                this._params1 = _param;
            }
        
    

}
    
}

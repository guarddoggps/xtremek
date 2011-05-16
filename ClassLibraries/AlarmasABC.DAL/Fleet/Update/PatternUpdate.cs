using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Fleet;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Fleet.Update
{
    public class PatternUpdate:DataAccessBase
    {

        public PatternUpdate(Pattern _Pattern)
        {
            this.Pattern = _Pattern;
           Command = StoredProcedure.Name.SP_UPDATE_PATTREN.ToString();
        }

        private Pattern _Pattern;

        public Pattern Pattern
        {
            get { return _Pattern; }
            set { _Pattern = value; }
        }
        
        public int UpdatePattern()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);                
                _ds= _db.Run(base.ConnectionString,returnParam());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
            return int.Parse(_ds.Tables[0].Rows[0][0].ToString());
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@comID", NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.Pattern.ComID ),
                                        DataBaseHelper.MakeParam("@patternID",  NpgsqlTypes.NpgsqlDbType.Integer,  4, ParameterDirection.Input,   this.Pattern.Id),
                                        DataBaseHelper.MakeParam("@patternName", NpgsqlTypes.NpgsqlDbType.Varchar,  50, ParameterDirection.Input,   this.Pattern.PatternName)                                        
                                    };

            return _param;
        }
    }
}

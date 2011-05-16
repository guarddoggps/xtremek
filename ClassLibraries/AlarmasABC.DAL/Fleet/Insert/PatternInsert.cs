using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Fleet;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Fleet.Insert
{
    public class PatternInsert:DataAccessBase
    {

        public PatternInsert(Pattern _Pattern)
        {
            this.Pattern = _Pattern;
           Command = StoredProcedure.Name.SP_INSERT_PATTERN.ToString();
        }

        private Pattern _Pattern;

        public Pattern Pattern
        {
            get { return _Pattern; }
            set { _Pattern = value; }
        }
        
        public DataSet insertPattern()
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
            return _ds;
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@comID", NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.Pattern.ComID ),
                                        DataBaseHelper.MakeParam("@patterName",  NpgsqlTypes.NpgsqlDbType.Varchar,  50, ParameterDirection.Input,   this.Pattern.PatternName)                                        
                                    };

            return _param;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Fleet;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Fleet.Delete
{
    public class SuppliesPerPatternDelete:DataAccessBase
    {

        public SuppliesPerPatternDelete(SuppliesPerPattern _supplies)
        {
            this.Supplies = _supplies;
           Command = StoredProcedure.Name.SP_DELETE_SUPPLIES_PER_PATTERN.ToString();
        }

        private SuppliesPerPattern _supplies;

        public SuppliesPerPattern Supplies
        {
            get { return _supplies; }
            set { _supplies = value; }
        }
        
        public void deleteSupplies()
        {           
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);                
                _db.Run(base.ConnectionString,returnParam());
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
                                        DataBaseHelper.MakeParam("@patternID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.Supplies.PatternID )
                                    };

            return _param;
        }
    }
}

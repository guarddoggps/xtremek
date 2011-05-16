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
    public class SuppliesUpdate:DataAccessBase
    {

        public SuppliesUpdate(Supplies _supplies)
        {
            this.Supplies = _supplies;
           Command = StoredProcedure.Name.SP_UPDATE_SUPPLIES.ToString();
        }

        private Supplies _supplies;

        public Supplies Supplies
        {
            get { return _supplies; }
            set { _supplies = value; }
        }
        
        public void UpdateSupplies()
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
                                        DataBaseHelper.MakeParam("@ID", NpgsqlTypes.NpgsqlDbType.Integer ,  4,  ParameterDirection.Input,   this.Supplies.ID),
                                        DataBaseHelper.MakeParam("@Quantity",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.Supplies.Quantity ),
                                        DataBaseHelper.MakeParam("@Cost",  NpgsqlTypes.NpgsqlDbType.Numeric,  50,  ParameterDirection.Input,   this.Supplies.Cost ),
                                        DataBaseHelper.MakeParam("@Unit", NpgsqlTypes.NpgsqlDbType.Varchar,  50,  ParameterDirection.Input,   this.Supplies.Unit )
                                    };

            return _param;
        }
    }
}

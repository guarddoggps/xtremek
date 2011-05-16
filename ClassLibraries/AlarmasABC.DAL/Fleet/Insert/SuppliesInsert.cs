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
    public class SuppliesInsert:DataAccessBase
    {

        public SuppliesInsert(Supplies _supplies)
        {
            this.Supplies = _supplies;
           Command = StoredProcedure.Name.SP_INSERT_SUPPLIES.ToString();
        }

        private Supplies _supplies;

        public Supplies Supplies
        {
            get { return _supplies; }
            set { _supplies = value; }
        }
        
        public int insertSupplies()
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
                                        DataBaseHelper.MakeParam("@supplies", NpgsqlTypes.NpgsqlDbType.Varchar,  50,  ParameterDirection.Input,   this.Supplies.SupplieS ),
                                        DataBaseHelper.MakeParam("@comID", NpgsqlTypes.NpgsqlDbType.Integer ,  4,  ParameterDirection.Input,   this.Supplies.ComID),
                                        DataBaseHelper.MakeParam("@Quantity",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.Supplies.Quantity ),
                                        DataBaseHelper.MakeParam("@cost",  NpgsqlTypes.NpgsqlDbType.Numeric,  50,  ParameterDirection.Input,   this.Supplies.Cost ),
                                        DataBaseHelper.MakeParam("@unit",  NpgsqlTypes.NpgsqlDbType.Varchar,  50,  ParameterDirection.Input,   this.Supplies.Unit ),
                                        DataBaseHelper.MakeParam("@retVal",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.Supplies.RetVal)
                                    };

			return _param;
        }
    }
}

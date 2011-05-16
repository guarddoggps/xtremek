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
    public class SuppliesPerPatternInsert:DataAccessBase
    {

        public SuppliesPerPatternInsert(SuppliesPerPattern _suppliesPerPattern)
        {
            this.SuppliesPerPattern = _suppliesPerPattern;
           Command = StoredProcedure.Name.SP_INSERT_SUPPLIES_PER_PATTERN.ToString();
        }

        private SuppliesPerPattern _suppliesPerPattern;

        public SuppliesPerPattern SuppliesPerPattern
        {
            get { return _suppliesPerPattern; }
            set { _suppliesPerPattern = value; }
        }

        
        
        public int insertSuppliesPerPattern()
        {
            DataSet _ds = new DataSet();
            int _i = 0;
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);                
                _db.Run(base.ConnectionString,returnParam());
                _i = 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
            return _i;
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@PatternID", NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.SuppliesPerPattern.PatternID ),
                                        DataBaseHelper.MakeParam("@SuppliesID",  NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.SuppliesPerPattern.SuppliesID ),
                                        DataBaseHelper.MakeParam("@Quantity",  NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.SuppliesPerPattern.Qyantity ),
                                        DataBaseHelper.MakeParam("@KmInterval",  NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.SuppliesPerPattern.KmInterval ),
                                        DataBaseHelper.MakeParam("@DaysInterval",  NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.SuppliesPerPattern.DaysInterval )   
                                    };

            return _param;
        }
    }
}

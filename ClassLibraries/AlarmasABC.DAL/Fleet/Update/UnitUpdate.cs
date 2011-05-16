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
    public class UnitUpdate : DataAccessBase
    {

        public UnitUpdate(Units _unit)
        {
            this.Unit = _unit;
           Command = StoredProcedure.Name.SP_UPDATE_UNIT.ToString();
        }

        private Units _unit;

        public Units Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
                     
        
        public int updateUnit()
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
                                        DataBaseHelper.MakeParam("@PatternID", NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.Unit.PatternID ),
                                        DataBaseHelper.MakeParam("@UnitID",  NpgsqlTypes.NpgsqlDbType.Integer ,  4,   ParameterDirection.Input,   this.Unit.UnitID )
                                    };

            return _param;
        }
    }
}

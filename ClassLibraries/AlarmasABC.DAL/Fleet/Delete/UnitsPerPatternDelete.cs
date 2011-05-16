using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Fleet.Delete
{
    public class UnitsPerPatternDelete:DataAccessBase
    {
        public UnitsPerPatternDelete()
        {
           Command = StoredProcedure.Name.SP_DELETE_PATTERN_PER_UNIT.ToString();
        }


        private int _patternID;

        public int PatternID
        {
            get { return _patternID; }
            set { _patternID = value; }
        }


        public void deleteUnitPerPattern()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
                _ds = _db.Run(base.ConnectionString, returnParam());
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
                                        DataBaseHelper.MakeParam("@patternID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._patternID)
                                    };

            return _param;
        }
    }
}

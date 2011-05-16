using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Fleet.Update
{
    public class UnitsPerPatternUpdate:DataAccessBase
    {
        public UnitsPerPatternUpdate()
        {
           Command = StoredProcedure.Name.SP_DELETEANDUPDATE_PATTERN_PER_UNIT.ToString();
        }


        private int _patternID;

        public int PatternID
        {
            get { return _patternID; }
            set { _patternID = value; }
        }

        private int _UnitID;

        public int UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }

        private string _operation;

        public string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }


        public void updateUnitPerPattern()
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
           // return int.Parse(Ds.Tables[0].Rows[0][0].ToString());
        }


       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@patternID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input, this._patternID),
                                        DataBaseHelper.MakeParam("@unitID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input, this._UnitID),
                                        DataBaseHelper.MakeParam("@operation", NpgsqlTypes.NpgsqlDbType.Varchar,  20,  ParameterDirection.Input, this._operation)
                                    };

            return _param;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlarmasABC.Core.Admin;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class UnitTypeDataInsert: DataAccessBase
    {
        public UnitTypeDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_UNIT_TYPE.ToString();
        }

        private UnitType _unitType;
        public UnitType UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
        }

        public void AddUnitType()
        {
            UnitTypeInsertParams prm = new UnitTypeInsertParams(this._unitType);
            try
            {                
                DataBaseHelper db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                db.Run(base.ConnectionString, prm.Parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Data Access Error:: " + ex.Message);
            }
            finally
            {
                prm = null;
            }
        }
    }

    class UnitTypeInsertParams
    {
       private NpgsqlParameter[] _parameters;
        public NpgsqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private UnitType _unitType;
        public UnitTypeInsertParams(UnitType unit)
        {
            this._unitType = unit;
            build();
        }

        private void build()
        {            
                NpgsqlParameter[] parameters = { 
                                        DataBaseHelper.MakeParam("@typeName",       NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,   _unitType.TypeName),
                                        DataBaseHelper.MakeParam("@comID",          NpgsqlTypes.NpgsqlDbType.Integer,         4,      ParameterDirection.Input,   _unitType.ComID),
                                        };
                this._parameters = parameters;
        }
    }
}

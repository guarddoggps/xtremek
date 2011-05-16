using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlarmasABC.Core.Admin;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class UnitModelDataInsert : DataAccessBase
    {
        public UnitModelDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_UNIT_MODEL.ToString();
        }

        private UnitModel _unitModel;
        public UnitModel UnitModel
        {
            get { return _unitModel; }
            set { _unitModel = value; }
        }

        public void AddUnitModel()
        {
            UnitModelInsertParams prm = new UnitModelInsertParams(this._unitModel);
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

    class UnitModelInsertParams
    {
       private NpgsqlParameter[] _parameters;
        public NpgsqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private UnitModel _unitModel;
        public UnitModelInsertParams(UnitModel unitM)
        {
            this._unitModel = unitM;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] parameters = { 
                                        DataBaseHelper.MakeParam("@unitModel",      NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,   _unitModel.UnitModels),
                                        DataBaseHelper.MakeParam("@description",       NpgsqlTypes.NpgsqlDbType.Varchar,     200,    ParameterDirection.Input,   _unitModel.Description),
                                        DataBaseHelper.MakeParam("@comID",          NpgsqlTypes.NpgsqlDbType.Integer,         4,      ParameterDirection.Input,   _unitModel.ComID)
                                        };
            this._parameters = parameters;
        }
    }
}

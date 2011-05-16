using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;using Npgsql;

    namespace AlarmasABC.DAL.Update
    {
        public class UnitModelUpdate : DataAccessBase
        {
            private UnitModel _unitModel;
            public UnitModel UnitModel
            {
                get { return _unitModel; }
                set { _unitModel = value; }
            }

            public UnitModelUpdate()
            {
               Command = StoredProcedure.Name.SP_UPDATE_UNIT_MODEL.ToString();
            }

            public void updateUnitModel()
            {
                makeModelParam _mp = new makeModelParam(this._unitModel);

                try
                {
                    DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                    _db.Run(base.ConnectionString, _mp._params1);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _mp = null;
                }

            }
        }

        class makeModelParam
        {

            private UnitModel _unitModel;
            public UnitModel UnitModel
            {
                get { return _unitModel; }
                set { _unitModel = value; }
            }

           private NpgsqlParameter[] _params;

            public NpgsqlParameter[] _params1
            {
                get { return _params; }
                set { _params = value; }
            }


            public makeModelParam(UnitModel _unitModel)
            {
                this._unitModel = _unitModel;
                build();
            }

            private void build()
            {
                NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@unitModel",     NpgsqlTypes.NpgsqlDbType.Varchar,  20,    ParameterDirection.Input,   _unitModel.UnitModels),
                                        DataBaseHelper.MakeParam("@description",    NpgsqlTypes.NpgsqlDbType.Varchar,  150,   ParameterDirection.Input,   _unitModel.Description),
                                        DataBaseHelper.MakeParam("@comID",         NpgsqlTypes.NpgsqlDbType.Integer,         4,   ParameterDirection.Input,   _unitModel.ComID),
                                        DataBaseHelper.MakeParam("@unitModelID",    NpgsqlTypes.NpgsqlDbType.Integer,         4,   ParameterDirection.Input,   _unitModel.UnitModelID)
                                        
                                    };
                this._params1 = _param;
            }
        }
    

}

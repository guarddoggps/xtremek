using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;using Npgsql;


namespace AlarmasABC.DAL.Update
{
    
    public class UnitTypeUpdate : DataAccessBase
        {
            private UnitType _unitType;

            public UnitType UnitType
            {
              get { return _unitType; }
              set { _unitType = value; }
            }
            

            public UnitTypeUpdate()
            {
               Command = StoredProcedure.Name.SP_UPDATE_UNIT_TYPE.ToString();
            }

            public void updateUnitType()
            {
                makeTypeParam _mp = new makeTypeParam(this._unitType);

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

        class makeTypeParam
        {

            private UnitType _unitType;

            public UnitType UnitType
            {
              get { return _unitType; }
              set { _unitType = value; }
            }
            
           private NpgsqlParameter[] _params;

            public NpgsqlParameter[] _params1
            {
                get { return _params; }
                set { _params = value; }
            }


            public makeTypeParam(UnitType _unitType)
            {
                this._unitType = _unitType;
                build();
            }

            private void build()
            {
                NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@typeName",     NpgsqlTypes.NpgsqlDbType.Varchar,  50,    ParameterDirection.Input,   _unitType.TypeName),
                                        DataBaseHelper.MakeParam("@comID",         NpgsqlTypes.NpgsqlDbType.Integer,         4,   ParameterDirection.Input,   _unitType.ComID),
                                        DataBaseHelper.MakeParam("@typeID",          NpgsqlTypes.NpgsqlDbType.Integer,         4,   ParameterDirection.Input,   _unitType.TypeID)
                                                                              
                                    };
                this._params1 = _param;
            }
        
    

}

    
}

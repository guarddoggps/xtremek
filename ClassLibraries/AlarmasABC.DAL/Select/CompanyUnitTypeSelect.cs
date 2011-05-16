using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{

    public class CompanyUnitTypeSelect : DataAccessBase
    {
        private UnitType _unitType;

            public UnitType UnitType
            {
              get { return _unitType; }
              set { _unitType = value; }
            }
            private DataSet _ds;

            public DataSet Ds
            {
                get { return _ds; }
                set { _ds = value; }
            }

           
            public CompanyUnitTypeSelect()
            {
                Command = @"SELECT * FROM tblUnitType WHERE comID = :comID ORDER BY typeName ASC;";
            }

            public void selectCompanyUnitType()
            {
                
                
                makeTypeParam _mp = new makeTypeParam(this._unitType);

                try
                {
                    DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                   this._ds =  _db.Run(base.ConnectionString, _mp._params1);
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
                                    
                                        DataBaseHelper.MakeParam("comID",NpgsqlTypes.NpgsqlDbType.Integer,         4,   ParameterDirection.Input,_unitType.ComID )
                                                                              
                                    };
                this._params1 = _param;
            }
        
    

}
        
    

}

        


      
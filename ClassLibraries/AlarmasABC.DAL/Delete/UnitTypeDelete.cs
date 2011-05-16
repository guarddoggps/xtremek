using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    
      public class UnitTypeDelete:DataAccessBase
       {
         public UnitTypeDelete()
        {
           Command = StoredProcedure.Name.SP_DELETE_UNIT_TYPE.ToString();
        }

         private UnitType _unitType;

        public UnitType UnitType
        {
          get { return _unitType; }
          set { _unitType = value; }
        }
        

        public void DeleteUnitType()
        {
            makeTypeParam _mp = new makeTypeParam(this.UnitType);
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _mp.Param);
            }
            catch(Exception ex)
            {
                throw new Exception(":: "+ex.Message);
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

        public makeTypeParam(UnitType _unitType)
        {
            this._unitType = _unitType;
            build();
        }

       private NpgsqlParameter[] _param;

        public NpgsqlParameter[] Param
        {
            get { return _param; }
            set { _param = value; }
        }

        public void build()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@typeID", NpgsqlTypes.NpgsqlDbType.Integer, 4, ParameterDirection.Input, _unitType.TypeID)
                                     };

            this.Param = _params;
        }
    
    }
}

    



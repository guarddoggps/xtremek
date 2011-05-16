using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    
     public class UnitModelDelete:DataAccessBase
    {
         public UnitModelDelete()
        {
           Command = StoredProcedure.Name.SP_DELETE_UNIT_MODEL.ToString();
        }


         private UnitModel _unitModel;
         public UnitModel UnitModel
        {
            get { return _unitModel; }
            set { _unitModel = value; }
        }

        public void DeleteUnitModel()
        {
            makeModelParam _mp = new makeModelParam(this._unitModel);
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);
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

    class makeModelParam
    {
        private UnitModel _unitModel;

        public makeModelParam(UnitModel _unitModel)
        {
            this._unitModel = _unitModel;
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
                                        DataBaseHelper.MakeParam("@unitModelID", NpgsqlTypes.NpgsqlDbType.Integer, 4, ParameterDirection.Input, _unitModel.UnitModelID)
                                     };

            this.Param = _params;
        }
    
    }
}

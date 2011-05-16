using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Fleet.Update
{
    public class PatternMaintenaceUpdate:DataAccessBase
    {
        public PatternMaintenaceUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_UNITS_PATTERN.ToString();
        }

        private int _unitID;
        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        public void UpdateMaintenace()
        {
            DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);

            try
            {
                _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::FLEET::UPDATE::"+ex.Message);
            }

            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                                     DataBaseHelper.MakeParam("@UNITID",   NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UnitID)
                                     };

            return _params;
        }
        
    }
}

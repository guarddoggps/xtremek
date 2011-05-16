using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class DeleteUnit:DataAccessBase
    {
        public DeleteUnit()
        {
           Command = StoredProcedure.Name.SP_DELETE_UNIT.ToString();
        }

        public DeleteUnit(string status)
        {
            if (status == "Disable")
            {
                Command = StoredProcedure.Name.SP_UNIT_DISABLE.ToString();
            }
            if (status == "Enable")
            {
                Command = StoredProcedure.Name.SP_UNIT_ENABLE.ToString();
            }
        }

        public void UpdateUnitStatus()
        {
             DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, GetParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::UNIT::"+ex.Message);
            }
        }

        private int _unitID;
        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        public void DelUnit()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, GetParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::DELEUNIT::"+ex.Message);
            }
        }

       private NpgsqlParameter[] GetParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@unitID",NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UnitID)
                                    };
            return _param;
        }
    }
}

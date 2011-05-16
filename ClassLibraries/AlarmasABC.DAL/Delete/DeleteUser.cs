using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class DeleteUser:DataAccessBase
    {
        public DeleteUser()
        {
           Command = StoredProcedure.Name.SP_DELETE_USER.ToString();
        }

        public DeleteUser(string disable)
        {
            Command = StoredProcedure.Name.SP_USER_DISABLE.ToString();
        }

        private int _uID;
        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }

        public void DelUser()
        {
            DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, GetParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::DELETEUSER::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }
        public void DisableUser()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, GetParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::DisableUser::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] GetParam()
        {
            NpgsqlParameter[] _param = { 
                                    DataBaseHelper.MakeParam("@uID",   NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UID)
                                    };
            return _param;
        }
    }
}

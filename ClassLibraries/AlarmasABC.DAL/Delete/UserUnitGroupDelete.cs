using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class UserUnitGroupDelete:DataAccessBase
    {
        public UserUnitGroupDelete()
        {
           Command = StoredProcedure.Name.SP_DELETE_UNIT_GROUP.ToString();
        }

        private int _userID;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        public void invoke()
        {
            DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                 
            try
            {
                _db.Run(base.ConnectionString, GetParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::DELETE::"+ex.Message);
            }
        }

       private NpgsqlParameter[] GetParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@userID",    NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UserID),
                                        DataBaseHelper.MakeParam("@comID",      NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                     };
            return _params;
        }
    }
}

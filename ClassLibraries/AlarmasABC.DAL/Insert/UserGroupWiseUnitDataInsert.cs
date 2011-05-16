using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class UserGroupWiseUnitDataInsert:DataAccessBase
    {
        public UserGroupWiseUnitDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_USER_GROUPWISE_UNIT.ToString();
        }

        #region Private variables and properties

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _groupID;
            public int GroupID
            {
                get { return _groupID; }
                set { _groupID = value; }
            }

            private int _userID;
            public int UserID
            {
                get { return _userID; }
                set { _userID = value; }
            }

        #endregion

        public void invoke()
        {
            DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
            try
            {
                _db.Run(base.ConnectionString, returnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::Invoke:: " + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                     DataBaseHelper.MakeParam("@groupID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.GroupID),                                     
                                     DataBaseHelper.MakeParam("@comID",     NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID),
                                     DataBaseHelper.MakeParam("@uID",     NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UserID)
                                     };
            return _params;
        }
    }

}

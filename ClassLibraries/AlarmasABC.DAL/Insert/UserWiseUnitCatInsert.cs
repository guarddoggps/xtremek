using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class UserWiseUnitCatInsert:DataAccessBase
    {
        public UserWiseUnitCatInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_USERWISE_UNITCAT.ToString();
        }

        #region Private Variables and properties

        private int _groupID;

        public int GroupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }
        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        private int _userID;

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _unitGroupID;

        public int UnitGroupID
        {
            get { return _unitGroupID; }
            set { _unitGroupID = value; }
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
                throw new Exception("DAL:UserWiseUnitCatInsert:Invoke:: " + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                     DataBaseHelper.MakeParam("@comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._comID),
                                     DataBaseHelper.MakeParam("@uID",   NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._userID),
                                     DataBaseHelper.MakeParam("@unitGroupID",   NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._unitGroupID)
                                     };
            return _params;
        }



    }
}

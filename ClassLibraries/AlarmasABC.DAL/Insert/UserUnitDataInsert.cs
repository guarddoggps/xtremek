using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class UsersUnitDataInsert:DataAccessBase
    {
        public UsersUnitDataInsert()
        {
        }

        #region Private Variables and Properties
        
                private int _unitGroupID;
                public int UnitGroupID
                {
                    get { return _unitGroupID; }
                    set { _unitGroupID = value; }
                }

                private int _comID;
                public int ComID
                {
                    get { return _comID; }
                    set { _comID = value; }
                }

                private int _unitID;
                public int UnitID
                {
                    get { return _unitID; }
                    set { _unitID = value; }
                }

        #endregion

        public void addGroupWiseUnit()
        {
           Command = StoredProcedure.Name.SP_INSERT_USER_GROUPWISE_UNIT.ToString();
            DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, ReturnGroupParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::Insert::UsersUnitDataInsert::AddGroupWiseUnits::" + ex.Message);
            }
            finally
            {
                _db = null;
            }

        }

        public void addUnits()
        {
            Command = StoredProcedure.Name.SP_INSERT_USER_UNIT.ToString();
            DataBaseHelper _db = new DataBaseHelper();
            try
            {
                _db.Run(base.ConnectionString, ReturnParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::addUnits::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] ReturnParam()
        {
            NpgsqlParameter[] _param = { 
                                    DataBaseHelper.MakeParam("@unitID",NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UnitID),
                                    DataBaseHelper.MakeParam("@comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                    };
            return _param;
        }

        private NpgsqlParameter[] ReturnGroupParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@groupID",    NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._unitGroupID),
                                        DataBaseHelper.MakeParam("@comID",      NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                    };
            return _param;
        }

    }
}

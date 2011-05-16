using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class UnitAndUnitGroupSelect:DataAccessBase
    {
        public UnitAndUnitGroupSelect()
        {
           
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


        public void LoadListedUnits(IList<UserGroup> _GLists)
        {

			Command = "SELECT unitID,unitName FROM tblUnits WHERE coalesce(isDelete,'0') != '1'" +
					  " AND comID = :comID AND unitID IN (SELECT unitID FROM tblUnitUserWise" +
				      " WHERE uID = :uID) ORDER BY unitName ASC;";
            		  
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnUnitParam());

                // _GLists.Add(new UserGroup(0, ));

                while (_dr.Read())
                {
                    _GLists.Add(new UserGroup(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if (_dr != null)
                {
                    _db = null;
                    _dr = null;
                    _GLists = null;
                }

            }
        }

        public void LoadNotListedUnits(IList<UserGroup> _GLists)
        {

			Command = @"SELECT unitID,unitName FROM tblUnits WHERE unitID NOT IN" +
					  @"(SELECT unitID FROM tblUnitUserWise WHERE uID = :uID)" +
            	      @"AND coalesce(isDelete,'0') != '1' AND comID = :comID ORDER BY unitName ASC;";
	
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnUnitParam());

                // _GLists.Add(new UserGroup(0, ));

                while (_dr.Read())
                {
                    _GLists.Add(new UserGroup(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if (_dr != null)
                {
                    _db = null;
                    _dr = null;
                    _GLists = null;
                }

            }
        }

        public void LoadListedUnitGroups(IList<UserGroup> _GLists)
        {

            Command = @"SELECT typeID,typeName FROM tblUnitType WHERE typeID IN" + 
					  @" (SELECT DISTINCT t.typeID FROM tblUnitType t INNER JOIN tblUnits u" +
					  @" ON t.typeID = u.typeID AND u.unitID IN (SELECT unitID FROM tblUnitUserWise" +
					  @" WHERE uID = :uID)) AND comID = :comID AND coalesce(isDelete,'0') != '1'" +
					  @" ORDER BY typeName ASC;";
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnUnitGroupParam());

                // _GLists.Add(new UserGroup(0, ));

                while (_dr.Read())
                {
                    _GLists.Add(new UserGroup(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if (_dr != null)
                {
                    _db = null;
                    _dr = null;
                    _GLists = null;
                }

            }
        }

        public void LoadNotListedUnitGroups(IList<UserGroup> _GLists)
        {

			Command = @"SELECT typeID,typeName FROM tblUnitType WHERE typeID NOT IN" +
					  @" (SELECT DISTINCT t.typeID FROM tblUnitType t INNER JOIN tblUnits u ON" +
					  @" t.typeID = u.typeID AND u.unitID IN (SELECT unitID FROM tblUnitUserWise" +
					  @" WHERE uID = :uID)) AND comID = :comID AND coalesce(isDelete,'0') != '1'" +
            		  @" ORDER BY typeName ASC;";
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnUnitGroupParam());

                // _GLists.Add(new UserGroup(0, ));

                while (_dr.Read())
                {
                    _GLists.Add(new UserGroup(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if (_dr != null)
                {
                    _db = null;
                    _dr = null;
                    _GLists = null;
                }

            }
        }


       private NpgsqlParameter[] returnUnitParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("uID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._userID),
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                    };

            return _param;
        }

        private NpgsqlParameter[] returnUnitGroupParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("uID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._userID),
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._comID)
                                    };

            return _param;
        }

    }
}

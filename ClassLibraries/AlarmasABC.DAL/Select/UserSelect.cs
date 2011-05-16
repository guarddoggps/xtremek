using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class UserSelect : DataAccessBase
    {
        public UserSelect()
        {
            Command = "SELECT * FROM tblUser WHERE coalesce(isDelete,'0') != '1' AND uID = :uID;" +
					  " " + 
					  "SELECT schemeID FROM tblUserWiseScheme WHERE userID = :uID;";
        }

        public UserSelect(User _user)
        {
            this.User = _user;
			Command = @"SELECT uID,login FROM tblUser WHERE coalesce(isDelete,'0') != '1'" +
					  @"AND comID = :comID ORDER BY login ASC;";
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }


        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectUser()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString, returnSelectParam());
            }
            catch (Exception ex)
            {
                throw new Exception(" SelectUser :: " + ex.Message);
            }
        }

        
        public void UserDropDownList(IList<User> _users)
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

                while (_dr.Read())
                {
					_users.Add(new User(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _users = null;
                }

            }
        }

        public void CreateUserDropDownList(IList<User> _users)
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

                _users.Add(new User(0, "Create new user"));

                while (_dr.Read())
                {
					_users.Add(new User(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _users = null;
                }

            }
        }

       private NpgsqlParameter[] returnSelectParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("uID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._user.UID)
                                    };

            return _param;
        }
        private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._user.ComID)
                                    };

            return _param;
        }
    }


}

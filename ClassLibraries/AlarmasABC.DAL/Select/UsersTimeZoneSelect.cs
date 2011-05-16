using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    public class UsersTimeZoneSelect:DataAccessBase
    {

        public UsersTimeZoneSelect(User _user)
        {
            this.User = _user;
			Command = @"SELECT uID,login FROM tblUser WHERE coalesce(isDelete,'0') != '1'" +
					  @" AND comID = :comID ORDER BY login ASC;";
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

        public void UserDropDownList(IList<User> _users)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParams());

                _users.Add(new User(0, "Select User"));

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

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._user.ComID)
                                    };
            return _param;
        }
    }
}

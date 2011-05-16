using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class UserGroupSelect:DataAccessBase
    {
        public UserGroupSelect()
        {
			Command = "SELECT groupID,groupName,companyName,tblGroup.isDelete,tblGroup.comID" +
					  " AS comID FROM tblGroup INNER JOIN tblCompany ON tblGroup.comID =" +
					  " tblCompany.comID WHERE coalesce(tblGroup.isDelete,'0') != '1'" +
					  " ORDER BY groupName ASC;";
        }

        public UserGroupSelect(UserGroup _userGroup)
        {
            this._usergrouppObj = _userGroup;
			Command = "SELECT groupID,groupName FROM tblGroup WHERE coalesce(isDelete,'0') != '1'" +
            		  "AND groupName != 'Administrator' AND comID = :comID ORDER BY groupName ASC;";
        }

        private UserGroup _usergrouppObj;
        public UserGroup UsergrouppObj
        {
            get { return _usergrouppObj; }
            set { _usergrouppObj = value; }
        }


        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectUserGroup()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            { 
            }
        }

        public void UserGroupDropDownList(IList<UserGroup> _userGroup)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());
                _userGroup.Add(new UserGroup(0, "Select Group"));

                while (_dr.Read())
                {
                    _userGroup.Add(new UserGroup(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _userGroup = null;
                }

            }
        }

       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   _usergrouppObj.ComID)
                                    };

            return _param;
        }

    }
}

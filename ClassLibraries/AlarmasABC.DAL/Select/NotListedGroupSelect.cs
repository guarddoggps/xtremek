using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class NotListedGroupSelect:DataAccessBase
    {
        public NotListedGroupSelect()
        {
 
        }

        public NotListedGroupSelect(UserGroup _ugroup)
        {
            this.UsrGrp = _ugroup;
			Command = @"SELECT groupID,groupName FROM tblGroup WHERE groupID NOT IN" +
					  @" (SELECT groupID FROM tblGroupWiseUnit WHERE unitID = :unitID)" +
					  @" AND tblGroup.comID= :comID AND coalesce(isDelete,'0') != '1'" +
            		  @" ORDER BY groupName ASC;";
        }

        private UserGroup _UsrGrp;

        public UserGroup UsrGrp
        {
            get { return _UsrGrp; }
            set { _UsrGrp = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectNotListedGroup()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        public void LoadNotListedGroupList(IList<UserGroup> _GLists)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

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

       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        
                                        DataBaseHelper.MakeParam("unitID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._UsrGrp.UnitID),
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._UsrGrp.ComID)
                                    };

            return _param;
        }


    }
}

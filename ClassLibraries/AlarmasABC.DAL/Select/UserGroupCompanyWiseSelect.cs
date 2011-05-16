using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Select
{
    public class UserGroupCompanyWiseSelect:DataAccessBase
    {

        public UserGroupCompanyWiseSelect()
        {
			Command = @"SELECT * FROM tblGroup WHERE coalesce(isDelete,'0') != '1' AND comID = :comID" +
            		  @" ORDER BY groupName ASC;";
        }

        #region Private Variables and Properties

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        #endregion

        public void SelectGroup()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);

            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                     };
            return _params;
        }
    }
}

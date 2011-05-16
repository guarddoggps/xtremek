using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public class UnitCountSelect:DataAccessBase
	{
        public UnitCountSelect()
		{
        }

        #region Private variables and Properties

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _uID;
            public int UID
            {
                get { return _uID; }
                set { _uID = value; }
            }

            private int _typeID;
            public int TypeID
            {
                get { return _typeID; }
                set { _typeID = value; }
            }

            private DataSet _ds;
            public DataSet Ds
            {
                get { return _ds; }
                set { _ds = value; }
            }
            
        #endregion

        public void GetUnitCount()
		{
        	Command = "SELECT coalesce(COUNT(deviceID), '0') AS unitCount FROM" +
				       " (SELECT DISTINCT deviceID from vwUserWiseUnits where uID = " + UID + 
					   " AND comID= " + ComID + " AND typeID = " + TypeID + ") AS deviceID;";
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                this._ds = _db.Run(base.ConnectionString, returnParams());
            }
            catch (Exception ex)
            {
                throw new Exception(" DAL::GetUnitCount::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("comID",     NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID),
                                        DataBaseHelper.MakeParam("uID",        NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UID),
                                        DataBaseHelper.MakeParam("typeID",     NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.TypeID)
                                     };
            return _params;
        }
    }
}

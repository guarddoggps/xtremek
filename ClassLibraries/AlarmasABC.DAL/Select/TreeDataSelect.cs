using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL.Select
{
    public  class TreeDataSelect:DataAccessBase
    {
        

        public TreeDataSelect()
        {
			Command = "SELECT DISTINCT typeID,typeName FROM vwUserWiseUnits WHERE uID = :uID" +
					  " AND comID = :comID ORDER BY typeName;" +
                    
                      "SELECT DISTINCT deviceID,unitName,typeID,unitID FROM vwUserWiseUnits" +
					  " WHERE uID = :uID AND comID = :comID ORDER BY unitName;" +
					
                      "SELECT DISTINCT(unitID) FROM tblUnitUserWise WHERE uID = :uID;";
        }

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

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }
		
        public void SelectData()
		{
			DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
			try
			{
				_ds = _db.Run(base.ConnectionString, returnParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::TreeDataSelect:: SelectData " + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID),
                                        DataBaseHelper.MakeParam("uID",    NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UID)
                                    };
            return _param;
        }
    }
}

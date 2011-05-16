using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Queries
{
    public class ExecuteSQL:DataAccessBase
    {
        public ExecuteSQL()
        {            
        }

        public bool IsExistData(string _strSQL)
        {
            DataBaseHelper _db = new DataBaseHelper();
            DataTable _dt=_db.getTable(base.ConnectionString, false, _strSQL);

            if (_dt.Rows.Count > 0)
                return true;
            else 
                return false;
            
        }

        public DataTable ExecuteSQLQuery(string _strSQL)
        {
            DataTable _dt=new DataTable();
            DataBaseHelper _db = new DataBaseHelper();

            try
            {
                _dt = _db.getTable(base.ConnectionString, false, _strSQL);
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::Queries::ExecuteSQL::" + ex.Message);
            }
            finally
            {
                _dt = null;
                _db = null;
            }
        
        }

        public void ExecuteNonQuery(string _strSQL)
        {
            try 
            {
                DataBaseHelper _db = new DataBaseHelper();
                _db.ExecuteNonQuery(_strSQL, CommandType.Text);
            }
            catch(Exception ex)
            {
            
            }
        }

        public DataSet getDataSet(string strSQL)
        {
            DataBaseHelper _db = new DataBaseHelper();
            return _db.Run(base.ConnectionString, strSQL);            
        }

    }
}

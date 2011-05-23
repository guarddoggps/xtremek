
using System;
using System.Data;

namespace AlarmasABC.DAL.Misc
{
	public class ExecuteSQL : DataAccessBase
	{
		public ExecuteSQL()
		{
		}
		
		public DataSet getDataSet(string query)
		{
			DataBaseHelper db = new DataBaseHelper();
			return db.GetDataSet(query);	
		}
		
		public DataTable getDataTable(string query)
		{
			DataBaseHelper db = new DataBaseHelper();
			return db.GetDataTable(query);	
		}
	}
}

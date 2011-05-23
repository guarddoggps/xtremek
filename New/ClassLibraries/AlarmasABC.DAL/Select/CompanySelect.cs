
using System;
using System.Data;

using Npgsql;

using AlarmasABC.Core;


namespace AlarmasABC.DAL.Select
{
	public class CompanySelect : DataAccessBase
	{
		public CompanySelect()
		{
			Command = Function.Name.COMPANY_SELECT.ToString();
		}
		
		private string companyName;
		public string CompanyName
		{
			get { return companyName; }
			set { companyName = value; }
		}
		      
		public DataTable Run()
		{
			DataTable dt = new DataTable();
			DataBaseHelper db = new DataBaseHelper();
			
			try
			{
				dt = db.GetDataTable(Command, getParams());
			}
			catch (Exception ex)
			{
				ErrorHandling.ErrorOcurred(ex);
			}
			finally
			{
				db = null;
			}
			
			return dt;
		}
		
		NpgsqlParameter[] getParams()
		{
			NpgsqlParameter[] p = { DataBaseHelper.MakeParam( "@name", NpgsqlTypes.NpgsqlDbType.Varchar, 80, companyName ) };
			
			return p;
		}
	}
}

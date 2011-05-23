
using System;
using System.Data;
using Npgsql;
using AlarmasABC.Core;

namespace AlarmasABC.DAL.Select
{
	public class UserSelect : DataAccessBase
	{
		public UserSelect()
		{
			Command = Function.Name.USER_SELECT.ToString();
		}
		
		private string username;
		public string UserName
		{
			get { return username; }
			set { username = value; }
		}
		
		private string password;
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		
		public DataTable Run()
		{
			DataTable dt = new DataTable();
			DataBaseHelper db = new DataBaseHelper();
			
			Console.WriteLine("Username: " + username);
			Console.WriteLine("Password: " + password);
			
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
			NpgsqlParameter[] p = { DataBaseHelper.MakeParam( "@username", 	NpgsqlTypes.NpgsqlDbType.Varchar, 	80, username ),
									DataBaseHelper.MakeParam( "@password",	NpgsqlTypes.NpgsqlDbType.Varchar, 	40, password )
			};
			
			return p;
		}
	}
}

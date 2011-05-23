
using System;
using System.Configuration;

namespace AlarmasABC.DAL
{
	public class DataAccessBase
	{
		public DataAccessBase()
		{
		}
		
		private string command;
		protected string Command
		{
			get { return command; }
			set { command = value; }
		}
		
		protected string ConnectionString
		{
			get { return ConfigurationManager.ConnectionStrings["Database"].ToString(); }
		}
		
		protected string RGConnectionString
		{
			get { return ConfigurationManager.ConnectionStrings["RG_Database"].ToString(); }
		}
	}
}

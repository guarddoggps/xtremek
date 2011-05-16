/* ===============================================================================
   Cygnus Innovation Ltd.
 * Designed By Md. Ataur Rahaman
 * 
 * 
 * Descripttion: This is the base class For accessing Or Communicating with DataBase
// ==============================================================================*/


using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AlarmasABC.DAL
{
    public class DataAccessBase
    {
		private string _commmand;

        /// <summary>
        /// Get or set StoredProcedure name that you want to connect.
        /// </summary>
		
		protected string Command
		{
			get { return _commmand; }
			set { _commmand = value; }
		}

        /// <summary>
        /// Get The Connection string of database. (Read Only).
        /// </summary>
        protected string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["XtremeK_PostgreSQL_Connection"].ToString(); }
        }
		
		protected string RGConnectionString
		{
            get { return ConfigurationManager.ConnectionStrings["RG_PostgreSQL_Connection"].ToString(); }
		}
    }
}

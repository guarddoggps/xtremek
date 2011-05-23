
using System;
using System.Data;
using Npgsql;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL
{
	public class DataBaseHelper : DataAccessBase
	{		
		private CommandType commandType;
		
		/// <summary>
       	/// The DataBaseHelper class constructor. Sets the commandType parameter to CommandType.Text
        /// </summary>
		public DataBaseHelper()
		{
			commandType = CommandType.Text;
		}
		
		/// <summary>
       	/// Use SqlHelper class to get a DataSet object
        /// </summary>
        /// <param name="query">Specifiy SQL query to run in order to get DataSet</param>
		public DataSet GetDataSet(string query)
		{
            DataSet ds;
            ds = SqlHelper.ExecuteDataset(this.ConnectionString, commandType, query);
            return ds;
		}
		
		/// <summary>
       	/// Use SqlHelper class to get a DataTable object
        /// </summary>
        /// <param name="query">Specifiy SQL query to run in order to get DataTable</param>
		public DataTable GetDataTable(string query)
		{
			DataTable dt;
			dt = SqlHelper.ExecuteDataTable(this.ConnectionString, false, query);
			return dt;
		}
		
		public DataTable GetDataTable(string command, NpgsqlParameter [] parameters)
		{
			DataTable dt;
			Console.WriteLine(parameters[0].Value);
			dt = SqlHelper.ExecuteDataTable(this.ConnectionString, CommandType.StoredProcedure, command, parameters);
			return dt;
		}
		
		/// <summary>
        /// Make stored procedure param.
        /// </summary>
        /// <param name="ParamName">Name of param.</param>
        /// <param name="DbType">Param type.</param>
        /// <param name="Size">Param size.</param>
        /// <param name="Direction">Parm direction.</param>
        /// <param name="Value">Param value.</param>
        /// <returns>New parameter.</returns>
		public static NpgsqlParameter MakeParam(string ParamName, NpgsqlTypes.NpgsqlDbType DbType, Int32 Size, object Value)
        {
            NpgsqlParameter param;

            if (Size > 0)
                param = new NpgsqlParameter(ParamName, DbType, Size);
            else
                param = new NpgsqlParameter(ParamName, DbType);

            param.Direction = ParameterDirection.Input;
            param.Value = Value;

            return param;
        }
	}
}

/* ===============================================================================
   Cygnus Innovation Ltd.
 * Designed By Md. Ataur Rahaman
 * 
 * 
 * Descripttion: This is the ....
// ==============================================================================*/


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;

using Microsoft.ApplicationBlocks.Data;

namespace AlarmasABC.DAL
{
	public class DataBaseHelper: DataAccessBase
   	{
    	private NpgsqlParameter[] _parameters;
		private CommandType _commandType;


      	/// <summary>
       	/// Costructor that helps to initialize with the procedure name.
        /// </summary>
        /// <param name="storedprocedurename"></param>
       	public DataBaseHelper(string command, CommandType commandType)
       	{
           	Command = command;
			_commandType = commandType;
       	}

       /// <summary>
       /// 
       /// </summary>
       public DataBaseHelper()
       {
       }


        /// <summary>
        /// Run the procedure(Inherited from Base) with the specific transaction
        /// </summary>
        /// <param name="transaction">Sql Transaction</param>
        public void Run( NpgsqlTransaction transaction )
		{
			SqlHelper.ExecuteNonQuery( transaction , _commandType , Command , Parameters );
		}


        /// <summary>
        /// Run the procedure(Inherited from Base) with the specific transaction and required Parameters.
        /// </summary>
        /// <param name="transaction">Sql Transaction</param>
        /// <param name="parameters">Parameters that required by the procedure.</param>
        public void Run( NpgsqlTransaction transaction , NpgsqlParameter[] parameters )
		{
			SqlHelper.ExecuteNonQuery( transaction , _commandType, Command , parameters );
		}

        
        /// <summary>
        /// Run the procedure(Inherited from Base) with required Parameters And Specific Connection String.
        /// </summary>
        /// <param name="connectionstring">Connection string for the Database</param>
        /// <param name="parameters">Parameters that required by the procedure.</param>
        /// <returns> return type is System.Data.DataSet</returns>
		public DataSet Run( string connectionstring , NpgsqlParameter[ ] parameters )
		{
			DataSet ds;
			ds = SqlHelper.ExecuteDataset( connectionstring , _commandType, Command , parameters );
			return ds;
		}


        public DataSet Run(string connectionstring, string textSQL)
        {
            DataSet ds;
            ds = SqlHelper.ExecuteDataset(connectionstring, _commandType, textSQL);
            return ds;
        }

        /// <summary>
        /// Call the procedure(Inherited from Base) for getting some scalar/single value. provide required Parameters And Specific Connection String.
        /// </summary>
        /// <param name="connectionstring">Connection string for the Database</param>
        /// <param name="parameters">Parameters that required by the procedure.</param>
        /// <returns>Return the scalar value as System.object type.</returns>
		public object RunScalar( string connectionstring , NpgsqlParameter[ ] parameters )
		{
			object obj;
			obj = SqlHelper.ExecuteScalar( connectionstring , Command , parameters );
			return obj;
		}


        /// <summary>
        /// Run the procedure(Inherited from Base) with specified Transaction and NpgsqlParameters list for getting Scalar onject.
        /// </summary>
        /// <param name="transaction">Sql Transaction</param>
        /// <param name="parameters">Parameters that required by the procedure.</param>
        /// <returns>Return the scalar value as System.object type.</returns>
		public object RunScalar( NpgsqlTransaction transaction , NpgsqlParameter[] parameters )
		{
			object obj;
			obj = SqlHelper.ExecuteScalar( transaction , Command , parameters );
			return obj;
		}


        /// <summary>
        /// Run the procedure(Inherited from Base) with specified connection string for getting Dataset.
        /// </summary>
        /// <param name="connectionstring">Connection string for the Database.</param>
        /// <returns>return System.Data.DataSet</returns>
		public DataSet Run( string connectionstring )
		{
			DataSet ds;
			ds = SqlHelper.ExecuteDataset( connectionstring , CommandType.Text, Command );
			return ds;
		}


       //public static DataTable ExecuteDataTable(SqlConnection connection, bool isProc, string commandText)

       public DataTable getTable(string connectionString, bool isProc, string commandText)
       {
           DataTable dt;
           dt = SqlHelper.ExecuteDataTable(connectionString, isProc, commandText);
           return dt;
       }

		
        /// <summary>
        /// Run the procedure(Inherited from Base). return type void.
        /// </summary>
        public void Run()
		{
			SqlHelper.ExecuteNonQuery( base.ConnectionString , _commandType, Command , Parameters );
		}


        /// <summary>
        /// Run the procedure(Inherited from Base) with the parameters list.
        /// </summary>
        /// <param name="parameters">Parameters that required by the procedure.</param>
        /// <returns>Return System.Data.SqlClient.NpgsqlDataReader</returns>
		public NpgsqlDataReader ExecuteReader( NpgsqlParameter[ ] parameters )
		{
			NpgsqlDataReader dr;
			dr = SqlHelper.ExecuteReader( base.ConnectionString , _commandType, Command , parameters );
			return dr;            
		}

       
       /// <summary>
       /// 
       /// </summary>
       /// <param name="parameters"></param>
       /// <returns></returns>
       public NpgsqlDataReader ExecuteReader(string connectionString)
       {
           //NpgsqlDataReader dr;
           return  SqlHelper.ExecuteReader(connectionString, CommandType.Text, Command);
       }

       /// <summary>
       /// Execute A valid Non Command Command.
       /// </summary>
       /// <param name="commandText"></param>
       /// <param name="commandType"></param>
       /// <returns></returns>
       public int ExecuteNonQuery(string commandText, CommandType commandType)
       {
           if (commandText.Contains("--") || commandText.Contains("Drop "))
           {
               throw new Exception("Not a Valid Sql Syntex.");
           }
           return SqlHelper.ExecuteNonQuery(base.ConnectionString, commandType, commandText);
       }


        /// <summary>
        /// Get or Set the Parameters that required by procedure.
        /// </summary>
		public NpgsqlParameter[ ] Parameters
		{
			get { return _parameters; }
			set { _parameters = value; }
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
        public static NpgsqlParameter MakeParam(string ParamName, NpgsqlTypes.NpgsqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            NpgsqlParameter param;

            if (Size > 0)
                param = new NpgsqlParameter(ParamName, DbType, Size);
            else
                param = new NpgsqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class ErrorReportSelect:DataAccessBase
    {

        public ErrorReportSelect(ErrorReport _errorReport)
        {
            this.ErrorReport= _errorReport;
            Command = @"SELECT * FROM tblErrorLog WHERE serviceName = :serviceName AND" +
					  @" errorTime BETWEEN :serviceName and :serviceName" +
	 			      @" ORDER BY errorTime DESC;";
        }

        private ErrorReport _errorReport;

        public ErrorReport ErrorReport
        {
            get { return _errorReport; }
            set { _errorReport = value; }
        }

        

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void getErrorReport()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString,returnParam());                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("serviceName", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ErrorReport.ServiceName),
                                        DataBaseHelper.MakeParam("startTime",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ErrorReport.StartTime),
                                        DataBaseHelper.MakeParam("endTime",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ErrorReport.EndTime)
                                    };

            return _param;
        }
    }
}

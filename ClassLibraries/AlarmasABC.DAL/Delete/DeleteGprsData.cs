using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class DeleteGprsData:DataAccessBase
    {
        public DeleteGprsData()
        {
            Command = StoredProcedure.Name.SP_DELETE_GPRS_DATA.ToString();
        }

        #region Private variables and Properties

        private string _startDate;
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private string _endDate;
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

            
        #endregion


        public void DeleteData()
        {
            DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);

            try
            {
                _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL::Delete::"+ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

        private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@startDate",    NpgsqlTypes.NpgsqlDbType.Varchar,     50, ParameterDirection.Input,   this.StartDate),
                                        DataBaseHelper.MakeParam("@endDate",    NpgsqlTypes.NpgsqlDbType.Varchar,     50, ParameterDirection.Input,   this.EndDate)                                     
                                     };
            return _params;
        }

        
    }
}

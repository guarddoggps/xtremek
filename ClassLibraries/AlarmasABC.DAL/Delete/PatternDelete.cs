using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class PatternDelete:DataAccessBase
    {

        public PatternDelete()
        {
           Command = StoredProcedure.Name.SP_DELETE_PATTERN.ToString();
        }


        private int _PatternId;

        public int PatternId
        {
            get { return _PatternId; }
            set { _PatternId = value; }
        }


        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void deletePattern()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);
                this._ds = _db.Run(base.ConnectionString, returnParams());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {

            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@patternID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._PatternId)
                                     };
            return _params;
        }
    }
}

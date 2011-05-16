using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class TimeZoneUpdate:DataAccessBase
    {

        public TimeZoneUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_TIMEZONE.ToString();
        }


        private float _timeZone;

        public float TimeZone
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }

        private int _uID;

        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }

    
        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void updateTimeZone()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                this._ds = _db.Run(base.ConnectionString, returnParams());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {

            }
            //return int.Parse(Ds.Tables[0].Rows[0][0].ToString());
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@timezone",  NpgsqlTypes.NpgsqlDbType.Double,8,ParameterDirection.Input,this._timeZone),
                                        DataBaseHelper.MakeParam("@uID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._uID)

                                     };
            return _params;
        }
    }
}

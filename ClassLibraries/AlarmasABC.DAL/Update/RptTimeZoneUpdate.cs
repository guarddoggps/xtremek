using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class RptTimeZoneUpdate:DataAccessBase
    {

        public RptTimeZoneUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_RPT_TIMEZONE.ToString();
        }

        private int _tzID;

        public int TzID
        {
            get { return _tzID; }
            set { _tzID = value; }
        }
        private float _tzValue;

        public float TzValue
        {
            get { return _tzValue; }
            set { _tzValue = value; }
        }
        private string _rptLocation;

        public string RptLocation
        {
            get { return _rptLocation; }
            set { _rptLocation = value; }
        }


        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void updateRptTimeZone()
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
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@rptLocation", NpgsqlTypes.NpgsqlDbType.Varchar,50,ParameterDirection.Input,this._rptLocation),
                                        DataBaseHelper.MakeParam("@tzValue",  NpgsqlTypes.NpgsqlDbType.Double,8,ParameterDirection.Input,this._tzValue),
                                        DataBaseHelper.MakeParam("@tzID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._tzID)

                                     };
            return _params;
        }
    }
}

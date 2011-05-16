using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    public class TimeZoneSelect:DataAccessBase
    {

		public TimeZoneSelect(RptTimeZone _tz)
        {
            this._tz = _tz;
			Command = @"SELECT tzID,rptLocation || '(UTC' || to_char(tzValue, '9') || ')'" +
            		  @" AS timezone FROM tblRptTimeZone;";
        }

        private RptTimeZone _tz;

        public RptTimeZone Tz
        {
            get { return _tz; }
            set { _tz = value; }
        }

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void TimeZoneDropDownList(IList<RptTimeZone> _TimeZone)
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(base.ConnectionString);

                _TimeZone.Add(new RptTimeZone(0, "Select Time Zone"));

                while (_dr.Read())
                {
                    _TimeZone.Add(new RptTimeZone(float.Parse(_dr[0].ToString()), _dr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if (_dr != null)
                {
                    _db = null;
                    _dr = null;
                    _TimeZone = null;
                }

            }
        }
    }
}

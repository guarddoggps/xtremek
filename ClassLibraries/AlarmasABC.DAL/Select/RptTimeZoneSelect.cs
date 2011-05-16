using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Delete
{
    public class RptTimeZoneSelect:DataAccessBase
    {

        public RptTimeZoneSelect()
        {
            Command = @"SELECT * FROM tblRptTimeZone;";
        }


        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectRptTimeZone()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {

            }
        }
    }
}

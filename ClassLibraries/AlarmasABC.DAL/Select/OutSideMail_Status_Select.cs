using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Select
{
    public class OutSideMail_Status_Select:DataAccessBase
    {
        

        public OutSideMail_Status_Select()
        {
			Command = @"SELECT isOutsideMail AS status FROM tblUnitWiseRules WHERE unitID = :uID" +
            		  @" AND geofenceID != 0 AND isGeofenceActive = '1';";
        }

        private int unitID;

        public int UnitID
        {
            get { return unitID; }
            set { unitID = value; }
        }

       
        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void getMailOutSideStatus()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
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
                                        DataBaseHelper.MakeParam("uID", NpgsqlTypes.NpgsqlDbType.Varchar,4,ParameterDirection.Input, this.UnitID)
                                     };
            return _params;
        }

       

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Fleet.Select
{
    public class GPRSSelect : DataAccessBase
    {
        public GPRSSelect()
        {
           Command = StoredProcedure.Name.SP_SELECT_GPRS.ToString();
        }


        private int deviceID;

        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }



        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectGPRS()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString, returnParam());
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
                                        DataBaseHelper.MakeParam("@deviceID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.deviceID),
                                        DataBaseHelper.MakeParam("@startDate",  NpgsqlTypes.NpgsqlDbType.Timestamp,  30,  ParameterDirection.Input,   this._startDate)
                                    };

            return _param;
        }
    }
}

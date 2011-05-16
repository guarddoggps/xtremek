using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Insert
{
	
	public class UnitCommandInsert:DataAccessBase
	{
		public UnitCommandInsert()
		{
           	Command = StoredProcedure.Name.FN_INSERT_UNIT_COMMAND.ToString();
		}
		
		private int deviceID;
		public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }
		
        private string msgBody;

        public string MsgBody
        {
            get { return msgBody; }
            set { msgBody = value; }
        }
		
        public void invoke()
        {
            DataBaseHelper db =new DataBaseHelper(Command,CommandType.StoredProcedure);
            try
            {
                db.Run(base.ConnectionString, returnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DAL:UnitCommandInsert:: " + ex.Message);
            }
            finally
            {
                db = null;
            }
        }

       	private NpgsqlParameter[] returnParams()
        {
        	NpgsqlParameter[] _params = {                                      
					DataBaseHelper.MakeParam("@msgBody",   NpgsqlTypes.NpgsqlDbType.Varchar,  200,  ParameterDirection.Input,   this.msgBody),                                                     
                    DataBaseHelper.MakeParam("@deviceID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.deviceID)
            };
            return _params;
        }
	}
}

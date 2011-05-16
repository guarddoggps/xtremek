using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Fleet.Select
{
    public class MaintainanceStatusSelect:DataAccessBase
    {
        public MaintainanceStatusSelect()
        {
           Command = StoredProcedure.Name.SP_SELECT_MANINTAINNANCE_STATUS.ToString();
        }


        private int _patternID;

        public int PatternID
        {
            get { return _patternID; }
            set { _patternID = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectMaintainanceStatus()
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
                                        DataBaseHelper.MakeParam("@patternID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._patternID),
                                        DataBaseHelper.MakeParam("@comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._comID)
                                    };

            return _param;
        }
    }
}

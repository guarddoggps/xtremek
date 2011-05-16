using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Fleet.Select
{
    public  class UnitSelect:DataAccessBase
    {
        public UnitSelect(int _comID)
        { 
            this.ComID = _comID;
           Command = StoredProcedure.Name.SP_SELECT_UNITS_COMWISE.ToString();
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

        public DataSet getUnit()
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
            return this.Ds;
        }


       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@ComID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                    };

            return _param;
        }
    }
}

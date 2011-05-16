using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Fleet.Select
{
    public  class UnitCountSelect:DataAccessBase
    {
        public UnitCountSelect()
        {

           Command = StoredProcedure.Name.SP_UNIT_COUNT_SELECT.ToString();
        }

      

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private string _comID;


        public string ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        public DataSet getUnitCount()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString,getParams());
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


       private NpgsqlParameter[] getParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@ComID", NpgsqlTypes.NpgsqlDbType.Varchar, 50, ParameterDirection.Input, this.ComID)
                                     };
            return _params;
        }


        
    }
}

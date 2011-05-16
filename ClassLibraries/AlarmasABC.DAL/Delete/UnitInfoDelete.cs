using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    public class UnitInfoDelete:DataAccessBase
    {

         public UnitInfoDelete()
        {
            
        }

         public UnitInfoDelete(Units _Units)
        {
            this.Units = _Units;
           Command = StoredProcedure.Name.SP_DELETE_UNIT_INFO.ToString();
     
        }

        //private int ComID;
        //public int ComID
        //{
        //    get { return ComID; }
        //    set { ComID = value; }
        //}

         private Units _Units;

         public Units Units
         {
             get { return _Units; }
             set { _Units = value; }
         }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void deleteUnits()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);
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
                                        DataBaseHelper.MakeParam("@unitID", NpgsqlTypes.NpgsqlDbType.Varchar,4,ParameterDirection.Input,this._Units.UnitID)
                                     };
            return _params;
        }


    }
}

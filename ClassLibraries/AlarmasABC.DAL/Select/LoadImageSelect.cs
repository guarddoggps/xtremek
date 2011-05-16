using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class LoadImageSelect:DataAccessBase
    {


        public LoadImageSelect(Units _iconSetUp)
        {
            this.Units = _iconSetUp;
            Command = @"SELECT iconName FROM tblUnits WHERE unitID = :unitID AND comID = :comID;";
        }

        private Units _units;

        public Units Units
        {
            get { return _units; }
            set { _units = value; }
        }



        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }


        public void selectImage()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
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
                                        DataBaseHelper.MakeParam("unitID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._units.UnitID),
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._units.ComID)
                                    };

            return _param;
        }

    }
}

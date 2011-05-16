using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class DeviceIDSelect:DataAccessBase
    {

        public DeviceIDSelect(Units _unit)
        {
            this.Units = _unit;
				Command = @"SELECT unitID,unitName FROM tblUnits a INNER JOINtblUnitType t" +
            		      @" ON t.typeID = a.typeID WHERE t.comID = :comID;";

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

        public void selectDeviceID()
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

        public void DeviceNameDropDownList(IList<Units> _Units)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

                _Units.Add(new Units(0, "Select Unit"));

                while (_dr.Read())
                {
                    _Units.Add(new Units(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if(_dr!=null)
                {
                    _db = null;
                    _dr=null;
                    _Units = null;
                }
                
            }
        }

       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._units.ComID)
                                    };

            return _param;
        }
    }
}

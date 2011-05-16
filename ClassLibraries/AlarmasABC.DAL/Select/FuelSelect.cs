using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class FuelSelect:DataAccessBase
    {
        public FuelSelect()
        {
            Command = @"SELECT id,fuelType FROM tblFuel ORDER BY fuelType ASC;";
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectFuel()
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

        public void FuelDropDownList(IList<Fuel> _fuels)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(base.ConnectionString);

                _fuels.Add(new Fuel(0, "Select Fuel"));

                while (_dr.Read())
                {
                    _fuels.Add(new Fuel(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _fuels = null;
                }
                
            }
        }
       

    }
}

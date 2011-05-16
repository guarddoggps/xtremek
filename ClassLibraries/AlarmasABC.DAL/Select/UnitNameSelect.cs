using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class UnitNameSelect:DataAccessBase
    {

        public UnitNameSelect()
        {
 
        }

        public UnitNameSelect(Units _unit)
        {
            this._units = _unit;
			Command = "SELECT unitID,unitName FROM tblUnits WHERE comID = :comID" +
            		  " AND coalesce(isDelete,'0') != '1' ORDER BY unitName ASC;";
		}
		
        public UnitNameSelect(Units _unit, string userID)
        {
            this._units = _unit;
			Command = "SELECT unitID,unitName FROM tblUnits WHERE comID = :comID" +
					" AND coalesce(isDelete,'0') != '1' AND unitID = (SELECT DISTINCT" + 
					" unitID FROM tblUnitUserWise WHERE uID = " + userID + " AND tblUnitUserWise.unitID" + 
					" = tblUnits.unitID) ORDER BY unitName ASC;";
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

        public void selectUnit()
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

        public void UnitNameDropDownList(IList<Units> _Units)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

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

        public void CreateUnitNameDropDownList(IList<Units> _Units)
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

                _Units.Add(new Units(0, "Create Unit"));

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
                if (_dr != null)
                {
                    _db = null;
                    _dr = null;
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

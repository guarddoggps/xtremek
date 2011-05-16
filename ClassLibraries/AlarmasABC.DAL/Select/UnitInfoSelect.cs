using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class UnitInfoSelect:DataAccessBase
    {

        public UnitInfoSelect()
		{
            Command = "SELECT * FROM tblUnits WHERE comID = :comID AND coalesce(isDelete,'0') != '1'" +
            		  " ORDER BY unitName ASC;";
        }

        public UnitInfoSelect(Units _Units)
        {
            this.Units = _Units;
			Command = "SELECT * FROM tblUnits WHERE unitID = :unitID AND coalesce(isDelete,'0') != '1'" +
            		  " ORDER BY unitName;";
     
        }

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

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

        public void selectUnits()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
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
                                        DataBaseHelper.MakeParam("unitID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._Units.UnitID)
                                     };
            return _params;
        }

        public void UnitDropDownList(IList<Units> _units)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnComParams());

                _units.Add(new Units(0, "Select Unit"));

                while (_dr.Read())
                {
                    _units.Add(new Units(int.Parse(_dr[0].ToString()), _dr[2].ToString()));
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
                    _units = null;
                }

            }
        }

        public void UnitListBoxItems(IList<Units> _units)
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnComParams());               

                while (_dr.Read())
                {
                    _units.Add(new Units(int.Parse(_dr[0].ToString()), _dr[2].ToString()));
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
                    _units = null;
                }

            }
        }

        private NpgsqlParameter[] returnComParams()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input, this.ComID)
                                    };
            return _param;
        }

    }
}

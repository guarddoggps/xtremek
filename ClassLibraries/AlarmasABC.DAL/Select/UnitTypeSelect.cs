using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{

    public class UnitTypeSelect : DataAccessBase
    {
        public UnitTypeSelect()
        {
           Command = StoredProcedure.Name.SP_SELECT_UNIT_TYPE.ToString();
        }

        public UnitTypeSelect(UnitType _uType)
        {
            this._UnitType = _uType;
			Command = "SELECT typeID,typeName FROM tblUnitType WHERE comID = :comID" +
            		  " ORDER BY typeName ASC;";
     
        }

        //private int ComID;
        //public int ComID
        //{
        //    get { return ComID; }
        //    set { ComID = value; }
        //}

        private UnitType _UnitType;

        public UnitType UnitType
        {
            get { return _UnitType; }
            set { _UnitType = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectUnitType()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
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


        public void UnitTypeDropDownList(IList<UnitType> _unitType)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParams());

                _unitType.Add(new UnitType(0, "Select Unit Category"));

                while (_dr.Read())
                {
                    _unitType.Add(new UnitType(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _unitType = null;
                }

            }
        }

        public void UnitTypeListBox(IList<UnitType> _unitType)
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParams());               

                while (_dr.Read())
                {
                    _unitType.Add(new UnitType(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _unitType = null;
                }

            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._UnitType.ComID)
                                     };
            return _params;
        }


    }

}

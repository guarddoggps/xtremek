using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class UnitModelAdSelect:DataAccessBase
    {

        public UnitModelAdSelect()
        {

        }

        public UnitModelAdSelect(UnitModel _Umodel)
        {
            this.UnitModel = _Umodel;
            Command = "SELECT unitModelID,unitModel FROM tblUnitModel WHERE comID = :comID;";
     
        }


        private UnitModel _UnitModel;

        public UnitModel UnitModel
        {
            get { return _UnitModel; }
            set { _UnitModel = value; }
        }

    

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectUnitModel()
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


        public void UnitModelAdDropDownList(IList<UnitModel> _unitModel)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParams());

                _unitModel.Add(new UnitModel(0, "Select Unit Category"));

                while (_dr.Read())
                {
                    _unitModel.Add(new UnitModel(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _unitModel = null;
                }

            }
        }

       private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this._UnitModel.ComID)
                                     };
            return _params;
        }





    }
}

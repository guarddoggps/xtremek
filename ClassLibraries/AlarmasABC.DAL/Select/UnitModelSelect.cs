using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
  
        public class UnitModelSelect : DataAccessBase
        {
            public UnitModelSelect()
            {
                Command = "SELECT unitModelID,unitModel,description,tblUnitModel.comID,companyName" +
						  " FROM tblCompany JOIN tblUnitModel ON (tblUnitModel.comID = tblCompany.comID)" +
						  " WHERE coalesce(tblCompany.isDelete,'0') != '1' ORDER BY unitModel ASC;";
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

            //public void CompanyDropDownList(IList<Company> _company)
            //{
            //   NpgsqlDataReader _dr = null;
            //    DataBaseHelper _db = new DataBaseHelperCommand);
            //    try
            //    {
            //        _dr = _db.ExecuteReader(base.ConnectionString);

            //        while (_dr.Read())
            //        {
            //            _company.Add(new Company(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(" :: " + ex.Message);
            //    }
            //    finally
            //    {
            //        if (_dr != null)
            //        {
            //            _db = null;
            //            _dr = null;
            //            _company = null;
            //        }

            //    }
            //}

        }
    
}

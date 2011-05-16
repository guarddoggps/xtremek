using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class CompanySelect:DataAccessBase
	{
        public CompanySelect()
        {
			Command = "SELECT * FROM tblCompany WHERE coalesce(isDelete,'0') != '1'" +
					  " ORDER BY companyName ASC;";
           	_comID = -1;
		}
		
		public CompanySelect(int comID) 
		{
			Command = "SELECT * FROM tblCompany WHERE coalesce(isDelete,'0') != '1'" +
					  " AND comID = :comID ORDER BY companyName ASC;";
            _comID = comID;
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
		}
		
		private int _comID;
		public int ComID
		{
			get { return _comID; }
			set { _comID = value; }
		}
    	

        public void selectCompany()
        {
            try
            {
				DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                if (_comID == -1) {
					this._ds = _db.Run(base.ConnectionString);
				} else {
					this._ds = _db.Run(base.ConnectionString, returnParams());
               	}
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        public void CompanyDropDownList(IList<Company> _company)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(base.ConnectionString);

                _company.Add(new Company(0,"Select Company"));

                while (_dr.Read())
                { 
                    _company.Add(new Company(int.Parse(_dr[0].ToString()),_dr[1].ToString()));
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
                    _company=null;
                }
                
            }
		}
		
       	private NpgsqlParameter[] returnParams()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,this.ComID),
                                     };
            return _params;
        }
        
    }

}

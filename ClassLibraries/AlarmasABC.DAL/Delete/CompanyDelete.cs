using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    public class CompanyDelete:DataAccessBase
    {
        public CompanyDelete()
        {
            Command = StoredProcedure.Name.SP_DELETE_COMPANY.ToString();
        }

        private Company _company;

        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public void deleteCompany()
        {
            makeParam _mp = new makeParam(this._company);
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _mp.Param);
            }
            catch(Exception ex)
            {
                throw new Exception(":: "+ex.Message);
            }

            finally
            {
                _mp = null;
            }
        }
    }

    class makeParam
    {
        private Company _company;

        public makeParam(Company _company)
        {
            this._company = _company;
            build();
        }

        private NpgsqlParameter[] _param;

        public NpgsqlParameter[] Param
        {
            get { return _param; }
            set { _param = value; }
        }

        public void build()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("comID",NpgsqlTypes.NpgsqlDbType.Integer,4,ParameterDirection.Input,_company.ComID)
                                     };

            this.Param = _params;
        }
    
    }
}

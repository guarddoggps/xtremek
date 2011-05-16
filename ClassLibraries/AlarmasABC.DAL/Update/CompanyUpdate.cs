using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Update
{
    public class CompanyUpdate:DataAccessBase
    {
        private Company _company;
        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public CompanyUpdate()
        {
           Command = StoredProcedure.Name.SP_UPDATE_COMPANY.ToString();
        }

        public void updateCompanyInfo()
        {
            makecompanyParam _cp=new makecompanyParam(this._company);

            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString,_cp._params1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            { 
                _cp=null;
            }

        }
    }

    class makecompanyParam
    {
        
        private Company _company;

        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }

       private NpgsqlParameter[] _params;

        public NpgsqlParameter[] _params1
        {
            get { return _params; }
            set { _params = value; }
        }


        public makecompanyParam(Company _company)
        {
            this._company = _company;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@comID",         NpgsqlTypes.NpgsqlDbType.Integer,  4,          ParameterDirection.Input,   _company.ComID),
                                        DataBaseHelper.MakeParam("@companyName",   NpgsqlTypes.NpgsqlDbType.Varchar,  70,    ParameterDirection.Input,   _company.CompanyName),
                                        DataBaseHelper.MakeParam("@address",        NpgsqlTypes.NpgsqlDbType.Varchar, 100,    ParameterDirection.Input,   _company.Address),
                                        DataBaseHelper.MakeParam("@phone",          NpgsqlTypes.NpgsqlDbType.Varchar,  20,    ParameterDirection.Input,   _company.Phone),
                                        DataBaseHelper.MakeParam("@website",        NpgsqlTypes.NpgsqlDbType.Varchar,  70,    ParameterDirection.Input,   _company.WebSite),
                                        DataBaseHelper.MakeParam("@email",          NpgsqlTypes.NpgsqlDbType.Varchar,  40,    ParameterDirection.Input,   _company.Email),
                                        DataBaseHelper.MakeParam("@isActive",      NpgsqlTypes.NpgsqlDbType.Boolean,  1,          ParameterDirection.Input,   _company.IsActive),
                                    };
            this._params1 = _param;
        }
    }
}

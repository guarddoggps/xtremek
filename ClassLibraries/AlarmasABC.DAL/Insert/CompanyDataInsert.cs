using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;



namespace AlarmasABC.DAL.Insert
{
    public class CompanyDataInsert:DataAccessBase
    {

        public CompanyDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_COMPANY.ToString();
        }


        private Company _company;
        /// <summary>
        /// Set/Get Comapny Object
        /// </summary>

        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public void addCompanyinfo()
        {
            companyParams _cp = new companyParams(this._company);
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _cp.Param);
            }
            catch (Exception ex)
            {
                throw new Exception(" Company Add " + ex.Message);
            }
            finally
            {
                _cp = null;
            }

        }

    }

    class companyParams
    {
        private Company _company;
        private int _retValue = 0;

        public companyParams(Company _company)
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

        private void build()
        {
            
           NpgsqlParameter[] _params = { 
                                       DataBaseHelper.MakeParam("@comName",    NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._company.CompanyName),
                                       DataBaseHelper.MakeParam("@address",     NpgsqlTypes.NpgsqlDbType.Varchar,     100,    ParameterDirection.Input,       this._company.Address),
                                       DataBaseHelper.MakeParam("@phone",       NpgsqlTypes.NpgsqlDbType.Varchar,     20,     ParameterDirection.Input,       this._company.Phone),
                                       DataBaseHelper.MakeParam("@email",       NpgsqlTypes.NpgsqlDbType.Varchar,     40,     ParameterDirection.Input,       this._company.Email),
                                       DataBaseHelper.MakeParam("@webSite",     NpgsqlTypes.NpgsqlDbType.Varchar,     80,     ParameterDirection.Input,       this._company.WebSite),
                                       DataBaseHelper.MakeParam("@password",    NpgsqlTypes.NpgsqlDbType.Varchar,     150,    ParameterDirection.Input,       this._company.Password),
                                       DataBaseHelper.MakeParam("@retVal",     NpgsqlTypes.NpgsqlDbType.Integer,          4,      ParameterDirection.Output,      _retValue)
                                     };
           this._param = _params;
        }


    }
}

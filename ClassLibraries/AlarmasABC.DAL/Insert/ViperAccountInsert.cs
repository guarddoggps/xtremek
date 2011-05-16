using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class ViperAccountInsert:DataAccessBase
    {
        public ViperAccountInsert()
        {
           Command = StoredProcedure.Name.SP_ADDVIPERACCOUNT.ToString();
        }

        private VAccount _vAccount;

        public VAccount VAccount
        {
            get { return _vAccount; }
            set { _vAccount = value; }
        }

        public void AddViperAccount()
        {
            ViperAccountParams _vaI = new ViperAccountParams(this._vAccount);
            
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _vaI.Param);
            }
            catch (Exception ex)
            {
                throw new Exception(" Viper Account Add " + ex.Message);
            }
            finally
            {
                _vaI = null;
            }

        }

        
    }

    class ViperAccountParams
    {
        private VAccount _vAcc;
        private int _retValue;

        public ViperAccountParams(VAccount _va)
        {
            this._vAcc = _va;
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
                                       


                                       DataBaseHelper.MakeParam("@FNAME",    NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._vAcc.FName),
                                       DataBaseHelper.MakeParam("@LNAME",     NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._vAcc.LName),
                                       DataBaseHelper.MakeParam("@@INI",     NpgsqlTypes.NpgsqlDbType.Varchar,     10,    ParameterDirection.Input,       this._vAcc.Ini),
                                       DataBaseHelper.MakeParam("@COM",       NpgsqlTypes.NpgsqlDbType.Varchar,     100,     ParameterDirection.Input,       this._vAcc.CName),
                                       DataBaseHelper.MakeParam("@SADDRESS",  NpgsqlTypes.NpgsqlDbType.Varchar,     200,     ParameterDirection.Input,       this._vAcc.SAddress),
                                       DataBaseHelper.MakeParam("@APT",     NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._vAcc.Apt),
                                       DataBaseHelper.MakeParam("@CITY",     NpgsqlTypes.NpgsqlDbType.Varchar,     20,     ParameterDirection.Input,       this._vAcc.City),
                                       DataBaseHelper.MakeParam("@STATE",     NpgsqlTypes.NpgsqlDbType.Varchar,     20,     ParameterDirection.Input,       this._vAcc.State),
                                       DataBaseHelper.MakeParam("@ZIP",     NpgsqlTypes.NpgsqlDbType.Varchar,     20,     ParameterDirection.Input,       this._vAcc.Zip),
                                       DataBaseHelper.MakeParam("@COUNTRY",     NpgsqlTypes.NpgsqlDbType.Varchar,  20,     ParameterDirection.Input,       this._vAcc.Country),
                                       DataBaseHelper.MakeParam("@HPHONE",     NpgsqlTypes.NpgsqlDbType.Varchar,   15,     ParameterDirection.Input,       this._vAcc.HPhone),
                                       DataBaseHelper.MakeParam("@OPHONE",     NpgsqlTypes.NpgsqlDbType.Varchar,   15,     ParameterDirection.Input,       this._vAcc.OPhone),
                                       DataBaseHelper.MakeParam("@CPHONE",     NpgsqlTypes.NpgsqlDbType.Varchar,   15,     ParameterDirection.Input,       this._vAcc.CPhone),
                                       DataBaseHelper.MakeParam("@EMAIL",     NpgsqlTypes.NpgsqlDbType.Varchar,    100,     ParameterDirection.Input,       this._vAcc.Email),
                                       DataBaseHelper.MakeParam("@DOB",     NpgsqlTypes.NpgsqlDbType.Timestamp,     30,     ParameterDirection.Input,       this._vAcc.DOB),
                                       DataBaseHelper.MakeParam("@SQUESTION",     NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._vAcc.SecurityQuestion),
                                       DataBaseHelper.MakeParam("@SANS",     NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._vAcc.SecurityAnswer)
                                      
                                     };
           this._param = _params;
        }


    }
}

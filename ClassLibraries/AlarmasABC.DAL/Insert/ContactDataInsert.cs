using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;using Npgsql;
namespace AlarmasABC.DAL.Insert
{
   
    public class ContactDataInsert : DataAccessBase
    {
        public ContactDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_CONTACT_INFO.ToString();
        }

        private Contact _contact;

        public Contact Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        public void addContactInfo()
        {
            ContactParams _cPara = new ContactParams(this._contact);

            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _cPara.Param);
            }
            catch (Exception ex)
            {
                throw new Exception(" Add Image URL " + ex.Message);
            }
            finally
            {
                _cPara = null;
            }

        }


    }

    class ContactParams
    {


        private Contact _cInfo;

        public Contact CInfo
        {
          get { return _cInfo; }
          set { _cInfo = value; }
        }

        public ContactParams(Contact _cData)
        {
            this._cInfo = _cData;
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
                                       


                                       DataBaseHelper.MakeParam("@userName",    NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._cInfo.UserName),
                                       DataBaseHelper.MakeParam("@email",     NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._cInfo.Email),
                                       DataBaseHelper.MakeParam("@department",     NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._cInfo.Department),
                                       DataBaseHelper.MakeParam("@description",     NpgsqlTypes.NpgsqlDbType.Varchar,     500,     ParameterDirection.Input,       this._cInfo.Description)
                                      
                                     };
            this._param = _params;
        }


    }
}

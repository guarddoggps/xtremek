using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Security.Update
{
    public class SchemeInfoUpdate:DataAccessBase
    {

        public SchemeInfoUpdate(string _schemeID, string _schemeName, int _comID,bool _defaultScheme)
        {
			this.SchemeID = _schemeID;
            this.SchemeName = _schemeName;
            this.ComID = _comID;
            this.DefaultScheme = bool.Parse(_defaultScheme.ToString());
           Command = StoredProcedure.Name.SP_UPDATE_SCHEME_INFO.ToString();
        }

        private string _schemeID;

        public string SchemeID
        {
            get { return _schemeID; }
            set { _schemeID = value; }
        }
		
        private string _schemeName;

        public string SchemeName
        {
            get { return _schemeName; }
            set { _schemeName = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        private bool _defaultScheme;

        public bool DefaultScheme
        {
            get { return _defaultScheme; }
            set { _defaultScheme = value; }
        }
        public int saveSchemeInfo()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.StoredProcedure);                
                _ds= _db.Run(base.ConnectionString,returnParam());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            { 
            
            }
            return int.Parse(_ds.Tables[0].Rows[0][0].ToString());
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@schemeID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.SchemeID),
                                        DataBaseHelper.MakeParam("@schemeName", NpgsqlTypes.NpgsqlDbType.Varchar,  50,  ParameterDirection.Input,   this.SchemeName),
                                        DataBaseHelper.MakeParam("@comID", NpgsqlTypes.NpgsqlDbType.Integer,  50,  ParameterDirection.Input,   this.ComID),
                                        DataBaseHelper.MakeParam("@defaultScheme", NpgsqlTypes.NpgsqlDbType.Boolean,  1,  ParameterDirection.Input,   this.DefaultScheme)
                                    };

            return _param;
        }
    }
}

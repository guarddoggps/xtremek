using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Security.Select
{
    public class SchemeInfoSelect:DataAccessBase
    {

        public SchemeInfoSelect(string _schemeID)
        {
            this.SchemeID = _schemeID;
           Command = @"SELECT id,schemeName,defaultScheme FROM tblSecurityScheme WHERE id = :schemeID;";
        }

        private string _schemeID;

        public string SchemeID
        {
            get { return _schemeID; }
            set { _schemeID = value; }
        }

        
            

       
        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void getSchemeInfo()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString,returnParam());                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("schemeID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.SchemeID)
                                    };

            return _param;
        }
    }
}

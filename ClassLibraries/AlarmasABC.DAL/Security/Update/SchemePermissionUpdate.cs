using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Security;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Security.Update
{
    public class SchemePermissionUpdate:DataAccessBase
    {

        public SchemePermissionUpdate(SchemePermission _schemePermission)
        {
            this.SchemePermission = _schemePermission;            
           Command = StoredProcedure.Name.SP_UPDATE_SCHEME_PERMISSION.ToString();
        }

        private SchemePermission _schemePermission;

        public SchemePermission SchemePermission
        {
            get { return _schemePermission; }
            set { _schemePermission = value; }
        }
        
        public int saveSchemePermission()
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
			
			if (_ds.Tables[0].Rows.Count > 0) 
            {
				return int.Parse(_ds.Tables[0].Rows[0][0].ToString());
			} 
			else
			{ 
				return -1;
            }
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@comID", NpgsqlTypes.NpgsqlDbType.Integer,  50,  ParameterDirection.Input,   this.SchemePermission.ComID),
                                        DataBaseHelper.MakeParam("@schemeID", NpgsqlTypes.NpgsqlDbType.Integer,  50,  ParameterDirection.Input,   this.SchemePermission.SchemeID),
                                        DataBaseHelper.MakeParam("@formID",  NpgsqlTypes.NpgsqlDbType.Integer,  50,  ParameterDirection.Input,   this.SchemePermission.FormID),
                                        DataBaseHelper.MakeParam("@fullAccess", NpgsqlTypes.NpgsqlDbType.Smallint,  1,  ParameterDirection.Input,   Convert.ToInt16(this.SchemePermission.FullAccess)),
                                        DataBaseHelper.MakeParam("@delete",  NpgsqlTypes.NpgsqlDbType.Smallint,  1,  ParameterDirection.Input,   Convert.ToInt16(this.SchemePermission.Delete)),
                                        DataBaseHelper.MakeParam("@view	",  NpgsqlTypes.NpgsqlDbType.Smallint,  1,  ParameterDirection.Input,   Convert.ToInt16(this.SchemePermission.View)),
                                        DataBaseHelper.MakeParam("@insert",  NpgsqlTypes.NpgsqlDbType.Smallint,  1,  ParameterDirection.Input,   Convert.ToInt16(this.SchemePermission.Insert)),
                                        DataBaseHelper.MakeParam("@edit",  NpgsqlTypes.NpgsqlDbType.Smallint,  1,  ParameterDirection.Input,   Convert.ToInt16(this.SchemePermission.Edit))
                                    };

            return _param;
        }
    }
}

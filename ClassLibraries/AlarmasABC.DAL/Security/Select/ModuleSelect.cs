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
    public class ModuleSelect:DataAccessBase
    {

        public ModuleSelect(string _schemeID)
        {
            this.SchemeID = _schemeID;
			Command = "SELECT * FROM tblModule;" +
				
					  "SELECT formID,schemeID,fullAccess,delete,view,insert,edit,moduleID,formName" +
					  " FROM tblSchemePermission sp INNER JOIN tblForms f ON f.id = sp.formID" +
					  " WHERE schemeID = :schemeID" +
					  " UNION " +
					  "SELECT id,NULL,0,0,0,0,0,moduleID,formName FROM tblForms WHERE id NOT IN" +
               		  " (SELECT formID FROM tblSchemePermission WHERE schemeID = :schemeID);";

        }

        private string _schemeID;

        public string SchemeID
        {
            get { return _schemeID; }
            set { _schemeID = value; }
        }

        public DataSet getModule()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);                
                _ds= _db.Run(base.ConnectionString,returnParam());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
            return _ds;
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("schemeID", NpgsqlTypes.NpgsqlDbType.Integer,  4, ParameterDirection.Input,   this.SchemeID)
                                    };

            return _param;
        }
    }
}

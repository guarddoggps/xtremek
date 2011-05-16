using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Security.Select
{
    public class SchemeModuleSelect:DataAccessBase
    {

		public SchemeModuleSelect()
       	{
          	Command = @"SELECT * FROM tblModule;" +
				
					  @"SELECT id AS formID,moduleID,formName,1 AS fullAccess,1 AS delete," +
					  @"1 AS view,1 AS insert,1 AS edit FROM tblForms;";
       	}
        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectSchemeModule()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {

            }
        }




    }
}

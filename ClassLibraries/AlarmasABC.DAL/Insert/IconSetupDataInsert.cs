using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data;
using AlarmasABC.Core;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Insert
{
    public class IconSetupDataInsert: DataAccessBase
    {
        public IconSetupDataInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_ICON.ToString();
        }
        private IconSetup _IconSetup;

        public IconSetup IconSetup
        {
            get { return _IconSetup; }
            set { _IconSetup = value; }
        }

        public void AddIconSetup()
        {
            IconSetupInsertParams prm = new IconSetupInsertParams(this._IconSetup);
            try
            {
                DataBaseHelper db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                db.Run(base.ConnectionString,prm.Parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Data Access Error :: "+ ex.Message);
            }
            finally
            {
                prm = null;
            }
        }
    }
    class IconSetupInsertParams
    {
       private NpgsqlParameter[] _parameters;

        public NpgsqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private IconSetup _IconSetup;

        public IconSetupInsertParams(IconSetup Icon)
        {
            this._IconSetup = Icon;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] Parameters = {
                                        DataBaseHelper.MakeParam("@iconName",NpgsqlTypes.NpgsqlDbType.Varchar, 50, ParameterDirection.Input, _IconSetup.IconName ),
                                        DataBaseHelper.MakeParam("@comID",NpgsqlTypes.NpgsqlDbType.Integer, 4,ParameterDirection.Input, _IconSetup.ComID),
                                        };
            this._parameters = Parameters;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

using AlarmasABC.Core;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Delete
{
    public class IconDelete:DataAccessBase
    {
        public IconDelete()
        {
           Command = StoredProcedure.Name.SP_DELETE_ICON.ToString();
        }
        private IconSetup _IconSetup;

        public IconSetup IconSetup
        {
            get { return _IconSetup; }
            set { _IconSetup = value; }
        }   

        public void DeleteIcon()
        {
            makeIconParam _mip = new makeIconParam(this._IconSetup);
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _mip.Param);
            }
            catch (Exception ex)
            {
                throw new Exception(":: " + ex.Message);
            }

            finally
            {
                _mip = null;
            }
        }
    }

    class makeIconParam
    {
        private IconSetup _IconSetup;

        public makeIconParam(IconSetup _IconSetup)
        {
            this._IconSetup = _IconSetup;
            build();
        }
       private NpgsqlParameter[] _param;

        public NpgsqlParameter[] Param
        {
            get { return _param; }
            set { _param = value; }
        }
        public void build()
        {
            NpgsqlParameter[] _params = { 
                                        DataBaseHelper.MakeParam("@iconID", NpgsqlTypes.NpgsqlDbType.Integer, 4, ParameterDirection.Input, _IconSetup.Id)
                                     };

            this.Param = _params;
        }
    }
}

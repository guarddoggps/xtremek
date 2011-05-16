using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;

using AlarmasABC.Core;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class IconSelect: DataAccessBase
    {
        public IconSelect()
        {
            Command = @"SELECT id,iconName,'../Icon/'||iconName||'.png' AS icon FROM tblIcon;";
        }

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }


        public void selectIcon()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
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

        public void IconLoadOnDataList(IList<IconSetup> _ICLists)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(base.ConnectionString);

                // _GLists.Add(new UserGroup(0, ));

                while (_dr.Read())
                {
                    _ICLists.Add(new IconSetup(int.Parse(_dr[0].ToString()),_dr[1].ToString(), _dr[2].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if (_dr != null)
                {
                    _db = null;
                    _dr = null;
                    _ICLists = null;
                }

            }
        }
       


    }
}

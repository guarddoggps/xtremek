using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Security;

namespace AlarmasABC.DAL.Select
{
    public class SecuritySchemeSelect:DataAccessBase
    {
        
        public SecuritySchemeSelect()
        {
				Command = @"SELECT * FROM tblSecurityScheme WHERE comID = :comID;" +
            			  " " +
					  	  @"SELECT * FROM tblSecurityScheme WHERE comID = :comID AND defaultScheme = '1';";
        }


        #region Private variables and properties

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        #endregion

        public void SchemeDropDownList(IList<SecurityScheme> _Scheme)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(GetParams());

                _Scheme.Add(new SecurityScheme(0, "Select Scheme"));

                while (_dr.Read())
                {
                    _Scheme.Add(new SecurityScheme(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _Scheme = null;
                }

            }
        }

       private NpgsqlParameter[] GetParams()
        {
            NpgsqlParameter[] _param = { 
                                    DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                    };

            return _param;
        }

    }
}

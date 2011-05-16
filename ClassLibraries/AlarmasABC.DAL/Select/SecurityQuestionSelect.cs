using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class SecurityQuestionSelect:DataAccessBase
    {
        public SecurityQuestionSelect()
        {
            Command = @"SELECT * FROM tblSecurityQuestion;";
        }


        public void SecurityQuestionDropDownList(IList<SecurityQuestion> _securityQes)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(base.ConnectionString);

                _securityQes.Add(new SecurityQuestion(0, "Select Question"));

                while (_dr.Read())
                {
                    _securityQes.Add(new SecurityQuestion(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" :: " + ex.Message);
            }
            finally
            {
                if(_dr!=null)
                {
                    _db = null;
                    _dr=null;
                    _securityQes = null;
                }
                
            }
        }
        
    }

}

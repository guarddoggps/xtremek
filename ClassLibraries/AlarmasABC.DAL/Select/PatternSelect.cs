using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class PatternSelect:DataAccessBase
    {

        public PatternSelect(Pattern _pattern)
        {
            this.Pattern = _pattern;
			Command = "SELECT unitID,unitName,coalesce(isActivePattern,'0') AS path" +
            		  " FROM tblUnits WHERE patternID = :patternID ORDER BY unitName ASC;";


        }

        private Pattern _Pattern;

        public Pattern Pattern
        {
            get { return _Pattern; }
            set { _Pattern = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void selectPattern()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString, returnParam());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        public void PatternDropDownList(IList<Pattern> _Pattern)
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

                _Pattern.Add(new Pattern(0, "Select Pattern"));

                while (_dr.Read())
                {
                    _Pattern.Add(new Pattern(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _Pattern = null;
                }
                
            }
        }

       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("patternID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._Pattern.ComID)
                                    };

            return _param;
        }


    }
}

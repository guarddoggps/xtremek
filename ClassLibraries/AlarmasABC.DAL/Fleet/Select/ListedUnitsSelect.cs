using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Fleet.Select
{
    public class ListedUnitsSelect:DataAccessBase
    {
        public ListedUnitsSelect()
        {
           Command = StoredProcedure.Name.SP_LOADLISTEDUNITS_FOR_SUPPLIES.ToString();
        }


        private int _patternID;

        public int PatternID
        {
            get { return _patternID; }
            set { _patternID = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }


        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void LoadListedUnitsForSupply(IList<Units> _GLists)
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(returnParam());

                // _GLists.Add(new UserGroup(0,));

                while (_dr.Read())
                {
                    _GLists.Add(new Units(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _GLists = null;
                }

            }
        }


       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@patternID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._patternID),
                                                                                                                             DataBaseHelper.MakeParam("@comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this._comID)
                                    };

            return _param;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Select
{
    public class MainMapData:DataAccessBase
    {
        public MainMapData()
        {
			Command = StoredProcedure.Name.FN_SELECT_MAINMAP_DATA.ToString();
        }

        public MainMapData(bool _UnitType)
        {
			Command = StoredProcedure.Name.FN_SELECT_MAINMAP_DATA_UNITTYPE.ToString();  
        }

        #region Private variables and Properties

            private int _comID;
            public int ComID
            {
                get { return _comID; }
                set { _comID = value; }
            }

            private int _userID;
            public int UserID
            {
                get { return _userID; }
                set { _userID = value; }
            }

            private int _typeID;
            public int TypeID
            {
                get { return _typeID; }
                set { _typeID = value; }
            }

            private IList<MapData> _mapdata;
            public IList<MapData> Mapdata
            {
                get { return _mapdata; }
                set { _mapdata = value; }
            }

        #endregion


        public void getMapData()
        {
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);
            try
            {
                _dr = _db.ExecuteReader(returnParams());

                while (_dr.Read())
                {
                    this._mapdata.Add(new MapData(_dr[0].ToString(), decimal.Parse(_dr[1].ToString()), decimal.Parse(_dr[2].ToString()),
                        _dr[3].ToString(),_dr[4].ToString(),_dr[5].ToString(),_dr[6].ToString(),_dr[7].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" DAL::Select: MainMapData "+ex.Message);
            }

            finally
            {
                _dr=null;
                _db=null;
            }
        }

        public void getTypedMapData()
        {
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.StoredProcedure);
            try
			{
                _dr = _db.ExecuteReader(returnTypedParams());

                while (_dr.Read())
				{
                    this._mapdata.Add(new MapData(_dr[0].ToString(), decimal.Parse(_dr[1].ToString()), decimal.Parse(_dr[2].ToString()),
                        _dr[3].ToString(), _dr[4].ToString(), _dr[5].ToString(), _dr[6].ToString(),_dr[7].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" DAL::Select: MainMapData " + ex.Message);
            }

            finally
            {
                _dr = null;
                _db = null;
            }
        }

       	private NpgsqlParameter[] returnTypedParams()
       	{
      		NpgsqlParameter[] _params = { 
                                         DataBaseHelper.MakeParam("@comID",        NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID),   
                                         DataBaseHelper.MakeParam("@uID",           NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UserID),
                                         DataBaseHelper.MakeParam("@typeID",    NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.TypeID)
                                            
                                         };
            return _params;
      	}

        private NpgsqlParameter[] returnParams()
        {


                NpgsqlParameter[] _params = { 
                                            DataBaseHelper.MakeParam("@comID",     NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID),
                                            DataBaseHelper.MakeParam("@uID",      NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.UserID)
                                         };


                return _params;
        }
    }
}

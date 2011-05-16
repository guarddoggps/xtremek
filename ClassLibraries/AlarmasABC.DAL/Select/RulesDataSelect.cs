using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Tracking;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
    public class RulesDataSelect:DataAccessBase
    {
        public RulesDataSelect()
        {
			Command = @"SELECT rulesID,'Unit Speed Should ' || rulesOperatorName || ' '" + 
					  @" || rulesValue || ' mph'" +
            		  @" AS rules FROM tblRules WHERE comID = :comID;";
        }

        //public RulesDataSelect(int type)
        //{
        //   Command = StoredProcedure.Name.SP_SELECT_UNASSIGN_UNITS_3.ToString();
        //}


        public RulesDataSelect(string geo)
        {
            if(geo == "geo")
            {
                Command = @"SELECT id,name FROM tblGeofence WHERE comID = :comID ORDER BY name ASC;";
            }
            else if(geo =="Units")
            {
				Command = @"SELECT * FROM tblUnitType WHERE comID = :comID;" +
                 		  " " +
						  @"SELECT * FROM tblUnits WHERE comID = :comID AND unitID IN" +
						  @" (SELECT unitID FROM tblUnitUserWise WHERE uID = :uID AND" +
						  @" unitID NOT IN (SELECT unitID FROM tblUnitWiseRules WHERE comID = :comID));";
            }
            else if (geo == "User3")
            {
				Command = @"SELECT * FROM tblUnitType WHERE comID = :comID;" +
                 		  " " +
						  @"SELECT * FROM tblUnits WHERE comID = :comID AND unitID NOT IN" +
						  @" (SELECT unitID FROM tblUnitWiseRules WHERE comID = :comID);";
            }
            else if (geo == "AssignedUnits" || geo == "units")
            {
				Command = @"SELECT tblUnitWiseRules.unitID,unitName FROM tblUnitWiseRules" +
                		  @" JOIN tblUnits ON (tblUnitWiseRules.unitID = tblUnits.unitID)" +
						  @" WHERE tblUnitWiseRules.unitID IN (SELECT unitID FROM tblUnitWiseRules" +
						  @" WHERE rulesID = :rulesID);";
            }
            else if (geo == "unitInfo")
            {
				Command = @"SELECT email,subject,description,isActive,geofenceID FROM tblUnitWiseRules" +
                		  @" WHERE unitID = :unitID;";
            }


        }

        private RulesData _rulesData;

        public RulesData RulesData
        {
            get { return _rulesData; }
            set { _rulesData = value; }
        }

        
        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private int _uID;

        public int UID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private int _rulesID;

        public int RulesID
        {
            get { return _rulesID; }
            set { _rulesID = value; }
        }

        private int _unitID;

        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }


        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void GetRulesData()
        {
            
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::RulesDataSelect::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

        public void GetUnit()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParam());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::UnitDataSelect::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

        public void GetAssignedUnits()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParam("AssignedUnits"));
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::UnitDataSelect::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

        public void GetUnit3()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParams());
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::UnitDataSelect::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

        public void UnitRuleInfo()
        {
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                this.Ds = _db.Run(base.ConnectionString, ReturnParam(1));
            }
            catch (Exception ex)
            {
                throw new Exception("DLL::UnitDataSelect::" + ex.Message);
            }
            finally
            {
                _db = null;
            }
        }

        public void RulesDropDownList(IList<RulesData> _rules, string comID)
        {
            this.ComID = int.Parse(comID);
           NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(ReturnParams());

                _rules.Add(new RulesData(0, "Select Rule"));

                while (_dr.Read())
                {
                    _rules.Add(new RulesData(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _rules = null;
                }

            }
        }

        public void GeoDropDownList(IList<Geofence> _geo, string comID)
        {
            this.ComID = int.Parse(comID);
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(ReturnParams());

                _geo.Add(new Geofence(0, "Select Geofence"));

                while (_dr.Read())
                {
                    _geo.Add(new Geofence(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _geo = null;
                }

            }
        }

        public void UnitsDropDownList(IList<Units> _units, string rulesID)
        {
            this.RulesID = int.Parse(rulesID);
            NpgsqlDataReader _dr = null;
            DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
            try
            {
                _dr = _db.ExecuteReader(ReturnParams("units"));

                _units.Add(new Units(0, "Select Units"));

                while (_dr.Read())
                {
                    _units.Add(new Units(int.Parse(_dr[0].ToString()), _dr[1].ToString()));
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
                    _units = null;
                }

            }
        }

       private NpgsqlParameter[] ReturnParams()
        {
            NpgsqlParameter[] _params = {
                                        DataBaseHelper.MakeParam("comID", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.ComID)
                                     };
            return _params;
        }

        private NpgsqlParameter[] ReturnParams(string type)
        {
            NpgsqlParameter[] _params = {
                                        DataBaseHelper.MakeParam("rulesID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.RulesID)
                                     };
            return _params;
        }


        private NpgsqlParameter[] ReturnParam()
        {
            NpgsqlParameter[] _params = {
                                        DataBaseHelper.MakeParam("comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.RulesData.ComID),
                                        DataBaseHelper.MakeParam("uID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.RulesData.UnitID)
                                     };
            return _params;
        }

        private NpgsqlParameter[] ReturnParam(string type)
        {
            NpgsqlParameter[] _params = {
                                        DataBaseHelper.MakeParam("rulesID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.RulesData.RulesID)
                                     };
            return _params;
        }

        private NpgsqlParameter[] ReturnParam(int type)
        {
            NpgsqlParameter[] _params = {
                                        DataBaseHelper.MakeParam("uID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input,   this.RulesData.UnitID)
                                     };
            return _params;
        }
    }
}

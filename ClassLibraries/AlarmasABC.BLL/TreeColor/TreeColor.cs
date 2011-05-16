using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Queries;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.TreeColor
{
    public class TreeColor:IAlopekBusinessLogic
    {
        #region Private variables and Properties 

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        private string _unitID;

        public string UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }
        private string _comID;

        public string ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private string _desiredString;

        public string DesiredString
        {
            get { return _desiredString; }
            set { _desiredString = value; }
        }
        #endregion

        public void invoke()
        {
           GetColor();
        }

        private void GetColor()
        {
            try
            {
                if (SpeedRule())
                {
                    this.DesiredString = "Red";
                    return;
                }
                else if (GeofenceRule())
                {
                    this.DesiredString = "Red";
                    return;
                }
                else
                    TimeDuration();
                
            }
            catch (Exception ex)
            { 
                throw new Exception("GetColor::"+ex.Message);
            }
        }

        private bool SpeedRule()
        {
            try
            {
                DataSet _ds = new DataSet();
                string _strSQL = "select RulesValue from tblrules r inner join tblunitwiserules ur on r.RulesID=ur.RulesID where UnitID=" + this.UnitID.ToString() + " and comid=" + this.ComID.ToString() + "; ";
                _strSQL += "select top 1 velocity from tblGprs where deviceID=(select deviceID from tblunits where unitid=" + this.UnitID.ToString() + " and comid=" + this.ComID.ToString() + ") order by rectime desc ";

                ExecuteSQL _execute = new ExecuteSQL();
                _ds = _execute.getDataSet(_strSQL);

                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        if (int.Parse(_ds.Tables[0].Rows[0]["RulesValue"].ToString()) < int.Parse(_ds.Tables[1].Rows[0]["velocity"].ToString()))
                        {
                            return true;
                        }
                    }
                }
                

            }
            catch (Exception ex)
            {
                throw new Exception("GetColor::SpeedRule::" + ex.Message);
            }

            return false;
        }

        private bool GeofenceRule()
        {
            try
            {
                OutSideMail_Status_Select mailStatus = new OutSideMail_Status_Select();
                mailStatus.UnitID = int.Parse(this.UnitID);
                mailStatus.getMailOutSideStatus();
                this._ds = mailStatus.Ds;
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    bool status = bool.Parse(_ds.Tables[0].Rows[0]["STATUS"].ToString());

                    if (status)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


            }
            catch (Exception ex)
            {
				throw new Exception("TreeColor::GeofenceRule(): " + ex.Message);
            }
            return false;
        }

        private void TimeDuration()
        {
            TimeSpan _ts = new TimeSpan();
            _ts = this.EndDate - this.StartDate;
            int _days = int.Parse(_ts.Days.ToString());
            int _hours = int.Parse(_ts.Hours.ToString());

            if (_days < 1)
            {
                if (_hours < 2)
                {
                    this.DesiredString = "Green";
                }
                else
                {
                    this.DesiredString = "Gray";
                }
            }
            else
            {
                this.DesiredString = "Gray";
            }
        }
    }
}

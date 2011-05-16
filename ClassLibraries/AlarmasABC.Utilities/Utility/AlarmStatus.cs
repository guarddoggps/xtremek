using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Timers;
using Npgsql;
using System.IO;
using System.Collections;

using AlarmasABC.DAL;


namespace AlarmasABC.Utilities
{

    public partial class Service1 //: ServiceBase
    {

        string warningMsg = "";
        public Service1()
        {

        }

        public bool alarmStatus(string unitID, string speed, string recTimeRevised, string comID, double lat, double lng)
        {
            string rulesValue, rulesOP;
            DataBaseHelper db = new DataBaseHelper();
            DataSet ds = new DataSet();
            DataSet dsRules = new DataSet();
            string strSQL = "select  isnull(rulesID,0) as rulesID from tblunitwiserules where unitid=" + unitID + " and isActive=1;";
            strSQL += " select  isnull(geofenceid,0) as geofenceid from tblunitwiserules where unitid=" + unitID + " and isGeofenceActive=1";
            ds = db.Run(strSQL);
            //db.Close();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strSQL = "select f.rulesfor,r.rulesOperator,r.RulesValue from tblrules r inner join tblrulesfor f on f.rulesforid=r.rulesforid where rulesid=" + ds.Tables[0].Rows[i]["rulesID"].ToString() + "";
                dsRules = db.Run(strSQL);
                //db.Close();

                for (int j = 0; j < dsRules.Tables[0].Rows.Count; j++)
                {
                    rulesValue = dsRules.Tables[0].Rows[j]["RulesValue"].ToString();
                    rulesOP = dsRules.Tables[0].Rows[j]["rulesOperator"].ToString();

                    if (dsRules.Tables[0].Rows[j]["rulesfor"].ToString() == "Time")
                    {
                        if (chkUnitTimeStatus(recTimeRevised, unitID, comID, rulesValue))
                        {
                            return true;
                        }
                    }


                    if (dsRules.Tables[0].Rows[j]["rulesfor"].ToString() == "Speed")
                    {

                        if (chkUnitSpeedStatus(unitID, speed, rulesOP, comID, rulesValue))
                        {
                            return true;
                        }
                    }
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    if (ds.Tables[1].Rows[i]["geofenceid"].ToString() != "")
                    {
                        if (chkGeofence.isViolate(unitID, comID, lat, lng, ds.Tables[1].Rows[i]["geofenceid"].ToString()))
                        {
                            return true;
                        }
                    }
            return false;

        }

        private bool chkUnitSpeedStatus(string unitID, string speed, string RulesOp, string comID, string rulesValue)
        {


            switch (RulesOp)
            {
                case "<":
                    if (Convert.ToDouble(speed) > Convert.ToDouble(rulesValue))
                    {
                        warningMsg += " Unit is Almost Stopped   ";
                        return true;
                    }
                    break;
                case ">":
                    if (Convert.ToDouble(speed) < Convert.ToDouble(rulesValue))
                    {
                        warningMsg += "Unit exceeds its speed limit  ";
                        return true;
                    }
                    break;

            }


            return false;


        }

        private bool chkUnitTimeStatus(string recTimeRevised, string unitID, string comID, string rulesValue)
        {

            DateTime dt1;
            bool existsWarning = false;
            dt1 = Convert.ToDateTime(recTimeRevised);
            DateTime dt2 = new DateTime();
            dt2 = System.DateTime.Now;
            TimeSpan ts = dt2 - dt1;
            existsWarning = chkTime(ts, rulesValue);
            if (existsWarning)
            {
                return true;
            }

            return false;
        }

        private bool chkTime(TimeSpan ts, string timeLimit)
        {
            int year, month, day, hour, minute;
            int hourLimit, minuteLimit;

            year = System.DateTime.Now.Year;
            month = System.DateTime.Now.Month;
            day = System.DateTime.Now.Day;
            hour = ts.Hours;
            minute = ts.Minutes;

            hourLimit = Convert.ToInt32(timeLimit.Substring(0, timeLimit.IndexOf("-")));
            timeLimit = timeLimit.Remove(0, hourLimit.ToString().Length + 1);
            minuteLimit = Convert.ToInt32(timeLimit.Substring(0, timeLimit.IndexOf("-")));

            if (ts.Days > 0)
            {
                warningMsg += "Stopped for " + ts.Days + " days  ";
                return true;
            }
            else
            {
                if (hour > hourLimit)
                {
                    warningMsg += " Unit is Stopped for " + hour + " hour  ";
                    return true;
                }
                else if (hour <= hourLimit)
                {
                    if (minute > minuteLimit)
                    {
                        warningMsg += " Unit is Stopped for " + minute + " Minutes  ";
                        return true;
                    }
                }
            }
            return false;

        }
        private void mail2User(string unitID, string unitName, string city, string state, string country, string lat, string lng, string comID, string Msg)
        {

            /*try
            {
                string strSQL = "select UserName,Email from tbluser u inner join tblunituserwise us on us.uid=u.uid and unitid=" + unitID + " and comID=" + comID + ;

                string subject = "The Unit " + unitName + " (" + unitID + ") has delivered a message Alarm in the system!";
                string AlarmMsg =  + "\n";
                AlarmMsg += "Alarm Msg :" + Msg + "\n";
                AlarmMsg += "The location of the Unit is: \n";
                AlarmMsg += "City :" + city + "\n";
                AlarmMsg += "State :" + state + "\n";
                AlarmMsg += "Country :" + country + "\n";
                AlarmMsg += "Latitude :" + lat + "\n";
                AlarmMsg += "Longitude :" + lng;

                Database db = new Database();
                DataSet ds = new DataSet();
                ds = db.getDataSet(strSQL);
                db.Close();
                ArrayList recpnt = new ArrayList();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    recpnt.Add(ds.Tables[0].Rows[i]["Email"].ToString());
                }


                if (recpnt.Count < 1)
                    recpnt.Add("admin@alopek.us");



            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }*/

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Update;


namespace AlarmasABC.BLL.TreeColor
{
    public class ProcessTreeColor
    {
        public ProcessTreeColor()
        {
 
        }

        #region Properties
        string warningMsg =;

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

        #region fuctions
            /// <summary>
            /// Sets Color of the Unit
            /// </summary>
            /// <param name="this.StartDate"></param>
            /// <param name="this.EndDate"></param>
            /// <returns></returns>
           public void invoke()
            {
                
               
                string color = ;
                string speed=;
                string recTime=;
                string comID = this.ComID;
                string dID = this.UnitID;
                int dayCount = 0;
                int hourCount=0;
                double lat=0,lng=0;

                DateTime sDate = new DateTime();
                DateTime eDate = new DateTime();

                sDate = this.StartDate;
                eDate = this.EndDate;        

                TimeSpan ts = eDate - sDate;
                dayCount = ts.Days;
                int timeDuration  = ts.Minutes;
                hourCount = ts.Hours;
               
                try
                {
                    
                    DataSet ds = new DataSet();
                    
                    #region Old
                    //string strSQL = "select distinct Cast(velocity*0.621 as int) as velocity,recTimeRevised,recTime,lat,long  from tblGPRS where recTime=(select max(recTime)from tblGPRS where deviceID =(select deviceid from tblunits where unitid=" + unitID + ")) and deviceID =(select deviceid from tblunits where unitid=" + unitID + ")";
                    //ds = db.getDataSet(strSQL);            
                    #endregion

                    #region SP_SELECT_TREEcOLOR_1
                    Units _units= new Units();
                    _units.UnitID = int.Parse(this.UnitID); 
                    ProcessTreeColor1 _proTreeColor1 = new ProcessTreeColor1();
                    _proTreeColor1.Units = _units;
                    _proTreeColor1.invoke();
                    ds = _proTreeColor1.Ds;
                    #endregion

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        speed = ds.Tables[0].Rows[0]["velocity"].ToString();
                        recTime = ds.Tables[0].Rows[0]["recTime"].ToString();
                        lat = double.Parse(ds.Tables[0].Rows[0]["lat"].ToString());
                        lng = double.Parse(ds.Tables[0].Rows[0]["long"].ToString());
                    }
                    //Service1 sa = new Service1();

                    DateTime dtNow = Convert.ToDateTime("01/01/2000");
                    long nrecTime = long.Parse(recTime);
                    dtNow = dtNow.AddSeconds(double.Parse(nrecTime.ToString()));
                    recTime = dtNow.ToString();

                    if (alarmStatus(dID, speed, recTime, comID,lat,lng))
                    {
                        color = "Red";

                    }
                    else
                    {
                        if (dayCount ==0)
                        {
                            if (hourCount < 2)
                            {
                                color = "Green";
                            }
                            else
                                color = "Gray";
                        }
                        else
                            color = "Gray";
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("ProcessTreeColor::"+ex.Message);
                }        

                this.DesiredString = color;

            }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="speed"></param>
        /// <param name="recTimeRevised"></param>
        /// <param name="comID"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
           public bool alarmStatus(string unitID, string speed, string recTimeRevised, string comID, double lat, double lng)
           {
               string rulesValue, rulesOP;
               //Database db = new Database();
               DataSet ds = new DataSet();
               DataSet dsRules = new DataSet();


               #region Old
               //string strSQL = "select  isnull(rulesID,0) as rulesID from tblunitwiserules where unitid=" + unitID + " and isActive=1;";
               //strSQL += " select  isnull(geofenceid,0) as geofenceid from tblunitwiserules where unitid=" + unitID + " and isGeofenceActive=1";
               //ds = db.getDataSet(strSQL);
               #endregion

               #region SP_SELECT_TREEcOLOR_2
               Units _units = new Units();
               _units.UnitID = int.Parse(this.UnitID);
               ProcessTreeColor2 _proTreeColor2 = new ProcessTreeColor2();
               _proTreeColor2.Units = _units;
               _proTreeColor2.invoke();
               ds = _proTreeColor2.Ds;

               RulesData _rules = new RulesData();
               ProcessTreeColor3 _proTreeColor3 = new ProcessTreeColor3();
               #endregion

               //db.Close();
               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
               {
                   #region old
                   //strSQL = "select f.rulesfor,r.rulesOperator,r.RulesValue from tblrules r inner join tblrulesfor f on f.rulesforid=r.rulesforid where rulesid=" + ds.Tables[0].Rows[i]["rulesID"].ToString() + ;
                   //dsRules = db.getDataSet(strSQL);
                   #endregion


                   #region SP_SELECT_TREEcOLOR_3
                   _rules.RulesID = int.Parse(ds.Tables[0].Rows[i]["rulesID"].ToString());
                   _proTreeColor3.Rules = _rules;
                   _proTreeColor3.invoke();
                   dsRules = _proTreeColor3.Ds;
                   #endregion

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
                       if (ds.Tables[1].Rows[i]["geofenceid"].ToString() != )
                       {
                           if (isViolate(unitID, comID, lat, lng, ds.Tables[1].Rows[i]["geofenceid"].ToString()))
                           {
                               return true;
                           }
                       }
               return false;

           }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="recTimeRevised"></param>
        /// <param name="unitID"></param>
        /// <param name="comID"></param>
        /// <param name="rulesValue"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="timeLimit"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="speed"></param>
        /// <param name="RulesOp"></param>
        /// <param name="comID"></param>
        /// <param name="rulesValue"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="comID"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="geofenceID"></param>
        /// <returns></returns>
           private bool isViolate(string unitID, string comID, double lat, double lng, string geofenceID)
           {
               double _distance;
               double _radius = 0;
               double _Lat1 = 0, _Lng1 = 0, _Lat2 = 0, _Lng2 = 0;

               //string _strSQL = ;
               #region old
               //_strSQL = "select centerLat,centerLng,radius from tblGeofence where comID=" + comID + " and ID=" + geofenceID + ;
               #endregion

               

               try
               {
                   
                   //Database _db = new Database();
                   DataSet _ds = new DataSet();
                   //Ds = _db.getDataSet(_strSQL);

                   #region SP_SELECT_TREEcOLOR_4
                   Geofence _geofence = new Geofence();
                   _geofence.ComID = int.Parse(comID);
                   _geofence.Id = int.Parse(geofenceID);
                   ProcessTreeColor4 _proTreeColor4 = new ProcessTreeColor4();
                   _proTreeColor4.Geofence = _geofence;
                   _proTreeColor4.invoke();
                   _ds = _proTreeColor4.Ds;
                   #endregion

                   if (_ds.Tables[0].Rows.Count > 0)
                   {
                       _Lat1 = Convert.ToDouble(_ds.Tables[0].Rows[0]["centerLat"].ToString());
                       _Lng1 = double.Parse(_ds.Tables[0].Rows[0]["centerLng"].ToString());
                       _radius = double.Parse(_ds.Tables[0].Rows[0]["radius"].ToString());
                       _Lat2 = lat;
                       _Lng2 = lng;

                       _distance = CalcDistance1(_Lat1, _Lng1, _Lat2, _Lng2);
                       if (_distance > _radius)
                       {
                           return true;
                       }
                   }


               }
               catch (Exception ex)
               {
                   throw new Exception("ProcessTreeColor::isViolate::" + ex.Message);
               }

               return false;
           }
           /// <summary>
           /// Calculate the distance between two geocodes. Defaults to using Miles.
           /// </summary>
           private  double CalcDistance1(double lat1, double lng1, double lat2, double lng2)
           {
               return CalcDistance2(lat1, lng1, lat2, lng2, GeoCodeCalcMeasurement.Kilometers);
           }
           public enum GeoCodeCalcMeasurement : int
           {
               Miles = 0,
               Kilometers = 1
           }
           public const double EarthRadiusInMiles = 3956.0;// Earth's Radius in Miles
           public const double EarthRadiusInKilometers = 6367.0; //Earth's Radius in Kilometers

           /// <summary>
           /// Calculate the distance between two geocodes.
           /// </summary>
           public  double CalcDistance2(double lat1, double lng1, double lat2, double lng2, GeoCodeCalcMeasurement m)
           {
               double radius = EarthRadiusInKilometers;
               if (m == GeoCodeCalcMeasurement.Miles) { radius = EarthRadiusInMiles; }
               return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * Math.Pow(Math.Sin((DiffRadian(lng1, lng2)) / 2.0), 2.0)))));
           }
           public  double DiffRadian(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }
           public static double ToRadian(double val) { return val * (Math.PI / 180); }   
        #endregion
    }
}

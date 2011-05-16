using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Utilities
/// </summary> 
/// 

namespace AlarmasABC.Utilities
{
    public class Utilities
    {
        public Utilities()
        {

        }
        public static string colorCode(DateTime dtT, DateTime dtF)
        {
            string retSrt;
            int dayF, dayT, MonthF, MonthT, YearF, YearT;
            int HourF, HourT, MinF, MinT;

            YearF = dtF.Year;
            YearT = dtT.Year;
            MonthF = dtF.Month;
            MonthT = dtT.Month;
            dayF = dtF.Day;
            dayT = dtT.Day;
            HourF = dtF.Hour;
            HourT = dtT.Hour;
            MinF = dtF.Minute;
            MinT = dtT.Minute;
            if (YearF == YearT)
            {
                if (MonthF > MonthT)
                {
                    retSrt = "Red";
                }
                else
                {
                    if ((dayF - dayT) == 1)
                    {
                        HourF = HourF + 24;
                        MinF = MinF + 60;
                        if ((HourF - HourT) >= 2)
                        {
                            retSrt = "Red";
                        }
                        else
                        {
                            if ((MinF - MinT) >= 5)
                            {
                                retSrt = "Black";
                            }
                            else
                                retSrt = "Green";
                        }
                    }
                    else if ((dayF - dayT) == 0)
                    {
                        if ((HourF - HourT) >= 2)
                        {
                            retSrt = "Red";
                        }
                        else
                        {
                            if ((MinF - MinT) >= 5)
                            {
                                retSrt = "Black";
                            }
                            else
                                retSrt = "Green";
                        }
                    }
                    else
                        retSrt = "Red";

                }

            }
            else
            {
                retSrt = "Red";
            }
            return retSrt;
        }
    }

}

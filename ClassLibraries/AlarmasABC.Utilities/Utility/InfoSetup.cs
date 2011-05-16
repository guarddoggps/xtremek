using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Npgsql;

using AlarmasABC.DAL;

/// <summary>
/// This Class is used to Add New Company, Vehicle Setup and Create New User
/// </summary>
/// 

namespace AlarmasABC.Utilities
{
    public class infoSetup
    {
        public infoSetup()
        {
        }

        public static int saveComInfo(string comName, string address, string phone, string email, string web, string password)
        {
            int result;
            DataBaseHelper db = new DataBaseHelper();
            NpgsqlParameter[] param ={
                        DataBaseHelper.MakeParam("@comName",NpgsqlTypes.NpgsqlDbType.Varchar,70, ParameterDirection.Input, comName),
                        DataBaseHelper.MakeParam("@address",NpgsqlTypes.NpgsqlDbType.Varchar,100, ParameterDirection.Input, address),
                        DataBaseHelper.MakeParam("@phone",NpgsqlTypes.NpgsqlDbType.Varchar,20, ParameterDirection.Input, phone),
                        DataBaseHelper.MakeParam("@email",NpgsqlTypes.NpgsqlDbType.Varchar,40, ParameterDirection.Input, email),
                        DataBaseHelper.MakeParam("@website",NpgsqlTypes.NpgsqlDbType.Varchar,20, ParameterDirection.Input, web),
                        DataBaseHelper.MakeParam("@pass",NpgsqlTypes.NpgsqlDbType.Varchar,70, ParameterDirection.Input, password),
                        DataBaseHelper.MakeParam("@retVal",NpgsqlTypes.NpgsqlDbType.Integer,1, ParameterDirection.Output, 0)
                        };

            db.Run("createCompany", param);
            //db.Close();
            if ((int)param[6].Value == 0)
                result = 0;
            else if ((int)param[6].Value > 0)
            {
                result = (int)param[6].Value;
            }
            else
                result = -1;
            return result;

        }

        public static int saveComInfo(string comName, string address, string phone, string email, string web)
        {
            int result;
            DataBaseHelper db = new DataBaseHelper();
            NpgsqlParameter[] param ={
                        DataBaseHelper.MakeParam("@comName",NpgsqlTypes.NpgsqlDbType.Varchar,70, ParameterDirection.Input, comName),
                        DataBaseHelper.MakeParam("@address",NpgsqlTypes.NpgsqlDbType.Varchar,100,ParameterDirection.Input, address),
                        DataBaseHelper.MakeParam("@phone",NpgsqlTypes.NpgsqlDbType.Varchar,20, ParameterDirection.Input, phone),
                        DataBaseHelper.MakeParam("@email",NpgsqlTypes.NpgsqlDbType.Varchar,40, ParameterDirection.Input, email),
                        DataBaseHelper.MakeParam("@website",NpgsqlTypes.NpgsqlDbType.Varchar,20, ParameterDirection.Input, web),
                        DataBaseHelper.MakeParam("@pass",NpgsqlTypes.NpgsqlDbType.Varchar,70, ParameterDirection.Input, ""),
                        DataBaseHelper.MakeParam("@retVal",NpgsqlTypes.NpgsqlDbType.Integer,1, ParameterDirection.Output, 0)
                        };

            db.Run("createCompany", param);
            //db.Close();
            if ((int)param[6].Value == 0)
                result = 0;
            else if ((int)param[6].Value == 1)
                result = 1;
            else
                result = (int)param[6].Value;
            return result;

        }

        public static int saveVehicleInfo()
        {
            return 0;
        }
        public static int saveUserInfo()
        {
            return 0;
        }
        public static bool saveTimeZone(string location, string utcVal)
        {
            int retVal = 0;
            string strInsertCommand = "insert into tblrptTimeZone(rptLocation,tzValue)";
            strInsertCommand += "values(";
            strInsertCommand += "'" + location + "',";
            strInsertCommand += "'" + utcVal + "')";

            try
            {
                DataBaseHelper db = new DataBaseHelper();
                retVal = db.ExecuteNonQuery(strInsertCommand, CommandType.StoredProcedure);

            }
            catch (NpgsqlException ex)
            {
                ex.Message.ToString();
            }
			
			if (retVal >= 1)
				return true;
			else 
				return false;
        }
    }

}

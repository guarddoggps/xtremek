using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;using Npgsql;
using AlarmasABC.DAL;

/// <summary>
/// Summary description for FormPermission
/// </summary>
/// 
namespace AlarmasABC.Utilities
{

    public static class FormPermission
    {
        //public FormPermission()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        public static DataSet ds = new DataSet();

        //public struct PermissionInfo
        //{
        //    public bool delete;
        //    public bool view;
        //    public bool edit;
        //    public bool insert;
        //    public int formIDs; 
        //}
        public static void LoadPermission(string userID)
        {
            DataBaseHelper db = new DataBaseHelper();


            //PermissionInfo []  userPermission = new PermissionInfo[20];
            //int[] formIDs;
            //formIDs = new int[20];

            try
            {


                int i = 0;

				string sql = @"SELECT * FROMtblSchemePermission JOIN tblForms" +
                 			 @" ON(tblSchemePermission.formID = tblForms.id)";
				sql += @" WHERE schemeID = (SELECT schemeID FROM tblUserWiseScheme WHERE userID =" 
                	+ userID.ToString() + ");";

                ds = db.Run(sql);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow row in ds.Tables[0].Rows)
                //    {
                //        userPermission[i].delete =  (bool)row["delete"];
                //        userPermission[i].view = (bool)row["view"];
                //        userPermission[i].insert = (bool)row["insert"];
                //        userPermission[i].edit = (bool)row["Edit"];
                //        userPermission[i].formIDs = (int)row["formID"];

                //        i++;
                //    }
                //}

            }
            catch (Exception ex)
            {
                //return formIDs;
            }

            finally
            {
            }

            //return userPermission;

        }
    }
}
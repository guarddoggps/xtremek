
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;

using AlarmasABC.DAL.Misc;

namespace XtremeK
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// If the company ID is not valid,
			// we redirect to the login page
			if (Global.ComID == Global.IDNotSet) 
			{
				Response.Redirect("Login.aspx");
			}
		}
	}
}

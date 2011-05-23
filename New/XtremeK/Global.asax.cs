
using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace XtremeK
{
	public class Global : System.Web.HttpApplication
	{
		public static readonly int IDNotSet = -1;
		
		public static readonly string SESSION_COMID 		= 	"comID";
		public static readonly string SESSION_USERID 	= 	"userID";
		public static readonly string SESSION_USERNAME 	= 	"userName";
		
		public static int GetIntSessionParam(string param)
		{
			if (HttpContext.Current.Session[param] == null)
				return IDNotSet;
			else
				return int.Parse(HttpContext.Current.Session[param].ToString());
		}
		
		public static void SetIntSessionParam(string param, int val)
		{
			HttpContext.Current.Session[param] = val;
		}
		
		public static int ComID
		{
			get { return GetIntSessionParam(SESSION_COMID); }
			set { SetIntSessionParam(SESSION_COMID, value); }
		}
		
		public static int UserID
		{
			get { return GetIntSessionParam(SESSION_USERID); }
			set { SetIntSessionParam(SESSION_USERID, value); }
		}
		
		public static string UserName
		{
			get { return HttpContext.Current.Session[SESSION_USERNAME].ToString(); }
			set { HttpContext.Current.Session[SESSION_USERNAME] = value; }
		}
		
		public static string AppName
		{
			get { return ConfigurationManager.AppSettings["appName"].ToString(); }
		}
		
		public static string CompanyName
		{
			get { return ConfigurationManager.AppSettings["companyName"].ToString(); }
		}
		
		public static string LogoImage
		{
			get { return ConfigurationManager.AppSettings["logo"].ToString(); }
		}
		
		public static string LogoUrl
		{
			get { return ConfigurationManager.AppSettings["logoUrl"].ToString(); }
		}
		
		protected virtual void Application_Start(object sender, EventArgs e)
		{
		}
		
		protected virtual void Session_Start(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_BeginRequest(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_EndRequest(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_Error(object sender, EventArgs e)
		{
		}
		
		protected virtual void Session_End(object sender, EventArgs e)
		{
		}
		
		protected virtual void Application_End(object sender, EventArgs e)
		{
		}
	}
}

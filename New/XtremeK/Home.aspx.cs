
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using AlarmasABC.DAL.Misc;


namespace XtremeK
{
	public partial class Home : System.Web.UI.Page
	{
		private string script;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//Page.Header.Title = Global.AppName + " Tracking System";
				logo.ImageUrl = "../Images/Logos/" + Global.LogoImage + ".png";
				logo.ToolTip = Global.LogoUrl;
				
				loadMarkers();
			}
		}
		
		protected void Logo_Click(object sender, EventArgs e)
		{
			if (Global.LogoUrl.Length > 0) 
			{
				Response.Redirect(Global.LogoUrl);
			}
		}
		
	 	protected void btn_Click(object sender, EventArgs e)
		{
			Console.WriteLine("Button Click!");
		}
		
        protected void OnAjaxUpdate(object sender, ToolTipUpdateEventArgs args)
        {
            //this.UpdateToolTip(args.Value, args.UpdatePanel);
        }
		
        private void UpdateToolTip(string elementID, UpdatePanel panel)
        {
			
        }
		
		private void scriptAdd(string line)
		{
			script += line + "\n";
		}
		
		private void loadMarkers() 
		{
			ExecuteSQL exec = new ExecuteSQL();
			DataTable dt = new DataTable();
			
			string deviceID = "1113";
			dt = exec.getDataTable("select * from unitreports where deviceID = " + deviceID + ";");
			
			script = "";
			
			/*str += "var pos =  \n";
			str += "var options = { zoom: 6, center: pos, mapTypeId: google.maps.MapTypeId.HYBRID }; \n";
			str += "map = new google.maps.Map(document.getElementById('Map'), options ); \n";
			str += "map.setZoom(16); \n";*/
			
			scriptAdd("options = {");
			scriptAdd("  zoom: 6, ");
			scriptAdd("  center: new google.maps.LatLng(28.99085069, -82.09594444), ");
			scriptAdd("  mapTypeId: google.maps.MapTypeId.HYBRID ");
			scriptAdd("};");
			scriptAdd("map = new google.maps.Map(document.getElementById('Map'), options);");
			scriptAdd("");
			
			if (dt.Rows.Count > 0) 
			{
				string lat = "";
				string lon = "";
				
				for (int i = 0; i < dt.Rows.Count; i++) 
				{
					lat = dt.Rows[i]["lat"].ToString();
					lon = dt.Rows[i]["lon"].ToString();
					
					scriptAdd("markers[" + i + "] = new google.maps.Marker({");
					scriptAdd("  position: new google.maps.LatLng(" + lat + "," + lon + ")");
					scriptAdd("});");
					scriptAdd("markers[" + i + "].setMap(map)");
					scriptAdd("");
				}
					
				scriptAdd("map.setCenter(new google.maps.LatLng(" + lat + "," + lon + "));");
				scriptAdd("map.setZoom(16);");
				scriptAdd("");
			}
			
			
            ClientScript.RegisterStartupScript(this.GetType(), "marker", script, true);
		}
	}
	
}

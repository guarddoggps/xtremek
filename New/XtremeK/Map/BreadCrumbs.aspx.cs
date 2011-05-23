
using System;
using System.Data;
using System.Web;
using System.Web.UI;

using AlarmasABC.DAL.Misc;

namespace XtremeK
{
	public partial class BreadCrumbs : System.Web.UI.Page
	{
		private string script;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{				
				if (Request.QueryString["deviceID"] == null) 
				{
					Console.WriteLine("No Device ID!");
				}
				else
				{
					loadMarkers();
				}
			}
		}
		
		private void scriptAdd(string line)
		{
			script += line + "\n";
		}
		
		private void loadMarkers() 
		{
			ExecuteSQL exec = new ExecuteSQL();
			DataTable dt = new DataTable();
			
			string deviceID = Request.QueryString["deviceID"].ToString();
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


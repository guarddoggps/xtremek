using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlarmasABC.DAL.Queries;

public partial class Tracking_Alerts : System.Web.UI.Page
{
	private const string dateFormat = "MM/dd/yyyy";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["unitID"] != null)
        {
            string unitID = Request.QueryString["unitID"].ToString();
			if (!IsPostBack) {
   				_alertDate.SelectedDate = DateTime.Now;
	            GetAlerts(DateTime.Now.ToString(dateFormat));
			}
        }

    }

    private void LoadAlerts(string unitID)
    {
        try
        {
            string _strSQL = "SELECT alertType,alertMessage,alertTime FROM tblAlert WHERE comID=" + 
                             Session["trkCompany"].ToString() + " AND unitID = " + unitID + 
							 " ORDER BY alertTime DESC;";

            
            ExecuteSQL _executeSQL = new ExecuteSQL();
            DataSet _ds = _executeSQL.getDataSet(_strSQL);

			Session["_ds"] = _ds;
            _grdAlert.DataSource = _ds;
            _grdAlert.DataBind();
        }
        catch (Exception ex)
        { 
        
        }
    }

	protected void GetAlerts(string date)
	{
        try
        {
            string unitID = Request.QueryString["unitID"].ToString();
            string _strSQL = "SELECT alertType,alertMessage,alertTime FROM tblAlert WHERE comID = " + 
                             Session["trkCompany"].ToString() + " AND unitID = " + unitID + 
							 " and alertTime::date = '" + date + "' ORDER BY alertTime DESC;";


            ExecuteSQL _executeSQL = new ExecuteSQL();
            DataSet _ds = _executeSQL.getDataSet(_strSQL);
			Session["_ds"] = _ds;
			_grdAlert.DataSource = _ds;
		    _grdAlert.DataBind();


		    if (_grdAlert.Rows.Count < 1)
		    {
				DateTime dateTime = new DateTime();
				dateTime = DateTime.Parse(date);
		        _lblMessage.Text = "No alerts occured for this unit on " + 
								   dateTime.ToString(dateFormat) + ".";
		        _grdAlert.Visible = false;
		    }
		    else
		    {
		        _lblMessage.Text = "";
		        _grdAlert.Visible = true;
		    }
        }
        catch (Exception ex)
        { 
        	Console.WriteLine(ex.Message.ToString());
        }
		
	}

	protected void _alertDate_SelectedDateChanged(object s, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
	{
		Session["_ds"] = null;
		GetAlerts(_alertDate.SelectedDate.ToString());
	}

    protected void _grdAlert_Paging(object sender, GridViewPageEventArgs e)
    {
		_grdAlert.PageIndex = e.NewPageIndex;
		_grdAlert.DataSource = Session["_ds"];
		_grdAlert.DataBind();
    }


    protected void _grdAlert_DataBound(object sender, GridViewRowEventArgs e)
    {

		string alertType = e.Row.Cells[0].Text;    
		if(e.Row.RowType == DataControlRowType.DataRow) {
		  	if (alertType == "Event") {
			 	e.Row.BackColor = Color.FromArgb(5, 255, 242, 106);
			} else if (alertType == "Geofence" || alertType == "Red Alert" || 
					alertType == "Speeding") {
				if (e.Row.Cells[1].Text.Contains("is inside") || e.Row.Cells[1].Text.Contains("is within")) {
					e.Row.BackColor = Color.FromArgb(5, 112, 222, 69);
				} else {
			 		e.Row.BackColor = Color.FromArgb(5, 255, 118, 106);
				}
			}
		}
    }
}

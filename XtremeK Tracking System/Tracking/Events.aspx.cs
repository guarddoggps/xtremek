using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlarmasABC.BLL.ErrorReports;

public partial class Tracking_Events : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadEvents();
    }

    private void LoadEvents()
    {
        ProcessEvents _events = new ProcessEvents();

        try
        {
            _events.UserID = int.Parse(Session["uID"].ToString());
            _events.ComID = int.Parse(Session["trkCompany"].ToString());
            _events.invoke();
            _grdAlert.DataSource = _events.Ds;
            _grdAlert.DataBind();

            if (_grdAlert.Items.Count < 1)
            {
                _lblMessage.Text = "No Events occured today !!!";
                _grdAlert.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            _events = null;
        }
    }
}

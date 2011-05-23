
using System;
using System.Drawing;
using System.Configuration;
using System.Web;
using System.Web.UI;

using AlarmasABC.BLL;
using AlarmasABC.Core;
using AlarmasABC.Utilities;

namespace XtremeK
{
	public partial class Login : System.Web.UI.Page
	{
		private const bool debug = true;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack) 
			{
				Page.Header.Title = Global.AppName + " Login Page";
				//logo.ImageUrl = "../Images/Logos/" + Global.LogoImage + ".png";
			}
		}
		
		protected void Logo_Click(object sender, EventArgs e)
		{
			if (Global.LogoUrl.Length > 0) 
			{
				Response.Redirect(Global.LogoUrl);
			}
		}
		
		protected virtual void loginButtonClicked(object sender, EventArgs e)
		{
			int rc = 0;
			string companyName = Global.CompanyName;
			
			// Run the ProcessLogin class
			ProcessLogin processLogin = new ProcessLogin();
			processLogin.CompanyName = companyName;
			processLogin.UserName = usernameBox.Text;
			//processLogin.Password = EncDec.GetEncryptedText(passwordBox.Text);
			processLogin.Password = passwordBox.Text;
			rc = processLogin.LoginUser();
			
			// Check the return code
			switch (rc)
			{
			/* Login successful! */
			case ProcessLogin.LOGIN_SUCCESS:
			{
				AlarmasABC.Core.Login login = processLogin.LoginData;
				
				if (debug)
				{
					Console.WriteLine("ComID: " + login.ComID);
					Console.WriteLine("Company Name: " + login.CompanyName);
					Console.WriteLine(" ");
					Console.WriteLine("User ID: " + login.UserID);
					Console.WriteLine("User Name: " + login.UserName);
					Console.WriteLine("Role: " + login.Role);
				}
				
				// Set global session variables
				Global.ComID = login.ComID;
				Global.UserID = login.UserID;
				Global.UserName = login.UserName;
				
				messageLabel.Text = "Username and password correct!";
				messageLabel.ForeColor = Color.Green;
				
				// TODO: Redirect to Home page
				Response.Redirect("Home.aspx");
				break;
			}
			/* Username or password is incorrect */
			case ProcessLogin.LOGIN_INCORRECT:
				messageLabel.Text = "Invalid username or password!";
				messageLabel.ForeColor = Color.Red;
				break;
			/* User is not active */
			case ProcessLogin.LOGIN_INACTIVE:
				messageLabel.Text = "This user has been deactivated by the administrator.";
				messageLabel.ForeColor = Color.Blue;
				break;
			/* Login is not allowed for this user */
			case ProcessLogin.LOGIN_DISALLOWED:
				messageLabel.Text = "This user is not allowed to login.";
				messageLabel.ForeColor = Color.Blue;
				break;
			case ProcessLogin.LOGIN_COMPANY_NOEXIST:
				messageLabel.Text = "Company '" + companyName + "' does not exist!";
				messageLabel.ForeColor = Color.Red;
				break;
			}
		}
	}
	
}

<%@ Page Language="C#" Inherits="XtremeK.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Login Page</title>
	<link href="CSS/Login.css" rel="stylesheet" type="text/css"/>
	
	<style type="text/css">
        html,body,form
        { 
            overflow: hidden;
            height:100%;
        } 
	</style>
</head>
<body>
	<form id="form1" runat="server">
		<div class="page">
			<div class="header">
				<a class="logo_abc" href="http://alarmasabc.net"></a>
				<div class="logo_xtremek"></div>
				<!--<asp:ImageButton ID="logo" runat="server" CssClass="header_logo" ImageUrl="../Images/logo_xtremek.png" OnClock="Logo_Click"></asp:ImageButton>-->

			</div>
			
			<div class="floater"></div>
			
			<div class="wrapper">
				<div class="form">
					<div class="center">
					
						<div class="form_label">User Name</div>
						<div class="form_text_field">
							<asp:TextBox ID="usernameBox" Width="150px" CssClass="form_text_box" runat="server"></asp:TextBox>
						</div>
						
						<div class="form_label">Password</div>
						<div class="form_text_field">
							<asp:TextBox ID="passwordBox" Width="150px" CssClass="form_text_box" runat="server" TextMode="Password"></asp:TextBox>
						</div>
						
						<div class="form_label"></div>
						<div class="form_text_field">					
					    	<asp:ImageButton ID="loginButton" runat="server" ImageUrl="Images/submit.png" OnClick="loginButtonClicked" />
						</div>
						
						<div class="form_label"></div>
						<div class="form_text_field">
						    <a style="font-size: 0.8em" href="PasswordRetrive.aspx" >Forgot your password?</a>
						</div>
						
						<asp:Label ID="messageLabel" CssClass="form_message" runat="server"></asp:Label>
						
						<div style="width: 84px" class="form_label"></div>
						<div class="form_text_field_link">
						    <a href="NewAccount.aspx" >Create new account</a>
						</div>
						
						<div class="form_terms">
							<div>By clicking "Submit" you agree to the following <a href="TermsAndConditions.aspx">Terms and Conditions</a></div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</form>
</body>
</html>
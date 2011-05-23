<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="XtremeK.Home" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
	<title>Home Page</title>
	<link href="../CSS/Home.css" rel="stylesheet" type="text/css" />
	
	<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true">
	</script>
	
	<script type="text/javascript">
	    var map = null;
	    var options = null;
	    var markers = Array();
	    
		function init() {
			// TODO: Do init stuff here?
		}
		
	</script>
</head>
<body scroll="no">
	<form id="form1" runat="server">
		
    	<asp:ScriptManager ID="scriptManager" runat="server"  AsyncPostBackTimeout="300">
    	</asp:ScriptManager>
    
		<div class="page">
            <div class="header">
            	<!-- The first logo. Default is the XtremeK logo -->
                <asp:ImageButton ID="logo" 
                				 runat="server" 
                				 CssClass="header_logo" 
                				 ImageUrl="../Images/logo_xtremek.png" 
                				 OnClock="Logo_Click" />
                				 
                				 
				<div class="subheader">		
				    <div class="hbutton	">
						<asp:ImageButton ID="emailButton" 
										 runat="server" 
										 ImageUrl="Images/mail_up.jpg" 
										 onmouseover="this.src='Images/mail_over.jpg';" 
										 onmouseout="this.src='Images/mail_up.jpg';"/>	
				    </div>
								
				    <div class="hbutton	">
						<asp:ImageButton ID="printButton" 
						                 runat="server" 
						                 ImageUrl="Images/print_up.jpg" 
						                 onmouseover="this.src='Images/print_over.jpg';" 
						                 onmouseout="this.src='Images/print_up.jpg';"/>		
				    </div>	             	    
	
									
				    <div class="hbutton	">
						<asp:ImageButton ID="refreshButton" 
						                 runat="server" 
						                 ImageUrl="Images/refresh_up.jpg" 
						                 onmouseover="this.src='Images/refresh_over.jpg';" 
						                 onmouseout="this.src='Images/refresh_up.jpg';"/>	
				    </div>		             	    
					
								
					<div class="hbutton	">	
						<asp:ImageButton ID="tagButton" 
						  				 runat="server" 
						  				 ImageUrl="Images/tag_up.jpg" 
						  				 onmouseover="this.src='Images/tag_over.jpg';"
						  				 onmouseout="this.src='Images/tag_up.jpg';"/>	
				    </div>		             	    
				
				</div>
            </div>
            
    		<div id="Map" class="map" style="width:100%; "></div>
    		
    		<telerik:RadToolTipManager ID="RadToolTipManager1" 
            						   runat="server" 
    								   AnimationDuration="300" 
    								   ShowDelay="200"
            						   EnableShadow="true" 
            						   HideDelay="1" Width="453px" 
            						   Height="210px"
            						   RelativeTo="Element" 
            						   Animation="Slide" 
            						   Position="BottomCenter" 
            						   OnAjaxUpdate="OnAjaxUpdate"
            						   Skin="Telerik" />
            
    		<div class="footer">
    			<div class="footer_center">
					<asp:ImageButton ID="unitsButton" 
							  		 runat="server" 
							  		 CssClass="footer_button"
							  		 ImageUrl="Images/button_units.png" />	
					<asp:ImageButton ID="adminButton" 
							  		 runat="server" 
							  		 CssClass="footer_button"
							  		 ImageUrl="Images/button_admin.png" />	
					<asp:ImageButton ID="reportsButton" 
							  		 runat="server" 
							  		 CssClass="footer_button"
							  		 ImageUrl="Images/button_reports.png" />	
    			</div>
    		</div>				 
		</div>
	</form>
</body>
</html>
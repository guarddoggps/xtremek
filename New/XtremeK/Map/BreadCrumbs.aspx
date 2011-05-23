<%@ Page Language="C#" Inherits="XtremeK.BreadCrumbs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title></title>
	<link href="~/CSS/Home.css" rel="stylesheet" type="text/css" />
	
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
<body onload="init()">
	<form id="form1" runat="server">
		<asp:ScriptManager ID="scriptManager" 
						   runat="server" 
						   AsyncPostBackTimeout="300" />
		
    	<div id="Map" style="width:100%; height:100%"></div>
	</form>
</body>
</html>

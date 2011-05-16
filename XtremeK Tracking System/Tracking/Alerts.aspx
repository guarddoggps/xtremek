<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alerts.aspx.cs" Inherits="Tracking_Alerts" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alerts</title>
    <link href="../CSS/ajaxstyle.css" rel="stylesheet" type="text/css" />
    <script src="../Js/DateTime.js" type="text/javascript"></script>
    <script type="text/javascript">
     	var previousRow;
     
		function highlightRow(row)
      	{
        	var index=row.id; 
        
         
            if(previousRow!=undefined)
            {
                
                row.style.backgroundColor = '#CCCCCC';
                previousRow.style.backgroundColor = "White";
                previousRow.setAttribute("onmouseout","this.style.backgroundColor='white'");
                previousRow = row;
                row.setAttribute("onmouseout","this.style.backgroundColor='white'");
                row.setAttribute("onmouseover","this.style.backgroundColor='#CCCCCC'");

            }
            else
            {
                row.style.backgroundColor = '#CCCCCC';
                previousRow = row;
                previousRow.setAttribute("onmouseover","this.style.backgroundColor='white'");
                row.setAttribute("onmouseout","this.style.backgroundColor='#CCCCCC'");

            }            
       
        	accessParent(index);  
      	}

		function accessParent(i)
		{
			window.parent.MarkerShow(i);
		}   
    </script>

	<style type="text/css">
		﻿html, body,form
		{
            background-color:#f5f5f5;
		}

		.Header
		{
    		background-image: url(../CSS/FormImages/header.png);
			background-repeat: repeat;
			color:Black;
			font-weight: bold;
			font-size: 12px;
			height: 27px;
		}
		.AlertCol
		{
			color:Black;
			font-size: 12px;
			height: 18px;
		}
		.Footer
		{
			height:25px;
			font-size: 12px;
			font-weight: bold;
			color: Black;
		}
		.datetime {
			position: absolute;
			left: 180px;
			top: 12px;
			width:10px;
			height:25px;
			text-align:left;
			font-family: Verdana;
			color: Black;
			font-size:14px;
		}

		.pageUpdateProgress
		{
			/*background-color:#330099;*/
			/*background-color:#7A8486;*/
			color:#fff;
			width: 150px;
			text-align: center;
			vertical-align: middle;
			position: absolute;
			bottom: 40%;
			left: 38%;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="_scriptManager" runat="server"></asp:ScriptManager>

    <asp:UpdateProgress ID="siteUpdateProgress" DisplayAfter="50"  runat="server">
        <ProgressTemplate>
        
		<div class="TransparentGrayBackground"></div>
        	<div class="pageUpdateProgress">
                <asp:Image  ID="ajaxLoadNotificationImage" 
                            runat="server" 
                            ImageUrl="../Images/loading.gif" 
                            AlternateText="[image]" />
            </div>
     	</ProgressTemplate>
	</asp:UpdateProgress>

    <div>
		<div class="datetime">
        	<asp:UpdatePanel runat="server" ID="updateDate">
        		<ContentTemplate>
					<telerik:RadDatePicker ID="_alertDate" runat="server" OnSelectedDateChanged="_alertDate_SelectedDateChanged" AutoPostBack="true">
					</telerik:RadDatePicker>
				</ContentTemplate>
        	</asp:UpdatePanel>		
		</div>
		<div class="footbtn">
        	<asp:UpdatePanel runat="server" ID="UpdateBtn">
        		<ContentTemplate>
        			<asp:ImageButton ID="_btn_Today" runat="server" OnClientClick="getToday();" ImageUrl="~/Images/Button/Today.gif" />
        			<asp:ImageButton ID="_btn_Yesterday" runat="server"  OnClientClick="getYesterday();" ImageUrl="~/Images/Button/Yesterday.gif" />
        		</ContentTemplate>
        	</asp:UpdatePanel>				 
		</div>

		<div>      
	        <asp:UpdatePanel ID="updateGrid" runat="server">   
	            <ContentTemplate>	                         
					<asp:GridView ID="_grdAlert" runat="server" BorderColor="#AAAAAAA" Width="100%" AllowPaging="true" OnPageIndexChanging="_grdAlert_Paging" OnRowDataBound="_grdAlert_DataBound" AutoGenerateColumns="False">
						<HeaderStyle CssClass="Header" />
						<PagerStyle HorizontalAlign="Right" CssClass="Footer" />

						<Columns>
							<asp:BoundField DataField="alertType" HeaderText="Alert Type" ItemStyle-CssClass="AlertCol" />
						    <asp:BoundField DataField="alertMessage" HeaderText="Alert Message" ItemStyle-CssClass="AlertCol" />
						    <asp:BoundField DataField="alertTime" HeaderText="Alert Time" ItemStyle-CssClass="AlertCol" />
						</Columns>
					</asp:GridView>
	        	</ContentTemplate>
	        </asp:UpdatePanel>
   		</div>

		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="ErrorMessage">
                      <asp:HiddenField ID="_hHasValue" runat="server" />
                     <asp:Label ID="_lblMessage" runat="server" Text=""></asp:Label>       
                    
                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
    </div>

	<script type="text/javascript">
		function getToday() 
		{
			var d = new Date();
			var day = d.getDate();
			var month = d.getMonth();
			var year = d.getFullYear();
            var dateCntl1=$find("<%= _alertDate.ClientID%>");
			dateCntl1.set_selectedDate(new Date(year, month, day));
		}
		function getYesterday() 
		{
			var d = new Date();
			var day = d.getDate();
			var month = d.getMonth();
			var year = d.getFullYear();
            var dateCntl1=$find("<%= _alertDate.ClientID%>");
			dateCntl1.set_selectedDate(new Date(year, month, day - 1));
		}
	</script>
    </form>
</body>
</html>

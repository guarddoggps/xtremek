<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Events.aspx.cs" Inherits="Tracking_Events" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Events</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="_manager" runat="server">
        </asp:ScriptManager>
        
        <div>
            <asp:Label ID="_lblMessage" runat="server" ForeColor="Green"></asp:Label>
        </div>
        
        <div> 
                
                <telerik:RadGrid ID="_grdAlert" runat="server" Width="500px" AllowPaging="True" PageSize="10" EnableAJAX="True" GridLines="Both" Skin="Default" AutoGenerateColumns="False">
             
                        <PagerStyle Mode="NextPrevAndNumeric" NextPageText="next" PrevPageText="previous" />
                                           
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="alertType" HeaderText="Alert Type" UniqueName="alertType"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="alertMessage" HeaderText="Alert Message" UniqueName="alertMessage"><ItemStyle Width="150px" /></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="unitName" HeaderText="Unit Name" UniqueName="unitName"><ItemStyle Width="120px" /></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="alertTime" HeaderText="Event Occured" UniqueName="alertTime"><ItemStyle Width="170px" /></telerik:GridBoundColumn>
                    </Columns>
                
                    <RowIndicatorColumn Visible="False"> <HeaderStyle Width="20px" /> </RowIndicatorColumn>
                
                    <ExpandCollapseColumn Resizable="False" Visible="False"> <HeaderStyle Width="20px" /> </ExpandCollapseColumn>
                
                </MasterTableView>
                
            </telerik:RadGrid>
        </div>
        
    </div>
    </form>
</body>
</html>

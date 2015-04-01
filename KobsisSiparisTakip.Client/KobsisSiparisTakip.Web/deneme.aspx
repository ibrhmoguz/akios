<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deneme.aspx.cs" Inherits="ACKSiparisTakip.deneme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
            <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />

            <telerik:RadButton ID="RadButton1" runat="server" Text="RadButton" OnClick="Button1_Click"></telerik:RadButton>

            <telerik:RadTextBox ID="RadTextBox1" runat="server"></telerik:RadTextBox>
            <telerik:RadCalendar ID="RadCalendar1" runat="server"></telerik:RadCalendar>

            <telerik:RadRibbonBar ID="RadRibbonBarMenu" runat="server" Width="100%" SkinID="WebBlue">
                <telerik:RibbonBarTab Text="Sipariş"></telerik:RibbonBarTab>
                <telerik:RibbonBarTab Text="İş Takvimi"></telerik:RibbonBarTab>
                <telerik:RibbonBarTab Text="Yönetim Konsolu"></telerik:RibbonBarTab>
            </telerik:RadRibbonBar>
        </div>
    </form>
</body>
</html>

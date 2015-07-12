<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Akios.WebClient.Login" %>

<!DOCTYPE html>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="App_Themes/Theme/Template/Template.css" type="text/css" rel="stylesheet" />
    <link href="App_Themes/Theme/StyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <script type="text/javascript">
        var modalDiv = null;
        function showNotification() {
            $find("<%=RadNotificationACKMaster.ClientID %>").show();
        }

        function showModalDiv(sender, args) {
            if (!modalDiv) {
                modalDiv = document.createElement("div");
                modalDiv.style.width = "100%";
                modalDiv.style.height = "100%";
                modalDiv.style.backgroundColor = "#aaaaaa";
                modalDiv.style.position = "absolute";
                modalDiv.style.left = "0px";
                modalDiv.style.top = "0px";
                modalDiv.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=50)";
                modalDiv.style.opacity = ".5";
                modalDiv.style.MozOpacity = ".5";
                modalDiv.setAttribute("unselectable", "on");
                modalDiv.style.zIndex = (sender.get_zIndex() - 1).toString();
                document.body.appendChild(modalDiv);
            }
            modalDiv.style.display = "";
        }

        function hideModalDiv() {
            modalDiv.style.display = "none";
        }
    </script>
    <form id="FormLogin" runat="server" defaultbutton="LB_Login">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <table style="width: 100%">
            <tr>
                <br />
                <br />
                <br />
                <br />
                <br />
            </tr>
            <tr style="display: none">
                <td colspan="2">
                    <telerik:RadBinaryImage ID="imgLogo" runat="server" ImageUrl="~/App_Themes/Theme/Raster/ackLogo.PNG" />
                    <br />
                    <h2 class="title" style="color: skyblue; border-color: black; text-align: center; align-self: center">Sipariş Takip Programı</h2>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle;">
                    <img src="App_Themes/Theme/Raster/user.png" alt="" style="position: relative; top: 4px; left: 0px;" />
                    <asp:TextBox ID="userName" runat="server" ToolTip="Kullanıcı adınızı giriniz." Width="95px"></asp:TextBox>
                    <img src="App_Themes/Theme/Raster/lock.gif" alt="" style="position: relative; top: 6px;" />
                    <asp:TextBox ID="password" runat="server" TextMode="Password" ToolTip="Şifrenizi Giriniz." Width="75px"></asp:TextBox>
                    <asp:LinkButton ID="LB_Login" runat="server" OnClick="LB_Login_Click" Text="Giriş" ForeColor="Blue" Font-Underline="true"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle">
                    <asp:Image ID="imgCaptcha" runat="server" Width="172px" Height="40" alt="Visual verification" title="" src="Captcha.aspx" vspace="5" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtResimDogrulama" runat="server" ToolTip="Doğrulama metnini giriniz." Width="172" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <telerik:RadNotification ID="RadNotificationACKMaster" runat="server" Position="Center" AutoCloseDelay="3000" Width="300px" EnableRoundedCorners="true"
            Height="120px" Text="Lorem ipsum dolor sit amet" OnClientShowing="showModalDiv" OnClientHidden="hideModalDiv"
            ShowCloseButton="true" ShowTitleMenu="false" TitleIcon="" EnableShadow="true">
        </telerik:RadNotification>
    </form>
</body>
</html>

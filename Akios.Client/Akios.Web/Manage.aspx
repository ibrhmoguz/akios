<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Akios.Web.Manage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="7" style="text-align: center; font-size: 11pt;">MANAGE<br />
            </th>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCommand" runat="server" Width="100%" Height="200" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadButton ID="btnExecute" runat="server" Text="Çalıştır" OnClick="btnExecute_Click">
                    <Icon PrimaryIconCssClass="rbOk" PrimaryIconLeft="4" PrimaryIconTop="3" />
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdList" runat="server" AutoGenerateColumns="true" AllowPaging="false" PageSize="30"
                    Width="100%" CssClass="AnaTablo" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                    EmptyDataText="Kayıt yok!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataRowStyle-CssClass="TdRenkAciklama">
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

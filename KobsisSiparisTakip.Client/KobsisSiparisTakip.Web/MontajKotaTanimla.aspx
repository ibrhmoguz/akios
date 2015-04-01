<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="MontajKotaTanimla.aspx.cs" Inherits="KobsisSiparisTakip.Web.MontajKotaTanimla" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="7" style="text-align: center; font-size:  11pt;">MONTAJ KOTA TANIMLAMA<br />
            </th>
        </tr>
        <tr>
            <td colspan="7">
                <br />
            </td>
        </tr>
        <tr>
            <th style="width: 8%">Montaj Tarihi: </th>
            <td style="width: 15%">
                <telerik:RadDatePicker ID="rdtMontajTarih" runat="server" Width="150px"></telerik:RadDatePicker>
            </td>
            <th style="width: 8%">Montaj Kotası: </th>
            <td style="width: 8%">
                <telerik:RadTextBox ID="txtMontajKota" runat="server" RenderMode="Lightweight" Width="50"></telerik:RadTextBox>
            </td>
            <th style="width: 8%">Montaj Kapalı: </th>
            <td style="width: 8%">
                <asp:CheckBox ID="chcBoxMontajKabul" runat="server" Text="Montaj Kapalı" Checked="false" ToolTip="Belirtmiş olduğunuz günde montaj yapılmayacak ise seçiniz" />

            </td>
            <td>
                <telerik:RadButton ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_Click">
                    <Icon PrimaryIconCssClass="rbOk" PrimaryIconLeft="4" PrimaryIconTop="3" />
                </telerik:RadButton>
            </td>

        </tr>
        <tr>
            <td colspan="7">
                <br />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grdMontajKota" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="AnaTablo"
        AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2" EmptyDataText="Montaj kotası tanımlanmamıştır!"
        EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
        EmptyDataRowStyle-CssClass="TdRenkAciklama" DataKeyNames="MONTAJKOTAID" OnRowDeleting="grdMontajKota_RowDeleting">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="MONTAJTARIHI" HeaderText="MONTAJ TARİHİ" />
            <asp:BoundField DataField="MAXMONTAJSAYI" HeaderText="MONTAJ KOTASI" />
            <asp:BoundField DataField="MONTAJKABUL" HeaderText="DURUM" ItemStyle-Font-Size="12px" />
            <asp:CommandField HeaderText="SİL" DeleteText="Sil" DeleteImageUrl="~/App_Themes/Theme/Raster/clear.png" ShowDeleteButton="true" ButtonType="Image" />
        </Columns>
    </asp:GridView>
</asp:Content>

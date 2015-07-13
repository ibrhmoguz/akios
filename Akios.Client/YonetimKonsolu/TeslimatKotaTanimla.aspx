<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="TeslimatKotaTanimla.aspx.cs" Inherits="Akios.WebClient.YonetimKonsolu.TeslimatKotaTanimla" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="7" style="text-align: center; font-size: 11pt;">TESLİMAT KOTA TANIMLAMA<br />
            </th>
        </tr>
        <tr>
            <td colspan="7">
                <br />
            </td>
        </tr>
        <tr>
            <th style="width: 8%">Teslimat Tarihi: </th>
            <td style="width: 15%">
                <telerik:RadDatePicker ID="rdtTeslimatTarih" runat="server" Width="150px"></telerik:RadDatePicker>
            </td>
            <th style="width: 8%">Teslimat Kotası: </th>
            <td style="width: 8%">
                <telerik:RadTextBox ID="txtTeslimatKota" runat="server" RenderMode="Lightweight" Width="50"></telerik:RadTextBox>
            </td>
            <th style="width: 8%">Teslimat Kapalı: </th>
            <td style="width: 8%">
                <asp:CheckBox ID="chcBoxTeslimatKabul" runat="server" Text="Teslimat Kapalı" Checked="false" ToolTip="Belirtmiş olduğunuz günde teslimat yapılmayacak ise seçiniz" />

            </td>
            <td>
                <telerik:RadButton ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_Click">
                    <Icon PrimaryIconCssClass="rbOk" PrimaryIconLeft="4" PrimaryIconTop="3" />
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td colspan="7">
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grdTeslimatKota" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="grid"
        AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2" EmptyDataText="Teslimat kotası tanımlanmamıştır!"
        EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
        EmptyDataRowStyle-CssClass="TdRenkAciklama" DataKeyNames="TeslimatKOTAID" OnRowDeleting="grdTeslimatKota_RowDeleting">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="SIRANO" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="TESLIMATTARIHI" HeaderText="TESLIMAT TARİHİ" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="MAXTESLIMATSAYI" HeaderText="TESLIMAT KOTASI" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="TESLIMATKABUL" HeaderText="DURUM" ItemStyle-HorizontalAlign="Center" />
            <asp:CommandField HeaderText="SİL" DeleteText="Sil" DeleteImageUrl="~/App_Themes/Theme/Raster/iptal.gif" ShowDeleteButton="true" ButtonType="Image" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</asp:Content>

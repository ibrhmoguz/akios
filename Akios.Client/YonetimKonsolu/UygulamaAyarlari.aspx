<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="UygulamaAyarlari.aspx.cs" Inherits="Akios.WebClient.YonetimKonsolu.UygulamaAyarlari" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="5" style="text-align: center; font-size:  11pt;">UYGULAMA AYARLARI<br />
            </th>
        </tr>
        <tr>
            <td colspan="5">
                <br />
            </td>
        </tr>
        <tr>
            <th style="width: 11%">Teslimat Kota Kontrolü: </th>
            <td style="width: 8%">
                <asp:CheckBox ID="chcBoxTeslimatKotaKontrolu" runat="server" Text="" Checked="true" ToolTip="Teslimat kota kontrolünü aktifleştir." />
            </td>
            <th style="width: 13%">Teslimat Kota Varsayılan Değeri: </th>
            <td style="width: 8%">
                <telerik:RadTextBox ID="txtTeslimatKotaVarsayilan" runat="server" Width="50" RenderMode="Lightweight" MaxLength="99">
                </telerik:RadTextBox>
            </td>
            <td>
                <telerik:RadButton ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_Click">
                    <Icon PrimaryIconCssClass="rbOk" PrimaryIconLeft="4" PrimaryIconTop="3" />
                </telerik:RadButton>
            </td>
        </tr>
    </table>
</asp:Content>

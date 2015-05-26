<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="SiparisGoruntule.aspx.cs" Inherits="Kobsis.Web.Siparis.SiparisGoruntule" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <td rowspan="6" style="text-align: center">
                <asp:Image ID="imgFirmaLogo" runat="server" Width="150" Height="125" />
            </td>
            <td colspan="2" rowspan="3" style="width: 45%; font-size: x-large; text-align: center; font-weight: 700">
                <b>
                    <asp:Label ID="lblSiparisSeri" runat="server"></asp:Label></b>
            </td>
            <td style="width: 30%; text-align: left">
                <b>
                    <asp:Label ID="lblFirmaAdi" runat="server"></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td style="font-size: xx-small; text-align: left">
                <b>Adres: </b>
                <asp:Label ID="lblFirmaAdres" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="font-size: xx-small; text-align: left">
                <b>Telefon: </b>
                <asp:Label ID="lblFirmaTelefon" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <td colspan="2" rowspan="3" style="font-size: x-large; text-align: center">
                <b>SİPARİŞ FORMU</b>
            </td>
            <td style="font-size: xx-small; text-align: left">
                <b>Faks : </b>
                <asp:Label ID="lblFirmaFaks" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <td style="font-size: xx-small; text-align: left">
                <b>Web : </b>
                <asp:Label ID="lblFirmaWebAdres" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <td style="font-size: xx-small; text-align: left">
                <b>e-posta : </b>
                <asp:Label ID="lblFirmaMail" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th style="width: 12%">Sipariş No : </th>
            <td style="width: 20%">
                <asp:Label ID="lblSiparisNo" runat="server"></asp:Label>
            </td>
            <th style="width: 10%">Adı : </th>
            <td style="width: 20%">
                <asp:Label ID="lblMusteriAd" runat="server"></asp:Label>
            </td>
            <th rowspan="2" style="width: 10%">Adres : </th>
            <td rowspan="2">
                <asp:Label ID="lblMusteriAdres" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Sipariş Adeti : </th>
            <td>
                <asp:Label ID="lblSiparisAdeti" runat="server"></asp:Label>
            </td>
            <th style="width: 10%">Soyadı </th>
            <td>
                <asp:Label ID="lblMusteriSoyad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Sipariş Tarihi : </th>
            <td>
                <asp:Label ID="lblSiparisTarih" runat="server"></asp:Label>
            </td>
            <th>Ev Tel : </th>
            <td>
                <asp:Label ID="lblMusteriEvTel" runat="server"></asp:Label>
            </td>
            <th>İl :</th>
            <td>
                <asp:Label ID="lblMusteriIl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Teslim Tarihi : </th>
            <td>
                <asp:Label ID="lblTeslimTarih" runat="server"></asp:Label>
            </td>
            <th>iş Tel : </th>
            <td>
                <asp:Label ID="lblMusteriIsTel" runat="server"></asp:Label>
            </td>
            <th>İlçe :</th>
            <td>
                <asp:Label ID="lblMusteriIlce" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Firma Adı:
            </th>
            <td>
                <asp:Label ID="lblMusteriFirmaAdi" runat="server"></asp:Label>
            </td>
            <th>Cep Tel : </th>
            <td>
                <asp:Label ID="lblMusteriCepTel" runat="server"></asp:Label>
            </td>
            <th>Semt :</th>
            <td>
                <asp:Label ID="lblMusteriSemt" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="divSiparisFormKontrolleri" runat="server" Width="100%"></asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
                <br />
                <telerik:RadButton ID="btnGuncelle" runat="server" Text="Güncelle" OnClick="btnGuncelle_Click">
                    <Icon PrimaryIconCssClass="rbOk" PrimaryIconLeft="4" PrimaryIconTop="3" />
                </telerik:RadButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadButton ID="btnYazdir" runat="server" Text="Yazdır">
                        <Icon PrimaryIconCssClass="rbPrint" PrimaryIconLeft="4" PrimaryIconTop="3" />
                    </telerik:RadButton>
            </td>
        </tr>
    </table>
</asp:Content>

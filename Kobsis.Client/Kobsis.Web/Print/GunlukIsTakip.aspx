<%@ Page Title="" Language="C#" MasterPageFile="~/Print/PrinterFriendly.Master" AutoEventWireup="true" CodeBehind="GunlukIsTakip.aspx.cs" Inherits="Kobsis.Web.Print.GunlukIsTakip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table style="width: 100%" class="normalTablo">
        <tr>
            <td rowspan="4" style="width: 70px; padding: 5px 5px 5px 5px">
                <asp:Image ID="imgFirmaLogo" runat="server" ImageUrl="~/App_Themes/Theme/Raster/ackLogo.PNG" Width="70" Height="70" />
            </td>
            <td rowspan="4" style="text-align: center; vertical-align: central">
                <h3>Günlük İş Takip Formu</h3>
            </td>
        </tr>
        <tr>
            <td style="width: 170px; vertical-align: middle; padding: 3px 3px 3px 3px">Doküman Kodu : F27</td>
        </tr>
        <tr>
            <td style="vertical-align: middle; padding: 3px 3px 3px 3px">Yürürlük Tarihi : 20.08.2004</td>
        </tr>
        <tr>
            <td style="vertical-align: middle; padding: 3px 3px 3px 3px">Rev. No-Tarihi : 0</td>
        </tr>
        <tr>
            <td colspan="3" style="font-weight: bold; vertical-align: middle; padding: 3px 3px 3px 3px">TARİH:
                <asp:Label ID="lblTarih" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="grdSiparisler" runat="server" AutoGenerateColumns="false" Width="100%" AllowPaging="False">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="SIRANO" />
                        <asp:BoundField DataField="SiparisNo" HeaderText="SİPARİŞ NO" />
                        <asp:BoundField DataField="Musteri" HeaderText="MÜŞTERİ/FİRMA" />
                        <asp:BoundField DataField="Adres" HeaderText="ADRES" />
                        <asp:BoundField DataField="Tel" HeaderText="TEL" ItemStyle-Font-Size="12px" />
                        <asp:BoundField DataField="Semt" HeaderText="SEMT" />
                        <asp:BoundField DataField="MontajEkibi" HeaderText="MONTAJ EKİBİ" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

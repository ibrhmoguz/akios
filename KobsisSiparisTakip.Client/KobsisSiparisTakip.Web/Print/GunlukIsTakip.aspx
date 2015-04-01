<%@ Page Title="" Language="C#" MasterPageFile="~/Print/PrinterFriendly.Master" AutoEventWireup="true" CodeBehind="GunlukIsTakip.aspx.cs" Inherits="KobsisSiparisTakip.Web.Print.GunlukIsTakip" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table style="width: 100%" class="normalTablo">
        <tr>
            <td rowspan="4" style="width: 70px; padding: 5px 5px 5px 5px">
                <telerik:RadBinaryImage ID="imgLogo" runat="server" ImageUrl="~/App_Themes/Theme/Raster/ackLogo.PNG" Width="70" Height="70" />
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
                <asp:GridView ID="grdSiparisler" runat="server" AutoGenerateColumns="false" Width="100%"
                    AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="SIPARISNO" HeaderText="SİPARİŞ NO" />
                        <asp:BoundField DataField="MUSTERI" HeaderText="MÜŞTERİ/FİRMA" />
                        <asp:BoundField DataField="TEL" HeaderText="TEL" />
                        <%--<asp:BoundField DataField="ADRES" HeaderText="ADRES" />--%>
                        <asp:BoundField DataField="SEMT" HeaderText="SEMT" />
                        <%-- <asp:BoundField DataField="MONTAJEKIBI" HeaderText="MONTAJ EKİBİ" />--%>
                        <asp:BoundField DataField="ACIKLAMA" HeaderText="AÇIKLAMA" />
                        <%--<asp:BoundField DataField="KAPICINSI" HeaderText="KAPI CİNSİ" />--%>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <table class="normalTablo" style="width: 100%; text-align: center">
        <tr>
            <td colspan="3">MONTAJ EKİBİ:
                <br />
                <br />
                <br />
            </td>
            <td colspan="3">MONTAJ EKİBİ:</td>
            <td colspan="3">MONTAJ EKİBİ:</td>
        </tr>
        <tr>
            <td>SİPARİŞ NO</td>
            <td>SEMT</td>
            <td>KAPI CİNSİ</td>
            <td>SİPARİŞ NO</td>
            <td>SEMT</td>
            <td>KAPI CİNSİ</td>
            <td>SİPARİŞ NO</td>
            <td>SEMT</td>
            <td>KAPI CİNSİ</td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>

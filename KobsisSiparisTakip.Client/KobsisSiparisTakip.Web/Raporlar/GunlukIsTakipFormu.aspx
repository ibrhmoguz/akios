<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="GunlukIsTakipFormu.aspx.cs" Inherits="KobsisSiparisTakip.Web.GunlukIsTakipFormu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size: 11pt;">GÜNLÜK İŞ TAKİP FORMU<br />
            </th>
        </tr>
        <tr>
            <td style="text-align: center">
                <telerik:RadDatePicker ID="rdtTarih" runat="server" Width="150px"></telerik:RadDatePicker>
                <telerik:RadButton ID="btnSorgula" runat="server" Text="Sorgula" OnClick="btnSorgula_Click">
                    <Icon PrimaryIconCssClass="rbSearch" />
                </telerik:RadButton>
                <telerik:RadButton ID="btnYazdir" runat="server" Text="Yazdır" Visible="false">
                    <Icon PrimaryIconCssClass="rbPrint" PrimaryIconLeft="4" PrimaryIconTop="3" />
                </telerik:RadButton>
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="grdSiparisler" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="AnaTablo"
                    AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2" EmptyDataText="Bugün sipariş teslimatı yoktur!"
                    EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataRowStyle-CssClass="TdRenkAciklama">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="SIPARISNO" HeaderText="SİPARİŞ NO" />
                        <asp:BoundField DataField="MUSTERI" HeaderText="MÜŞTERİ/FİRMA" />
                        <asp:BoundField DataField="TEL" HeaderText="TEL" ItemStyle-Font-Size="12px" />
                        <asp:BoundField DataField="SEMT" HeaderText="SEMT" />
                        <asp:BoundField DataField="MONTAJEKIBI" HeaderText="MONTAJ EKİBİ" />
                        <asp:BoundField DataField="ACIKLAMA" HeaderText="AÇIKLAMA" ItemStyle-Font-Size="10px" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>

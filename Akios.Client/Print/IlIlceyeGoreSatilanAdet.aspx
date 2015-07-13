<%@ Page Title="" Language="C#" MasterPageFile="~/Print/PrinterFriendly.Master" AutoEventWireup="true" CodeBehind="IlIlceyeGoreSatilanAdet.aspx.cs" Inherits="Akios.WebClient.Print.IlIlceyeGoreSatilanAdet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table style="width: 100%" class="CerceveTablo">
        <tr>
            <td rowspan="4" style="width: 70px; padding: 5px 5px 5px 5px">
               <asp:Image ID="imgFirmaLogo"  runat="server" ImageUrl="~/App_Themes/Theme/Raster/ackLogo.PNG" Width="70" Height="70" />
            </td>
            <td rowspan="4" style="text-align: center; vertical-align: central">
                <h3>İl/İlçe Bazlı Satılan Adet Raporu</h3>
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
            <td colspan="3">
                <br />
                <asp:GridView ID="grdRaporIl" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
                    Width="100%" CssClass="AnaTabloRapor" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                    EmptyDataText="İl bulunamamıştır!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataRowStyle-CssClass="TdRenkAciklama">
                    <Columns>
                        <asp:BoundField DataField="Il" HeaderText="İL" ItemStyle-Width="15%" ItemStyle-CssClass="thIlkKolon" />
                        <asp:BoundField DataField="1" HeaderText="1" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="2" HeaderText="2" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="3" HeaderText="3" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="4" HeaderText="4" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="5" HeaderText="5" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="6" HeaderText="6" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="7" HeaderText="7" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="8" HeaderText="8" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="9" HeaderText="9" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="10" HeaderText="10" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="11" HeaderText="11" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="12" HeaderText="12" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="Yillik" HeaderText="Yıllık" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="Yuzde(%)" HeaderText="%" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="grdRaporIlce" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
                    Width="100%" CssClass="AnaTabloRapor" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                    EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataRowStyle-CssClass="TdRenkAciklama">
                    <Columns>
                        <asp:BoundField DataField="IlIlce" HeaderText="İLÇE" ItemStyle-Width="15%" ItemStyle-CssClass="thIlkKolon" />
                        <asp:BoundField DataField="1" HeaderText="1" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="2" HeaderText="2" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="3" HeaderText="3" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="4" HeaderText="4" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="5" HeaderText="5" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="6" HeaderText="6" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="7" HeaderText="7" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="8" HeaderText="8" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="9" HeaderText="9" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="10" HeaderText="10" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="11" HeaderText="11" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="12" HeaderText="12" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="Yillik" HeaderText="Yıllık" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="Yuzde(%)" HeaderText="%" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="grdRaporSemt" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
                    Width="100%" CssClass="AnaTabloRapor" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                    EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataRowStyle-CssClass="TdRenkAciklama">
                    <Columns>
                        <asp:BoundField DataField="Semt" HeaderText="SEMT" ItemStyle-Width="15%" ItemStyle-CssClass="thIlkKolon" />
                        <asp:BoundField DataField="1" HeaderText="1" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="2" HeaderText="2" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="3" HeaderText="3" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="4" HeaderText="4" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="5" HeaderText="5" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="6" HeaderText="6" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="7" HeaderText="7" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="8" HeaderText="8" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="9" HeaderText="9" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="10" HeaderText="10" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="11" HeaderText="11" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="12" HeaderText="12" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="Yillik" HeaderText="Yıllık" ItemStyle-Width="6%" />
                        <asp:BoundField DataField="Yuzde(%)" HeaderText="%" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

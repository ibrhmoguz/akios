<%@ Page Title="" Language="C#" MasterPageFile="~/AkiosMasterPage.Master" AutoEventWireup="true" CodeBehind="SeriyeGoreSatilanAdet.aspx.cs" Inherits="Akios.AdminWebClient.Raporlar.SeriyeGoreSatilanAdet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size: 11pt;">SERİYE GÖRE SATILAN ADET/TUTAR<br />
            </th>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <th style="width: 3%">Yıl: </th>
                        <td style="width: 10%">
                            <telerik:RadDropDownList ID="ddlYil" runat="server" RenderMode="Lightweight" Width="120">
                            </telerik:RadDropDownList>
                        </td>
                        <th style="width: 3%">İl: </th>
                        <td style="width: 11%">
                            <telerik:RadDropDownList ID="ddlMusteriIl" runat="server" AutoPostBack="true" RenderMode="Lightweight" OnSelectedIndexChanged="ddlMusteriIl_SelectedIndexChanged" Width="150">
                            </telerik:RadDropDownList>
                        </td>
                        <th style="width: 3%">İlçe: </th>
                        <td style="width: 11%">
                            <telerik:RadDropDownList ID="ddlMusteriIlce" runat="server" AutoPostBack="True" RenderMode="Lightweight" Width="150">
                            </telerik:RadDropDownList>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnSorgula" runat="server" Text="Sorgula" OnClick="btnSorgula_Click">
                                <Icon PrimaryIconCssClass="rbSearch" />
                            </telerik:RadButton>
                            <telerik:RadButton ID="btnYazdir" runat="server" Text="Yazdır" Visible="false">
                                <Icon PrimaryIconCssClass="rbPrint" PrimaryIconLeft="4" PrimaryIconTop="3" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br />
                            <asp:GridView ID="grdSatisAdetRapor" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
                                Width="100%" CssClass="grid" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                                EmptyDataText="Satış bulunamamıştır!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                EmptyDataRowStyle-CssClass="TdRenkAciklama">
                                <Columns>
                                    <asp:BoundField DataField="TOPLAM SATIŞ" HeaderText="TOPLAM SATIŞ" ItemStyle-Width="11%" />
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
                                    <asp:BoundField DataField="Yuzde(%)" HeaderText="Yüzde(%)" />
                                </Columns>
                            </asp:GridView>

                            <br />
                            <asp:GridView ID="grdSatisTutarRapor" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
                                Width="100%" CssClass="grid" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                                EmptyDataText="Satış tutarı bulunamamıştır!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                EmptyDataRowStyle-CssClass="TdRenkAciklama">
                                <Columns>
                                    <asp:BoundField DataField="TOPLAM TUTAR" HeaderText="TOPLAM TUTAR" ItemStyle-Width="11%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="1" HeaderText="1" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="2" HeaderText="2" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="3" HeaderText="3" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="4" HeaderText="4" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="5" HeaderText="5" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="6" HeaderText="6" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="7" HeaderText="7" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="8" HeaderText="8" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="9" HeaderText="9" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="10" HeaderText="10" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="11" HeaderText="11" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="12" HeaderText="12" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="Yillik" HeaderText="Yıllık" ItemStyle-Width="6%" />
                                    <asp:BoundField DataFormatString="{0:###,###,###.00}" DataField="Yuzde(%)" HeaderText="Yüzde(%)" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlMusteriIl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlMusteriIlce" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AkiosMasterPage.Master" AutoEventWireup="true" CodeBehind="PersonelTanimlama.aspx.cs" Inherits="Akios.AdminWebClient.YonetimKonsolu.PersonelTanimlama" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size: 11pt;">PERSONEL LİSTESİ<br />
            </th>
        </tr>
        <tr>
            <td>
                <div style="padding-top: 25px; text-align: center; width: 45%;">
                    <table class="AnaTablo">
                        <tr>
                            <th>Ad</th>
                            <th>Soyad</th>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txtAd" runat="server"></telerik:RadTextBox></td>
                            <td>
                                <telerik:RadTextBox ID="txtSoyad" runat="server"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnEkle" runat="server" Text="Ekle" OnClick="btnEkle_Click">
                                    <Icon PrimaryIconCssClass="rbAdd" PrimaryIconLeft="4" PrimaryIconTop="3" />
                                </telerik:RadButton>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <br />
                    <asp:Repeater ID="RP_Personel" runat="server" OnItemCommand="RP_Personel_ItemCommand">
                        <HeaderTemplate>
                            <div style="text-align: center; width: 70%;">
                                <table class="grid" style="width: 100%">
                                    <tr>
                                        <th style="width: 15%"></th>
                                        <th>Personel</th>
                                    </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input id="Ad" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "AD") %>' type="hidden" />
                                    <asp:ImageButton OnClientClick=" return confirm('Silmek istediğinize emin misiniz?') " ID="LB_Sil" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' runat="server" ImageUrl="~/App_Themes/Theme/Raster/iptal.gif" />
                                </td>
                                <td><%# DataBinder.Eval(Container.DataItem, "AD") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></table></div></FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

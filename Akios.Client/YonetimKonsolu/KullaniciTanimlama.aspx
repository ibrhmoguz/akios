<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="KullaniciTanimlama.aspx.cs" Inherits="Akios.WebClient.YonetimKonsolu.KullaniciTanimlama" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size: 11pt;">KULLANICI LİSTESİ<br />
            </th>
        </tr>
        <tr>
            <td>
                <div style="padding-top: 25px; text-align: center; width: 45%;">

                    <table class="AnaTablo">
                        <tr>
                            <th>Kullanıcı Adı</th>
                            <th>Yetki</th>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txtKullaniciAdi" runat="server"></telerik:RadTextBox></td>
                            <td>
                                <telerik:RadDropDownList ID="ddlKullaniciRol" runat="server">
                                </telerik:RadDropDownList>
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
                    <asp:Repeater ID="RP_Kullanici" runat="server" OnItemCommand="RP_Kullanici_ItemCommand">
                        <HeaderTemplate>
                            <div style="text-align: center; width: 70%;">
                                <table class="grid" style="width: 100%">
                                    <tr>
                                        <th></th>
                                        <th>Kullanıcı Adı</th>
                                        <th>Rolü</th>
                                    </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input id="kullanici" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "KullaniciAdi") %>' type="hidden" />
                                    <input id="yetki" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "RolAdi") %>' type="hidden" />
                                    <asp:ImageButton OnClientClick=" return confirm('Silmek istediğinize emin misiniz?') " ID="LB_Sil" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KULLANICIADI") %>' runat="server" ImageUrl="~/App_Themes/Theme/Raster/iptal.gif" />
                                </td>
                                <td><%# DataBinder.Eval(Container.DataItem, "KullaniciAdi") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "RolAdi") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></table></div></FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

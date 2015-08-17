<%@ Page Title="" Language="C#" MasterPageFile="~/AkiosMasterPage.Master" AutoEventWireup="true" CodeBehind="KullaniciTanimlama.aspx.cs" Inherits="Akios.AdminWebClient.YonetimKonsolu.KullaniciTanimlama" %>

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
                <table>
                    <tr>
                        <th>Kullanıcı Adı:</th>
                        <td>
                            <telerik:RadTextBox ID="txtKullaniciAdi" runat="server"></telerik:RadTextBox>
                        </td>
                        <th>Yetki:</th>
                        <td>
                            <telerik:RadDropDownList ID="ddlKullaniciRol" runat="server"></telerik:RadDropDownList>
                        </td>
                        <td>
                            <telerik:RadButton ID="btnEkle" runat="server" Text="Ekle" OnClick="btnEkle_Click">
                                <Icon PrimaryIconCssClass="rbAdd" PrimaryIconLeft="4" PrimaryIconTop="3" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Repeater ID="RP_Kullanici" runat="server" OnItemCommand="RP_Kullanici_ItemCommand">
                    <HeaderTemplate>
                        <table class="grid" style="width: 100%">
                            <tr>
                                <th style="width: 5%"></th>
                                <th>Müşteri Adı</th>
                                <th>Kullanıcı Adı</th>
                                <th>Rolü</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <input id="musteri" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Adi") %>' type="hidden" />
                                <input id="kullanici" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "KullaniciID") %>' type="hidden" />
                                <input id="yetki" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "RolAdi") %>' type="hidden" />
                                <asp:ImageButton OnClientClick=" return confirm('Silmek istediğinize emin misiniz?') " ID="LB_Sil" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KullaniciID") %>' runat="server" ImageUrl="~/App_Themes/Theme/Raster/iptal.gif" />
                            </td>
                            <td><%# DataBinder.Eval(Container.DataItem, "Adi") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem, "KullaniciAdi") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem, "RolAdi") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
</asp:Content>

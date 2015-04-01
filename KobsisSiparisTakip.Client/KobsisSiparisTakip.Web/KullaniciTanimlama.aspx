<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="KullaniciTanimlama.aspx.cs" Inherits="KobsisSiparisTakip.Web.KullaniciTanimlama" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size:  11pt;">KULLANICI LİSTESİ<br />
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
                                <telerik:RadDropDownList ID="ddlYetki" runat="server" SelectedText="Seçiniz" SelectedValue="S">
                                    <Items>
                                        <telerik:DropDownListItem runat="server" Selected="True" Text="Seçiniz" Value="S" />
                                        <telerik:DropDownListItem runat="server" Text="Yönetici" Value="admin" />
                                        <telerik:DropDownListItem runat="server" Text="Kullanıcı" Value="user" />
                                    </Items>
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
                    <asp:Repeater ID="RP_Kullanici" runat="server"
                        OnItemCommand="RP_Kullanici_ItemCommand">
                        <HeaderTemplate>
                            <table class="AnaTablo">
                                <tr>
                                    <th></th>
                                    <th>Kullanıcı Adı</th>
                                    <th>Yetki</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input id="kullanici" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "KULLANICIADI") %>' type="hidden" />
                                    <input id="yetki" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "YETKI") %>' type="hidden" />
                                    <asp:LinkButton OnClientClick=" return confirm('Silmek istediğinize emin misiniz?') " ID="LB_Sil" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KULLANICIADI") %>' runat="server">Sil</asp:LinkButton>
                                </td>
                                <td><%# DataBinder.Eval(Container.DataItem, "KULLANICIADI") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "YETKI") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></table></FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

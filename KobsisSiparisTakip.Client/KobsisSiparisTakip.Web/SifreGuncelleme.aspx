<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="SifreGuncelleme.aspx.cs" Inherits="KobsisSiparisTakip.Web.SifreGuncelleme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size:  11pt;">ŞİFRE GÜNCELLEME<br />
            </th>
        </tr>
        <tr>
            <td>
                <div style="padding-top: 25px; text-align: center; width: 45%;">

                    <table class="AnaTablo">
                        <tr>
                            <th>Kullanıcı Adı&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                            <th>Yeni Şifre </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblKullanici" runat="server" Text="Label"></asp:Label></td>
                            <td>
                                <telerik:RadTextBox ID="txtSifre" runat="server"></telerik:RadTextBox>
                                <telerik:RadButton ID="BTN_Guncelle" runat="server" Text="Güncelle" OnClick="BTN_Guncelle_Click">
                                    <Icon PrimaryIconCssClass="rbEdit" PrimaryIconLeft="4" PrimaryIconTop="3" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Repeater ID="RP_Sifre" runat="server"
                        OnItemCommand="RP_Sifre_ItemCommand">
                        <HeaderTemplate>
                            <table class="AnaTablo">
                                <tr>
                                    <th></th>
                                    <th>Kullanıcı Adı  </th>
                                    <th>Şifre</th>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input id="kullanici" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "KULLANICIADI") %>' type="hidden" />
                                    <input id="sifre" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "SIFRE") %>' type="hidden" />
                                </td>
                                <td><%# DataBinder.Eval(Container.DataItem, "KULLANICIADI") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "SIFRE") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>

                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

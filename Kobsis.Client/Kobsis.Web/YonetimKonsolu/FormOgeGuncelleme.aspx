<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="FormOgeGuncelleme.aspx.cs" Inherits="Kobsis.Web.YonetimKonsolu.FormOgeGuncelleme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .rgAdvPart {
            display: none;
        }
    </style>
    <div style="width: 100%">
        <br />
        <table class="AnaTablo" style="width: 100%">
            <tr>
                <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size: 11pt;">FORM ÖĞELERİ<br />
                </th>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <th style="width: 20%">Düzenlemek istediğiniz öğeyi seçiniz : </th>
                <td>
                    <telerik:RadDropDownList ID="ddlOge" runat="server" OnSelectedIndexChanged="ddlOge_SelectedIndexChanged" AutoPostBack="True"></telerik:RadDropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadGrid ID="rgOgeler1" runat="server" AllowPaging="True" OnItemCommand="rgOgeler1_ItemCommand" OnPageIndexChanged="rgOgeler1_PageIndexChanged">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID"
                                    UniqueName="ID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AD" HeaderText="AD" SortExpression="AD"
                                    UniqueName="AD">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="NOVA" HeaderText="NOVA"
                                    UniqueName="NOVA">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="KROMA" HeaderText="KROMA"
                                    UniqueName="KROMA">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="GUARD" HeaderText="GUARD"
                                    UniqueName="GUARD">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="YANGIN" HeaderText="YANGIN/PORTE"
                                    UniqueName="YANGIN">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridButtonColumn Text="Sil" CommandName="Delete" ButtonType="ImageButton" />
                            </Columns>
                            <EditFormSettings>
                                <EditColumn ButtonType="ImageButton" />
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr id="trKayitEkle1" runat="server" visible="false">
                <td colspan="2">
                    <br />
                    <asp:LinkButton ID="lbYeniKayit" runat="server" OnClick="lbYeniKayit_Click"> Yeni Kayıt İçin Tıklayınız </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table runat="server" id="tbKayitEkle1" visible="false">
                        <tr>
                            <th style="width: 10%;">Ad :</th>
                            <td style="width: 30%;">
                                <telerik:RadTextBox ID="txtAd" runat="server"></telerik:RadTextBox>
                            </td>
                            <th style="width: 15%;">Kapı Türü :</th>
                            <td>
                                <asp:CheckBoxList ID="cbxKapiTuru" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                    <asp:ListItem>Nova</asp:ListItem>
                                    <asp:ListItem>Kroma</asp:ListItem>
                                    <asp:ListItem>Guard</asp:ListItem>
                                    <asp:ListItem>Yangin/Porte</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <telerik:RadButton ID="rbKayitEkle" runat="server" Text="Ekle" OnClick="btnEkle_Click">
                                    <Icon PrimaryIconCssClass="rbAdd" PrimaryIconLeft="4" PrimaryIconTop="3" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trKapiModel" runat="server" visible="false">
                <th>Kapı serisini seçiniz : </th>
                <td>
                    <telerik:RadDropDownList ID="ddlKapiSeri" runat="server" OnSelectedIndexChanged="ddlKapiSeri_SelectedIndexChanged" AutoPostBack="True"></telerik:RadDropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2">

                    <telerik:RadGrid ID="rgOgeler2" runat="server" AllowPaging="True" OnItemCommand="rgOgeler1_ItemCommand" OnPageIndexChanged="rgOgeler2_PageIndexChanged">

                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID"
                                    UniqueName="ID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AD" HeaderText="AD"
                                    UniqueName="AD">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn Text="Sil" CommandName="Delete" ButtonType="ImageButton" />
                            </Columns>
                            <EditFormSettings>
                                <EditColumn ButtonType="ImageButton" />
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr id="trKayitEkle2" runat="server" visible="false">
                <td colspan="2">
                    <br />
                    <asp:LinkButton ID="lbYeniKayit2" runat="server" OnClick="lbYeniKayit2_Click"> Yeni Kayıt İçin Tıklayınız </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table runat="server" id="tbKayitEkle2" visible="false">
                        <tr>
                            <th style="width: 10%;">Ad :</th>
                            <td style="width: 30%;">
                                <telerik:RadTextBox ID="txtAd2" runat="server"></telerik:RadTextBox>
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButton1" runat="server" Text="Ekle" OnClick="btnEkle2_Click">
                                    <Icon PrimaryIconCssClass="rbAdd" PrimaryIconLeft="4" PrimaryIconTop="3" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

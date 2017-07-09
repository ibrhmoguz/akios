<%@ Page Title="" Language="C#" MasterPageFile="~/AkiosMasterPage.Master" AutoEventWireup="true" CodeBehind="MusteriTanimlama.aspx.cs" Inherits="Akios.AdminWebClient.YonetimKonsolu.MusteriTanimlama" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size: 11pt;">MÜŞTERİ LİSTESİ<br />
            </th>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <th>Adı:</th>
                        <td>
                            <telerik:RadTextBox ID="txtAd" runat="server" Width="250"></telerik:RadTextBox>
                        </td>
                        <th>Yetkili Kişi:</th>
                        <td>
                            <telerik:RadTextBox ID="txtYetkiliKisi" runat="server"></telerik:RadTextBox>
                        </td>
                        <th>Kod:</th>
                        <td>
                            <telerik:RadTextBox ID="txtKod" runat="server"></telerik:RadTextBox>
                        </td>
                        <th>Web:</th>
                        <td>
                            <telerik:RadTextBox ID="txtWeb" runat="server"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>Mail:</th>
                        <td>
                            <telerik:RadTextBox ID="txtMail" runat="server" Width="250"></telerik:RadTextBox>
                        </td>
                        <th>Tel:</th>
                        <td>
                            <telerik:RadMaskedTextBox ID="txtTel" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                        </td>
                        <th>Cep:</th>
                        <td>
                            <telerik:RadMaskedTextBox ID="txtCep" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                        </td>
                        <th>Faks:</th>
                        <td>
                            <telerik:RadMaskedTextBox ID="txtFaks" runat="server" Mask="(###) ### ## ##" LabelWidth="64px" Width="160px"></telerik:RadMaskedTextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>Adres:</th>
                        <td>
                            <telerik:RadTextBox ID="txtAdres" runat="server" Width="250" TextMode="MultiLine"></telerik:RadTextBox>
                        </td>
                        <th>Logo:</th>
                        <td>
                            <asp:FileUpload ID="logoFileUpload" runat="server" Width="160px" AllowMultiple="false"></asp:FileUpload>
                        </td>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="8" style="text-align: center">
                            <br />
                            <telerik:RadButton ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_OnClick">
                                <Icon PrimaryIconCssClass="rbSave" PrimaryIconLeft="4" PrimaryIconTop="3" />
                            </telerik:RadButton>
                            <telerik:RadButton ID="btnIptal" runat="server" Text="İptal">
                                <Icon PrimaryIconCssClass="rbCancel" PrimaryIconLeft="4" PrimaryIconTop="3" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdMusteriler" runat="server" AutoGenerateColumns="false" AllowPaging="True" DataKeyNames="MusteriID,LogoID"
                    PageSize="30" Width="100%" CssClass="grid" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                    EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataRowStyle-CssClass="TdRenkAciklama" OnRowDataBound="grdMusteriler_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="imgMusteri" runat="server" Width="50" Height="50" />
                            </ItemTemplate>
                            <ItemStyle Width="55"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MusteriID" HeaderText="ID" />
                        <asp:BoundField DataField="Kod" HeaderText="Kod" />
                        <asp:BoundField DataField="YetkiliKisi" HeaderText="Yetkili Kişi" />
                        <asp:BoundField DataField="Adi" HeaderText="Ad" />
                        <asp:BoundField DataField="Adres" HeaderText="Adres" />
                        <asp:BoundField DataField="Mobil" HeaderText="Mobil" />
                        <asp:BoundField DataField="Faks" HeaderText="Faks" />
                        <asp:BoundField DataField="Mail" HeaderText="Mail" />
                        <asp:BoundField DataField="Web" HeaderText="Web" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

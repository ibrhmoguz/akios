<%@ Page Title="" Language="C#" MasterPageFile="~/AkiosMasterPage.Master" AutoEventWireup="true" CodeBehind="SiparisSorgula.aspx.cs" Inherits="Akios.WebClient.Siparis.SiparisSorgula" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divSiparisSorgula" runat="server" style="width: 100%" class="RadGrid_Current_Theme">
        <br />
        <table class="AnaTablo" style="width: 100%">
            <tr>
                <th class="TdRenkAciklama" colspan="2" style="text-align: center; font-size: 11pt;">SİPARİŞ SORGULAMA<br />
                </th>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <th style="width: 8%">Sipariş No : </th>
                            <td>
                                <telerik:RadTextBox ID="txtSiparisNo" runat="server" RenderMode="Lightweight" Width="155"></telerik:RadTextBox>
                            </td>
                            <th>Sipariş Seri: </th>
                            <td>
                                <telerik:RadDropDownList ID="ddlSiparisSeri" runat="server" DataValueField="SiparisSeriID" DataTextField="SeriAdi" AutoPostBack="true"></telerik:RadDropDownList>
                            </td>
                            <th style="width: 8%">Adres : </th>
                            <td>
                                <telerik:RadTextBox ID="txtMusteriAdres" runat="server" Width="250px" RenderMode="Lightweight"></telerik:RadTextBox>
                            </td>

                            <th rowspan="5" style="width: 8%">Teslimat Ekibi</th>
                            <td rowspan="5">
                                <telerik:RadListBox ID="ListBoxTeslimatEkibi" runat="server" Width="160" Height="100%" SelectionMode="Multiple" CheckBoxes="true" DataValueField="ID" DataTextField="AD">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Sipariş Adeti : </th>
                            <td>
                                <telerik:RadTextBox ID="txtSiparisAdeti" runat="server" RenderMode="Lightweight" Width="155"></telerik:RadTextBox>
                            </td>
                            <th style="width: 8%">Adı : </th>
                            <td style="width: 18%">
                                <telerik:RadTextBox ID="txtMusteriAd" runat="server" RenderMode="Lightweight"></telerik:RadTextBox>
                            </td>
                            <th>Soyadı </th>
                            <td>
                                <telerik:RadTextBox ID="txtMusteriSoyad" runat="server" RenderMode="Lightweight"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Sipariş Tarihi : </th>
                            <td>
                                <telerik:RadDatePicker ID="rdtSiparisTarihBas" runat="server" Width="105"></telerik:RadDatePicker>
                                <telerik:RadDatePicker ID="rdtSiparisTarihBit" runat="server" Width="105"></telerik:RadDatePicker>
                            </td>
                            <th>Ev Tel : </th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriEvTel" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                            <th>İl :</th>
                            <td>
                                <telerik:RadComboBox ID="ddlMusteriIl" runat="server" AutoPostBack="true" EmptyMessage="İl Seçiniz" Skin="Telerik" OnSelectedIndexChanged="ddlMusteriIl_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Teslim Tarihi : </th>
                            <td>
                                <telerik:RadDatePicker ID="rdtTeslimTarihBas" runat="server" Width="105"></telerik:RadDatePicker>
                                <telerik:RadDatePicker ID="rdtTeslimTarihBit" runat="server" Width="105"></telerik:RadDatePicker>
                            </td>
                            <th>iş Tel : </th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriIsTel" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                            <th>İlçe :</th>
                            <td>
                                <telerik:RadComboBox ID="ddlMusteriIlce" runat="server" AutoPostBack="True" EmptyMessage="İlçe Seçiniz" RenderMode="Lightweight" OnSelectedIndexChanged="ddlMusteriIlce_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Firma Adı:
                            </th>
                            <td>
                                <telerik:RadTextBox ID="txtMusteriFirmaAdi" runat="server" RenderMode="Lightweight" Width="155"></telerik:RadTextBox>
                            </td>
                            <th>Cep Tel : </th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriCepTel" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                            <th>Semt :</th>
                            <td>
                                <telerik:RadComboBox ID="ddlMusteriSemt" runat="server" AutoPostBack="false" EmptyMessage="Semt Seçiniz" RenderMode="Lightweight">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="divSiparisFormKontrolleri" runat="server" Width="100%"></asp:Panel>
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <telerik:RadButton ID="btnSorgula" runat="server" Text="Sorgula" OnClick="btnSorgula_Click">
                        <Icon PrimaryIconCssClass="rbSearch" />
                    </telerik:RadButton>
                    <telerik:RadButton ID="btnTemizle" runat="server" Text="Temizle" OnClick="btnTemizle_Click">
                        <Icon PrimaryIconCssClass="rbRefresh" />
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
    </div>
    <table id="tblSonuc" runat="server" visible="false" style="width: 100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <th>
                            <h5>TOPLAM SİPARİŞ ADEDİ:</h5>
                        </th>
                        <th>
                            <asp:Label ID="lblToplamSiparis" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        </th>
                        <th>
                            <h5>TOPLAM ÜRÜN ADEDİ:</h5>
                        </th>
                        <th>
                            <asp:Label ID="lblToplamUrun" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label></th>
                    </tr>
                </table>
                <asp:GridView ID="grdSiparisler" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnPageIndexChanging="grdSiparisler_PageIndexChanging"
                    OnRowDataBound="grdSiparisler_RowDataBound" Width="100%" CssClass="grid" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
                    EmptyDataText="Sipariş bulunamamıştır!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataRowStyle-CssClass="TdRenkAciklama">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="SeriAdi" HeaderText="SERİ ADI" />
                        <asp:BoundField DataField="SiparisNo" HeaderText="SİPARİŞ NO" />
                        <asp:BoundField DataField="Adet" HeaderText="SİPARİŞ ADEDİ" />
                        <asp:BoundField DataField="SiparisTarih" HeaderText="SİPARİŞ TARİHİ" />
                        <asp:BoundField DataField="TeslimTarih" HeaderText="TESLİM TARİHİ" />
                        <asp:BoundField DataField="Musteri" HeaderText="MÜŞTERİ/FİRMA" />
                        <asp:BoundField DataField="MusteriAdres" HeaderText="MÜŞTERİ ADRES" />
                        <asp:BoundField DataField="IlAd" HeaderText="İL" />
                        <asp:BoundField DataField="IlceAd" HeaderText="İLÇE" />
                        <asp:BoundField DataField="SemtAd" HeaderText="SEMT" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkGoruntule" runat="server" Text="Siparişi Görüntüle" ForeColor="Blue" Font-Underline="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlMusteriIl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlMusteriIlce" />
                    <telerik:AjaxUpdatedControl ControlID="ddlMusteriSemt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlMusteriIlce">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlMusteriSemt" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
</asp:Content>

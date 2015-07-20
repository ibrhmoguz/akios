<%@ Page Title="" Language="C#" MasterPageFile="~/AkiosMasterPage.Master" AutoEventWireup="true" CodeBehind="MusteriTanimlama.aspx.cs" Inherits="AkiosAkios.AdminWebClient.YonetimKonsolu.MusteriTanimlama" %>

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
                <div style="padding-top: 25px; text-align: center; width: 45%;">
                    <table class="AnaTablo" style="width: 100%">
                        <tr>
                            <th style="width: 12%">Müşteri Kod : </th>
                            <td style="width: 20%">
                                <telerik:RadTextBox ID="txtKod" runat="server" Enabled="True" RenderMode="Lightweight" Width="155"></telerik:RadTextBox>
                            </td>
                            <th style="width: 10%">Müşteri Ad : </th>
                            <td style="width: 20%">
                                <telerik:RadTextBox ID="txtMusteriAd" runat="server" RenderMode="Lightweight"></telerik:RadTextBox>
                            </td>
                            <th rowspan="2" style="width: 10%">Müşteri Adres : </th>
                            <td rowspan="2">
                                <telerik:RadTextBox ID="txtMusteriAdres" runat="server" TextMode="MultiLine" Height="50px" Width="250px" RenderMode="Lightweight"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Yetkili Kişi : </th>
                            <td>
                                <telerik:RadTextBox ID="txtYetkiliKisi" runat="server" Text="1" RenderMode="Lightweight" Width="155"></telerik:RadTextBox>
                            </td>
                            <th>Müşteri iş Tel : </th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriIsTel" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Müşteri Web Adres : </th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriWeb" runat="server" Text="www.musteriweb.com.tr" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                            <th>Müşteri Cep Tel : </th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriCepTel" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                            <th>Müşteri Logo :</th>
                            <td>
                                <telerik:RadAsyncUpload runat="server" ID="ulMusteriLogo"
                            TargetFolder="../../Logos/"
                            HideFileInput="true"
                            Skin="Telerik"
                            MultipleFileSelection="Automatic"
                            AllowedFileExtensions=".jpeg,.jpg,.png"
                            OnClientFileUploadFailed="onUploadFailed" OnClientFileSelected="onFileSelected"
                            OnClientFileUploaded="onFileUploaded" Height="25px" Width="250px" />
                        <span class="allowed-attachments">Logoyu seçiniz... (<%= String.Join( ",", ulMusteriLogo.AllowedFileExtensions ) %>)
                        </span>
                            </td>
                        </tr>
                        <tr>
                            <th>Müşteri E-Mail Adres : </th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriEmail" runat="server" Text="musterimail@musteriweb.com.tr" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                            
                            <th>Müşteri Faks :</th>
                            <td>
                                <telerik:RadMaskedTextBox ID="txtMusteriFaks" runat="server" Mask="(###) ### ## ##" RenderMode="Lightweight"></telerik:RadMaskedTextBox>
                            </td>
                            <td>

                            </td>
                            <td>
                                <telerik:RadBinaryImage runat="server" ID="biMusteriLogo" DataValue='<%# Eval("Photo") == DBNull.Value? new System.Byte[0]: Eval("Photo") %>'
                                AutoAdjustImageControlSize="false" Width="110px" Height="110px"/>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="divSiparisFormKontrolleri" runat="server" Width="100%"></asp:Panel>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <br />
                                <telerik:RadButton ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_Click">
                                    <Icon PrimaryIconCssClass="rbSave" PrimaryIconLeft="4" PrimaryIconTop="3" />
                                </telerik:RadButton>
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
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

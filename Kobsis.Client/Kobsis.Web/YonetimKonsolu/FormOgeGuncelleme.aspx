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
                    <telerik:RadDropDownList ID="ddlReferanslar" runat="server" OnSelectedIndexChanged="ddlReferanslar_SelectedIndexChanged" AutoPostBack="True" RenderMode="Lightweight"></telerik:RadDropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:GridView ID="gvReferansDetay" runat="server" AutoGenerateColumns="true" Width="100%" CssClass="AnaTablo" ShowFooter="True"
                        AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2" EmptyDataText="Form öğe detayı tanımlanmamıştır!"
                        EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                        EmptyDataRowStyle-CssClass="TdRenkAciklama" FooterStyle="ThBaslikRenk2" OnRowDeleting="gvReferansDetay_RowDeleting" OnRowDataBound="gvReferansDetay_RowDataBound">
                        <Columns>
                            <asp:CommandField HeaderText="SİL" DeleteText="Sil" DeleteImageUrl="~/App_Themes/Theme/Raster/clear.png" ShowDeleteButton="true" ButtonType="Image" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

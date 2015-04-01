<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="Hata.aspx.cs" Inherits="KobsisSiparisTakip.Web.Hata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <td class="TdRenkAciklama" style="font-size: large; text-align: center; color: red">
                <b>HATA OLUŞTU!</b>
            </td>
        </tr>
        <tr>
            <td>Uygulamada hata oluştu ve hata kayıt edildi. Menüyü kullanarak işleminize devam edebilirsiniz.
            </td>
        </tr>
    </table>
</asp:Content>

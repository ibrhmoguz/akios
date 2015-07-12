<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="Hatalar.aspx.cs" Inherits="Akios.WebClient.YonetimKonsolu.Hatalar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-top: 15px; text-align: center; width: 100%;">
        <br />
        Hata Sayısı:
        <asp:Label ID="lblhataSayisi" runat="server"></asp:Label>

        <asp:GridView ID="grdHatalar" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
            Width="100%" CssClass="grid" AlternatingRowStyle-BackColor="Wheat" HeaderStyle-CssClass="ThBaslikRenk2"
            EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
            EmptyDataRowStyle-CssClass="TdRenkAciklama">
            <Columns>
                <asp:BoundField DataField="EXCEPTION" HeaderText="Hata" />
                <asp:BoundField DataField="METHODNAME" HeaderText="Metod" />
                <asp:BoundField DataField="USERNAME" HeaderText="Kullanici" />
                <asp:BoundField DataField="DATE" HeaderText="Tarih" />
            </Columns>
        </asp:GridView>

        <br />
    </div>
</asp:Content>

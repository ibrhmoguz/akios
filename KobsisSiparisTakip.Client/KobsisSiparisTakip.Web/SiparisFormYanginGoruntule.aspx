<%@ Page Title="" Language="C#" MasterPageFile="~/KobsisMasterPage.Master" AutoEventWireup="true" CodeBehind="SiparisFormYanginGoruntule.aspx.cs" Inherits="KobsisSiparisTakip.Web.SiparisFormYanginGoruntule" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <td rowspan="6" style="text-align: center">
                <telerik:RadBinaryImage ID="imgLogo" runat="server" ImageUrl="~/App_Themes/Theme/Raster/ackLogo.PNG" />
            </td>
            <td colspan="2" rowspan="3" style="width: 45%; font-size: x-large; text-align: center;">

                <b>
                    <asp:Label ID="lblKapiTur" runat="server"></asp:Label></b>
            </td>
            <td style="width: 30%; text-align: left">
                <b>ANKARA ÇELİK KAPI SAN. TİC. LTD. ŞTİ. </b>
            </td>
        </tr>
        <tr>

            <td style="font-size: smaller; text-align: left">
                <b>Adres: </b>Alınteri Bulvarı No:212 Ostim/ANKARA
            </td>
        </tr>
        <tr>
            <td style="font-size: smaller; text-align: left">
                <b>Telefon: </b>(0 312) 385 37 83 - 84
            </td>

        </tr>
        <tr>
            <td colspan="2" rowspan="3" style="font-size: x-large; text-align: center">
                <b>SİPARİŞ FORMU</b>
            </td>
            <td style="font-size: smaller; text-align: left">
                <b>Faks : </b>(0 312) 354 61 81
            </td>

        </tr>
        <tr>
            <td style="font-size: smaller; text-align: left">
                <b>Web : </b>www.ankaracelikkapi.com.tr
            </td>

        </tr>
        <tr>
            <td style="font-size: smaller; text-align: left">
                <b>e-posta : </b>ankara@celikkapi.net
            </td>

        </tr>

    </table>
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th style="width: 15%">Ölçü Tarihi:   </th>
            <td style="width: 35%">
                <asp:Label ID="lblOlcuTarihSaat" runat="server"></asp:Label>
            </td>
            <th style="width: 10%">Sipariş Tarihi :  </th>
            <td>
                <asp:Label ID="lblSiparisTarih" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Bayi Adı : </th>
            <td>
                <asp:Label ID="lblBayiAdi" runat="server"></asp:Label>
            </td>
            <th>Sipariş No : </th>
            <td>
                <asp:Label ID="lblSiparisNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <th>Sipariş Durumu : </th>
            <td>
                <asp:Label ID="lblSiparisDurum" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th colspan="4">MÜŞTERİ/FİRMA BİLGİLERİ </th>
        </tr>
        <tr>
            <th style="width: 15%">Firma Adı:
            </th>
            <td style="width: 35%">
                <asp:Label ID="lblFirmaAdi" runat="server"></asp:Label>
            </td>
            <th style="width: 10%">Sipariş Adedi: </th>
            <td>
                <asp:Label ID="lblSiparisAdedi" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Adı : </th>
            <td>
                <asp:Label ID="lblAd" runat="server"></asp:Label>
            </td>
            <th rowspan="5">Adresi : </th>
            <td rowspan="5">
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblAdres" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr>
            <th style="width: 10%">Soyadı: </th>
            <td>
                <asp:Label ID="lblSoyad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>

            <th>Ev Tel : </th>
            <td>
                <asp:Label ID="lblEvTel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>iş Tel : </th>
            <td>
                <asp:Label ID="lblIsTel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Cep Tel : </th>
            <td>
                <asp:Label ID="lblCepTel" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>

            <th style="width: 15%">Dış Kapı Modeli :</th>
            <td style="width: 35%">
                <asp:Label ID="lblDisKapiModeli" runat="server"></asp:Label>
            </td>
            <th style="width: 10%">Kapı Cinsi :</th>
            <td>
                <asp:Label ID="lblYanginKapiCins" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <th style="width: 10%">İç Kapı Modeli :</th>
            <td>
                <asp:Label ID="lblIcKapiModeli" runat="server"></asp:Label>
            </td>

            <th>Kasa Tipi :</th>
            <td>
                <asp:Label ID="lblYanginKasaTipi" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Metal Rengi :</th>
            <td>
                <asp:Label ID="lblYanginMetalRengi" runat="server"></asp:Label>
            </td>
            <th>Eşik :</th>
            <td>
                <asp:Label ID="lblEsik" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <th>Kilit Sistemi :</th>
            <td>
                <asp:Label ID="lblKilitSistemi" runat="server"></asp:Label>
            </td>
            <th>Çekme Kolu :</th>
            <td>
                <asp:Label ID="lblCekmeKolu" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="trYangin1" runat="server" visible="false">
            <th>Panik Bar :</th>
            <td>
                <asp:Label ID="lblYanginPanikBar" runat="server"></asp:Label>
            </td>
            <th>Kol ve Dış Müdahale Kolu :</th>
            <td>
                <asp:Label ID="lblYanginKol" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="trYangin2" runat="server" visible="false">
            <th>Menteşe Tipi :</th>
            <td>
                <asp:Label ID="lblYanginMenteseTip" runat="server"></asp:Label>
            </td>
            <th>Hidrolik Kapatıcı :</th>
            <td>
                <asp:Label ID="lblYanginHidrolikKapatici" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="trPorte1" runat="server" visible="false">
            <th>Cumba :</th>
            <td>
                <asp:Label ID="lblCumba" runat="server"></asp:Label>
            </td>
            <th>Dürbün :</th>
            <td>
                <asp:Label ID="lblDurbun" runat="server"> </asp:Label>

            </td>
        </tr>
        <tr id="trPorte2" runat="server" visible="false">
            <th>Barel :</th>
            <td>
                <asp:Label ID="lblBarelTipi" runat="server"></asp:Label>
            </td>
            <th>Taktak :</th>
            <td>
                <asp:Label ID="lblTaktak" runat="server"></asp:Label>
            </td>
        </tr>

    </table>
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <td rowspan="10" colspan="2" style="width: 20%">
                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" ImageUrl="~/App_Themes/Theme/Raster/olcu3.png" />
            </td>
            <th colspan="4" style="text-align: center">ÖLÇÜM ve MONTAJ</th>
        </tr>
        <tr>
            <th style="width: 10%">Ölçümü Alan Kişi : </th>
            <td style="width: 20%">
                <asp:Label ID="lblOlcumAlan" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <th  style="width: 10%">Teslim Tarihi:</th>
            <td>
                <asp:Label ID="lblTeslimTarihi" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Montaj Şekli: </th>
            <td>
                <asp:Label ID="lblMontajSekli" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <th>Teslim Şekli: </th>
            <td>
                <asp:Label ID="lblTeslimSekli" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>İç Kasa Genişliği:</th>
            <td>
                <asp:Label ID="lblIcKasaGenisligi" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <th>Dış Sol Pervaz:</th>
            <td>
                <asp:Label ID="lblDisSolPervaz" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>İç Kasa Yüksekliği:</th>
            <td>
                <asp:Label ID="lblIcKasaYuksekligi" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <th>Dış Sağ Pervaz:</th>
            <td>
                <asp:Label ID="lblDisSagPervaz" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Duvar Kalınlığı:</th>
            <td>
                <asp:Label ID="lblDuvarKalinligi" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <th>Dış Üst Pervaz:</th>
            <td>
                <asp:Label ID="lblDisUstPervaz" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Açılım:</th>
            <td>
                <asp:Label ID="lblAcilim" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <th>İç Sol Pervaz:</th>
            <td>
                <asp:Label ID="lblIcSolPervaz" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>Dış Kasa İç Pervaz Farkı:</th>
            <td>
                <asp:Label ID="lblDisKasaIcPervazFarki" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <th>İç Sağ Pervaz:</th>
            <td>
                <asp:Label ID="lblIcSagPervaz" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>İç Üst Pervaz:</th>
            <td>
                <asp:Label ID="lblIcUstPervaz" runat="server" RenderMode="Lightweight"></asp:Label>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <th>Üretim Notları: </th>
            <td colspan="3">
                <asp:Label ID="lblOlcumBilgileri" runat="server" Width="100%" TextMode="MultiLine" RenderMode="Lightweight"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table class="AnaTablo" style="width: 100%">
        <tr>
            <th>NOT
            </th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNot" runat="server"></asp:Label>
            </td>
        </tr>

    </table>
    <br />
    <table class="AnaTablo" runat="server" id="tbMusteriSozlesme" style="width: 100%">
        <tr>
            <th colspan="2" style="text-align: center; font-size: large;">MÜŞTERİ SÖZLEŞMESİ </th>
        </tr>
        <tr>
            <td style="width: 45%">
                <table>
                    <tr>
                        <th style="width: 15%">Adı Soyadı : </th>
                        <td style="width: 35%">
                            <asp:Label ID="lblMusteriAdSoyad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Adresi : </th>
                        <td>
                            <asp:Label ID="lblMusteriAdres" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Cep Tel : </th>
                        <td>
                            <asp:Label ID="lblMusteriCepTel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Fiyat : </th>
                        <td>
                            <asp:Label ID="lblFiyat" runat="server" CssClass="NumericFieldClass"></asp:Label>
                            TL
                        </td>
                    </tr>
                    <tr>
                        <th>Vergi Dairesi : </th>
                        <td>
                            <asp:Label ID="lblVergiDairesi" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Vergi Numarası : </th>
                        <td>
                            <asp:Label ID="lblVergiNumarasi" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: left">
                <table>
                    <tr>
                        <td></td>
                        <th style="text-align: center">Peşin</th>
                        <th style="text-align: center">Kalan</th>
                        <th style="text-align: center">Ödeme Notu</th>
                    </tr>
                    <tr>
                        <th>Nakit:</th>
                        <td>
                            <asp:Label ID="lblNakitPesin" runat="server" CssClass="NumericFieldClass"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNakitKalan" runat="server" CssClass="NumericFieldClass"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNakitOdemeNotu" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Kredi Kartı:</th>
                        <td>
                            <asp:Label ID="lblKKartiPesin" runat="server" CssClass="NumericFieldClass"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblKKartiKalan" runat="server" CssClass="NumericFieldClass"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblKKartiOdemeNotu" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Çek:</th>
                        <td>
                            <asp:Label ID="lblCekPesin" runat="server" CssClass="NumericFieldClass"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCekKalan" runat="server" CssClass="NumericFieldClass"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCekOdemeNotu" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">Yukarıda yazılı olan şartlarda sipariş verdim. İhtilaf halinde Ankara Mahkemeleri yetkilidir.
                    <b>Müşteri tarafından aksi yazılı olarak Ankara Çelik Kapı'ya bildirilmedikçe kapıların ölçüleri ve
                    açılış yönleri mevcut takılı olan kapıya göre imal edilecektir.</b>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <b>MÜŞTERİ
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       SİPARİŞ ALAN YETKİLİ</b>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <br />
                <telerik:RadButton ID="btnGuncelle" runat="server" Text="Güncelle" OnClick="btnGuncelle_Click">
                    <Icon PrimaryIconCssClass="rbOk" PrimaryIconLeft="4" PrimaryIconTop="3" />
                </telerik:RadButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadButton ID="btnYazdir" runat="server" Text="Yazdır">
                        <Icon PrimaryIconCssClass="rbPrint" PrimaryIconLeft="4" PrimaryIconTop="3" />
                    </telerik:RadButton>
            </td>
        </tr>
    </table>
</asp:Content>

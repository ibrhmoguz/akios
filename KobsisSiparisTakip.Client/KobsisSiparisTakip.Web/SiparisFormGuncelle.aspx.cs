using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Business.DataTypes;
using KobsisSiparisTakip.Web.Helper;
using Telerik.Web.UI;
using System.Globalization;

namespace KobsisSiparisTakip.Web
{
    public partial class SiparisFormGuncelle : KobsisBasePage
    {
        public string SiparisID
        {
            get
            {
                if (!String.IsNullOrEmpty(Request.QueryString["SiparisID"]))
                {
                    return Request.QueryString["SiparisID"].ToString();
                }
                else
                    return String.Empty;
            }
        }

        public string SeriAdi
        {
            get
            {
                if (!String.IsNullOrEmpty(Request.QueryString["SeriAdi"]))
                {
                    return Request.QueryString["SeriAdi"].ToString();
                }
                else
                    return String.Empty;
            }
        }

        public KapiTipi KapiTip
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Request.QueryString["SeriAdi"]))
                {
                    string tip = Request.QueryString["SeriAdi"].ToString();
                    if (tip == KapiTipi.Nova.ToString().ToUpper())
                        return KapiTipi.Nova;
                    else if (tip == KapiTipi.Kroma.ToString().ToUpper())
                        return KapiTipi.Kroma;
                    else
                        return KapiTipi.Guard;
                }
                else
                    return KapiTipi.Nova;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FormDoldur();
                FormBilgileriniGetir();
            }
        }

        private void FormBilgileriniGetir()
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("ID", this.SiparisID);

            DataTable dtSiparis = new SiparisIslemleriBS().SiparisBilgileriniGetir(prms);
            if (dtSiparis.Rows.Count == 0)
                return;

            DataRow rowSiparis = dtSiparis.Rows[0];

            DataTable dtMontaj = new MontajBS().MontajBilgisiGetir(this.SiparisID);
            if (dtMontaj.Rows.Count == 0)
                return;

            DataRow rowMontaj = dtMontaj.Rows[0];

            Musteri musteri = new Musteri();
            Siparis siparis = new Siparis();
            Olcum olcum = new Olcum();
            Sozlesme sozlesme = new Sozlesme();

            musteri.MusteriAd = (rowSiparis["MUSTERIAD"] != DBNull.Value) ? rowSiparis["MUSTERIAD"].ToString() : String.Empty;
            musteri.MusteriSoyad = (rowSiparis["MUSTERISOYAD"] != DBNull.Value) ? rowSiparis["MUSTERISOYAD"].ToString() : String.Empty;
            musteri.MusteriAdres = (rowSiparis["MUSTERIADRES"] != DBNull.Value) ? rowSiparis["MUSTERIADRES"].ToString() : String.Empty;
            musteri.MusteriCepTel = (rowSiparis["MUSTERICEPTEL"] != DBNull.Value) ? rowSiparis["MUSTERICEPTEL"].ToString() : String.Empty;
            musteri.MusteriEvTel = (rowSiparis["MUSTERIEVTEL"] != DBNull.Value) ? rowSiparis["MUSTERIEVTEL"].ToString() : String.Empty;
            musteri.MusteriIl = (rowSiparis["MUSTERIIL"] != DBNull.Value) ? rowSiparis["MUSTERIIL"].ToString() : String.Empty;
            musteri.MusteriIlce = (rowSiparis["MUSTERIILCE"] != DBNull.Value) ? rowSiparis["MUSTERIILCE"].ToString() : String.Empty;
            musteri.MusteriIsTel = (rowSiparis["MUSTERIISTEL"] != DBNull.Value) ? rowSiparis["MUSTERIISTEL"].ToString() : String.Empty;

            siparis.SiparisID = (rowSiparis["ID"] != DBNull.Value) ? rowSiparis["ID"].ToString() : String.Empty;
            siparis.SiparisNo = (rowSiparis["SIPARISNO"] != DBNull.Value) ? rowSiparis["SIPARISNO"].ToString() : String.Empty;
            siparis.FirmaAdi = (rowSiparis["FIRMAADI"] != DBNull.Value) ? rowSiparis["FIRMAADI"].ToString() : String.Empty;
            siparis.AksesuarRenk = (rowSiparis["AKSESUARRENK"] != DBNull.Value) ? rowSiparis["AKSESUARRENK"].ToString() : String.Empty;
            siparis.AluminyumRenk = (rowSiparis["ALUMINYUMRENK"] != DBNull.Value) ? rowSiparis["ALUMINYUMRENK"].ToString() : String.Empty;
            siparis.Baba = (rowSiparis["BABA"] != DBNull.Value) ? rowSiparis["BABA"].ToString() : String.Empty;
            siparis.BarelTip = (rowSiparis["BARELTIP"] != DBNull.Value) ? rowSiparis["BARELTIP"].ToString() : String.Empty;
            siparis.BayiAd = (rowSiparis["BAYIADI"] != DBNull.Value) ? rowSiparis["BAYIADI"].ToString() : String.Empty;
            siparis.CekmeKolu = (rowSiparis["CEKMEKOLU"] != DBNull.Value) ? rowSiparis["CEKMEKOLU"].ToString() : String.Empty;
            siparis.Cita = (rowSiparis["CITA"] != DBNull.Value) ? rowSiparis["CITA"].ToString() : String.Empty;
            siparis.ContaRenk = (rowSiparis["CONTARENK"] != DBNull.Value) ? rowSiparis["CONTARENK"].ToString() : String.Empty;
            siparis.DisKapiModel = (rowSiparis["DISKAPIMODEL"] != DBNull.Value) ? rowSiparis["DISKAPIMODEL"].ToString() : String.Empty;
            siparis.DisKapiRenk = (rowSiparis["DISKAPIRENK"] != DBNull.Value) ? rowSiparis["DISKAPIRENK"].ToString() : String.Empty;
            siparis.Durbun = (rowSiparis["DURBUN"] != DBNull.Value) ? rowSiparis["DURBUN"].ToString() : String.Empty;
            siparis.KayitYapmayanKamera = (rowSiparis["KAYITSIZKAMERA"] != DBNull.Value) ? rowSiparis["KAYITSIZKAMERA"].ToString() : String.Empty;
            siparis.KayitYapanKamera = (rowSiparis["KAYITYAPANKAMERA"] != DBNull.Value) ? rowSiparis["KAYITYAPANKAMERA"].ToString() : String.Empty;
            siparis.Alarm = (rowSiparis["ALARM"] != DBNull.Value) ? rowSiparis["ALARM"].ToString() : String.Empty;
            siparis.OtomatikKilit = (rowSiparis["OTOKILIT"] != DBNull.Value) ? rowSiparis["OTOKILIT"].ToString() : String.Empty;
            siparis.Esik = (rowSiparis["ESIK"] != DBNull.Value) ? rowSiparis["ESIK"].ToString() : String.Empty;
            siparis.IcKapiModel = (rowSiparis["ICKAPIMODEL"] != DBNull.Value) ? rowSiparis["ICKAPIMODEL"].ToString() : String.Empty;
            siparis.IcKapiRenk = (rowSiparis["ICKAPIRENK"] != DBNull.Value) ? rowSiparis["ICKAPIRENK"].ToString() : String.Empty;
            siparis.KilitSistem = (rowSiparis["KILITSISTEM"] != DBNull.Value) ? rowSiparis["KILITSISTEM"].ToString() : String.Empty;
            siparis.PervazTip = (rowSiparis["PERVAZTIP"] != DBNull.Value) ? rowSiparis["PERVAZTIP"].ToString() : String.Empty;
            siparis.TacTip = (rowSiparis["TACTIP"] != DBNull.Value) ? rowSiparis["TACTIP"].ToString() : String.Empty;
            //
            siparis.IcPervazRenk = (rowSiparis["ICPERVAZRENK"] != DBNull.Value) ? rowSiparis["ICPERVAZRENK"].ToString() : String.Empty;
            siparis.DisPervazRenk = (rowSiparis["DISPERVAZRENK"] != DBNull.Value) ? rowSiparis["DISPERVAZRENK"].ToString() : String.Empty;
            siparis.AplikeRenk = (rowSiparis["APLIKERENK"] != DBNull.Value) ? rowSiparis["APLIKERENK"].ToString() : String.Empty;
            siparis.Kanat = (rowSiparis["KANAT"] != DBNull.Value) ? rowSiparis["KANAT"].ToString() : String.Empty;
            siparis.KasaCitaRenk = (rowSiparis["KASACITARENK"] != DBNull.Value) ? rowSiparis["KASACITARENK"].ToString() : String.Empty;
            siparis.ZirhTip = (rowSiparis["ZIRHTIP"] != DBNull.Value) ? rowSiparis["ZIRHTIP"].ToString() : String.Empty;
            siparis.ZirhRenk = (rowSiparis["ZIRHRENK"] != DBNull.Value) ? rowSiparis["ZIRHRENK"].ToString() : String.Empty;
            siparis.CekmeKolTakilmaSekli = (rowSiparis["CEKMEKOLTAKILMASEKLI"] != DBNull.Value) ? rowSiparis["CEKMEKOLTAKILMASEKLI"].ToString() : String.Empty;
            siparis.CekmeKolRenk = (rowSiparis["CEKMEKOLRENK"] != DBNull.Value) ? rowSiparis["CEKMEKOLRENK"].ToString() : String.Empty;
            siparis.BolmeKayitSayi = (rowSiparis["BOLMEKAYITSAYI"] != DBNull.Value) ? rowSiparis["BOLMEKAYITSAYI"].ToString() : String.Empty;
            siparis.CamTip = (rowSiparis["CAMTIP"] != DBNull.Value) ? rowSiparis["CAMTIP"].ToString() : String.Empty;
            siparis.Ferforje = (rowSiparis["FERFORJE"] != DBNull.Value) ? rowSiparis["FERFORJE"].ToString() : String.Empty;
            siparis.FerforjeRenk = (rowSiparis["FERFORJERENK"] != DBNull.Value) ? rowSiparis["FERFORJERENK"].ToString() : String.Empty;
            siparis.MetalRenk = (rowSiparis["METALRENK"] != DBNull.Value) ? rowSiparis["METALRENK"].ToString() : String.Empty;
            siparis.KasaKaplama = (rowSiparis["KASAKAPLAMA"] != DBNull.Value) ? rowSiparis["KASAKAPLAMA"].ToString() : String.Empty;
            siparis.YanginKapiCins = (rowSiparis["YANGINKAPICINS"] != DBNull.Value) ? rowSiparis["YANGINKAPICINS"].ToString() : String.Empty;
            //
            siparis.SiparisTarih = (rowSiparis["SIPARISTARIH"] != DBNull.Value) ? Convert.ToDateTime(rowSiparis["SIPARISTARIH"].ToString()) : DateTime.MinValue;

            if (rowSiparis["NAKITPESIN"] != DBNull.Value)
                siparis.NakitPesin = rowSiparis["NAKITPESIN"].ToString();
            if (rowSiparis["NAKITKALAN"] != DBNull.Value)
                siparis.NakitKalan = rowSiparis["NAKITKALAN"].ToString();
            siparis.NakitOdemeNot = (rowSiparis["NAKITODEMENOTU"] != DBNull.Value) ? rowSiparis["NAKITODEMENOTU"].ToString() : String.Empty;
            if (rowSiparis["KKARTPESIN"] != DBNull.Value)
                siparis.KKartiPesin = rowSiparis["KKARTPESIN"].ToString();
            if (rowSiparis["KKARTKALAN"] != DBNull.Value)
                siparis.KKartiKalan = rowSiparis["KKARTKALAN"].ToString();
            if (rowSiparis["KKARTODEMENOTU"] != DBNull.Value)
                siparis.KKartiOdemeNot = rowSiparis["KKARTODEMENOTU"].ToString();
            if (rowSiparis["CEKPESIN"] != DBNull.Value)
                siparis.CekPesin = rowSiparis["CEKPESIN"].ToString();
            if (rowSiparis["CEKKALAN"] != DBNull.Value)
                siparis.CekKalan = rowSiparis["CEKKALAN"].ToString();

            siparis.CekOdemeNot = (rowSiparis["CEKODEMENOTU"] != DBNull.Value) ? rowSiparis["CEKODEMENOTU"].ToString() : String.Empty;
            siparis.Taktak = (rowSiparis["TAKTAK"] != DBNull.Value) ? rowSiparis["TAKTAK"].ToString() : String.Empty;
            siparis.KapiTipi = this.KapiTip.ToString();
            siparis.Durum = (rowSiparis["DURUM"] != DBNull.Value) ? rowSiparis["DURUM"].ToString() : String.Empty;
            siparis.SiparisAdedi = (rowSiparis["ADET"] != DBNull.Value) ? rowSiparis["ADET"].ToString() : String.Empty;
            siparis.Not = (rowSiparis["SIPARISNOT"] != DBNull.Value) ? rowSiparis["SIPARISNOT"].ToString() : String.Empty;

            olcum.MontajdaTakilacak = (rowSiparis["MONTAJDATAKILACAK"] != DBNull.Value) ? rowSiparis["MONTAJDATAKILACAK"].ToString() : String.Empty;
            olcum.MontajSekli = (rowSiparis["MONTAJSEKLI"] != DBNull.Value) ? rowSiparis["MONTAJSEKLI"].ToString() : String.Empty;
            olcum.OlcumAlanKisi = (rowSiparis["OLCUMALANKISI"] != DBNull.Value) ? rowSiparis["OLCUMALANKISI"].ToString() : String.Empty;
            olcum.OlcumBilgi = (rowSiparis["OLCUMBILGI"] != DBNull.Value) ? rowSiparis["OLCUMBILGI"].ToString() : String.Empty;
            if (rowSiparis["OLCUMTARIH"] != DBNull.Value)
                olcum.OlcumTarih = Convert.ToDateTime(rowSiparis["OLCUMTARIH"].ToString());
            olcum.TeslimSekli = (rowSiparis["TESLIMSEKLI"] != DBNull.Value) ? rowSiparis["TESLIMSEKLI"].ToString() : String.Empty;

            olcum.IcKasaGenislik = (rowSiparis["ICKASAGENISLIK"] != DBNull.Value) ? rowSiparis["ICKASAGENISLIK"].ToString() : String.Empty;
            olcum.IcKasaYukseklik = (rowSiparis["ICKASAYUKSEKLIK"] != DBNull.Value) ? rowSiparis["ICKASAYUKSEKLIK"].ToString() : String.Empty;
            olcum.DisKasaIcPervazFark = (rowSiparis["DISKASAICPERVAZFARK"] != DBNull.Value) ? rowSiparis["DISKASAICPERVAZFARK"].ToString() : String.Empty;
            olcum.DuvarKalinlik = (rowSiparis["DUVARKALINLIK"] != DBNull.Value) ? rowSiparis["DUVARKALINLIK"].ToString() : String.Empty;
            olcum.DisSolPervaz = (rowSiparis["DISSOLPERVAZ"] != DBNull.Value) ? rowSiparis["DISSOLPERVAZ"].ToString() : String.Empty;
            olcum.DisUstPervaz = (rowSiparis["DISUSTPERVAZ"] != DBNull.Value) ? rowSiparis["DISUSTPERVAZ"].ToString() : String.Empty;
            olcum.DisSagPervaz = (rowSiparis["DISSAGPERVAZ"] != DBNull.Value) ? rowSiparis["DISSAGPERVAZ"].ToString() : String.Empty;
            olcum.IcSolPervaz = (rowSiparis["ICSOLPERVAZ"] != DBNull.Value) ? rowSiparis["ICSOLPERVAZ"].ToString() : String.Empty;
            olcum.IcUstPervaz = (rowSiparis["ICUSTPERVAZ"] != DBNull.Value) ? rowSiparis["ICUSTPERVAZ"].ToString() : String.Empty;
            olcum.IcSagPervaz = (rowSiparis["ICSAGPERVAZ"] != DBNull.Value) ? rowSiparis["ICSAGPERVAZ"].ToString() : String.Empty;
            olcum.Acilim = (rowSiparis["ACILIM"] != DBNull.Value) ? rowSiparis["ACILIM"].ToString() : String.Empty;

            sozlesme.KalanOdeme = (rowSiparis["KALANODEME"] != DBNull.Value) ? rowSiparis["KALANODEME"].ToString() : String.Empty;
            sozlesme.MontajDurum = "A";
            sozlesme.MontajTeslimTarih = (rowMontaj["TESLIMTARIH"] != DBNull.Value) ? Convert.ToDateTime(rowMontaj["TESLIMTARIH"].ToString()) : DateTime.MinValue;
            sozlesme.Pesinat = (rowSiparis["PESINAT"] != DBNull.Value) ? rowSiparis["PESINAT"].ToString() : String.Empty;
            sozlesme.VergiDairesi = (rowSiparis["VERGIDAIRESI"] != DBNull.Value) ? rowSiparis["VERGIDAIRESI"].ToString() : String.Empty;
            sozlesme.VergiNumarası = (rowSiparis["VERGINUMARASI"] != DBNull.Value) ? rowSiparis["VERGINUMARASI"].ToString() : String.Empty;
            sozlesme.Fiyat = (rowSiparis["FIYAT"] != DBNull.Value) ? rowSiparis["FIYAT"].ToString() : String.Empty;

            lblKapiTur.Text = this.SeriAdi;
            txtAd.Text = musteri.MusteriAd;
            txtSoyad.Text = musteri.MusteriSoyad;
            txtAdres.Text = musteri.MusteriAdres;
            txtCepTel.Text = musteri.MusteriCepTel;
            txtEvTel.Text = musteri.MusteriEvTel;
            txtFirmaAdi.Text = siparis.FirmaAdi;
            RadComboBoxItem radComboBoxItemMusteriIl = ddlMusteriIl.FindItemByText(musteri.MusteriIl);
            if (radComboBoxItemMusteriIl != null)
            {
                radComboBoxItemMusteriIl.Selected = true;
                IlceleriGetirIlAdinaGore(musteri.MusteriIl);
            }
            RadComboBoxItem radComboBoxItemMusteriIlce = ddlMusteriIlce.FindItemByText(musteri.MusteriIlce);
            if (radComboBoxItemMusteriIlce != null)
            {
                radComboBoxItemMusteriIlce.Selected = true;
                SemtleriGetirIlceAdinaGore(musteri.MusteriIlce);
            }
            RadComboBoxItem radComboBoxItemMusteriSemt = ddlMusteriSemt.FindItemByText(musteri.MusteriSemt);
            if (radComboBoxItemMusteriSemt != null)
            {
                radComboBoxItemMusteriSemt.Selected = true;
            }
            txtIsTel.Text = musteri.MusteriIsTel;
            rdtOlcuSiparisTarih.SelectedDate = siparis.SiparisTarih;

            // DropDownSelectedIndexAyarla(ddlAksesuarRengi, siparis.AksesuarRenk);
            DropDownSelectedIndexAyarla(ddlAluminyumRengi, siparis.AluminyumRenk);
            DropDownSelectedIndexAyarla(ddlBarelTipi, siparis.BarelTip);
            DropDownSelectedIndexAyarla(ddlCekmeKolu, siparis.CekmeKolu);
            DropDownSelectedIndexAyarla(ddlKayitsizKam, siparis.KayitYapmayanKamera);
            DropDownSelectedIndexAyarla(ddlKayitYapanKam, siparis.KayitYapanKamera);
            DropDownSelectedIndexAyarla(ddlAlarm, siparis.Alarm);
            DropDownSelectedIndexAyarla(ddlOtomatikKilit, siparis.OtomatikKilit);
            DropDownSelectedIndexAyarla(ddlBaba, siparis.Baba);
            DropDownSelectedIndexAyarla(ddlCita, siparis.Cita);
            //DropDownSelectedIndexAyarla(ddlKapiNo, siparis.KapiNo);
            DropDownSelectedIndexAyarla(ddlContaRengi, siparis.ContaRenk);
            DropDownSelectedIndexAyarla(ddlDisKapiModeli, siparis.DisKapiModel);
            DropDownSelectedIndexAyarla(ddlDisKapiRengi, siparis.DisKapiRenk);
            DropDownSelectedIndexAyarla(ddlDurbun, siparis.Durbun);
            DropDownSelectedIndexAyarla(ddlEsik, siparis.Esik);
            DropDownSelectedIndexAyarla(ddlIcKapiModeli, siparis.IcKapiModel);
            DropDownSelectedIndexAyarla(ddlIcKapiRengi, siparis.IcKapiRenk);
            DropDownSelectedIndexAyarla(ddlKilitSistemi, siparis.KilitSistem);
            DropDownSelectedIndexAyarla(ddlPervazTipi, siparis.PervazTip);
            DropDownSelectedIndexAyarla(ddlTacTipi, siparis.TacTip);
            //
            DropDownSelectedIndexAyarla(ddlIcPervazRenk, siparis.IcPervazRenk);
            DropDownSelectedIndexAyarla(ddlDisPervazRenk, siparis.DisPervazRenk);
            DropDownSelectedIndexAyarla(ddlAplikeRenk, siparis.AplikeRenk);
            DropDownSelectedIndexAyarla(ddlKanatRenk, siparis.Kanat);
            DropDownSelectedIndexAyarla(ddlCitaRenk, siparis.KasaCitaRenk);
            DropDownSelectedIndexAyarla(ddlZirhTipi, siparis.ZirhTip);
            DropDownSelectedIndexAyarla(ddlZirhRengi, siparis.ZirhRenk);
            DropDownSelectedIndexAyarla(ddlCekmeKoluTakilmaSekli, siparis.CekmeKolTakilmaSekli);
            DropDownSelectedIndexAyarla(ddlCekmeKoluRengi, siparis.CekmeKolRenk);
            DropDownSelectedIndexAyarla(ddlCamTipi, siparis.CamTip);
            DropDownSelectedIndexAyarla(ddlFerforje, siparis.Ferforje);
            DropDownSelectedIndexAyarla(ddlFerforjeRenk, siparis.FerforjeRenk);
            DropDownSelectedIndexAyarla(ddlMetalRenk, siparis.MetalRenk);
            DropDownSelectedIndexAyarla(ddlKasaKaplama, siparis.KasaKaplama);
            //
            DropDownSelectedIndexAyarla(ddlTaktak, siparis.Taktak);
            DropDownSelectedIndexAyarla(ddlSiparisDurumu, siparis.Durum);
            DropDownSelectedIndexAyarla(ddlMontajSekli, olcum.MontajSekli);
            DropDownSelectedIndexAyarla(ddlTeslimSekli, olcum.TeslimSekli);
            DropDownSelectedIndexAyarla(ddlOlcumAlan, olcum.OlcumAlanKisi);
            DropDownSelectedIndexAyarla(ddlAcilim, olcum.Acilim);

            if (!string.IsNullOrWhiteSpace(siparis.NakitPesin)) txtNakitPesin.Text = siparis.NakitPesin.ToString();
            if (!string.IsNullOrWhiteSpace(siparis.NakitKalan)) txtNakitKalan.Text = siparis.NakitKalan.ToString();
            txtNakitOdemeNotu.Text = siparis.NakitOdemeNot;
            if (!string.IsNullOrWhiteSpace(siparis.KKartiPesin)) txtKKartiPesin.Text = siparis.KKartiPesin.ToString();
            if (!string.IsNullOrWhiteSpace(siparis.KKartiKalan)) txtKKartiKalan.Text = siparis.KKartiKalan.ToString();
            txtKKartiOdemeNotu.Text = siparis.KKartiOdemeNot;
            if (!string.IsNullOrWhiteSpace(siparis.CekPesin)) txtCekPesin.Text = siparis.CekPesin.ToString();
            if (!string.IsNullOrWhiteSpace(siparis.CekKalan)) txtCekKalan.Text = siparis.CekKalan.ToString();
            txtCekOdemeNotu.Text = siparis.CekOdemeNot;
            txtSiparisNo.Text = siparis.SiparisNo;
            txtBolmeKayitSayisi.Text = siparis.BolmeKayitSayi;
            txtBayiAdi.Text = siparis.BayiAd;
            txtSiparisAdedi.Text = siparis.SiparisAdedi;
            txtMontajdaTakilacaklar.Text = olcum.MontajdaTakilacak;
            txtOlcumBilgileri.Text = olcum.OlcumBilgi;
            rdtOlcuTarihSaat.SelectedDate = olcum.OlcumTarih;

            txtMusteriAdSoyad.Text = musteri.MusteriAd + " " + musteri.MusteriSoyad;
            txtMusteriAdres.Text = musteri.MusteriAdres + "" + musteri.MusteriIl + " / " + musteri.MusteriIlce;
            txtMusteriCepTel.Text = musteri.MusteriCepTel;
            rdpTeslimTarihi.SelectedDate = sozlesme.MontajTeslimTarih;
            txtVergiDairesi.Text = sozlesme.VergiDairesi;
            txtVergiNumarasi.Text = sozlesme.VergiNumarası;
            txtFiyat.Text = sozlesme.Fiyat;

            txtIcKasaGenisligi.Text = olcum.IcKasaGenislik;
            txtIcKasaYuksekligi.Text = olcum.IcKasaYukseklik;
            txtDisKasaIcPervazFarki.Text = olcum.DisKasaIcPervazFark;
            txtDuvarKalinligi.Text = olcum.DuvarKalinlik;
            txtDisSolPervaz.Text = olcum.DisSolPervaz;
            txtDisUstPervaz.Text = olcum.DisUstPervaz;
            txtDisSagPervaz.Text = olcum.DisSagPervaz;
            txtIcSolPervaz.Text = olcum.IcSolPervaz;
            txtIcUstPervaz.Text = olcum.IcUstPervaz;
            txtIcSagPervaz.Text = olcum.IcSagPervaz;

            KapiTurAyarla(siparis.SiparisNo);
        }

        private void KapiTurAyarla(string siparisNo)
        {
            if (String.IsNullOrEmpty(siparisNo))
                return;

            switch (siparisNo[0].ToString())
            {
                case "N":
                    lblKapiTur.Text = "NOVA";
                    lblStandartOlcu.Text = "930 x 2010";
                    break;
                case "K":
                    lblKapiTur.Text = "KROMA";
                    lblStandartOlcu.Text = "940 x 2000";
                    break;
                case "G":
                    lblKapiTur.Text = "GUARD";
                    trGuard.Visible = true;
                    break;
            }
        }

        private void DropDownSelectedIndexAyarla(RadDropDownList dp, string selectedValue)
        {
            dp.ClearSelection();
            if (!String.IsNullOrWhiteSpace(selectedValue))
            {
                DropDownListItem lidp = dp.FindItemByText(selectedValue);
                if (lidp != null && lidp.Selected == false)
                    lidp.Selected = true;
            }
            else
                dp.SelectedIndex = 0;
        }

        private void DropDownSelectedIndexAyarla(RadComboBox dp, string selectedValue)
        {
            dp.ClearSelection();
            if (!String.IsNullOrWhiteSpace(selectedValue))
            {
                RadComboBoxItem lidp = dp.FindItemByValue(selectedValue);
                if (lidp != null && lidp.Selected == false)
                    lidp.Selected = true;
            }
            else
                dp.SelectedIndex = 0;
        }

        private void FormDoldur()
        {
            string seriId = ((int)this.KapiTip).ToString();
            string seriAdi = this.KapiTip.ToString().ToUpper();

            lblKapiTur.Text = seriAdi;
            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("ID", seriId);
            prms.Add("KAPISERI", seriAdi);

            DataSet ds = new SiparisIslemleriBS().RefTanimlariniGetir(prms);
            if (ds.Tables.Count == 0)
                return;

            DataTable dtKapiModeli = ds.Tables["KAPIMODELI"];
            DataTable dtKapiRenk = ds.Tables["KAPIRENK"];
            DataTable dtKilitSistem = ds.Tables["KILITSISTEM"];
            DataTable dtCita = ds.Tables["CITA"];
            DataTable dtEsik = ds.Tables["ESIK"];
            DataTable dtAksesuarRenk = ds.Tables["AKSESUARRENK"];
            DataTable dtMontajSekli = ds.Tables["MONTAJSEKLI"];
            DataTable dtTeslimSekli = ds.Tables["TESLIMSEKLI"];
            DataTable dtAluminyumRenk = ds.Tables["ALUMINYUMRENK"];
            DataTable dtTacTip = ds.Tables["TACTIP"];
            DataTable dtPervazTip = ds.Tables["PERVAZTIP"];
            DataTable dtContaRenk = ds.Tables["CONTARENK"];
            DataTable dtPersonel = ds.Tables["PERSONEL"];
            DataTable dtBarelTip = ds.Tables["BARELTIP"];
            DataTable dtCekmeKol = ds.Tables["CEKMEKOL"];
            DataTable dtPervazRenk = ds.Tables["PERVAZRENK"];
            DataTable dtAplikeRenk = ds.Tables["APLIKERENK"];
            DataTable dtMetalRenk = ds.Tables["METALRENK"];
            DataTable dtZirhTip = ds.Tables["ZIRHTIP"];
            DataTable dtZirhRenk = ds.Tables["ZIRHRENK"];
            DataTable dtCekmeKolTakilmaSekli = ds.Tables["CEKMEKOLUTAKILMASEKLI"];
            DataTable dtCekmeKolRengi = ds.Tables["AKSESUARRENK"];
            DataTable dtCamTipi = ds.Tables["CAMTIP"];
            DataTable dtFerforje = ds.Tables["FERFORJE"];
            DataTable dtFerforjeRenk = ds.Tables["FERFORJERENK"];
            DataTable dtKanatRenk = ds.Tables["ALUMINYUMRENK"];


            DropDownBindEt(ddlIcKapiModeli, dtKapiModeli);
            DropDownBindEt(ddlDisKapiModeli, dtKapiModeli);
            DropDownBindEt(ddlIcKapiRengi, dtKapiRenk);
            DropDownBindEt(ddlDisKapiRengi, dtKapiRenk);
            DropDownBindEt(ddlKilitSistemi, dtKilitSistem);
            DropDownBindEt(ddlCita, dtCita);
            DropDownBindEt(ddlEsik, dtEsik);
            DropDownBindEt(ddlMontajSekli, dtMontajSekli);
            DropDownBindEt(ddlTeslimSekli, dtTeslimSekli);
            DropDownBindEt(ddlAluminyumRengi, dtAluminyumRenk);
            DropDownBindEt(ddlTacTipi, dtTacTip);
            DropDownBindEt(ddlPervazTipi, dtPervazTip);
            DropDownBindEt(ddlContaRengi, dtContaRenk);
            DropDownBindEt(ddlOlcumAlan, dtPersonel);
            DropDownBindEt(ddlBarelTipi, dtBarelTip);
            DropDownBindEt(ddlCekmeKolu, dtCekmeKol);
            DropDownBindEt(ddlMetalRenk, dtMetalRenk);
            DropDownBindEt(ddlIcPervazRenk, dtPervazRenk);
            DropDownBindEt(ddlDisPervazRenk, dtPervazRenk);
            DropDownBindEt(ddlAplikeRenk, dtAplikeRenk);
            DropDownBindEt(ddlKanatRenk, dtKanatRenk);
            DropDownBindEt(ddlCitaRenk, dtCita);
            DropDownBindEt(ddlZirhTipi, dtZirhTip);
            DropDownBindEt(ddlZirhRengi, dtZirhRenk);
            DropDownBindEt(ddlCekmeKoluTakilmaSekli, dtCekmeKolTakilmaSekli);
            DropDownBindEt(ddlCekmeKoluRengi, dtCekmeKolRengi);
            DropDownBindEt(ddlCamTipi, dtCamTipi);
            DropDownBindEt(ddlFerforje, dtFerforje);
            DropDownBindEt(ddlFerforjeRenk, dtFerforjeRenk);

            IlleriGetir();
        }

        private void DropDownBindEt(Telerik.Web.UI.RadDropDownList ddl, DataTable dt)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "AD";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
        }

        protected void IlleriGetir()
        {
            DataTable dt = new SiparisIslemleriBS().IlleriGetir();
            if (dt.Rows.Count > 0)
            {
                ddlMusteriIl.DataSource = dt;
                ddlMusteriIl.DataTextField = "ILAD";
                ddlMusteriIl.DataValueField = "ILKOD";
                ddlMusteriIl.DataBind();
                ddlMusteriIl.Items.Insert(0, new RadComboBoxItem("Seçiniz", "Seçiniz"));
            }
            else
            {
                ddlMusteriIl.DataSource = null;
                ddlMusteriIl.DataBind();
            }
        }

        protected void IlceleriGetir(string ilKod)
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("ILKOD", ilKod);

            DataTable dt = new SiparisIslemleriBS().IlceleriGetir(prms);

            if (dt.Rows.Count > 0)
            {
                ddlMusteriIlce.DataSource = dt;
                ddlMusteriIlce.DataTextField = "ILCEAD";
                ddlMusteriIlce.DataValueField = "ILCEKOD";
                ddlMusteriIlce.DataBind();
                ddlMusteriIlce.Items.Insert(0, new RadComboBoxItem("Seçiniz", "Seçiniz"));
            }
            else
            {
                ddlMusteriIlce.DataSource = null;
                ddlMusteriIlce.DataBind();
            }
        }

        protected void IlceleriGetirIlAdinaGore(string ilAd)
        {
            DataTable dt = new SiparisIslemleriBS().IlceleriGetir(ilAd);

            if (dt.Rows.Count > 0)
            {
                ddlMusteriIlce.DataSource = dt;
                ddlMusteriIlce.DataTextField = "ILCEAD";
                ddlMusteriIlce.DataValueField = "ILCEKOD";
                ddlMusteriIlce.DataBind();
                ddlMusteriIlce.Items.Insert(0, new RadComboBoxItem("Seçiniz", "Seçiniz"));
            }
            else
            {
                ddlMusteriIlce.DataSource = null;
                ddlMusteriIlce.DataBind();
            }
        }

        protected void SemtleriGetir(string ilceKod)
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("ILCEKOD", ilceKod);

            DataTable dt = new SiparisIslemleriBS().SemtleriGetir(prms);

            if (dt.Rows.Count > 0)
            {
                ddlMusteriSemt.DataSource = dt;
                ddlMusteriSemt.DataTextField = "SEMTAD";
                ddlMusteriSemt.DataValueField = "SEMTKOD";
                ddlMusteriSemt.DataBind();
                ddlMusteriSemt.Items.Insert(0, new RadComboBoxItem("Seçiniz", "Seçiniz"));
            }
            else
            {
                ddlMusteriSemt.DataSource = null;
                ddlMusteriSemt.DataBind();
            }
        }

        protected void SemtleriGetirIlceAdinaGore(string ilceAd)
        {
            DataTable dt = new SiparisIslemleriBS().SemtleriGetir(ilceAd);

            if (dt.Rows.Count > 0)
            {
                ddlMusteriSemt.DataSource = dt;
                ddlMusteriSemt.DataTextField = "SEMTAD";
                ddlMusteriSemt.DataValueField = "SEMTKOD";
                ddlMusteriSemt.DataBind();
                ddlMusteriSemt.Items.Insert(0, new RadComboBoxItem("Seçiniz", "Seçiniz"));
            }
            else
            {
                ddlMusteriSemt.DataSource = null;
                ddlMusteriSemt.DataBind();
            }
        }

        protected void ddlMusteriIl_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlMusteriIlce.Text = "";
            ddlMusteriIlce.Items.Clear();
            ddlMusteriSemt.Items.Clear();
            ddlMusteriSemt.Text = "";
            IlceleriGetir(e.Value);
        }

        protected void ddlMusteriIlce_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlMusteriSemt.Items.Clear();
            ddlMusteriSemt.Text = "";
            SemtleriGetir(e.Value);
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            Musteri musteri = new Musteri();
            Siparis siparis = new Siparis();
            Olcum olcum = new Olcum();
            Sozlesme sozlesme = new Sozlesme();

            musteri.MusteriAd = string.IsNullOrWhiteSpace(txtAd.Text) ? null : txtAd.Text;
            musteri.MusteriSoyad = string.IsNullOrWhiteSpace(txtSoyad.Text) ? null : txtSoyad.Text;
            musteri.MusteriAdres = string.IsNullOrWhiteSpace(txtAdres.Text) ? null : txtAdres.Text;
            musteri.MusteriCepTel = string.IsNullOrWhiteSpace(txtCepTel.Text) ? null : txtCepTel.Text;
            musteri.MusteriEvTel = string.IsNullOrWhiteSpace(txtEvTel.Text) ? null : txtEvTel.Text;
            musteri.MusteriIl = ComboBoxCheck(ddlMusteriIl) ? null : ddlMusteriIl.SelectedItem.Text;
            musteri.MusteriIlce = ComboBoxCheck(ddlMusteriIlce) ? null : ddlMusteriIlce.SelectedItem.Text;
            musteri.MusteriSemt = ComboBoxCheck(ddlMusteriSemt) ? null : ddlMusteriSemt.SelectedItem.Text;
            musteri.MusteriIsTel = string.IsNullOrWhiteSpace(txtIsTel.Text) ? null : txtIsTel.Text;

            siparis.SiparisID = this.SiparisID;
            siparis.FirmaAdi = string.IsNullOrWhiteSpace(txtFirmaAdi.Text) ? null : txtFirmaAdi.Text;
            // siparis.AksesuarRenk = DropDownCheck(ddlAksesuarRengi) ? null : ddlAksesuarRengi.SelectedText;
            siparis.AluminyumRenk = DropDownCheck(ddlAluminyumRengi) ? null : ddlAluminyumRengi.SelectedText;
            siparis.Baba = DropDownCheck(ddlBaba) ? null : ddlBaba.SelectedText;
            siparis.BarelTip = DropDownCheck(ddlBarelTipi) ? null : ddlBarelTipi.SelectedText;
            siparis.BayiAd = string.IsNullOrWhiteSpace(txtBayiAdi.Text) ? null : txtBayiAdi.Text;
            siparis.CekmeKolu = DropDownCheck(ddlCekmeKolu) ? null : ddlCekmeKolu.SelectedText;
            siparis.Cita = DropDownCheck(ddlCita) ? null : ddlCita.SelectedText;
            siparis.ContaRenk = DropDownCheck(ddlContaRengi) ? null : ddlContaRengi.SelectedText;
            siparis.DisKapiModel = DropDownCheck(ddlDisKapiModeli) ? null : ddlDisKapiModeli.SelectedText;
            siparis.DisKapiRenk = DropDownCheck(ddlDisKapiRengi) ? null : ddlDisKapiRengi.SelectedText;
            siparis.Durbun = DropDownCheck(ddlDurbun) ? null : ddlDurbun.SelectedText;
            siparis.KayitYapanKamera = DropDownCheck(ddlKayitYapanKam) ? null : ddlKayitYapanKam.SelectedText;
            siparis.KayitYapmayanKamera = DropDownCheck(ddlKayitsizKam) ? null : ddlKayitsizKam.SelectedText;
            siparis.Alarm = DropDownCheck(ddlAlarm) ? null : ddlAlarm.SelectedText;
            siparis.OtomatikKilit = DropDownCheck(ddlOtomatikKilit) ? null : ddlOtomatikKilit.SelectedText;
            siparis.Esik = DropDownCheck(ddlEsik) ? null : ddlEsik.SelectedText;
            siparis.IcKapiModel = DropDownCheck(ddlIcKapiModeli) ? null : ddlIcKapiModeli.SelectedText;
            siparis.IcKapiRenk = DropDownCheck(ddlIcKapiRengi) ? null : ddlIcKapiRengi.SelectedText;
            siparis.KilitSistem = DropDownCheck(ddlKilitSistemi) ? null : ddlKilitSistemi.SelectedText;
            siparis.PervazTip = DropDownCheck(ddlPervazTipi) ? null : ddlPervazTipi.SelectedText;
            siparis.SiparisTarih = rdtOlcuSiparisTarih.SelectedDate == null ? DateTime.Now : rdtOlcuSiparisTarih.SelectedDate.Value;
            siparis.TacTip = DropDownCheck(ddlTacTipi) ? null : ddlTacTipi.SelectedText;
            //
            siparis.BolmeKayitSayi = string.IsNullOrWhiteSpace(txtBolmeKayitSayisi.Text) ? null : txtBolmeKayitSayisi.Text;
            siparis.IcPervazRenk = DropDownCheck(ddlIcPervazRenk) ? null : ddlIcPervazRenk.SelectedText;
            siparis.DisPervazRenk = DropDownCheck(ddlDisPervazRenk) ? null : ddlDisPervazRenk.SelectedText;
            siparis.AplikeRenk = DropDownCheck(ddlAplikeRenk) ? null : ddlAplikeRenk.SelectedText;
            siparis.Kanat = DropDownCheck(ddlKanatRenk) ? null : ddlKanatRenk.SelectedText;
            siparis.KasaCitaRenk = DropDownCheck(ddlCitaRenk) ? null : ddlCitaRenk.SelectedText;
            siparis.ZirhTip = DropDownCheck(ddlZirhTipi) ? null : ddlZirhTipi.SelectedText;
            siparis.ZirhRenk = DropDownCheck(ddlZirhRengi) ? null : ddlZirhRengi.SelectedText;
            siparis.CekmeKolTakilmaSekli = DropDownCheck(ddlCekmeKoluTakilmaSekli) ? null : ddlCekmeKoluTakilmaSekli.SelectedText;
            siparis.CekmeKolRenk = DropDownCheck(ddlCekmeKoluRengi) ? null : ddlCekmeKoluRengi.SelectedText;
            siparis.CamTip = DropDownCheck(ddlCamTipi) ? null : ddlCamTipi.SelectedText;
            siparis.Ferforje = DropDownCheck(ddlFerforje) ? null : ddlFerforje.SelectedText;
            siparis.FerforjeRenk = DropDownCheck(ddlFerforjeRenk) ? null : ddlFerforjeRenk.SelectedText;
            siparis.MetalRenk = DropDownCheck(ddlMetalRenk) ? null : ddlMetalRenk.SelectedText;
            siparis.KasaKaplama = DropDownCheck(ddlKasaKaplama) ? null : ddlKasaKaplama.SelectedText;
            siparis.Durum = DropDownCheck(ddlSiparisDurumu) ? null : ddlSiparisDurumu.SelectedText;
            //
            siparis.UpdatedBy = Session["user"].ToString();
            siparis.UpdatedTime = DateTime.Now;
            siparis.Taktak = DropDownCheck(ddlTaktak) ? null : ddlTaktak.SelectedText;
            siparis.KapiTipi = this.KapiTip.ToString();
            siparis.FirmaAdi = string.IsNullOrWhiteSpace(txtFirmaAdi.Text) ? null : txtFirmaAdi.Text;
            siparis.SiparisAdedi = string.IsNullOrWhiteSpace(txtSiparisAdedi.Text) ? "1" : txtSiparisAdedi.Text;
            if (!string.IsNullOrWhiteSpace(txtNakitPesin.Text)) siparis.NakitPesin = txtNakitPesin.Text;
            if (!string.IsNullOrWhiteSpace(txtNakitKalan.Text)) siparis.NakitKalan = txtNakitKalan.Text;
            siparis.NakitOdemeNot = string.IsNullOrWhiteSpace(txtNakitOdemeNotu.Text) ? null : txtNakitOdemeNotu.Text;

            if (!string.IsNullOrWhiteSpace(txtKKartiPesin.Text))
                siparis.KKartiPesin = txtKKartiPesin.Text;
            else
                siparis.KKartiPesin = null;
            if (!string.IsNullOrWhiteSpace(txtKKartiKalan.Text))
                siparis.KKartiKalan = txtKKartiKalan.Text;
            else
                siparis.KKartiKalan = null;
            siparis.KKartiOdemeNot = string.IsNullOrWhiteSpace(txtKKartiOdemeNotu.Text) ? null : txtKKartiOdemeNotu.Text;

            if (!string.IsNullOrWhiteSpace(txtCekPesin.Text))
                siparis.CekPesin = txtCekPesin.Text;
            else
                siparis.CekPesin = null;
            if (!string.IsNullOrWhiteSpace(txtCekKalan.Text))
                siparis.CekKalan = txtCekKalan.Text;
            else
                siparis.CekKalan = null;
            siparis.CekOdemeNot = string.IsNullOrWhiteSpace(txtCekOdemeNotu.Text) ? null : txtCekOdemeNotu.Text;
            siparis.Not = string.IsNullOrWhiteSpace(txtNot.Text) ? null : txtNot.Text;
            olcum.MontajdaTakilacak = string.IsNullOrWhiteSpace(txtMontajdaTakilacaklar.Text) ? null : txtMontajdaTakilacaklar.Text;
            olcum.MontajSekli = DropDownCheck(ddlMontajSekli) ? null : ddlMontajSekli.SelectedText;
            olcum.OlcumAlanKisi = DropDownCheck(ddlOlcumAlan) ? null : ddlOlcumAlan.SelectedText;
            olcum.Acilim = DropDownCheck(ddlAcilim) ? null : ddlAcilim.SelectedText;
            olcum.OlcumBilgi = string.IsNullOrWhiteSpace(txtOlcumBilgileri.Text) ? null : txtOlcumBilgileri.Text;
            if (!string.IsNullOrEmpty(txtIcKasaGenisligi.Text)) olcum.IcKasaGenislik = txtIcKasaGenisligi.Text;
            if (!string.IsNullOrEmpty(txtIcKasaYuksekligi.Text)) olcum.IcKasaYukseklik = txtIcKasaYuksekligi.Text;
            if (!string.IsNullOrEmpty(txtDisKasaIcPervazFarki.Text)) olcum.DisKasaIcPervazFark = txtDisKasaIcPervazFarki.Text;
            if (!string.IsNullOrEmpty(txtDuvarKalinligi.Text)) olcum.DuvarKalinlik = txtDuvarKalinligi.Text;
            if (!string.IsNullOrEmpty(txtDisSolPervaz.Text)) olcum.DisSolPervaz = txtDisSolPervaz.Text;
            if (!string.IsNullOrEmpty(txtDisUstPervaz.Text)) olcum.DisUstPervaz = txtDisUstPervaz.Text;
            if (!string.IsNullOrEmpty(txtDisSagPervaz.Text)) olcum.DisSagPervaz = txtDisSagPervaz.Text;
            if (!string.IsNullOrEmpty(txtIcSolPervaz.Text)) olcum.IcSolPervaz = txtIcSolPervaz.Text;
            if (!string.IsNullOrEmpty(txtIcUstPervaz.Text)) olcum.IcUstPervaz = txtIcUstPervaz.Text;
            if (!string.IsNullOrEmpty(txtIcSagPervaz.Text)) olcum.IcSagPervaz = txtIcSagPervaz.Text;

            if (rdtOlcuTarihSaat.SelectedDate != null) olcum.OlcumTarih = rdtOlcuTarihSaat.SelectedDate.Value;
            olcum.TeslimSekli = DropDownCheck(ddlTeslimSekli) ? null : ddlTeslimSekli.SelectedText;
            sozlesme.MontajDurum = "A";
            if (rdpTeslimTarihi.SelectedDate != null) sozlesme.MontajTeslimTarih = rdpTeslimTarihi.SelectedDate.Value;
            sozlesme.VergiDairesi = string.IsNullOrWhiteSpace(txtVergiDairesi.Text) ? null : txtVergiDairesi.Text;
            sozlesme.VergiNumarası = string.IsNullOrWhiteSpace(txtVergiNumarasi.Text) ? null : txtVergiNumarasi.Text;
            sozlesme.Fiyat = string.IsNullOrWhiteSpace(txtFiyat.Text) ? null : txtFiyat.Text;

            //Montaj kota kontrolu acik ise
            if (Session["MONTAJ_KOTA_KONTROLU"].ToString() == "1")
            {
                MontajBS montajBS = new MontajBS();
                int yapilanMontajSayisi = montajBS.GunlukMontajSayisiniGetir(rdpTeslimTarihi.SelectedDate.Value);
                DataTable dt = montajBS.GunlukMontajKotaBilgisiGetir(rdpTeslimTarihi.SelectedDate.Value);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    bool montajKabul = Convert.ToBoolean(row["MONTAJKABUL"]);
                    if (!montajKabul)
                    {
                        MessageBox.Uyari(this.Page, rdpTeslimTarihi.SelectedDate.Value.Date.ToShortDateString() + " tarihi için montaj alınamaz!");
                        return;
                    }
                    else
                    {
                        int gunlukMontakKotaDegeri = Convert.ToInt32(row["MAXMONTAJSAYI"]);
                        if (yapilanMontajSayisi >= gunlukMontakKotaDegeri)
                        {
                            MessageBox.Uyari(this.Page, rdpTeslimTarihi.SelectedDate.Value.Date.ToShortDateString() + " tarihi için montaj kotası (" + gunlukMontakKotaDegeri.ToString() + ") değerine ulaşılmıştır.");
                            return;
                        }
                    }
                }
                else
                {
                    int kotaVarsayilanDegeri = Convert.ToInt32(Session["MONTAJ_KOTA_VARSAYILAN"]);
                    if (yapilanMontajSayisi >= kotaVarsayilanDegeri)
                    {
                        MessageBox.Uyari(this.Page, rdpTeslimTarihi.SelectedDate.Value.Date.ToShortDateString() + " tarihi için montaj kotası (" + kotaVarsayilanDegeri.ToString() + ") değerine ulaşılmıştır.");
                        return;
                    }
                }
            }

            bool state = new SiparisIslemleriBS().SiparisGuncelle(musteri, siparis, olcum, sozlesme);

            if (state)
            {
                MessageBox.Basari(this, "Sipariş güncellendi.");
                Response.Redirect("~/SiparisFormGoruntule.aspx?SiparisID=" + this.SiparisID + "&SeriAdi=" + lblKapiTur.Text);
            }
            else
                MessageBox.Hata(this, "Sipariş güncellenemedi.");
        }

        private bool DropDownCheck(RadDropDownList ddl)
        {
            if (ddl.SelectedItem == null || ddl.SelectedIndex == 0)
                return true;
            else
                return false;
        }

        private bool ComboBoxCheck(RadComboBox rcb)
        {
            if (rcb.SelectedItem == null || rcb.SelectedIndex == 0)
                return true;
            else
                return false;
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            FormElemanIcerikTemizle(this.Page.Form.Controls);
        }

        private void FormElemanIcerikTemizle(ControlCollection cc)
        {
            foreach (Control c in cc)
            {
                if (c is RadTextBox && c.ID != "txtSiparisNo")
                    ((RadTextBox)c).Text = string.Empty;
                if (c is RadNumericTextBox)
                    ((RadNumericTextBox)c).Text = string.Empty;
                if (c is RadDropDownList)
                {
                    RadDropDownList rdl = (RadDropDownList)c;
                    if (rdl.Items.Count > 0)
                        rdl.SelectedIndex = 0;
                }
                if (c is RadDateTimePicker)
                {
                    ((RadDateTimePicker)c).SelectedDate = null;
                }
                if (c is RadDatePicker)
                {
                    ((RadDatePicker)c).SelectedDate = null;
                }
                if (c is RadComboBox)
                {
                    RadComboBox rcb = ((RadComboBox)c);
                    if (rcb.Items.Count > 0)
                        rcb.SelectedIndex = 0;
                }
                if (c is RadMaskedTextBox)
                    ((RadMaskedTextBox)c).Text = string.Empty;

                FormElemanIcerikTemizle(c.Controls);
            }
        }
    }
}
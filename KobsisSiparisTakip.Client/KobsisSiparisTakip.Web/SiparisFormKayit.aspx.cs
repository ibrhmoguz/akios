using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Business.DataTypes;
using KobsisSiparisTakip.Web.Util;
using Telerik.Web.UI;
using System.Globalization;
using System.Configuration;

namespace KobsisSiparisTakip.Web
{
    public partial class SiparisFormKayit : KobsisBasePage
    {
        private static string ANKARA_IL_KODU = "6";

        public KapiTipi KapiTip
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Request.QueryString["KapiTipi"]))
                {
                    string tip = Request.QueryString["KapiTipi"].ToString();
                    if (tip == KapiTipi.Nova.ToString())
                        return KapiTipi.Nova;
                    else if (tip == KapiTipi.Kroma.ToString())
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
            this.MaintainScrollPositionOnPostBack = true;

            if (!Page.IsPostBack)
            {
                FormDoldur();
                CurrencyAyarla();
            }
        }

        private void CurrencyAyarla()
        {
            CultureInfo c = new CultureInfo("TR-tr");
            txtNakitPesin.Culture = c;
            txtNakitKalan.Culture = c;
            txtKKartiPesin.Culture = c;
            txtKKartiKalan.Culture = c;
            txtCekPesin.Culture = c;
            txtCekKalan.Culture = c;
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

            Kontrol();
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

        private void Kontrol()
        {
            switch (this.KapiTip.ToString())
            {
                case "Guard":
                    trGuard.Visible = true;
                    break;
                case "Nova":
                    lblStandartOlcu.Text = "930 x 2010";
                    break;
                case "Kroma":
                    lblStandartOlcu.Text = "940 x 2000";
                    break;
            }
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

                ddlMusteriIl.FindItemByValue(ANKARA_IL_KODU).Selected = true;
                IlceleriGetir(ANKARA_IL_KODU);
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

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (rdpTeslimTarihi.SelectedDate == null)
            {
                MessageBox.Uyari(this.Page, "Teslim tarihi girmelisiniz!");
                return;
            }

            Firma_Musteri musteri = new Firma_Musteri();
            Siparis siparis = new Siparis();
            Olcum olcum = new Olcum();
            Sozlesme sozlesme = new Sozlesme();

            if (!string.IsNullOrEmpty(txtAd.Text)) musteri.MusteriAd = txtAd.Text;
            if (!string.IsNullOrEmpty(txtSoyad.Text)) musteri.MusteriSoyad = txtSoyad.Text;
            if (!string.IsNullOrEmpty(txtAdres.Text)) musteri.MusteriAdres = txtAdres.Text;
            if (!string.IsNullOrEmpty(txtCepTel.Text)) musteri.MusteriCepTel = txtCepTel.Text;
            if (!string.IsNullOrEmpty(txtEvTel.Text)) musteri.MusteriEvTel = txtEvTel.Text;
            if (ComboBoxCheck(ddlMusteriIl)) musteri.MusteriIl = ddlMusteriIl.SelectedItem.Text;
            if (ComboBoxCheck(ddlMusteriIlce)) musteri.MusteriIlce = ddlMusteriIlce.SelectedItem.Text;
            if (ComboBoxCheck(ddlMusteriSemt)) musteri.MusteriSemt = ddlMusteriSemt.SelectedItem.Text;
            if (!string.IsNullOrEmpty(txtIsTel.Text)) musteri.MusteriIsTel = txtIsTel.Text;
            //if (DropDownCheck(ddlAksesuarRengi)) siparis.AksesuarRenk = ddlAksesuarRengi.SelectedText;
            if (DropDownCheck(ddlAluminyumRengi)) siparis.AluminyumRenk = ddlAluminyumRengi.SelectedText;
            if (DropDownCheck(ddlBaba)) siparis.Baba = ddlBaba.SelectedText;
            if (DropDownCheck(ddlBarelTipi)) siparis.BarelTip = ddlBarelTipi.SelectedText;
            if (!string.IsNullOrEmpty(txtBayiAdi.Text)) siparis.BayiAd = txtBayiAdi.Text;
            if (DropDownCheck(ddlCekmeKolu)) siparis.CekmeKolu = ddlCekmeKolu.SelectedText;
            if (DropDownCheck(ddlCita)) siparis.Cita = ddlCita.SelectedText;
            if (DropDownCheck(ddlContaRengi)) siparis.ContaRenk = ddlContaRengi.SelectedText;
            if (DropDownCheck(ddlDisKapiModeli)) siparis.DisKapiModel = ddlDisKapiModeli.SelectedText;
            if (DropDownCheck(ddlDisKapiRengi)) siparis.DisKapiRenk = ddlDisKapiRengi.SelectedText;
            if (DropDownCheck(ddlDurbun)) siparis.Durbun = ddlDurbun.SelectedText;
            if (DropDownCheck(ddlEsik)) siparis.Esik = ddlEsik.SelectedText;
            if (DropDownCheck(ddlIcKapiModeli)) siparis.IcKapiModel = ddlIcKapiModeli.SelectedText;
            if (DropDownCheck(ddlIcKapiRengi)) siparis.IcKapiRenk = ddlIcKapiRengi.SelectedText;
            if (DropDownCheck(ddlKilitSistemi)) siparis.KilitSistem = ddlKilitSistemi.SelectedText;
            if (DropDownCheck(ddlPervazTipi)) siparis.PervazTip = ddlPervazTipi.SelectedText;
            if (DropDownCheck(ddlTacTipi)) siparis.TacTip = ddlTacTipi.SelectedText;
            //
            if (DropDownCheck(ddlIcPervazRenk)) siparis.IcPervazRenk = ddlIcPervazRenk.SelectedText;
            if (DropDownCheck(ddlDisPervazRenk)) siparis.DisPervazRenk = ddlDisPervazRenk.SelectedText;
            if (DropDownCheck(ddlAplikeRenk)) siparis.AplikeRenk = ddlAplikeRenk.SelectedText;
            if (DropDownCheck(ddlKanatRenk)) siparis.Kanat = ddlKanatRenk.SelectedText;
            if (DropDownCheck(ddlCitaRenk)) siparis.KasaCitaRenk = ddlCitaRenk.SelectedText;
            if (DropDownCheck(ddlZirhTipi)) siparis.ZirhTip = ddlZirhTipi.SelectedText;
            if (DropDownCheck(ddlZirhRengi)) siparis.ZirhRenk = ddlZirhRengi.SelectedText;
            if (DropDownCheck(ddlCekmeKoluTakilmaSekli)) siparis.CekmeKolTakilmaSekli = ddlCekmeKoluTakilmaSekli.SelectedText;
            if (DropDownCheck(ddlCekmeKoluRengi)) siparis.CekmeKolRenk = ddlCekmeKoluRengi.SelectedText;
            if (!string.IsNullOrWhiteSpace(txtBolmeKayitSayisi.Text)) siparis.BolmeKayitSayi = txtBolmeKayitSayisi.Text;
            if (DropDownCheck(ddlCamTipi)) siparis.CamTip = ddlCamTipi.SelectedText;
            if (DropDownCheck(ddlFerforje)) siparis.Ferforje = ddlFerforje.SelectedText;
            if (DropDownCheck(ddlFerforjeRenk)) siparis.FerforjeRenk = ddlFerforjeRenk.SelectedText;
            if (DropDownCheck(ddlMetalRenk)) siparis.MetalRenk = ddlMetalRenk.SelectedText;
            if (DropDownCheck(ddlKasaKaplama)) siparis.KasaKaplama = ddlKasaKaplama.SelectedText;

            //
            siparis.SiparisTarih = rdtOlcuSiparisTarih.SelectedDate == null ? DateTime.Now : rdtOlcuSiparisTarih.SelectedDate.Value;
            if (!string.IsNullOrWhiteSpace(txtNot.Text)) siparis.Not = txtNot.Text;
            if (DropDownCheck(ddlTaktak)) siparis.Taktak = ddlTaktak.SelectedText;
            siparis.KapiTipi = this.KapiTip.ToString();
            siparis.Durum = "BEKLEYEN";
            if (!string.IsNullOrEmpty(txtFirmaAdi.Text)) siparis.FirmaAdi = txtFirmaAdi.Text;
            if (DropDownCheck(ddlKayitYapanKam)) siparis.KayitYapanKamera = ddlKayitYapanKam.SelectedText;
            if (DropDownCheck(ddlKayitsizKam)) siparis.KayitYapmayanKamera = ddlKayitsizKam.SelectedText;
            if (DropDownCheck(ddlAlarm)) siparis.Alarm = ddlAlarm.SelectedText;
            if (DropDownCheck(ddlOtomatikKilit)) siparis.OtomatikKilit = ddlOtomatikKilit.SelectedText;
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

            siparis.CreatedBy = SessionManager.KullaniciBilgi.KullaniciAdi;
            siparis.CreatedTime = DateTime.Now;
            if (!string.IsNullOrEmpty(txtMontajdaTakilacaklar.Text)) olcum.MontajdaTakilacak = txtMontajdaTakilacaklar.Text;
            if (DropDownCheck(ddlMontajSekli)) olcum.MontajSekli = ddlMontajSekli.SelectedText;
            if (DropDownCheck(ddlOlcumAlan)) olcum.OlcumAlanKisi = ddlOlcumAlan.SelectedText;
            if (!string.IsNullOrEmpty(txtOlcumBilgileri.Text)) olcum.OlcumBilgi = txtOlcumBilgileri.Text;
            if (rdtOlcuTarihSaat.SelectedDate != null) olcum.OlcumTarih = rdtOlcuTarihSaat.SelectedDate.Value;
            if (DropDownCheck(ddlTeslimSekli)) olcum.TeslimSekli = ddlTeslimSekli.SelectedText;
            if (DropDownCheck(ddlAcilim)) olcum.Acilim = ddlAcilim.SelectedText;

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

            sozlesme.MontajDurum = "A";
            if (rdpTeslimTarihi.SelectedDate != null) sozlesme.MontajTeslimTarih = rdpTeslimTarihi.SelectedDate.Value;
            if (!string.IsNullOrEmpty(txtVergiDairesi.Text)) sozlesme.VergiDairesi = txtVergiDairesi.Text;
            if (!string.IsNullOrEmpty(txtVergiNumarasi.Text)) sozlesme.VergiNumarası = txtVergiNumarasi.Text;
            if (!string.IsNullOrEmpty(txtFiyat.Text)) sozlesme.Fiyat = txtFiyat.Text;

            string seriAdi = this.KapiTip.ToString().ToUpper();

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

            string siparisID = new SiparisIslemleriBS().SiparisKaydet(musteri, siparis, olcum, sozlesme);

            if (siparisID != string.Empty)
            {
                MessageBox.Basari(this, "Sipariş eklendi.");
                Response.Redirect("~/SiparisFormGoruntule.aspx?SiparisID=" + siparisID + "&SeriAdi=" + seriAdi);
            }
            else
                MessageBox.Hata(this, "Sipariş eklenemedi.");
        }

        protected void btnIleri_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdres.Text) ||
                ddlMusteriIlce.SelectedItem == null ||
                string.IsNullOrWhiteSpace(ddlMusteriIlce.SelectedItem.Text) ||
                ddlMusteriIl.SelectedItem == null ||
                string.IsNullOrWhiteSpace(ddlMusteriIl.SelectedItem.Text) ||
                ddlMusteriSemt.SelectedItem == null ||
                string.IsNullOrWhiteSpace(ddlMusteriSemt.SelectedItem.Text))
            {
                MessageBox.Hata(this, "İl, ilçe ve semt alanlarını doldurmalısınız!");
                return;
            }

            tbMusteriSozlesme.Visible = true;
            txtMusteriAdres.Text = txtAdres.Text + " " + ddlMusteriSemt.SelectedItem.Text + "  " + ddlMusteriIlce.SelectedItem.Text + "  " + ddlMusteriIl.SelectedItem.Text;
            txtMusteriCepTel.Text = txtCepTel.Text;
        }

        private bool DropDownCheck(RadDropDownList ddl)
        {
            if (ddl.SelectedItem == null || ddl.SelectedIndex == 0)
                return false;
            else
                return true;
        }

        private bool ComboBoxCheck(RadComboBox rcb)
        {
            if (rcb.SelectedItem == null || rcb.SelectedIndex == 0)
                return false;
            else
                return true;
        }
    }
}

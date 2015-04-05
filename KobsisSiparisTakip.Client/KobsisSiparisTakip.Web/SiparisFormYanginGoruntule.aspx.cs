using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Business.DataTypes;
using KobsisSiparisTakip.Web.Util;
using Telerik.Web.UI;

namespace KobsisSiparisTakip.Web
{
    public partial class SiparisFormYanginGoruntule : KobsisBasePage
    {
        private DataTable SiparisBilgileri
        {
            get
            {
                if (Session["SiparisBilgileri"] != null)
                    return Session["SiparisBilgileri"] as DataTable;
                else
                    return null;
            }
            set
            {
                Session["SiparisBilgileri"] = value;
            }
        }

        private string SiparisID
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
                    return lblKapiTur.Text;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FormBilgileriniGetir();
                PopupPageHelper.OpenPopUp(btnYazdir, "Print/PrintYangin.aspx?SeriAdi=" + this.SeriAdi, "", true, false, true, false, false, false, 1024, 800, true, false, "onclick");
            }
        }

        private void FormBilgileriniGetir()
        {
            string adres, il, ilce, semt, ad, soyad;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("ID", this.SiparisID);

            DataTable dt = new SiparisIslemleriBS().SiparisBilgileriniGetir(prms);
            if (dt.Rows.Count == 0)
                return;

            this.SiparisBilgileri = dt;
            DataRow row = dt.Rows[0];

            string siparisNo = (row["SIPARISNO"] != DBNull.Value) ? row["SIPARISNO"].ToString() : String.Empty;
            lblSiparisNo.Text = siparisNo;
            adres = (row["MUSTERIADRES"] != DBNull.Value) ? row["MUSTERIADRES"].ToString() : String.Empty;
            il = (row["MUSTERIIL"] != DBNull.Value) ? row["MUSTERIIL"].ToString() : String.Empty;
            ilce = (row["MUSTERIILCE"] != DBNull.Value) ? row["MUSTERIILCE"].ToString() : String.Empty;
            semt = (row["MUSTERISEMT"] != DBNull.Value) ? row["MUSTERISEMT"].ToString() : String.Empty;
            ad = (row["MUSTERIAD"] != DBNull.Value) ? row["MUSTERIAD"].ToString() : String.Empty;
            soyad = (row["MUSTERISOYAD"] != DBNull.Value) ? row["MUSTERISOYAD"].ToString() : String.Empty;
            lblSiparisTarih.Text = (row["SIPARISTARIH"] != DBNull.Value) ? row["SIPARISTARIH"].ToString() : String.Empty;
            lblBayiAdi.Text = (row["BAYIADI"] != DBNull.Value) ? row["BAYIADI"].ToString() : String.Empty;
            lblAd.Text = ad;
            lblSoyad.Text = soyad;
            lblAdres.Text = adres + "   " + semt + " / " + ilce + " / " + il;
            lblFirmaAdi.Text = (row["FIRMAADI"] != DBNull.Value) ? row["FIRMAADI"].ToString() : String.Empty;
            lblEvTel.Text = (row["MUSTERIEVTEL"] != DBNull.Value) ? row["MUSTERIEVTEL"].ToString() : String.Empty;
            lblIsTel.Text = (row["MUSTERIISTEL"] != DBNull.Value) ? row["MUSTERIISTEL"].ToString() : String.Empty;
            lblCepTel.Text = (row["MUSTERICEPTEL"] != DBNull.Value) ? row["MUSTERICEPTEL"].ToString() : String.Empty;
            lblMusteriCepTel.Text = lblCepTel.Text;
            lblIcKapiModeli.Text = (row["ICKAPIMODEL"] != DBNull.Value) ? row["ICKAPIMODEL"].ToString() : String.Empty;
            lblDisKapiModeli.Text = (row["DISKAPIMODEL"] != DBNull.Value) ? row["DISKAPIMODEL"].ToString() : String.Empty;
            lblKilitSistemi.Text = (row["KILITSISTEM"] != DBNull.Value) ? row["KILITSISTEM"].ToString() : String.Empty;
            lblEsik.Text = (row["ESIK"] != DBNull.Value) ? row["ESIK"].ToString() : String.Empty;
            lblYanginPanikBar.Text = (row["PANIKBAR"] != DBNull.Value) ? row["PANIKBAR"].ToString() : String.Empty;
            lblYanginKol.Text = (row["MUDAHALEKOL"] != DBNull.Value) ? row["MUDAHALEKOL"].ToString() : String.Empty;
            lblYanginMenteseTip.Text = (row["MENTESE"] != DBNull.Value) ? row["MENTESE"].ToString() : String.Empty;
            lblYanginHidrolikKapatici.Text = (row["HIDROLIKKAPATICI"] != DBNull.Value) ? row["HIDROLIKKAPATICI"].ToString() : String.Empty;
            lblCumba.Text = (row["CUMBA"] != DBNull.Value) ? row["CUMBA"].ToString() : String.Empty;
            lblYanginMetalRengi.Text = (row["METALRENK"] != DBNull.Value) ? row["METALRENK"].ToString() : String.Empty;
            lblCekmeKolu.Text = (row["CEKMEKOLU"] != DBNull.Value) ? row["CEKMEKOLU"].ToString() : String.Empty;
            lblBarelTipi.Text = (row["BARELTIP"] != DBNull.Value) ? row["BARELTIP"].ToString() : String.Empty;
            lblDurbun.Text = (row["DURBUN"] != DBNull.Value) ? row["DURBUN"].ToString() : String.Empty;
            lblTaktak.Text = (row["TAKTAK"] != DBNull.Value) ? row["TAKTAK"].ToString() : String.Empty;
            lblYanginKapiCins.Text = (row["YANGINKAPICINS"] != DBNull.Value) ? row["YANGINKAPICINS"].ToString() : String.Empty;
            lblYanginKasaTipi.Text = (row["KASATIP"] != DBNull.Value) ? row["KASATIP"].ToString() : String.Empty;
            lblOlcumBilgileri.Text = (row["OLCUMBILGI"] != DBNull.Value) ? row["OLCUMBILGI"].ToString() : String.Empty;
            lblOlcuTarihSaat.Text = (row["OLCUMTARIH"] != DBNull.Value) ? Convert.ToDateTime(row["OLCUMTARIH"].ToString()).ToShortDateString() : String.Empty;
            lblOlcumAlan.Text = (row["OLCUMALANKISI"] != DBNull.Value) ? row["OLCUMALANKISI"].ToString() : String.Empty;
            lblMontajSekli.Text = (row["MONTAJSEKLI"] != DBNull.Value) ? row["MONTAJSEKLI"].ToString() : String.Empty;
            lblTeslimSekli.Text = (row["TESLIMSEKLI"] != DBNull.Value) ? row["TESLIMSEKLI"].ToString() : String.Empty;
            lblMusteriAdSoyad.Text = ad + " " + soyad;
            lblMusteriAdres.Text = adres + "   " + ilce + " / " + il;
            lblFiyat.Text = (row["FIYAT"] != DBNull.Value) ? row["FIYAT"].ToString() : String.Empty;
            lblVergiDairesi.Text = (row["VERGIDAIRESI"] != DBNull.Value) ? row["VERGIDAIRESI"].ToString() : String.Empty;
            lblVergiNumarasi.Text = (row["VERGINUMARASI"] != DBNull.Value) ? row["VERGINUMARASI"].ToString() : String.Empty;
            lblTeslimTarihi.Text = (row["TESLIMTARIH"] != DBNull.Value) ? Convert.ToDateTime(row["TESLIMTARIH"].ToString()).ToShortDateString() : String.Empty;
            lblSiparisDurum.Text = (row["DURUM"] != DBNull.Value) ? row["DURUM"].ToString() : String.Empty;
            lblSiparisAdedi.Text = (row["ADET"] != DBNull.Value) ? row["ADET"].ToString() : String.Empty;
            lblNakitPesin.Text = (row["NAKITPESIN"] != DBNull.Value) ? row["NAKITPESIN"].ToString() : String.Empty;
            lblNakitKalan.Text = (row["NAKITKALAN"] != DBNull.Value) ? row["NAKITKALAN"].ToString() : String.Empty;
            lblNakitOdemeNotu.Text = (row["NAKITODEMENOTU"] != DBNull.Value) ? row["NAKITODEMENOTU"].ToString() : String.Empty;
            lblKKartiPesin.Text = (row["KKARTPESIN"] != DBNull.Value) ? row["KKARTPESIN"].ToString() : String.Empty;
            lblKKartiKalan.Text = (row["KKARTKALAN"] != DBNull.Value) ? row["KKARTKALAN"].ToString() : String.Empty;
            lblKKartiOdemeNotu.Text = (row["KKARTODEMENOTU"] != DBNull.Value) ? row["KKARTODEMENOTU"].ToString() : String.Empty;
            lblCekPesin.Text = (row["CEKPESIN"] != DBNull.Value) ? row["CEKPESIN"].ToString() : String.Empty;
            lblCekKalan.Text = (row["CEKKALAN"] != DBNull.Value) ? row["CEKKALAN"].ToString() : String.Empty;
            lblCekOdemeNotu.Text = (row["CEKODEMENOTU"] != DBNull.Value) ? row["CEKODEMENOTU"].ToString() : String.Empty;
            lblNot.Text = (row["SIPARISNOT"] != DBNull.Value) ? row["SIPARISNOT"].ToString() : String.Empty;

            lblAcilim.Text = (row["ACILIM"] != DBNull.Value) ? row["ACILIM"].ToString() : String.Empty;
            lblIcKasaGenisligi.Text = (row["ICKASAGENISLIK"] != DBNull.Value) ? row["ICKASAGENISLIK"].ToString() : String.Empty;
            lblIcKasaYuksekligi.Text = (row["ICKASAYUKSEKLIK"] != DBNull.Value) ? row["ICKASAYUKSEKLIK"].ToString() : String.Empty;
            lblDisKasaIcPervazFarki.Text = (row["DISKASAICPERVAZFARK"] != DBNull.Value) ? row["DISKASAICPERVAZFARK"].ToString() : String.Empty;
            lblDuvarKalinligi.Text = (row["DUVARKALINLIK"] != DBNull.Value) ? row["DUVARKALINLIK"].ToString() : String.Empty;
            lblDisSolPervaz.Text = (row["DISSOLPERVAZ"] != DBNull.Value) ? row["DISSOLPERVAZ"].ToString() : String.Empty;
            lblDisUstPervaz.Text = (row["DISUSTPERVAZ"] != DBNull.Value) ? row["DISUSTPERVAZ"].ToString() : String.Empty;
            lblDisSagPervaz.Text = (row["DISSAGPERVAZ"] != DBNull.Value) ? row["DISSAGPERVAZ"].ToString() : String.Empty;
            lblIcSolPervaz.Text = (row["ICSOLPERVAZ"] != DBNull.Value) ? row["ICSOLPERVAZ"].ToString() : String.Empty;
            lblIcUstPervaz.Text = (row["ICUSTPERVAZ"] != DBNull.Value) ? row["ICUSTPERVAZ"].ToString() : String.Empty;
            lblIcSagPervaz.Text = (row["ICSAGPERVAZ"] != DBNull.Value) ? row["ICSAGPERVAZ"].ToString() : String.Empty;

            int siparisAdedi;
            if (Int32.TryParse(lblSiparisAdedi.Text, out siparisAdedi))
            {
                if (siparisAdedi > 1)
                {
                    lblSiparisNo.Text = siparisNo + ".1 / " + siparisNo + "." + siparisAdedi;
                }
            }

            KapiTurAyarla(siparisNo);
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SiparisFormYanginGuncelle.aspx?SiparisID=" + this.SiparisID + "&SeriAdi=" + this.SeriAdi);
        }

        private void KapiTurAyarla(string siparisNo)
        {
            if (String.IsNullOrEmpty(siparisNo))
                return;

            if (siparisNo[0] == 'Y')
            {
                trYangin1.Visible = true;
                trYangin2.Visible = true;
                lblKapiTur.Text = "YANGIN";
                lblKapiTur.Text = lblYanginKapiCins.Text == string.Empty ? this.SeriAdi : lblYanginKapiCins.Text;
            }
            else if (siparisNo[0] == 'P')
            {
                trPorte1.Visible = true;
                trPorte2.Visible = true;
                lblKapiTur.Text = "PORTE";
            }

        }
    }
}
using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Business.DataTypes;
using KobsisSiparisTakip.Web.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace KobsisSiparisTakip.Web
{
    public partial class SiparisKayit : KobsisBasePage
    {
        string ANKARA_IL_KODU = "6";
        public string SiparisSeri
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Request.QueryString["SiparisSeri"]))
                {
                    return Request.QueryString["SiparisSeri"].ToString();
                }
                else
                    return String.Empty;
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(this.SiparisSeri))
                {
                    MusteriBilgileriDoldur();
                    IlleriGetir();
                    GenerateForm();
                }
            }
        }

        private void MusteriBilgileriDoldur()
        {
            if (SessionManager.MusteriBilgi == null)
                return;
            lblSiparisSeri.Text = SessionManager.SiparisSeri.Where(q => q.SiparisSeriID == Convert.ToInt32(this.SiparisSeri)).FirstOrDefault().SeriAdi;
            lblFirmaAdi.Text = SessionManager.MusteriBilgi.Adi;
            lblFirmaAdres.Text = SessionManager.MusteriBilgi.Adres;
            lblFirmaFaks.Text = SessionManager.MusteriBilgi.Faks;
            lblFirmaMail.Text = SessionManager.MusteriBilgi.Mail;
            lblFirmaTelefon.Text = SessionManager.MusteriBilgi.Tel;
            lblFirmaWebAdres.Text = SessionManager.MusteriBilgi.Web;
            if (SessionManager.MusteriBilgi.LogoID.HasValue)
                imgFirmaLogo.ImageUrl = "ImageForm.aspx?ImageID=" + SessionManager.MusteriBilgi.LogoID.Value;
            else
                imgFirmaLogo.ImageUrl = "/App_Themes/Theme/Raster/BlankProfile.gif";
        }

        private void GenerateForm()
        {
            var generator = new FormGenerator()
            {
                SiparisSeri = this.SiparisSeri,
                IslemTipi = FormIslemTipi.Kaydet
            };
            generator.Generate(this.divSiparisFormKontrolleri);
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (rdtTeslimTarih.SelectedDate == null)
            {
                MessageBox.Uyari(this.Page, "Teslim tarihi girmelisiniz!");
                return;
            }

            //Montaj kota kontrolu acik ise
            if (SessionManager.MontajKotaKontrolu != null && SessionManager.MontajKotaKontrolu == "1")
            {
                MontajBS montajBS = new MontajBS();
                int yapilanMontajSayisi = montajBS.GunlukMontajSayisiniGetir(rdtTeslimTarih.SelectedDate.Value);
                DataTable dt = montajBS.GunlukMontajKotaBilgisiGetir(rdtTeslimTarih.SelectedDate.Value);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    bool montajKabul = Convert.ToBoolean(row["MONTAJKABUL"]);
                    if (!montajKabul)
                    {
                        MessageBox.Uyari(this.Page, rdtTeslimTarih.SelectedDate.Value.Date.ToShortDateString() + " tarihi için montaj alınamaz!");
                        return;
                    }
                    else
                    {
                        int gunlukMontakKotaDegeri = Convert.ToInt32(row["MAXMONTAJSAYI"]);
                        if (yapilanMontajSayisi >= gunlukMontakKotaDegeri)
                        {
                            MessageBox.Uyari(this.Page, rdtTeslimTarih.SelectedDate.Value.Date.ToShortDateString() + " tarihi için montaj kotası (" + gunlukMontakKotaDegeri.ToString() + ") değerine ulaşılmıştır.");
                            return;
                        }
                    }
                }
                else
                {
                    if (yapilanMontajSayisi >= SessionManager.MontajKotaVarsayilan)
                    {
                        MessageBox.Uyari(this.Page, rdtTeslimTarih.SelectedDate.Value.Date.ToShortDateString() + " tarihi için montaj kotası (" + SessionManager.MontajKotaVarsayilan.ToString() + ") değerine ulaşılmıştır.");
                        return;
                    }
                }
            }

            int? siparisID = null;
            if (this.SiparisID != string.Empty) { siparisID = Convert.ToInt32(this.SiparisID); }
            var parametreler = new List<DbParametre>();
            var paramID = new DbParametre() { ParametreAdi = "ID", ParametreDegeri = siparisID, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramSeriID = new DbParametre() { ParametreAdi = "SeriID", ParametreDegeri = this.SiparisSeri, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramSiparisTarih = new DbParametre() { ParametreAdi = "SiparisTarih", ParametreDegeri = rdtSiparisTarih.SelectedDate, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramTeslimTarihi = new DbParametre() { ParametreAdi = "TeslimTarih", ParametreDegeri = rdtTeslimTarih.SelectedDate, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramMusteriAd = new DbParametre() { ParametreAdi = "MusteriAd", ParametreDegeri = !string.IsNullOrWhiteSpace(txtMusteriAd.Text) ? txtMusteriAd.Text : null, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 100 };
            var paramMusteriSoyad = new DbParametre() { ParametreAdi = "MusteriSoyad", ParametreDegeri = !string.IsNullOrWhiteSpace(txtMusteriSoyad.Text) ? txtMusteriSoyad.Text : null, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 100 };
            var paramMusteriAdres = new DbParametre() { ParametreAdi = "MusteriAdres", ParametreDegeri = !string.IsNullOrWhiteSpace(txtMusteriAdres.Text) ? txtMusteriAdres.Text : null, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 500 };
            var paramMusteriIl = new DbParametre() { ParametreAdi = "MusteriIlKod", ParametreDegeri = (ddlMusteriIl.SelectedItem != null) ? ddlMusteriIl.SelectedValue : null, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramMusteriIlce = new DbParametre() { ParametreAdi = "MusteriIlceKod", ParametreDegeri = (ddlMusteriIlce.SelectedItem != null) ? ddlMusteriIlce.SelectedValue : null, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramMusteriSemt = new DbParametre() { ParametreAdi = "MusteriSemtKod", ParametreDegeri = (ddlMusteriSemt.SelectedItem != null) ? ddlMusteriSemt.SelectedValue : null, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramMusteriEvTel = new DbParametre() { ParametreAdi = "MusteriEvTel", ParametreDegeri = !string.IsNullOrWhiteSpace(txtMusteriEvTel.Text) ? txtMusteriEvTel.Text : null, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriIsTel = new DbParametre() { ParametreAdi = "MusteriIsTel", ParametreDegeri = !string.IsNullOrWhiteSpace(txtMusteriIsTel.Text) ? txtMusteriIsTel.Text : null, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriCepTel = new DbParametre() { ParametreAdi = "MusteriCepTel", ParametreDegeri = !string.IsNullOrWhiteSpace(txtMusteriCepTel.Text) ? txtMusteriCepTel.Text : null, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriFirmaAdi = new DbParametre() { ParametreAdi = "FirmaAdi", ParametreDegeri = !string.IsNullOrWhiteSpace(txtMusteriFirmaAdi.Text) ? txtMusteriFirmaAdi.Text : null, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 250 };
            var paramAdet = new DbParametre() { ParametreAdi = "Adet", ParametreDegeri = !string.IsNullOrWhiteSpace(txtSiparisAdeti.Text) ? txtSiparisAdeti.Text : null, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramCreatedBy = new DbParametre() { ParametreAdi = "CreatedBy", ParametreDegeri = SessionManager.KullaniciBilgi.KullaniciID, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramCreatedTime = new DbParametre() { ParametreAdi = "CreatedTime", ParametreDegeri = DateTime.Now, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramUpdatedBy = new DbParametre() { ParametreAdi = "UpdatedBy", ParametreDegeri = SessionManager.KullaniciBilgi.KullaniciID, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramUpdatedTime = new DbParametre() { ParametreAdi = "UpdatedTime", ParametreDegeri = DateTime.Now, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramDurum = new DbParametre() { ParametreAdi = "Durum", ParametreDegeri = 1, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };

            parametreler.Add(paramID);
            parametreler.Add(paramSeriID);
            parametreler.Add(paramSiparisTarih);
            parametreler.Add(paramTeslimTarihi);
            parametreler.Add(paramMusteriAd);
            parametreler.Add(paramMusteriSoyad);
            parametreler.Add(paramMusteriAdres);
            parametreler.Add(paramMusteriIl);
            parametreler.Add(paramMusteriIlce);
            parametreler.Add(paramMusteriSemt);
            parametreler.Add(paramMusteriEvTel);
            parametreler.Add(paramMusteriIsTel);
            parametreler.Add(paramMusteriCepTel);
            parametreler.Add(paramMusteriFirmaAdi);
            parametreler.Add(paramAdet);
            parametreler.Add(paramCreatedBy);
            parametreler.Add(paramCreatedTime);
            parametreler.Add(paramUpdatedBy);
            parametreler.Add(paramUpdatedTime);
            parametreler.Add(paramDurum);


            var siparisBS = new SiparisBS();
            var metadataList = siparisBS.SiparisMetdataGetir(SessionManager.MusteriBilgi.MusteriID.Value.ToString());
            foreach (SiparisMetadata metadata in metadataList)
            {
                var paramMetadata = new DbParametre()
                {
                    ParametreAdi = metadata.KolonAdi,
                    ParametreDegeri = KontrolDegeriBul(metadata.KontrolAdi, metadata.YerlesimTabloID),
                    VeriTipi = VeriTipiBelirle(metadata.VeriTipAdi),
                    ParametreBoyutu = 50
                };
                parametreler.Add(paramMetadata);
            }

            siparisID = siparisBS.SiparisKaydetGuncelle(parametreler, SessionManager.MusteriBilgi.Kod);
            if (siparisID.HasValue)
            {
                MessageBox.Basari(this, "Sipariş eklendi.");
                Response.Redirect("~/SiparisGoruntule.aspx?SiparisID=" + siparisID + "&SiparisSeri=" + this.SiparisSeri);
            }
            else
                MessageBox.Hata(this, "Sipariş eklenemedi.");
        }
        
        private object KontrolDegeriBul(string kontrolAdi, string yerlesimTabloID)
        {
            object kontrolDegeri = null;
            var control = KontrolBul(divSiparisFormKontrolleri, kontrolAdi.Replace(" ", string.Empty) + yerlesimTabloID);

            if (control is RadTextBox || control is RadMaskedTextBox || control is RadNumericTextBox)
            {
                kontrolDegeri = ((RadTextBox)control).Text;
            }
            else if (control is CheckBox)
            {
                kontrolDegeri = ((CheckBox)control).Checked;
            }
            else if (control is RadDateTimePicker)
            {
                kontrolDegeri = ((RadDateTimePicker)control).SelectedDate;
            }
            else if (control is RadDropDownList)
            {
                if (((RadDropDownList)control).SelectedItem != null)
                    kontrolDegeri = ((RadDropDownList)control).SelectedValue;
                else
                    kontrolDegeri = null;
            }
            else if (control is Label)
            {
                kontrolDegeri = ((Label)control).Text;
            }

            return kontrolDegeri;
        }

        WebControl wc1;
        private WebControl KontrolBul(WebControl wc, string kontrolID)
        {
            if (wc.ID == kontrolID)
                wc1 = wc;
            else
            {
                for (int i = 0; i < wc.Controls.Count; i++)
                {
                    WebControl wcChild = (WebControl)wc.Controls[i];
                    KontrolBul(wcChild, kontrolID);
                }
            }
            return wc1;
        }

        private SqlDbType VeriTipiBelirle(string veriTipAdi)
        {
            var sqlType = new SqlDbType();

            switch (veriTipAdi)
            {
                case VeriTipi.BOOLEAN:
                    sqlType = SqlDbType.Bit;
                    break;
                case VeriTipi.DATETIME:
                    sqlType = SqlDbType.DateTime;
                    break;
                case VeriTipi.DECIMAL:
                    sqlType = SqlDbType.Decimal;
                    break;
                case VeriTipi.INTEGER:
                    sqlType = SqlDbType.Int;
                    break;
                case VeriTipi.STRING:
                    sqlType = SqlDbType.VarChar;
                    break;
            }
            return sqlType;
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

        protected void ddlMusteriIlce_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlMusteriSemt.Items.Clear();
            ddlMusteriSemt.Text = "";
            SemtleriGetir(e.Value);
        }
    }
}
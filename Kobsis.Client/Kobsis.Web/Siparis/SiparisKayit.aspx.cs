using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Kobsis.Business;
using Kobsis.DataType;
using Kobsis.Generation;
using Kobsis.Util;
using Kobsis.Web.Helper;
using Telerik.Web.UI;

namespace Kobsis.Web.Siparis
{
    public partial class SiparisKayit : KobsisBasePage
    {
        string ANKARA_IL_KODU = "6";
        public string SiparisSeri
        {
            get
            {
                return !String.IsNullOrWhiteSpace(Request.QueryString["SiparisSeri"]) ? Request.QueryString["SiparisSeri"] : String.Empty;
            }
        }

        public string SiparisID
        {
            get
            {
                return !String.IsNullOrEmpty(Request.QueryString["SiparisID"]) ? Request.QueryString["SiparisID"] : String.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(this.SiparisSeri))
                {
                    IlleriGetir();

                    if (this.SiparisID != string.Empty)
                    {
                        SiparisBilgileriniGetir();
                    }

                    MusteriBilgileriDoldur();
                    GenerateForm();
                }
            }
            if (Page.IsPostBack)
            {
                GenerateForm();
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
                IslemTipi = (this.SiparisID != string.Empty) ? FormIslemTipi.Guncelle : FormIslemTipi.Kaydet
            };
            generator.Generate(this.divSiparisFormKontrolleri);
        }

        private void SiparisBilgileriniGetir()
        {
            DataTable dt = new SiparisBS().SiparisGetir(this.SiparisID);
            SessionManager.SiparisBilgi = dt;

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];
            txtSiparisNo.Text = row["SiparisNo"] != DBNull.Value ? row["SiparisNo"].ToString() : string.Empty;
            txtSiparisAdeti.Text = row["Adet"] != DBNull.Value ? row["Adet"].ToString() : string.Empty;
            if (row["SiparisTarih"] != DBNull.Value)
                rdtSiparisTarih.SelectedDate = Convert.ToDateTime(row["SiparisTarih"].ToString());
            if (row["TeslimTarih"] != DBNull.Value)
                rdtTeslimTarih.SelectedDate = Convert.ToDateTime(row["TeslimTarih"].ToString());
            txtMusteriFirmaAdi.Text = row["FirmaAdi"] != DBNull.Value ? row["FirmaAdi"].ToString() : string.Empty;
            txtMusteriAd.Text = row["MusteriAd"] != DBNull.Value ? row["MusteriAd"].ToString() : string.Empty;
            txtMusteriSoyad.Text = row["MusteriSoyad"] != DBNull.Value ? row["MusteriSoyad"].ToString() : string.Empty;
            txtMusteriEvTel.Text = row["MusteriEvTel"] != DBNull.Value ? row["MusteriEvTel"].ToString() : string.Empty;
            txtMusteriCepTel.Text = row["MusteriCepTel"] != DBNull.Value ? row["MusteriCepTel"].ToString() : string.Empty;
            txtMusteriIsTel.Text = row["MusteriIsTel"] != DBNull.Value ? row["MusteriIsTel"].ToString() : string.Empty;
            txtMusteriAdres.Text = row["MusteriAdres"] != DBNull.Value ? row["MusteriAdres"].ToString() : string.Empty;
            if (row["MusteriIlKod"] != DBNull.Value)
            {
                DropDownSelectedIndexAyarla(ddlMusteriIl, row["MusteriIlKod"].ToString());
                IlceleriGetir(ddlMusteriIl.SelectedValue);
            }
            if (row["MusteriIlceKod"] != DBNull.Value)
            {
                DropDownSelectedIndexAyarla(ddlMusteriIlce, row["MusteriIlceKod"].ToString());
                SemtleriGetir(ddlMusteriIlce.SelectedValue);
            }
            if (row["MusteriSemtKod"] != DBNull.Value)
                DropDownSelectedIndexAyarla(ddlMusteriSemt, row["MusteriSemtKod"].ToString());
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

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (rdtTeslimTarih.SelectedDate == null)
            {
                MessageBox.Uyari(this.Page, "Teslim tarihi girmelisiniz!");
                return;
            }

            //Teslimat kota kontrolu acik ise
            if (SessionManager.TeslimatKotaKontrolu != null && SessionManager.TeslimatKotaKontrolu == "1")
            {
                TeslimatBS teslimatBS = new TeslimatBS();
                int yapilanTeslimatSayisi = teslimatBS.GunlukTeslimatSayisiniGetir(rdtTeslimTarih.SelectedDate.Value);
                DataTable dt = teslimatBS.GunlukTeslimatKotaBilgisiGetir(rdtTeslimTarih.SelectedDate.Value);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    bool teslimatKabul = Convert.ToBoolean(row["TeslimatKabul"]);
                    if (!teslimatKabul)
                    {
                        MessageBox.Uyari(this.Page, rdtTeslimTarih.SelectedDate.Value.Date.ToShortDateString() + " tarihi için teslimat alınamaz!");
                        return;
                    }
                    else
                    {
                        int gunlukMontakKotaDegeri = Convert.ToInt32(row["MaxTeslimatSayi"]);
                        if (yapilanTeslimatSayisi >= gunlukMontakKotaDegeri)
                        {
                            MessageBox.Uyari(this.Page, rdtTeslimTarih.SelectedDate.Value.Date.ToShortDateString() + " tarihi için teslimat kotası (" + gunlukMontakKotaDegeri.ToString() + ") değerine ulaşılmıştır.");
                            return;
                        }
                    }
                }
                else
                {
                    if (yapilanTeslimatSayisi >= SessionManager.TeslimatKotaVarsayilan)
                    {
                        MessageBox.Uyari(this.Page, rdtTeslimTarih.SelectedDate.Value.Date.ToShortDateString() + " tarihi için teslimat kotası (" + SessionManager.TeslimatKotaVarsayilan.ToString() + ") değerine ulaşılmıştır.");
                        return;
                    }
                }
            }

            int? siparisID = null;
            if (this.SiparisID != string.Empty) { siparisID = Convert.ToInt32(this.SiparisID); }
            var parametreler = new List<DbParametre>();
            var paramError = new DbParametre() { ParametreAdi = "ErrorMessage", ParametreYonu = ParameterDirection.Output, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 4000 };
            var paramID = new DbParametre() { ParametreAdi = "ID", ParametreYonu = ParameterDirection.InputOutput, ParametreDegeri = siparisID, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
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
            var paramUpdatedBy = new DbParametre() { ParametreAdi = "UpdatedBy", ParametreDegeri = SessionManager.KullaniciBilgi.KullaniciID, VeriTipi = SqlDbType.Int, ParametreBoyutu = 50 };
            var paramDurum = new DbParametre { ParametreAdi = "DurumID", ParametreDegeri = 1, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };

            parametreler.Add(paramError);
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
            parametreler.Add(paramUpdatedBy);
            parametreler.Add(paramDurum);


            var siparisBS = new SiparisBS();
            var formGenerator = new FormGenerator();
            var metadataList = siparisBS.SiparisMetdataGetir(SessionManager.MusteriBilgi.MusteriID.Value.ToString());
            foreach (SiparisMetadata metadata in metadataList)
            {
                var paramMetadata = new DbParametre()
                {
                    ParametreAdi = metadata.KolonAdi,
                    ParametreDegeri = formGenerator.KontrolDegeriBul(divSiparisFormKontrolleri, metadata.KontrolAdi, metadata.YerlesimTabloID),
                    VeriTipi = formGenerator.VeriTipiBelirle(metadata.VeriTipAdi),
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

        protected void IlleriGetir()
        {
            DataTable dt = new ReferansDataBS().IlleriGetir();
            if (dt.Rows.Count > 0)
            {
                ddlMusteriIl.DataSource = dt;
                ddlMusteriIl.DataTextField = "IlAd";
                ddlMusteriIl.DataValueField = "IlKod";
                ddlMusteriIl.DataBind();
            }
            else
            {
                ddlMusteriIl.DataSource = null;
                ddlMusteriIl.DataBind();
            }
        }

        protected void IlceleriGetir(string ilKod)
        {
            DataTable dt = new ReferansDataBS().IlceleriGetir(ilKod);

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
            DataTable dt = new ReferansDataBS().SemtleriGetir(ilceKod);

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

        protected void ddlMusteriIl_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
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
    }
}
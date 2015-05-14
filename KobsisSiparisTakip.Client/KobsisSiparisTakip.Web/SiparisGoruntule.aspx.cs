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

namespace KobsisSiparisTakip.Web
{
    public partial class SiparisGoruntule : System.Web.UI.Page
    {
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
                    SiparisBilgileriniGetir();
                    GenerateForm();
                    PopupPageHelper.OpenPopUp(btnYazdir, "Print/Print.aspx?SiparisSeri=" + this.SiparisSeri, "", true, false, true, false, false, false, 1024, 800, true, false, "onclick");
                }
            }
            if (Page.IsPostBack)
            {
                GenerateForm();
            }
        }

        private void SiparisBilgileriniGetir()
        {
            DataTable dt = new SiparisBS().SiparisGetir(this.SiparisID);
            SessionManager.SiparisBilgi = dt;

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];
            lblSiparisNo.Text = row["SiparisNo"] != DBNull.Value ? row["SiparisNo"].ToString() : string.Empty;
            lblSiparisAdeti.Text = row["Adet"] != DBNull.Value ? row["Adet"].ToString() : string.Empty;
            lblSiparisTarih.Text = row["SiparisTarih"] != DBNull.Value ? Convert.ToDateTime(row["SiparisTarih"].ToString()).ToShortDateString() : string.Empty;
            lblTeslimTarih.Text = row["TeslimTarih"] != DBNull.Value ? Convert.ToDateTime(row["TeslimTarih"].ToString()).ToShortDateString() : string.Empty;
            lblMusteriFirmaAdi.Text = row["FirmaAdi"] != DBNull.Value ? row["FirmaAdi"].ToString() : string.Empty;
            lblMusteriAd.Text = row["MusteriAd"] != DBNull.Value ? row["MusteriAd"].ToString() : string.Empty;
            lblMusteriSoyad.Text = row["MusteriSoyad"] != DBNull.Value ? row["MusteriSoyad"].ToString() : string.Empty;
            lblMusteriEvTel.Text = row["MusteriEvTel"] != DBNull.Value ? row["MusteriEvTel"].ToString() : string.Empty;
            lblMusteriCepTel.Text = row["MusteriCepTel"] != DBNull.Value ? row["MusteriCepTel"].ToString() : string.Empty;
            lblMusteriIsTel.Text = row["MusteriIsTel"] != DBNull.Value ? row["MusteriIsTel"].ToString() : string.Empty;
            lblMusteriAdres.Text = row["MusteriAdres"] != DBNull.Value ? row["MusteriAdres"].ToString() : string.Empty;
            lblMusteriIl.Text = row["MusteriIl"] != DBNull.Value ? row["MusteriIl"].ToString() : string.Empty;
            lblMusteriIlce.Text = row["MusteriIlce"] != DBNull.Value ? row["MusteriIlce"].ToString() : string.Empty;
            lblMusteriSemt.Text = row["MusteriSemt"] != DBNull.Value ? row["MusteriSemt"].ToString() : string.Empty;
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
                IslemTipi = FormIslemTipi.Goruntule
            };
            generator.Generate(this.divSiparisFormKontrolleri);
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SiparisGuncelle.aspx?SiparisID=" + this.SiparisID + "&SiparisSeri=" + this.SiparisSeri);
        }
    }
}
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
    }
}
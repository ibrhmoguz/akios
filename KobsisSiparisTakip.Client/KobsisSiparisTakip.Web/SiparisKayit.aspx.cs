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
            var generator = new FormGenerator() { SiparisSeri = this.SiparisSeri, IslemTipi = FormIslemTipi.Kaydet };
            generator.Generate(this.divSiparisFormKontrolleri);
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {

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
using System;
using System.Data;
using Akios.Util;

namespace Akios.WebClient.Print
{
    public partial class GunlukIsTakip : System.Web.UI.Page
    {
        public string Tarih
        {
            get
            {
                return !String.IsNullOrEmpty(Request.QueryString["Tarih"]) ? Request.QueryString["Tarih"] : string.Empty;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MusteriLogoAyarla();
                RaporOlustur();
                lblTarih.Text = Tarih;
            }
        }

        private void MusteriLogoAyarla()
        {
            if (SessionManager.MusteriBilgi.LogoID.HasValue)
                imgFirmaLogo.ImageUrl = "ImageForm.aspx?ImageID=" + SessionManager.MusteriBilgi.LogoID.Value;
            else
                imgFirmaLogo.ImageUrl = "/App_Themes/Theme/Raster/BlankProfile.gif";
        }

        private void RaporOlustur()
        {
            DataTable dt = SessionManager.GunlukIsTakipListesi;

            if (dt.Rows.Count > 0)
            {
                grdSiparisler.DataSource = dt;
                grdSiparisler.DataBind();
            }
            else
            {
                grdSiparisler.DataSource = null;
                grdSiparisler.DataBind();
            }
        }
    }
}
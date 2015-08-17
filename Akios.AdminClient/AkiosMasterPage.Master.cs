using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Akios.Business;
using Akios.DataType;
using Akios.Util;
using Telerik.Web.UI;

namespace Akios.AdminWebClient
{
    public partial class AkiosMasterPage : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SeciliMenuAyarla();
                YetkiyeGoreMenuAyarla();
                MusteriListesiYukle();
            }
        }

        private void MusteriListesiYukle()
        {
            DataTable dt;
            if (SessionManager.MusteriListesi == null)
            {
                dt = new MusteriBS().MusteriListesiGetir();
                SessionManager.MusteriListesi = dt;
            }
            else
            {
                dt = SessionManager.MusteriListesi;
            }

            if (dt.Rows.Count > 0)
            {
                ddlMusteri.DataSource = dt;
                ddlMusteri.DataTextField = "Adi";
                ddlMusteri.DataValueField = "MusteriID";
                ddlMusteri.DataBind();
                ddlMusteri.Items.Insert(0, new DropDownListItem("Seçiniz", "0"));
            }
            else
            {
                ddlMusteri.DataSource = null;
                ddlMusteri.DataBind();
            }
        }

        private void YetkiyeGoreMenuAyarla()
        {
            if (SessionManager.KullaniciBilgi != null)
            {
                LabelUserName.Text += SessionManager.KullaniciBilgi.KullaniciAdi;
            }
        }

        private void SeciliMenuAyarla()
        {
            string url = Request.Url.AbsoluteUri;

            if (url.Contains("Musteri") ||
                    url.Contains("KullaniciTanimlama") ||
                    url.Contains("MusteriTanimlama") ||
                    url.Contains("PersonelTanimlama") ||
                    url.Contains("ReferansVeri") ||
                    url.Contains("UrunSeri") ||
                    url.Contains("Rapor") ||
                    url.Contains("Uyelik") ||
                    url.Contains("Hatalar") ||
                    url.Contains("FormOgeGuncelleme") ||
                    url.Contains("TeslimatKotaTanimla") ||
                    url.Contains("UygulamaAyarlari"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 0;
            }
            else if (url.Contains("FormOlustur") || url.Contains("FormYerlesim"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 1;
            }
            else if (url.Contains("SifreGuncelleme"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 2;
            }
        }

        protected void LB_Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var httpCookie = Response.Cookies[FormsAuthentication.FormsCookieName];
            if (httpCookie != null)
                httpCookie.Expires = DateTime.Now;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void RadRibbonBarMenu_Command(object sender, CommandEventArgs e)
        {
            NavigateUrl(e.CommandName);
        }

        protected void RadRibbonBarMenu_MenuItemClick(object sender, RibbonBarMenuItemClickEventArgs e)
        {
            NavigateUrl(e.Item.CommandName);
        }

        private void NavigateUrl(string url)
        {
            Response.Redirect(url);
        }
    }
}
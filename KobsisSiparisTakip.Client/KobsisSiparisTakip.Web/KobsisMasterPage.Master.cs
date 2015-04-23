using KobsisSiparisTakip.Business.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using KobsisSiparisTakip.Web.Util;
using System.Data;
using KobsisSiparisTakip.Business;
using Telerik.Web.UI;

namespace KobsisSiparisTakip.Web
{
    public partial class KobsisMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SiparisSeriYukle();
                SeciliMenuAyarla();
                YetkiyeGoreMenuAyarla();
            }
        }

        private void SiparisSeriYukle()
        {
            SiparisBarGroup.Items.Clear();
            DataTable dt = null;
            if (SessionManager.SiparisSeri == null)
            {
                dt = new SiparisSeriBS().SeriGetirMusteriIDGore(SessionManager.MusteriBilgi.MusteriID.Value);
                SessionManager.SiparisSeri = dt;
            }
            else
                dt = SessionManager.SiparisSeri;

            foreach (DataRow row in dt.Rows)
            {
                var seriAdi = row["SeriAdi"].ToString();
                var seriID = row["KapiSeriID"].ToString();
                var item = new RibbonBarButton()
                {
                    Text = seriAdi,
                    Size = RibbonBarItemSize.Large,
                    ImageUrlLarge = "App_Themes/Theme/Raster/porte.png",
                    CommandName = "SiparisKayit.aspx?SiparisSeri=" + seriID
                };
                SiparisBarGroup.Items.Add(item);
            }
        }

        private void YetkiyeGoreMenuAyarla()
        {
            if (SessionManager.KullaniciBilgi != null)
            {
                LabelUserName.Text += SessionManager.KullaniciBilgi.KullaniciAdi;
            }
            if (SessionManager.KullaniciBilgi != null && SessionManager.KullaniciBilgi.Rol != KullaniciRol.Yonetici)
            {
                RadRibbonBarMenu.Tabs[0].FindGroupByValue("Sorgula").Visible = false;
                RadRibbonBarMenu.Tabs[1].Visible = false;
                RadRibbonBarMenu.Tabs[2].Visible = false;
                RadRibbonBarMenu.Tabs[4].Visible = false;
            }
        }

        private void SeciliMenuAyarla()
        {
            string url = Request.Url.AbsoluteUri;

            if (url.Contains("Siparis"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 0;
            }
            else if (url.Contains("IsTakvimi"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 1;
            }
            else if (url.Contains("YonetimKonsolu") ||
                     url.Contains("KullaniciTanimlama") ||
                     url.Contains("PersonelTanimlama") ||
                     url.Contains("Hatalar") ||
                     url.Contains("FormOgeGuncelleme") ||
                     url.Contains("MontajKotaTanimla") ||
                     url.Contains("UygulamaAyarlari"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 2;
            }
            else if (url.Contains("SifreGuncelleme"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 3;
            }
            else if (url.Contains("GunlukIsTakipFormu") ||
                     url.Contains("KapiTipineGoreSatilanAdet") ||
                     url.Contains("IlIlceyeGoreSatilanAdet"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 4;
            }
        }

        protected void RadRibbonBarMenu_Command(object sender, CommandEventArgs e)
        {
            string url = e.CommandName;
            Response.Redirect(url);
        }

        protected void LB_Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void RadRibbonBarMenu_MenuItemClick(object sender, RibbonBarMenuItemClickEventArgs e)
        {
            string url = e.Item.CommandName;
            Response.Redirect(url);
        }

        protected void RadRibbonBarMenu_ButtonClick(object sender, RibbonBarButtonClickEventArgs e)
        {

        }
    }
}
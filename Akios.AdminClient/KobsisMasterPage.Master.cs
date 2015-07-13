using System;
using System.Collections.Generic;
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
    public partial class KobsisMasterPage : MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            MusteriRaporlariniYukle();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SeciliMenuAyarla();
                YetkiyeGoreMenuAyarla();
            }
        }

        private void MusteriRaporlariniYukle()
        {
            var tabRaporlar = RadRibbonBarMenu.Tabs[2];

            tabRaporlar.Groups.Clear();
            List<MusteriRapor> raporlar = null;
            if (SessionManager.MusteriRaporlar == null)
            {
                raporlar = new ReferansDataBS().MusteriRaporlariniGetir(SessionManager.MusteriBilgi.MusteriID.Value);
                SessionManager.MusteriRaporlar = raporlar;
            }
            else
                raporlar = SessionManager.MusteriRaporlar;

            foreach (MusteriRapor rapor in raporlar)
            {
                var ribbonBar = new RibbonBarGroup
                {
                    Text = rapor.RaporMenuBaslik
                };

                var button = new RibbonBarButton
                {
                    ID = "RibbonBarButton" + rapor.RaporId,
                    Text = rapor.RaporAdi,
                    CommandName = rapor.Dizin,
                    Size = RibbonBarItemSize.Large
                };

                if (!string.IsNullOrEmpty(rapor.IkonImajId))
                    button.ImageUrlLarge = "ImageForm.aspx?ImageID=" + rapor.IkonImajId;
                else
                    button.ImageUrlLarge = "/App_Themes/Theme/Raster/BlankProfile.gif";

                ribbonBar.Items.Add(button);
                tabRaporlar.Groups.Add(ribbonBar);
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

            if (url.Contains("YonetimKonsolu") ||
                    url.Contains("KullaniciTanimlama") ||
                    url.Contains("PersonelTanimlama") ||
                    url.Contains("Hatalar") ||
                    url.Contains("FormOgeGuncelleme") ||
                    url.Contains("TeslimatKotaTanimla") ||
                    url.Contains("UygulamaAyarlari"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 0;
            }
            else if (url.Contains("SifreGuncelleme"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 1;
            }
            else if (url.Contains("GunlukIsTakipFormu") ||
                     url.Contains("SeriyeGoreSatilanAdet") ||
                     url.Contains("IlIlceyeGoreSatilanAdet"))
            {
                RadRibbonBarMenu.SelectedTabIndex = 2;
            }
        }

        protected void LB_Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void RadRibbonBarMenu_Command(object sender, CommandEventArgs e)
        {
            NavigateURL(e.CommandName);
        }

        protected void RadRibbonBarMenu_MenuItemClick(object sender, RibbonBarMenuItemClickEventArgs e)
        {
            NavigateURL(e.Item.CommandName);
        }

        private void NavigateURL(string url)
        {
            Response.Redirect(url);
        }
    }
}
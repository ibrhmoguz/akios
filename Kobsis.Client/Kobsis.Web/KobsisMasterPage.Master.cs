using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Kobsis.Business;
using Kobsis.DataType;
using Kobsis.Util;
using Kobsis.Web.Helper;
using Telerik.Web.UI;

namespace Kobsis.Web
{
    public partial class KobsisMasterPage : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            SiparisSeriYukle();
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

        private void SiparisSeriYukle()
        {
            rbbSiparisSeriMenu.Items.Clear();
            List<SiparisSeri> seriList = null;
            if (SessionManager.SiparisSeri == null)
            {
                seriList = new ReferansDataBS().SeriGetirMusteriIdGore(SessionManager.MusteriBilgi.MusteriID.Value);
                SessionManager.SiparisSeri = seriList;
            }
            else
                seriList = SessionManager.SiparisSeri;

            foreach (SiparisSeri seri in seriList)
            {
                var item = new RibbonBarMenuItem()
                {
                    ID = seri.SiparisSeriID.ToString(),
                    Text = seri.SeriAdi,
                    Value = seri.SiparisSeriID.ToString(),
                    CommandName = "~/Siparis/SiparisKayit.aspx?SiparisSeri=" + seri.SiparisSeriID.ToString()
                };
                rbbSiparisSeriMenu.Items.Add(item);
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
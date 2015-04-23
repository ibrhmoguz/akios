using KobsisSiparisTakip.Business.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KobsisSiparisTakip.Web.Util
{
    public class KobsisBasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (SessionManager.KullaniciBilgi == null || !SessionManager.KullaniciBilgi.KullaniciID.HasValue)
            {
                Response.Redirect("Login.aspx");
            }
            else if (SessionManager.KullaniciBilgi.Rol == KullaniciRol.Kullanici)
            {
                string pageUrl = this.Request.Url.AbsoluteUri;

                if (!pageUrl.Contains("SiparisFormKayit") &&
                    !pageUrl.Contains("SiparisFormYanginKayit") &&
                    !pageUrl.Contains("SifreGuncelleme"))
                {
                    // Kullanici rolunun yetkisi olmayan sayfaya erisimi
                    // engellenip, kayit sayfasina yonlendirilir.
                    Response.Redirect("SiparisFormKayit.aspx?KapiTipi=Nova");
                }
            }

            base.OnLoad(e);
        }
    }
}
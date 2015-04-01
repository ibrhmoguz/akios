using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KobsisSiparisTakip.Web
{
    public class KobsisBasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (Session["yetki"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (Session["yetki"].ToString() == "Kullanici")
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
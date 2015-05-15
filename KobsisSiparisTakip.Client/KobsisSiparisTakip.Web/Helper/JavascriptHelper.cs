using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KobsisSiparisTakip.Web.Helper
{
    public static class JavascriptHelper
    {

        public static string ScriptTaglariArasinaAl(string pYazi)
        {
            return string.Format("<script type=\"text/javascript\">\r\n " +
                    " <!--\r\n " +
                    " {0}\r\n " +
                    "// -->\r\n " +
                    "</script>", pYazi);
        }

        private static void ScriptRegister(this Page pPage, string pJavascript, string pKey)
        {
            if (ScriptManager.GetCurrent(pPage) == null)
                return;

            if (ScriptManager.GetCurrent(pPage).IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(pPage, pPage.GetType(), pKey, pJavascript, true);
            }

            if (!pPage.ClientScript.IsClientScriptBlockRegistered(pKey))
            {
                pJavascript = ScriptTaglariArasinaAl(pJavascript);
                pPage.ClientScript.RegisterClientScriptBlock(pPage.GetType(), pKey, pJavascript);
            }
        }

        public static void ScriptRegisterFile(this Page pPage, string pJavascriptFileName, string pKey)
        {
            if (!pPage.ClientScript.IsClientScriptIncludeRegistered(pKey))
            {
                string url = pPage.ResolveClientUrl(pJavascriptFileName);
                pPage.ClientScript.RegisterClientScriptInclude(pKey, url);
            }
        }




        public static void ScriptEkle(this Page pPage, string pScript, string pKey)
        {
            ScriptRegister(pPage, pScript, pKey);
        }


        public static void Alert(this Page pPage, string pMessage, string pKey)
        {
            pMessage = pMessage.Replace("'", "\'");
            pMessage = string.Format("alert('{0}');", pMessage);
            JavascriptHelper.ScriptEkle(pPage, pMessage, pKey);
        }

        public static void Alert(this Page pPage, string pMessage)
        {
            Alert(pPage, pMessage, "Alert");
        }



        public static void PopUpiKapat()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(ScriptTaglariArasinaAl("javascript:window.close();"));
            HttpContext.Current.Response.End();
        }
        public static void SayfaRefresh()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(ScriptTaglariArasinaAl("javascript:window.opener.location=window.opener.location;"));
            HttpContext.Current.Response.End();
        }

        public static void PopUpiKapatAcanPencereyiRefresh()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(ScriptTaglariArasinaAl("javascript:window.opener.location=window.opener.location; window.close();"));
            HttpContext.Current.Response.End();
        }
        public static void PopUpiKapatAcanPencereyiYonlendir(string pPageUrl)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(ScriptTaglariArasinaAl("javascript:window.opener.location='" + pPageUrl + "'; window.close();"));
            HttpContext.Current.Response.End();
        }


        public static void PopUpWindowBaslangictaAc(this Page pPage, string pPageUrl)
        {
            PopUpWindowBaslangictaAc(pPage, pPageUrl, 400, 600, false);
        }

        public static void PopUpWindowBaslangictaAc(this Page pPage, string pPageUrl, int pWidth, int pHeight)
        {
            PopUpWindowBaslangictaAc(pPage, pPageUrl, pWidth, pHeight, false);
        }


        public static void PopUpWindowBaslangictaAc(this Page pPage, string pPageUrl, int pWidth, int pHeight, bool pResize)
        {

            var protocol = string.Empty;
            if (!pPage.ClientScript.IsStartupScriptRegistered(pPage.GetType(), "PopUp"))
            {
                if (!HttpContext.Current.Request.Url.Scheme.Contains("https"))
                {
                    protocol = "http://";
                }
                else
                {
                    protocol = "https://";
                }


                string script = "";
                if (pPageUrl.Contains("~"))
                {
                    pPageUrl = protocol + pPageUrl.Replace("~", HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath).Replace("//", "/");

                }
                script = string.Format("CreateWnd('{0}', {1}, {2}, {3});", pPageUrl, pWidth, pHeight, pResize ? "true" : "false");
                pPage.ClientScript.RegisterStartupScript(pPage.GetType(), "PopUp", ScriptTaglariArasinaAl(script));
            }
        }

        public static void PopUpWindowBaslangictaMaximizeAc(this Page pPage, string pPageUrl, int pWidth, int pHeight)
        {
            pPage.Session["maximizeWindow"] = "true";
            PopUpWindowBaslangictaAc(pPage, pPageUrl, pWidth, pHeight, true);
        }

        public static void PopUpWindowEventiEkle(WebControl pControl, string pPageUrl)
        {
            PopUpWindowEventiEkle(pControl, pPageUrl, 600, 800);
        }

        /// <summary>
        /// LinkButtonDinamicX.ascx ler ıcın overload edılmıstır.Baska UserControllerle kullanılması sakıncalıdır.
        /// </summary>
        /// <param name="pControl">LinkButtonDinamicX.ascx tipinde LinkButon</param>
        /// <param name="pPageUrl">Sayfanın linki</param>
        public static void PopUpWindowEventiEkle(UserControl pControl, string pPageUrl)
        {
            PopUpWindowEventiEkle(pControl, pPageUrl, 600, 800);
        }

        public static void PopUpWindowEventiEkle(WebControl pControl, string pPageUrl, int pWidth, int pHeight)
        {
            PopUpWindowEventiEkle(pControl, pPageUrl, pWidth, pHeight, false);
        }

        /// <summary>
        /// LinkButtonDinamicX.ascx ler ıcın overload edılmıstır.Baska UserControllerle kullanılması sakıncalıdır.
        /// </summary>
        /// <param name="pControl">LinkButtonDinamicX.ascx tipinde LinkButon</param>
        /// <param name="pPageUrl">Sayfanın linki</param>
        /// <param name="pWidth">Popup genişliği</param>
        /// <param name="pHeight">Popup yüksekliği</param>
        public static void PopUpWindowEventiEkle(UserControl pControl, string pPageUrl, int pWidth, int pHeight)
        {
            PopUpWindowEventiEkle(pControl, pPageUrl, pWidth, pHeight, false);
        }

        // CreateWnd defined in Popup.js...
        public static void PopUpWindowEventiEkle(object pControl, string pPageUrl, int pWidth, int pHeight, bool pResize)
        {
            string jsString = string.Empty;
            string protocol = string.Empty;

            if (!HttpContext.Current.Request.Url.Scheme.Contains("https"))
            {
                protocol = "http://";
            }
            else
            {
                protocol = "https://";
            }

            if (pPageUrl.Contains("~"))
            {
                pPageUrl = protocol + pPageUrl.Replace("~", HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath).Replace("//", "/");
            }

            jsString = string.Format("javascript:CreateWnd('{0}', {1}, {2}, {3});", pPageUrl, pWidth, pHeight, pResize ? "true" : "false");
            if (pControl is LinkButton)
            {
                (pControl as LinkButton).OnClientClick = jsString;
            }
            else if (pControl is HyperLink)
            {
                (pControl as HyperLink).NavigateUrl = jsString;
            }
            else if (pControl is UserControl)
            {
                (((pControl as UserControl).FindControl("UcLnkButton")) as LinkButton).OnClientClick = jsString;
            }
            else
            {
                (pControl as WebControl).Attributes.Add("OnClick", jsString);
            }
        }

        public static void PopUpWindowEventiEkleMaximizeAc(this Page pPage, WebControl pControl, string pPageUrl)
        {
            pPage.Session["maximizeWindow"] = "true";
            PopUpWindowEventiEkle(pControl, pPageUrl, 600, 800);
        }

        // belli bir süre bekel ve popup sayfayı kapatma
        /// <summary>
        /// belli bir süre bekledikten sonra popup sayfa oto olarakkapanır
        /// </summary>
        /// <param name="iBeklemeSuresi">bekleme süresi (milisaniye 1000ms = 1sn)</param>
        public static void JSBekleVePopUpKapat(int iBeklemeSuresi)
        {
            System.Web.HttpContext.Current.Response.Write("<script> \n");
            System.Web.HttpContext.Current.Response.Write(" var gbBeklemeSuresiDolduMu = false \n ");

            System.Web.HttpContext.Current.Response.Write(" function BeklemeSureKontrolEt() \n");
            System.Web.HttpContext.Current.Response.Write(" { \n");
            System.Web.HttpContext.Current.Response.Write("     if (gbBeklemeSuresiDolduMu) \n");
            System.Web.HttpContext.Current.Response.Write("     { \n");
            System.Web.HttpContext.Current.Response.Write("         clearTimeout(oBeklemeSure) \n");
            System.Web.HttpContext.Current.Response.Write("         window.close(); \n");
            System.Web.HttpContext.Current.Response.Write("     } \n");
            System.Web.HttpContext.Current.Response.Write("     else \n");
            System.Web.HttpContext.Current.Response.Write("     { \n");
            System.Web.HttpContext.Current.Response.Write("         gbBeklemeSuresiDolduMu = true \n");
            System.Web.HttpContext.Current.Response.Write("         var oBeklemeSure = setTimeout(\"BeklemeSureKontrolEt()\", " + iBeklemeSuresi + "); \n");
            System.Web.HttpContext.Current.Response.Write("     } \n");
            System.Web.HttpContext.Current.Response.Write(" } \n");

            System.Web.HttpContext.Current.Response.Write(" BeklemeSureKontrolEt(); \n");
            System.Web.HttpContext.Current.Response.Write("</script>");
        }

        // popup sayfayı kapatma
        public static void JSPopUpKapat()
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<script> window.close(); </script>");
            System.Web.HttpContext.Current.Response.End();
        }

    }
}

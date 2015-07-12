using System;
using System.Data;

namespace Akios.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            DataTable dt = new ConfigBS().ConfigBilgileriniGetir();

            foreach (DataRow row in dt.Rows)
            {
                Session[row["ConfigName"].ToString()] = row["ConfigValue"].ToString();
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //string user = string.Empty;
            //Exception exc = this.Server.GetLastError();
            //if (SessionManager.KullaniciBilgi != null && SessionManager.KullaniciBilgi.KullaniciAdi != "mangacece")
            //    user = SessionManager.KullaniciBilgi.KullaniciAdi;
            //new LogWriter().Write(AppModules.KobsisSiparisTakip, System.Diagnostics.EventLogEntryType.Error, exc, "", "", "", user);
            //Response.Redirect("Hata.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
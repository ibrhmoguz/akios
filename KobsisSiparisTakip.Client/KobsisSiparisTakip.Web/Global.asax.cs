using KobsisSiparisTakip.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebFrame.DataType.Common.Logging;

namespace KobsisSiparisTakip.Web
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
            string user = string.Empty;
            Exception exc = this.Server.GetLastError();
            if (Session["user"] != null && Session["user"].ToString() != "mangacece")
                user = Session["user"].ToString();
            new LogWriter().Write(AppModules.KobsisSiparisTakip, System.Diagnostics.EventLogEntryType.Error, exc, "", "", "", user);
            Response.Redirect("Hata.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KobsisSiparisTakip.Web.Util
{
    public class SessionManager
    {
        public static string MusteriId
        {
            get
            {
                if (HttpContext.Current.Session["MusteriID"] != null)
                {
                    return HttpContext.Current.Session["MusteriID"].ToString();
                }
                else
                    return String.Empty;
            }
            set
            {
                HttpContext.Current.Session["MusteriID"] = value;
            }
        }
    }
}
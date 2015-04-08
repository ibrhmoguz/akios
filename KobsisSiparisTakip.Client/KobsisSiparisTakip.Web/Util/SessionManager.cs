using KobsisSiparisTakip.Business.DataTypes;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static DataTable ReferansData
        {
            get
            {
                return (DataTable)HttpContext.Current.Session["ReferansData"] ?? null;
            }
            set
            {
                HttpContext.Current.Session["ReferansData"] = value;
            }
        }


        public static List<Layout> SiparisFormLayout
        {
            get
            {
                return (List<Layout>)HttpContext.Current.Session["SiparisFormLayout"] ?? null;
            }
            set
            {
                HttpContext.Current.Session["SiparisFormLayout"] = value;
            }
        }

        public static DataTable SiparisBilgi
        {
            get
            {
                return (DataTable)HttpContext.Current.Session["SiparisBilgi"] ?? null;
            }
            set
            {
                HttpContext.Current.Session["SiparisBilgi"] = value;
            }
        }
    }
}
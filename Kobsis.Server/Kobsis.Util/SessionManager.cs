using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Kobsis.DataType;

namespace Kobsis.Util
{
    public static class SessionManager
    {
        public static void Remove(string id)
        {
            if (HttpContext.Current.Session[id] != null)
            {
                HttpContext.Current.Session.Remove(id);
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public static DataTable ReferansData
        {
            get
            {
                if (HttpContext.Current.Session["ReferansData"] != null)
                    return (DataTable)HttpContext.Current.Session["ReferansData"];
                else
                    return null;
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
                if (HttpContext.Current.Session["SiparisFormLayout"] != null)
                    return (List<Layout>)HttpContext.Current.Session["SiparisFormLayout"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["SiparisFormLayout"] = value;
            }
        }

        public static List<Layout> SiparisSorgulaFormLayout
        {
            get
            {
                if (HttpContext.Current.Session["SiparisSorgulaFormLayout"] != null)
                    return (List<Layout>)HttpContext.Current.Session["SiparisSorgulaFormLayout"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["SiparisSorgulaFormLayout"] = value;
            }
        }

        public static DataTable SiparisBilgi
        {
            get
            {
                if (HttpContext.Current.Session["SiparisBilgi"] != null)
                    return (DataTable)HttpContext.Current.Session["SiparisBilgi"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["SiparisBilgi"] = value;
            }
        }

        public static Musteri MusteriBilgi
        {
            get
            {
                if (HttpContext.Current.Session["MusteriBilgi"] != null)
                    return (Musteri)HttpContext.Current.Session["MusteriBilgi"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["MusteriBilgi"] = value;
            }
        }

        public static Kullanici KullaniciBilgi
        {
            get
            {
                if (HttpContext.Current.Session["KullaniciBilgi"] != null)
                    return (Kullanici)HttpContext.Current.Session["KullaniciBilgi"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["KullaniciBilgi"] = value;
            }
        }

        public static string LoginAttemptUser
        {
            get
            {
                if (HttpContext.Current.Session["LoginAttemptUser"] != null)
                    return HttpContext.Current.Session["LoginAttemptUser"].ToString();
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["LoginAttemptUser"] = value;
            }
        }

        public static int? LoginAttemptCount
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["LoginAttemptCount"].ToString()))
                    return Convert.ToInt32(HttpContext.Current.Session["LoginAttemptCount"].ToString());
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["LoginAttemptCount"] = value;
            }
        }

        public static string CaptchaImageText
        {
            get
            {
                if (HttpContext.Current.Session["CaptchaImageText"] != null)
                    return HttpContext.Current.Session["CaptchaImageText"].ToString();
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["CaptchaImageText"] = value;
            }
        }

        public static List<SiparisSeri> SiparisSeri
        {
            get
            {
                if (HttpContext.Current.Session["SiparisSeri"] != null)
                    return (List<SiparisSeri>)HttpContext.Current.Session["SiparisSeri"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["SiparisSeri"] = value;
            }
        }

        public static string MontajKotaKontrolu
        {
            get
            {
                if (HttpContext.Current.Session["MONTAJ_KOTA_KONTROLU"] != null)
                    return HttpContext.Current.Session["MONTAJ_KOTA_KONTROLU"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["MONTAJ_KOTA_KONTROLU"] = value;
            }
        }

        public static int MontajKotaVarsayilan
        {
            get
            {
                if (HttpContext.Current.Session["MONTAJ_KOTA_VARSAYILAN"] != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session["MONTAJ_KOTA_VARSAYILAN"].ToString()))
                    return Convert.ToInt32(HttpContext.Current.Session["MONTAJ_KOTA_VARSAYILAN"].ToString());
                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["MONTAJ_KOTA_VARSAYILAN"] = value;
            }
        }

        public static DataTable PersonelListesi
        {
            get
            {
                if (HttpContext.Current.Session["PersonelListesi"] != null)
                    return (DataTable)HttpContext.Current.Session["PersonelListesi"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["PersonelListesi"] = value;
            }
        }

        public static DataTable SiparisSorguListesi
        {
            get
            {
                if (HttpContext.Current.Session["SiparisSorguListesi"] != null)
                    return (DataTable)HttpContext.Current.Session["SiparisSorguListesi"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["SiparisSorguListesi"] = value;
            }
        }
    }
}
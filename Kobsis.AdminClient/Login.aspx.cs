using System;
using System.Web.Security;
using Kobsis.Business;
using Kobsis.DataType;
using Kobsis.Util;
using Kobsis.Web.Helper;

namespace Kobsis.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionManager.CaptchaImageText = CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
            }
        }

        protected void LB_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userName.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Hata(this, "Kullanıcı adı ve şifre giriniz!");
                return;
            }

            if (SessionManager.LoginAttemptUser != null && SessionManager.LoginAttemptUser == userName.Text)
            {
                if (!string.IsNullOrWhiteSpace(SessionManager.LoginAttemptUser) && SessionManager.LoginAttemptCount.HasValue && SessionManager.LoginAttemptCount.Value > 0)
                {
                    if (!ResimKontroluYap())
                    {
                        MessageBox.Hata(this, "Güvenlik resmi doğrulanamadı! Tekrar deneyiniz.");
                        return;
                    }
                }
            }

            Kullanici k = new KullaniciBS().KullaniciBilgisiGetir(userName.Text, password.Text);

            if (k.KullaniciID.HasValue)
            {
                UserValid(k);
            }
            else
            {
                SessionManager.LoginAttemptUser = userName.Text;
                SessionManager.LoginAttemptCount = 1;
                imgCaptcha.Visible = true;
                txtResimDogrulama.Visible = true;

                MessageBox.Hata(this, "Kullanıcı adı ya da şifre hatalı. Tekrar deneyiniz.");
            }
        }

        private void UserValid(Kullanici k)
        {
            Musteri m = new MusteriBS().MusteriBilgiGetirKullaniciAdinaGore(k.KullaniciID.Value);
            SessionManager.MusteriBilgi = m;
            SessionManager.KullaniciBilgi = k;

            SessionManager.Remove("LoginAttemptUser");
            SessionManager.Remove("LoginAttemptCount");

            FormsAuthentication.RedirectFromLoginPage(userName.Text, false);
        }

        protected void LB_Logout_Click(object sender, EventArgs e)
        {
            SessionManager.Clear();
            //FormsAuthenticationProvider.LogOut();
        }

        private bool ResimKontroluYap()
        {
            if (SessionManager.CaptchaImageText != null && SessionManager.CaptchaImageText.ToString().CompareTo(txtResimDogrulama.Text) == 0)
                return true;

            return false;
        }
    }
}
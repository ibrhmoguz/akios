using System;
using System.Data;
using System.Web.Security;
using Kobsis.Business;
using Kobsis.DataType;
using Kobsis.Util;
using Kobsis.Web.Helper;

namespace Kobsis.Web
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionManager.CaptchaImageText = CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
                MusteriListesiYukle();
            }
        }

        private void MusteriListesiYukle()
        {
            DataTable dt = new MusteriBS().MusteriListesiGetir();
            if (dt.Rows.Count > 0)
            {
                ddlMusteri.DataSource = dt;
                ddlMusteri.DataTextField = "Adi";
                ddlMusteri.DataValueField = "MusteriID";
                ddlMusteri.DataBind();
            }
            else
            {
                ddlMusteri.DataSource = null;
                ddlMusteri.DataBind();
            }
        }

        protected void LB_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userName.Text) || string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(ddlMusteri.SelectedValue))
            {
                MessageBox.Hata(this, "Kullanıcı adı, şifre girip, müşteri seçiniz!");
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

            if (KullaniciKontrol())
            {
                UserValid();
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

        private void UserValid()
        {
            Musteri m = new MusteriBS().MusteriBilgiGetirMusteriIDGore(Convert.ToInt32(ddlMusteri.SelectedValue));
            SessionManager.MusteriBilgi = m;
            Kullanici k = new Kullanici()
            {
                KullaniciID = 0,
                Rol = KullaniciRol.Yonetici,
                MusteriID = m.MusteriID,
                KullaniciAdi = "mangacece"
            };
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

        private bool KullaniciKontrol()
        {
            if (userName.Text == "mangacece" && password.Text == "1Qaz2wSx!")
                return true;

            return false;
        }

        private bool ResimKontroluYap()
        {
            if (SessionManager.CaptchaImageText != null && SessionManager.CaptchaImageText.ToString().CompareTo(txtResimDogrulama.Text) == 0)
                return true;

            return false;
        }
    }
}
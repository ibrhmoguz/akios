using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Akios.AdminWebClient.Helper;
using Akios.Business;
using Akios.Util;

namespace Akios.AdminWebClient
{
    public partial class SifreGuncelleme : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SifreBilgisiDoldur();
        }

        private void SifreBilgisiDoldur()
        {
            if (SessionManager.KullaniciBilgi == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            lblKullanici.Text = SessionManager.KullaniciBilgi.KullaniciAdi;
            RP_Sifre.DataSource = new KullaniciBS().KullaniciSifreBilgisiGetir(SessionManager.KullaniciBilgi.KullaniciAdi, SessionManager.KullaniciBilgi.RolID);
            RP_Sifre.DataBind();
        }

        protected void BTN_Guncelle_Click(object sender, EventArgs e)
        {
            bool sonuc = false;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("KullaniciAdi", SessionManager.KullaniciBilgi.KullaniciAdi);
            prms.Add("Sifre", SessionManager.KullaniciBilgi.Sifre);
            prms.Add("RolID", SessionManager.KullaniciBilgi.RolID);
            prms.Add("YeniSifre", txtSifre.Text);

            sonuc = new KullaniciBS().KullaniciSifreGuncelle(prms);

            if (sonuc)
            {
                SessionManager.KullaniciBilgi.Sifre = txtSifre.Text;
                SifreBilgisiDoldur();
                MessageBox.Basari(this, "Şifre güncellendi.");
            }
            else
            {
                MessageBox.Hata(this, "Güncelleme işleminde hata oluştu!");
            }
        }

        protected void RP_Sifre_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            Response.Redirect(Page.Request.Url.ToString());

        }
    }
}
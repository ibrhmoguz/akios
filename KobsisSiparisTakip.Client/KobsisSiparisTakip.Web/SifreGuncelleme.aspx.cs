using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Web.Helper;


namespace KobsisSiparisTakip.Web
{
    public partial class SifreGuncelleme : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SifreBilgisiDoldur();
        }

        private void SifreBilgisiDoldur()
        {
            if (Session["user"] == null || Session["yetki"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string user, yetki;

            user = Session["user"].ToString();
            yetki = Session["yetki"].ToString();
            lblKullanici.Text = user;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("KULLANICIADI", user);
            prms.Add("YETKI", yetki);

            RP_Sifre.DataSource = new KullaniciBS().KullaniciSifreBilgisiGetir(prms);
            RP_Sifre.DataBind();
        }

        protected void BTN_Guncelle_Click(object sender, EventArgs e)
        {
            string sifre, user, yetki, yeniSifre;
            sifre = Session["sifre"].ToString();
            user = Session["user"].ToString();
            yetki = Session["yetki"].ToString();

            yeniSifre = txtSifre.Text;
            bool sonuc = false;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("KULLANICIADI", user);
            prms.Add("SIFRE", sifre);
            prms.Add("YETKI", yetki);
            prms.Add("YENISIFRE", yeniSifre);


            sonuc = new KullaniciBS().KullaniciSifreGuncelle(prms);

            if (sonuc)
            {
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
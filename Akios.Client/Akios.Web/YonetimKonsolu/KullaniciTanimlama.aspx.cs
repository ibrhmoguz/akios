using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Akios.Business;
using Akios.Util;
using Akios.WebClient.Helper;

namespace Akios.WebClient.YonetimKonsolu
{
    public partial class KullaniciTanimlama : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RolleriDoldur();
                KullaniciDoldur();
            }
        }

        private void RolleriDoldur()
        {
            DataTable dt = new ReferansDataBS().KullaniciRolleriGetir();

            if (dt.Rows.Count > 0)
            {
                ddlKullaniciRol.DataSource = dt;
                ddlKullaniciRol.DataTextField = "RolAdi";
                ddlKullaniciRol.DataValueField = "RolID";
                ddlKullaniciRol.DataBind();
                ddlKullaniciRol.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            }
            else
            {
                ddlKullaniciRol.DataSource = null;
                ddlKullaniciRol.DataBind();
            }
        }

        private void KullaniciDoldur()
        {
            RP_Kullanici.DataSource = new KullaniciBS().KullanicilariGetir();
            RP_Kullanici.DataBind();
        }

        protected void btnEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                MessageBox.Uyari(this, "Kullanıcı adı giriniz");
                return;
            }
            if (ddlKullaniciRol.SelectedIndex == 0)
            {
                MessageBox.Uyari(this, "Kullanıcı rolü seçiniz");
                return;
            }

            string kullanici = txtKullaniciAdi.Text.Trim();
            string yetki = ddlKullaniciRol.SelectedValue;
            string sifre = "12345";
            bool sonuc = false;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("KullaniciAdi", kullanici);
            prms.Add("RolID", yetki);
            prms.Add("Sifre", sifre);
            prms.Add("MusteriID", SessionManager.KullaniciBilgi.MusteriID);

            sonuc = new KullaniciBS().KullaniciTanimla(prms);

            if (sonuc)
            {
                KullaniciDoldur();
                MessageBox.Basari(this, "Kullanici eklendi.");
            }
            else
            {
                MessageBox.Hata(this, "Kullanıcı ekleme işleminde hata oluştu!");
            }
        }

        protected void RP_Kullanici_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            bool sonuc = false;

            if (e.CommandName == "Delete")
            {
                sonuc = new KullaniciBS().KullaniciSil(e.CommandArgument.ToString());

                if (sonuc)
                {
                    KullaniciDoldur();
                    MessageBox.Basari(this, "Kullanıcı silindi.");
                }
                else
                {
                    MessageBox.Hata(this, "Kullanıcı silme işleminde hata oluştu!");
                }
            }
        }
    }
}
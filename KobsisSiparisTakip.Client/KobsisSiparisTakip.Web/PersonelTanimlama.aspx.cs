using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Web.Util;
using KobsisSiparisTakip.Business.DataTypes;

namespace KobsisSiparisTakip.Web
{
    public partial class PersonelTanimlama : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.KullaniciBilgi.Rol == KullaniciRol.Kullanici)
            {
                MessageBox.Hata(this, "Bu sayfaya erişim yetkiniz yoktur!");
                return;
            }

            if (!Page.IsPostBack)
            {
                PersonelDoldur();
            }
        }

        private void PersonelDoldur()
        {
            RP_Personel.DataSource = new PersonelBS().PersonelListesiGetirGenel();
            RP_Personel.DataBind();
        }

        protected void btnEkle_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();

            bool sonuc = false;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("AD", ad);
            prms.Add("SOYAD", soyad);

            sonuc = new PersonelBS().PersonelTanimla(prms);

            if (sonuc)
            {
                PersonelDoldur();
                MessageBox.Basari(this, "Personel eklendi.");
            }
            else
            {
                MessageBox.Hata(this, "Personel ekleme işleminde hata oluştu!");
            }
        }

        protected void RP_Personel_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            bool sonuc = false;

            if (e.CommandName == "Delete")
            {
                string id = e.CommandArgument.ToString();

                Dictionary<string, object> prms = new Dictionary<string, object>();
                prms.Add("ID", id);
                //prms.Add("SOYAD", soyad);

                sonuc = new PersonelBS().PersonelSil(prms);

                if (sonuc)
                {
                    PersonelDoldur();
                    MessageBox.Basari(this, "Personel silindi.");
                }
                else
                {
                    MessageBox.Hata(this, "Personel silme işleminde hata oluştu!");
                }
            }
        }
    }
}
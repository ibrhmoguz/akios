using System;
using System.Data;
using System.Web.UI.WebControls;
using Akios.AdminWebClient.Helper;
using Akios.Business;
using Akios.DataType;
using Akios.Util;
using Telerik.Web.UI;

namespace Akios.AdminWebClient.YonetimKonsolu
{
    public partial class PersonelTanimlama : AkiosBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonelDoldur();
        }

        private void PersonelDoldur()
        {
            string musteriId = MusteriGetir();
            if (String.IsNullOrWhiteSpace(musteriId) || musteriId.Equals("0"))
            {
                DataTable dt = new PersonelBS().TumPersonelListesiGetir();
                RP_Personel.DataSource = dt;
                RP_Personel.DataBind();
            }
            else
            {
                DataTable dt = new PersonelBS().PersonelListesiGetir(Convert.ToInt32(musteriId));
                RP_Personel.DataSource = dt;
                RP_Personel.DataBind();
            }
            //SessionManager.PersonelListesi = dt;
        }

        private string MusteriGetir()
        {
            RadDropDownList rddlMusteri = (RadDropDownList)Master.FindControl("ddlMusteri");
            return rddlMusteri.SelectedValue;
        }

        protected void btnEkle_Click(object sender, EventArgs e)
        {
            string musteri = MusteriGetir();
            if (String.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Uyari(this, "Personel adı giriniz");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                MessageBox.Uyari(this, "Personel soyadı giriniz");
                return;
            }
            if (String.IsNullOrWhiteSpace(musteri) || musteri.Equals("0"))
            {
                MessageBox.Uyari(this, "Müşteri seçiniz");
                return;
            }
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();

            bool sonuc = false;

            sonuc = new PersonelBS().PersonelTanimla(musteri, ad, soyad);

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
                if (string.IsNullOrWhiteSpace(id)) return;

                sonuc = new PersonelBS().PersonelSil(Convert.ToInt32(id));

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
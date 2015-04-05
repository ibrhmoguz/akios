using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Web.Util;
using Telerik.Web.UI;

namespace KobsisSiparisTakip.Web
{
    public partial class FormOgeGuncelleme : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OgeDoldur();
            }
        }

        private void OgeDoldur()
        {
            DataTable dt = new YonetimKonsoluBS().TabloAdlariGetir();
            if (dt.Rows.Count > 0)
            {
                ddlOge.DataSource = dt;
                ddlOge.DataTextField = "AD";
                ddlOge.DataValueField = "TABLO";
                ddlOge.DataBind();
                ddlOge.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));

            }
            else
            {
                ddlOge.DataSource = null;
                ddlOge.DataBind();
            }
        }

        protected void ddlOge_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            SatirlariGridleriGizle();

            string tabloAdi = ddlOge.SelectedValue;
            Session["TabloAdi"] = tabloAdi;
            if (tabloAdi == "0")
            {
                SatirlariGridleriGizle();
            }
            else if (tabloAdi == "REF_KAPIMODEL")
            {
                trKapiModel.Visible = true;
                KapiSeriDoldur();
                rgOgeler1.Visible = false;
            }
            else
            {
                trKayitEkle1.Visible = true;
                GridDoldur(tabloAdi);
                rgOgeler1.Visible = true;
            }
        }

        private void KapiSeriDoldur()
        {
            DataTable dt = new YonetimKonsoluBS().KapiSeriGetir();
            if (dt.Rows.Count > 0)
            {
                ddlKapiSeri.DataSource = dt;
                ddlKapiSeri.DataTextField = "AD";
                ddlKapiSeri.DataValueField = "ID";
                ddlKapiSeri.DataBind();
                ddlKapiSeri.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            }
            else
            {
                ddlKapiSeri.DataSource = null;
                ddlKapiSeri.DataBind();
            }
        }

        protected void ddlKapiSeri_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            string kapiSeriId = ddlKapiSeri.SelectedValue;
            Session["kapiSeriId"] = kapiSeriId;
            GridDoldur2(kapiSeriId);
            rgOgeler2.Visible = true;
            trKayitEkle2.Visible = true;
        }

        public void GridDoldur(string tabloAdi)
        {
            DataSet ds = new YonetimKonsoluBS().RefTablolariGetir();
            if (ds.Tables.Count == 0)
                return;

            DataView dv = ds.Tables[tabloAdi].DefaultView;
            GridBind(dv);
            Session["YonetimSayfasiOgeListesi1"] = dv;
        }

        private void GridBind(DataView dv)
        {
            rgOgeler1.DataSource = dv;
            rgOgeler1.DataBind();
        }

        public void GridDoldur2(string kapiSeriId)
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("KAPISERIID", kapiSeriId);

            DataTable dt = new YonetimKonsoluBS().KapiModelGetir(prms);
            if (dt.Rows.Count == 0)
                return;

            GridBind2(dt);
            Session["YonetimSayfasiOgeListesi2"] = dt;
        }

        private void GridBind2(DataTable dt)
        {
            rgOgeler2.DataSource = dt;
            rgOgeler2.DataBind();
        }

        protected void rgOgeler1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            bool sonuc = false;
            string tabloAdi = Session["TabloAdi"].ToString();

            if (e.CommandName == "Delete")
            {
                string id = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString();
                Dictionary<string, object> prms = new Dictionary<string, object>();
                prms.Add("TABLOADI", tabloAdi);
                prms.Add("ID", id);
                sonuc = new YonetimKonsoluBS().OgeSil(prms);

                if (sonuc)
                {
                    if (tabloAdi == "REF_KAPIMODEL")
                    {
                        GridDoldur2(Session["kapiSeriId"].ToString());
                        MessageBox.Basari(this, "Seçiminiz silindi.");

                    }
                    else
                    {
                        GridDoldur(tabloAdi);
                        MessageBox.Basari(this, "Seçiminiz silindi.");
                    }
                }
                else
                    MessageBox.Hata(this, "Silme işleminde hata oluştu!");
            }
        }

        protected void btnEkle_Click(object sender, EventArgs e)
        {
            bool sonuc, nova, kroma, guard,yangin;
            string ad, tabloAdi;

            ad = string.Empty;
            sonuc = false;
            nova = false;
            kroma = false;
            guard = false;
            yangin = false;

            tabloAdi = Session["TabloAdi"].ToString();

            ad = txtAd.Text;

            if (cbxKapiTuru.Items[0].Selected)
                nova = true;
            if (cbxKapiTuru.Items[1].Selected)
                kroma = true;
            if (cbxKapiTuru.Items[2].Selected)
                guard = true;
            if (cbxKapiTuru.Items[3].Selected)
                yangin = true;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("TABLOADI", tabloAdi);
            prms.Add("AD", ad);
            prms.Add("NOVA", nova);
            prms.Add("KROMA", kroma);
            prms.Add("GUARD", guard);
            prms.Add("YANGIN", yangin);
            sonuc = new YonetimKonsoluBS().OgeEkle(prms);

            if (sonuc)
            {
                GridDoldur(tabloAdi);
                txtAd.Text = string.Empty;
                cbxKapiTuru.ClearSelection();
                MessageBox.Basari(this, "Seçiminiz eklendi.");
            }
            else
                MessageBox.Hata(this, "Ekleme işleminde hata oluştu!");
        }

        protected void btnEkle2_Click(object sender, EventArgs e)
        {
            bool sonuc;
            string ad, kapiSeriId, tabloAdi;

            ad = string.Empty;
            sonuc = false;
            kapiSeriId = ddlKapiSeri.SelectedValue.ToString();


            tabloAdi = Session["TabloAdi"].ToString();
            ad = txtAd2.Text;

            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("TABLOADI", tabloAdi);
            prms.Add("KAPISERIID", kapiSeriId);
            prms.Add("AD", ad);
            sonuc = new YonetimKonsoluBS().KapiModelEkle(prms);

            if (sonuc)
            {
                GridDoldur2(kapiSeriId);
                txtAd.Text = string.Empty;
                MessageBox.Basari(this, "Seçiminiz eklendi.");
            }
            else
                MessageBox.Hata(this, "Ekleme işleminde hata oluştu!");
        }

        protected void lbYeniKayit_Click(object sender, EventArgs e)
        {
            tbKayitEkle1.Visible = true;
        }

        protected void lbYeniKayit2_Click(object sender, EventArgs e)
        {
            tbKayitEkle2.Visible = true;
        }

        public void SatirlariGridleriGizle()
        {
            trKapiModel.Visible = false;
            trKayitEkle1.Visible = false;
            trKayitEkle2.Visible = false;
            tbKayitEkle1.Visible = false;
            tbKayitEkle2.Visible = false;
            rgOgeler1.Visible = false;
            rgOgeler2.Visible = false;
        }

        protected void rgOgeler1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            rgOgeler1.CurrentPageIndex = e.NewPageIndex;
            GridBind((DataView)Session["YonetimSayfasiOgeListesi1"]);
        }

        protected void rgOgeler2_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            rgOgeler2.CurrentPageIndex = e.NewPageIndex;
            GridBind2((DataTable)Session["YonetimSayfasiOgeListesi2"]);
        }
    }
}
using KobsisSiparisTakip.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using KobsisSiparisTakip.Web.Helper;

namespace KobsisSiparisTakip.Web
{
    public partial class SiparisSorgula : KobsisBasePage
    {
        private DataTable PersonelListesi
        {
            get
            {
                if (Session["SiparisSorgula_PersonelListesi"] != null)
                    return Session["SiparisSorgula_PersonelListesi"] as DataTable;
                else
                    return null;
            }
            set
            {
                Session["SiparisSorgula_PersonelListesi"] = value;
            }
        }

        private DataTable SorguSonucListesi
        {
            get
            {
                if (Session["SiparisSorgula_SorguSonucListesi"] != null)
                    return Session["SiparisSorgula_SorguSonucListesi"] as DataTable;
                else
                    return null;
            }
            set
            {
                Session["SiparisSorgula_SorguSonucListesi"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VarsayilanDegerleriAyarla();
            }
        }

        private void VarsayilanDegerleriAyarla()
        {
            PersonelListesiYukle();
            DropDownlariDoldur();
            //KapiSeriDoldur();
        }

        private void PersonelListesiYukle()
        {
            DataTable dt = new PersonelBS().PersonelListesiGetir();
            ListBoxMontajEkibi.DataSource = dt;
            ListBoxMontajEkibi.DataBind();
            this.PersonelListesi = dt;
        }

        private void DropDownlariDoldur()
        {
            DataSet ds = new YonetimKonsoluBS().RefTablolariGetir();
            if (ds.Tables.Count == 0)
                return;

            DataTable dtKapiModeli = ds.Tables["REF_TUMKAPIMODELLERI"];
            DataTable dtKapiRenk = ds.Tables["REF_KAPIRENK"];
            DataTable dtKilitSistem = ds.Tables["REF_KILITSISTEM"];
            DataTable dtCita = ds.Tables["REF_CITA"];
            DataTable dtEsik = ds.Tables["REF_ESIK"];
            DataTable dtAksesuarRenk = ds.Tables["REF_AKSESUARRENK"];
            DataTable dtMontajSekli = ds.Tables["REF_MONTAJSEKLI"];
            DataTable dtTeslimSekli = ds.Tables["REF_TESLIMSEKLI"];
            DataTable dtAluminyumRenk = ds.Tables["REF_ALUMINYUMRENK"];
            DataTable dtTacTip = ds.Tables["REF_TACTIP"];
            DataTable dtPervazTip = ds.Tables["REF_PERVAZTIP"];
            DataTable dtContaRenk = ds.Tables["REF_CONTARENK"];
            DataTable dtPersonel = ds.Tables["REF_PERSONEL"];


            DropDownBindEt(ddlIcKapiModeli, dtKapiModeli);
            DropDownBindEt(ddlDisKapiModeli, dtKapiModeli);
            DropDownBindEt(ddlIcKapiRengi, dtKapiRenk);
            DropDownBindEt(ddlDisKapiRengi, dtKapiRenk);
            DropDownBindEt(ddlKilitSistemi, dtKilitSistem);
            DropDownBindEt(ddlCita, dtCita);
            DropDownBindEt(ddlEsik, dtEsik);
            DropDownBindEt(ddlAksesuarRengi, dtAksesuarRenk);
            DropDownBindEt(ddlMontajSekli, dtMontajSekli);
            DropDownBindEt(ddlAluminyumRengi, dtAluminyumRenk);
            DropDownBindEt(ddlTacTipi, dtTacTip);
            DropDownBindEt(ddlPervazTipi, dtPervazTip);
            DropDownBindEt(ddlContaRengi, dtContaRenk);

            IlleriGetir();
        }

        private void DropDownBindEt(RadDropDownList ddl, DataTable dt)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "AD";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
        }

        protected void btnSorgula_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();

            if (String.IsNullOrWhiteSpace(txtSiparisNo.Text))
                prms.Add("SiparisNo", null);
            else
                prms.Add("SiparisNo", txtSiparisNo.Text);

            if (rdtSiparisTarihiBas.SelectedDate == null)
                prms.Add("SiparisTarihiBas", null);
            else
                prms.Add("SiparisTarihiBas", rdtSiparisTarihiBas.SelectedDate);

            if (rdtSiparisTarihiBit.SelectedDate == null)
                prms.Add("SiparisTarihiBit", null);
            else
                prms.Add("SiparisTarihiBit", rdtSiparisTarihiBit.SelectedDate);

            if (String.IsNullOrWhiteSpace(txtMusteriAd.Text))
                prms.Add("MusteriAd", null);
            else
                prms.Add("MusteriAd", txtMusteriAd.Text);

            if (String.IsNullOrWhiteSpace(txtMusteriSoyad.Text))
                prms.Add("MusteriSoyad", null);
            else
                prms.Add("MusteriSoyad", txtMusteriSoyad.Text);

            if (rdpTeslimTarihiBas.SelectedDate == null)
                prms.Add("TeslimTarihiBas", null);
            else
                prms.Add("TeslimTarihiBas", rdpTeslimTarihiBas.SelectedDate);

            if (rdpTeslimTarihiBit.SelectedDate == null)
                prms.Add("TeslimTarihiBit", null);
            else
                prms.Add("TeslimTarihiBit", rdpTeslimTarihiBit.SelectedDate);

            string personelListesi = String.Empty;
            foreach (RadListBoxItem item in ListBoxMontajEkibi.Items)
            {
                if (item.Checked)
                {
                    if (!String.IsNullOrWhiteSpace(personelListesi))
                        personelListesi += ",";
                    personelListesi += item.Value;
                }
            }

            if (String.IsNullOrWhiteSpace(personelListesi))
                prms.Add("PersonelListesi", null);
            else
                prms.Add("PersonelListesi", personelListesi);

            if (ddlIcKapiModeli.SelectedIndex == 0)
                prms.Add("ddlIcKapiModeli", null);
            else
                prms.Add("ddlIcKapiModeli", ddlIcKapiModeli.SelectedText);

            if (ddlDisKapiModeli.SelectedIndex == 0)
                prms.Add("ddlDisKapiModeli", null);
            else
                prms.Add("ddlDisKapiModeli", ddlDisKapiModeli.SelectedText);

            if (ddlIcKapiRengi.SelectedIndex == 0)
                prms.Add("ddlIcKapiRengi", null);
            else
                prms.Add("ddlIcKapiRengi", ddlIcKapiRengi.SelectedText);

            if (ddlDisKapiRengi.SelectedIndex == 0)
                prms.Add("ddlDisKapiRengi", null);
            else
                prms.Add("ddlDisKapiRengi", ddlDisKapiRengi.SelectedText);

            if (ddlKilitSistemi.SelectedIndex == 0)
                prms.Add("ddlKilitSistemi", null);
            else
                prms.Add("ddlKilitSistemi", ddlKilitSistemi.SelectedText);

            if (ddlCita.SelectedIndex == 0)
                prms.Add("ddlCita", null);
            else
                prms.Add("ddlCita", ddlCita.SelectedText);

            if (ddlEsik.SelectedIndex == 0)
                prms.Add("ddlEsik", null);
            else
                prms.Add("ddlEsik", ddlKilitSistemi.SelectedText);

            if (ddlAksesuarRengi.SelectedIndex == 0)
                prms.Add("ddlAksesuarRengi", null);
            else
                prms.Add("ddlAksesuarRengi", ddlAksesuarRengi.SelectedText);

            if (ddlMontajSekli.SelectedIndex == 0)
                prms.Add("ddlMontajSekli", null);
            else
                prms.Add("ddlMontajSekli", ddlMontajSekli.SelectedText);

            if (ddlAluminyumRengi.SelectedIndex == 0)
                prms.Add("ddlAluminyumRengi", null);
            else
                prms.Add("ddlAluminyumRengi", ddlAluminyumRengi.SelectedText);

            if (ddlTacTipi.SelectedIndex == 0)
                prms.Add("ddlTacTipi", null);
            else
                prms.Add("ddlTacTipi", ddlTacTipi.SelectedText);

            if (ddlPervazTipi.SelectedIndex == 0)
                prms.Add("ddlPervazTipi", null);
            else
                prms.Add("ddlPervazTipi", ddlPervazTipi.SelectedText);

            if (ddlContaRengi.SelectedIndex == 0)
                prms.Add("ddlContaRengi", null);
            else
                prms.Add("ddlContaRengi", ddlContaRengi.SelectedText);

            if (ddlMusteriIl.SelectedIndex == 0)
                prms.Add("Il", null);
            else
                prms.Add("Il", ddlMusteriIl.SelectedItem.Text);

            if (ddlMusteriIlce.SelectedIndex == 0)
                prms.Add("Ilce", null);
            else
                prms.Add("Ilce", ddlMusteriIlce.SelectedItem.Text);

            if (ddlMusteriSemt.SelectedIndex == 0)
                prms.Add("Semt", null);
            else
                prms.Add("Semt", ddlMusteriSemt.SelectedItem.Text);

            if (ddlSiparisDurumu.SelectedIndex == 0)
                prms.Add("Durum", null);
            else
                prms.Add("Durum", ddlSiparisDurumu.SelectedText);

            if (ddlKapiSeri.SelectedIndex == 0)
                prms.Add("ddlKapiSeri", null);
            else
                prms.Add("ddlKapiSeri", ddlKapiSeri.SelectedValue.ToString());

            if (String.IsNullOrWhiteSpace(txtAdres.Text))
                prms.Add("Adres", null);
            else
                prms.Add("Adres", txtAdres.Text);

            if (String.IsNullOrWhiteSpace(txtTel.Text))
                prms.Add("Tel", null);
            else
                prms.Add("Tel", txtTel.Text);

            DataTable dt = new SiparisIslemleriBS().SiparisSorgula(prms);

            grdSiparisler.DataSource = dt;
            grdSiparisler.DataBind();
            this.SorguSonucListesi = dt;
            ToplamSiparisDegerHesapla();
            tblSonuc.Visible = true;
        }

        private void ToplamSiparisDegerHesapla()
        {
            DataTable dt = (DataTable)this.SorguSonucListesi;
            if (dt == null)
                return;

            int temp = 0;
            lblToplamSiparis.Text = dt.Rows.Count.ToString();

            foreach (DataRow row in dt.Rows)
            {
                if (row["ADET"] != DBNull.Value)
                    temp += Convert.ToInt32(row["ADET"]);
            }
            lblToplamKapi.Text = temp.ToString();
        }

        protected void ddlMusteriIl_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            ddlMusteriIlce.Items.Clear();
            ddlMusteriSemt.Items.Clear();
            IlceleriGetir(e.Value);
        }

        protected void ddlMusteriIlce_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            SemtleriGetir(e.Value);
        }

        private void IlleriGetir()
        {
            DataTable dt = new ReferansDataBS().IlleriGetir();
            if (dt.Rows.Count > 0)
            {
                ddlMusteriIl.DataSource = dt;
                ddlMusteriIl.DataTextField = "ILAD";
                ddlMusteriIl.DataValueField = "ILKOD";
                ddlMusteriIl.DataBind();
            }
            else
            {
                ddlMusteriIl.DataSource = null;
                ddlMusteriIl.DataBind();
            }
            ddlMusteriIl.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            ddlMusteriIlce.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            ddlMusteriSemt.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
        }

        private void IlceleriGetir(string ilKod)
        {
            DataTable dt = new ReferansDataBS().IlceleriGetir(ilKod);

            if (dt.Rows.Count > 0)
            {
                ddlMusteriIlce.DataSource = dt;
                ddlMusteriIlce.DataTextField = "ILCEAD";
                ddlMusteriIlce.DataValueField = "ILCEKOD";
                ddlMusteriIlce.DataBind();
            }
            else
            {
                ddlMusteriIlce.DataSource = null;
                ddlMusteriIlce.DataBind();
            }
            ddlMusteriIlce.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            ddlMusteriSemt.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            ddlMusteriIlce.SelectedIndex = 0;
            ddlMusteriSemt.SelectedIndex = 0;
        }

        private void SemtleriGetir(string ilceKod)
        {
            DataTable dt = new ReferansDataBS().SemtleriGetir(ilceKod);

            if (dt.Rows.Count > 0)
            {
                ddlMusteriSemt.DataSource = dt;
                ddlMusteriSemt.DataTextField = "SEMTAD";
                ddlMusteriSemt.DataValueField = "SEMTKOD";
                ddlMusteriSemt.DataBind();
            }
            else
            {
                ddlMusteriSemt.DataSource = null;
                ddlMusteriSemt.DataBind();
            }
            ddlMusteriSemt.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            ddlMusteriSemt.SelectedIndex = 0;
        }

        protected void grdSiparisler_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSiparisler.PageIndex = e.NewPageIndex;
            grdSiparisler.DataSource = this.SorguSonucListesi;
            grdSiparisler.DataBind();
        }

        protected void grdSiparisler_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink link = (HyperLink)e.Row.FindControl("lnkGoruntule");
            if (link != null)
            {
                DataRowView view = (DataRowView)e.Row.DataItem;
                string url = string.Empty;
                string kapiCinsi = view.Row.ItemArray[1].ToString();

                if (kapiCinsi[0] == 'Y' || kapiCinsi[0] == 'P')
                    url = "~/SiparisFormYanginGoruntule.aspx";
                else
                    url = "~/SiparisFormGoruntule.aspx";

                link.NavigateUrl = url + "?SiparisID=" + view.Row.ItemArray[0].ToString();
            }
        }
        private void KapiSeriDoldur()
        {
            DataTable dt = new YonetimKonsoluBS().KapiSeriGetir();
            if (dt.Rows.Count > 0)
            {
                ddlKapiSeri.DataSource = dt;
                ddlKapiSeri.DataTextField = "AD";
                ddlKapiSeri.DataValueField = "VALUE";
                ddlKapiSeri.DataBind();
                ddlKapiSeri.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));

            }
            else
            {
                ddlKapiSeri.DataSource = null;
                ddlKapiSeri.DataBind();
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            ddlKapiSeri.SelectedIndex = 0;
            ddlPervazTipi.SelectedIndex = 0;
            ddlTacTipi.SelectedIndex = 0;
            ddlAksesuarRengi.SelectedIndex = 0;
            ddlEsik.SelectedIndex = 0;
            ddlMusteriSemt.Items.Clear();
            ddlMusteriSemt.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            ddlMusteriIlce.Items.Clear();
            ddlMusteriIlce.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            ddlKilitSistemi.SelectedIndex = 0;
            ddlMusteriIl.SelectedIndex = 0;
            ddlContaRengi.SelectedIndex = 0;
            ddlIcKapiRengi.SelectedIndex = 0;
            ddlDisKapiModeli.SelectedIndex = 0;
            ddlAluminyumRengi.SelectedIndex = 0;
            ddlDisKapiRengi.SelectedIndex = 0;
            ddlIcKapiModeli.SelectedIndex = 0;
            ddlMusteriSemt.SelectedIndex = 0;
            ddlMontajSekli.SelectedIndex = 0;
            ddlCita.SelectedIndex = 0;
            ddlSiparisDurumu.SelectedIndex = 0;
            txtMusteriSoyad.Text = string.Empty;
            txtMusteriAd.Text = string.Empty;
            txtSiparisNo.Text = string.Empty;
            txtAdres.Text = string.Empty;
            txtTel.Text = string.Empty;
            rdpTeslimTarihiBit.SelectedDate = null;
            rdpTeslimTarihiBas.SelectedDate = null;
            rdtSiparisTarihiBit.SelectedDate = null;
            rdtSiparisTarihiBas.SelectedDate = null;
            foreach (RadListBoxItem item in ListBoxMontajEkibi.Items)
                if (item.Checked) item.Checked = false;
        }
    }
}
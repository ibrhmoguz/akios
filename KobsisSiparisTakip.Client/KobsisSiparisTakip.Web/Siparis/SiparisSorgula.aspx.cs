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
using KobsisSiparisTakip.Business.DataTypes;

namespace KobsisSiparisTakip.Web
{
    public partial class SiparisSorgula : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Yukle();
            }
            if (Page.IsPostBack) { GenerateForm(); }
        }

        private void Yukle()
        {
            PersonelListesiYukle();
            IlleriGetir();
            SiparisSeriYukle();
        }

        private void SiparisSeriYukle()
        {
            ddlSiparisSeri.DataSource = SessionManager.SiparisSeri;
            ddlSiparisSeri.DataBind();
            ddlSiparisSeri.Items.Insert(0, new DropDownListItem("Seçiniz", "0"));
        }

        private void PersonelListesiYukle()
        {
            if (SessionManager.MusteriBilgi.MusteriID == null) return;
            DataTable dt = null;
            if (SessionManager.PersonelListesi == null || SessionManager.PersonelListesi.Rows.Count == 0)
            {
                dt = new PersonelBS().PersonelListesiGetir(SessionManager.MusteriBilgi.MusteriID.Value);
                SessionManager.PersonelListesi = dt;
            }
            else
                dt = SessionManager.PersonelListesi;

            ListBoxMontajEkibi.DataSource = dt;
            ListBoxMontajEkibi.DataBind();
        }

        protected void btnSorgula_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();

            /*
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
            */
            DataTable dt = new SiparisIslemleriBS().SiparisSorgula(prms);

            grdSiparisler.DataSource = dt;
            grdSiparisler.DataBind();
            SessionManager.SiparisSorguListesi = dt;
            ToplamSiparisDegerHesapla();
            tblSonuc.Visible = true;
        }

        private void ToplamSiparisDegerHesapla()
        {
            DataTable dt = SessionManager.SiparisSorguListesi;
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

        protected void grdSiparisler_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSiparisler.PageIndex = e.NewPageIndex;
            grdSiparisler.DataSource = SessionManager.SiparisSorguListesi;
            grdSiparisler.DataBind();
        }

        protected void grdSiparisler_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink link = (HyperLink)e.Row.FindControl("lnkGoruntule");
            if (link != null)
            {
                DataRowView view = (DataRowView)e.Row.DataItem;
                string siparisID = view.Row.ItemArray[0].ToString();
                string siparisSeri = view.Row.ItemArray[1].ToString();
                link.NavigateUrl = "~/SiparisGoruntule.aspx?SiparisID=" + siparisID + "&SiparisSeri=" + siparisSeri;
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            foreach (RadListBoxItem item in ListBoxMontajEkibi.Items)
                if (item.Checked) item.Checked = false;
        }

        protected void IlleriGetir()
        {
            DataTable dt = new ReferansDataBS().IlleriGetir();
            if (dt.Rows.Count > 0)
            {
                ddlMusteriIl.DataSource = dt;
                ddlMusteriIl.DataTextField = "IlAd";
                ddlMusteriIl.DataValueField = "IlKod";
                ddlMusteriIl.DataBind();
            }
            else
            {
                ddlMusteriIl.DataSource = null;
                ddlMusteriIl.DataBind();
            }

            ddlMusteriIl.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seçiniz", "0"));
            ddlMusteriIlce.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seçiniz", "0"));
            ddlMusteriSemt.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seçiniz", "0"));
        }

        protected void IlceleriGetir(string ilKod)
        {
            DataTable dt = new ReferansDataBS().IlceleriGetir(ilKod);

            if (dt.Rows.Count > 0)
            {
                ddlMusteriIlce.DataSource = dt;
                ddlMusteriIlce.DataTextField = "IlceAd";
                ddlMusteriIlce.DataValueField = "IlceKod";
                ddlMusteriIlce.DataBind();
            }
            else
            {
                ddlMusteriIlce.DataSource = null;
                ddlMusteriIlce.DataBind();
            }
            ddlMusteriIlce.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seçiniz", "0"));
            ddlMusteriSemt.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seçiniz", "0"));
            ddlMusteriIlce.SelectedIndex = 0;
            ddlMusteriSemt.SelectedIndex = 0;
        }

        protected void SemtleriGetir(string ilceKod)
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

            ddlMusteriSemt.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seçiniz", "0"));
            ddlMusteriSemt.SelectedIndex = 0;
        }

        protected void ddlMusteriIl_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlMusteriIlce.Text = "";
            ddlMusteriIlce.Items.Clear();
            ddlMusteriSemt.Items.Clear();
            ddlMusteriSemt.Text = "";
            IlceleriGetir(e.Value);
        }

        protected void ddlMusteriIlce_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlMusteriSemt.Items.Clear();
            ddlMusteriSemt.Text = "";
            SemtleriGetir(e.Value);
        }

        private void GenerateForm()
        {
            if (ddlSiparisSeri.SelectedIndex == 0) return;

            var generator = new FormGenerator()
            {
                SiparisSeri = ddlSiparisSeri.SelectedValue,
                IslemTipi = FormIslemTipi.Sorgula
            };
            generator.Generate(this.divSiparisFormKontrolleri);
        }
    }
}
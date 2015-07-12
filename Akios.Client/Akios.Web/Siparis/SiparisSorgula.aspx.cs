using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Kobsis.Business;
using Kobsis.DataType;
using Kobsis.Generation;
using Kobsis.Util;
using Telerik.Web.UI;

namespace Akios.Web.Siparis
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

            ListBoxTeslimatEkibi.DataSource = dt;
            ListBoxTeslimatEkibi.DataBind();
        }

        protected void btnSorgula_Click(object sender, EventArgs e)
        {
            var parametreler = new List<DbParametre>();

            var paramSiparisNo = new DbParametre() { ParametreAdi = "SiparisNo", ParametreDegeri = string.IsNullOrWhiteSpace(txtSiparisNo.Text) ? null : txtSiparisNo.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramSeriID = new DbParametre() { ParametreAdi = "SeriID", ParametreDegeri = ddlSiparisSeri.SelectedIndex <= 0 ? null : ddlSiparisSeri.SelectedValue, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramAdet = new DbParametre() { ParametreAdi = "Adet", ParametreDegeri = string.IsNullOrWhiteSpace(txtSiparisAdeti.Text) ? null : txtSiparisAdeti.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramSiparisTarihBas = new DbParametre() { ParametreAdi = "SiparisTarihBas", ParametreDegeri = rdtSiparisTarihBas.SelectedDate, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramSiparisTarihBit = new DbParametre() { ParametreAdi = "SiparisTarihBit", ParametreDegeri = rdtSiparisTarihBit.SelectedDate, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramTeslimTarihBas = new DbParametre() { ParametreAdi = "TeslimTarihBas", ParametreDegeri = rdtTeslimTarihBas.SelectedDate, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramTeslimTarihBit = new DbParametre() { ParametreAdi = "TeslimTarihBit", ParametreDegeri = rdtTeslimTarihBit.SelectedDate, VeriTipi = SqlDbType.DateTime, ParametreBoyutu = 50 };
            var paramFirmaAdi = new DbParametre() { ParametreAdi = "FirmaAdi", ParametreDegeri = string.IsNullOrWhiteSpace(txtMusteriFirmaAdi.Text) ? null : txtMusteriFirmaAdi.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 250 };
            var paramMusteriAdi = new DbParametre() { ParametreAdi = "MusteriAdi", ParametreDegeri = string.IsNullOrWhiteSpace(txtMusteriAd.Text) ? null : txtMusteriAd.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 100 };
            var paramMusteriSoyad = new DbParametre() { ParametreAdi = "MusteriSoyad", ParametreDegeri = string.IsNullOrWhiteSpace(txtMusteriSoyad.Text) ? null : txtMusteriSoyad.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 100 };
            var paramMusteriEvTel = new DbParametre() { ParametreAdi = "MusteriEvTel", ParametreDegeri = string.IsNullOrWhiteSpace(txtMusteriEvTel.Text) ? null : txtMusteriEvTel.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriIsTel = new DbParametre() { ParametreAdi = "MusteriIsTel", ParametreDegeri = string.IsNullOrWhiteSpace(txtMusteriIsTel.Text) ? null : txtMusteriIsTel.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriCepTel = new DbParametre() { ParametreAdi = "MusteriCepTel", ParametreDegeri = string.IsNullOrWhiteSpace(txtMusteriCepTel.Text) ? null : txtMusteriCepTel.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriAdres = new DbParametre() { ParametreAdi = "MusteriAdres", ParametreDegeri = string.IsNullOrWhiteSpace(txtMusteriAdres.Text) ? null : txtMusteriAdres.Text, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 500 };
            var paramMusteriIlKod = new DbParametre() { ParametreAdi = "MusteriIlKod", ParametreDegeri = ddlMusteriIl.SelectedIndex <= 0 ? null : ddlMusteriIl.SelectedValue, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriIlceKod = new DbParametre() { ParametreAdi = "MusteriIlceKod", ParametreDegeri = ddlMusteriIlce.SelectedIndex <= 0 ? null : ddlMusteriIlce.SelectedValue, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };
            var paramMusteriSemtKod = new DbParametre() { ParametreAdi = "MusteriSemtKod", ParametreDegeri = ddlMusteriSemt.SelectedIndex <= 0 ? null : ddlMusteriSemt.SelectedValue, VeriTipi = SqlDbType.VarChar, ParametreBoyutu = 50 };

            parametreler.Add(paramSiparisNo);
            parametreler.Add(paramSeriID);
            parametreler.Add(paramAdet);
            parametreler.Add(paramSiparisTarihBas);
            parametreler.Add(paramSiparisTarihBit);
            parametreler.Add(paramTeslimTarihBas);
            parametreler.Add(paramTeslimTarihBit);
            parametreler.Add(paramFirmaAdi);
            parametreler.Add(paramMusteriAdi);
            parametreler.Add(paramMusteriSoyad);
            parametreler.Add(paramMusteriEvTel);
            parametreler.Add(paramMusteriIsTel);
            parametreler.Add(paramMusteriCepTel);
            parametreler.Add(paramMusteriAdres);
            parametreler.Add(paramMusteriIlKod);
            parametreler.Add(paramMusteriIlceKod);
            parametreler.Add(paramMusteriSemtKod);

            var siparisBS = new SiparisBS();
            var formGenerator = new FormGenerator();
            var metadataList = siparisBS.SiparisMetdataGetir(SessionManager.MusteriBilgi.MusteriID.Value.ToString()).Where(q => q.SorgulanacakMi);

            foreach (SiparisMetadata metadata in metadataList)
            {
                var paramMetadata = new DbParametre()
                {
                    ParametreAdi = metadata.KolonAdi,
                    ParametreDegeri = formGenerator.KontrolDegeriBul(divSiparisFormKontrolleri, metadata.KontrolAdi, metadata.YerlesimTabloID),
                    VeriTipi = formGenerator.VeriTipiBelirle(metadata.VeriTipAdi),
                    ParametreBoyutu = 50
                };
                parametreler.Add(paramMetadata);
            }

            DataTable dt = siparisBS.SiparisSorgula(parametreler, SessionManager.MusteriBilgi.Kod);

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

            lblToplamSiparis.Text = dt.Rows.Count.ToString();
            lblToplamUrun.Text = (from DataRow row in dt.Rows
                                  where row["Adet"] != DBNull.Value
                                  select Convert.ToInt32(row["Adet"])).Sum().ToString();
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
                link.NavigateUrl = "~/Siparis/SiparisGoruntule.aspx?SiparisID=" + siparisID + "&SiparisSeri=" + siparisSeri;
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            foreach (RadListBoxItem item in ListBoxTeslimatEkibi.Items)
            {
                if (item.Checked) item.Checked = false;
            }
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
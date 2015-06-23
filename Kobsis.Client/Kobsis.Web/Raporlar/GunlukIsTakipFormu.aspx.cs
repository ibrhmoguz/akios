using System;
using System.Data;
using System.Web.UI.WebControls;
using Kobsis.Business;
using Kobsis.Util;
using Kobsis.Web.Helper;

namespace Kobsis.Web.Raporlar
{
    public partial class GunlukIsTakipFormu : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rdtTarih.SelectedDate = DateTime.Now;
                RaporOlustur();
            }
        }

        protected void btnSorgula_Click(object sender, EventArgs e)
        {
            RaporOlustur();
        }

        private void RaporOlustur()
        {
            if (rdtTarih.SelectedDate == null)
            {
                MessageBox.Uyari(this.Page, "Lütfen gün seçiniz!");
                return;
            }

            DataTable dt = new RaporBS().GunlukIsTakipFormuListele(rdtTarih.SelectedDate.Value.Date, SessionManager.MusteriBilgi.Kod);

            if (dt.Rows.Count > 0)
            {
                grdSiparisler.DataSource = dt;
                grdSiparisler.DataBind();
                btnYazdir.Visible = true;
            }
            else
            {
                grdSiparisler.DataSource = null;
                grdSiparisler.DataBind();
                btnYazdir.Visible = false;
            }

            SessionManager.GunlukIsTakipListesi = dt;
            string tarih = rdtTarih.SelectedDate.Value.ToShortDateString();
            PopupPageHelper.OpenPopUp(btnYazdir, "../Print/GunlukIsTakip.aspx?Tarih=" + tarih, "", true, false, true, false, false, false, 1024, 800, true, false, "onclick");
        }

        protected void grdSiparisler_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSiparisler.PageIndex = e.NewPageIndex;
            grdSiparisler.DataSource = SessionManager.GunlukIsTakipListesi;
            grdSiparisler.DataBind();
        }
    }
}
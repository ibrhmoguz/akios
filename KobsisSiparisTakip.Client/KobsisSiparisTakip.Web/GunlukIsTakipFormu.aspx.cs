using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Web.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KobsisSiparisTakip.Web
{
    public partial class GunlukIsTakipFormu : KobsisBasePage
    {
        private DataTable SorguSonucListesi
        {
            get
            {
                if (Session["GunlukIsTakipListesi"] != null)
                    return Session["GunlukIsTakipListesi"] as DataTable;
                else
                    return null;
            }
            set
            {
                Session["GunlukIsTakipListesi"] = value;
            }
        }

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
            DataTable dt = new RaporBS().GunlukIsTakipFormuListele(rdtTarih.SelectedDate.Value.Date);

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

            this.SorguSonucListesi = dt;
            string tarih = rdtTarih.SelectedDate.Value.ToShortDateString();
            PopupPageHelper.OpenPopUp(btnYazdir, "Print/GunlukIsTakip.aspx?Tarih=" + tarih, "", true, false, true, false, false, false, 1024, 800, true, false, "onclick");
        }

        protected void grdSiparisler_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSiparisler.PageIndex = e.NewPageIndex;
            grdSiparisler.DataSource = this.SorguSonucListesi;
            grdSiparisler.DataBind();
        }
    }
}
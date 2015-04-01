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
    public partial class Hatalar : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["yetki"].ToString() == "Kullanici")
            {
                MessageBox.Hata(this, "Bu sayfaya erişim yetkiniz yoktur!");
                return;
            }

            if (!Page.IsPostBack)
            {
                HatalariYukle();
            }
        }

        private void HatalariYukle()
        {
            DataTable dt = new YonetimKonsoluBS().HatalariGetir();

            if (dt.Rows.Count > 0)
            {
                grdHatalar.DataSource = dt;
                lblhataSayisi.Text = dt.Rows.Count.ToString();
            }
            else
                grdHatalar.DataSource = null;

            grdHatalar.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KobsisSiparisTakip.Web.Print
{
    public partial class KapiTipineGoreSatilanAdet : System.Web.UI.Page
    {
        private DataTable SatisAdetListesi
        {
            get
            {
                if (Session["SatisAdetListesi"] != null)
                    return Session["SatisAdetListesi"] as DataTable;
                else
                    return null;
            }
            set
            {
                Session["SatisAdetListesi"] = value;
            }
        }

        private DataTable SatisTutarListesi
        {
            get
            {
                if (Session["SatisTutarListesi"] != null)
                    return Session["SatisTutarListesi"] as DataTable;
                else
                    return null;
            }
            set
            {
                Session["SatisTutarListesi"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RaporOlustur();
            }
        }

        private void RaporOlustur()
        {
            GridDoldur(grdSatisAdetRapor, this.SatisAdetListesi);
            GridDoldur(grdSatisTutarRapor, this.SatisTutarListesi);
        }

        private void GridDoldur(GridView gv, DataTable dtSatisAdet)
        {
            if (dtSatisAdet.Rows.Count > 0)
            {
                gv.DataSource = dtSatisAdet;
                gv.DataBind();
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();
            }
        }
    }
}
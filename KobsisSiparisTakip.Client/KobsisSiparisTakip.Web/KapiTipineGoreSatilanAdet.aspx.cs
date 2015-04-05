using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Web.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KobsisSiparisTakip.Web
{
    public partial class KapiTipineGoreSatilanAdet : KobsisBasePage
    {
        private static string ANKARA_IL_KODU = "6";

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
                VarsayilanDegerleriYukle();
            }
        }

        private void VarsayilanDegerleriYukle()
        {
            IlleriGetir();
            YillariYukle();
        }

        private void YillariYukle()
        {
            ddlYil.Items.Clear();
            for (int i = 2014; i < 2030; i++)
            {
                Telerik.Web.UI.DropDownListItem item = new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString());
                if (item.Value == DateTime.Now.Year.ToString())
                    item.Selected = true;
                ddlYil.Items.Add(item);
            }
        }

        protected void ddlMusteriIl_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {
            ddlMusteriIlce.Items.Clear();
            IlceleriGetir(e.Value);
        }

        private void IlleriGetir()
        {
            DataTable dt = new SiparisIslemleriBS().IlleriGetir();
            if (dt.Rows.Count > 0)
            {
                ddlMusteriIl.DataSource = dt;
                ddlMusteriIl.DataTextField = "ILAD";
                ddlMusteriIl.DataValueField = "ILKOD";
                ddlMusteriIl.DataBind();

                ddlMusteriIl.FindItemByValue(ANKARA_IL_KODU).Selected = true;
                IlceleriGetir(ANKARA_IL_KODU);
            }
            else
            {
                ddlMusteriIl.DataSource = null;
                ddlMusteriIl.DataBind();
            }
            ddlMusteriIl.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
        }

        private void IlceleriGetir(string ilKod)
        {
            Dictionary<string, object> prms = new Dictionary<string, object>();
            prms.Add("ILKOD", ilKod);

            DataTable dt = new SiparisIslemleriBS().IlceleriGetir(prms);
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
            ddlMusteriIlce.SelectedIndex = 0;
        }

        protected void btnSorgula_Click(object sender, EventArgs e)
        {
            Sorgula();
        }

        private void Sorgula()
        {
            string il = null;
            string ilce = null;

            if (ddlMusteriIl.SelectedIndex != 0)
                il = ddlMusteriIl.SelectedText;
            if (ddlMusteriIlce.SelectedIndex != 0)
                ilce = ddlMusteriIlce.SelectedText;

            DataSet ds = new RaporBS().KapiTipineGoreSatilanAdet(il, ilce, ddlYil.SelectedValue);

            DataTable dtSatisAdet = ds.Tables[0];
            DataTable dtSatisTutar = ds.Tables[1];

            dtSatisAdet = YuzdeDegerleriHesaplaAdet(dtSatisAdet);

            if (dtSatisAdet.Rows.Count > 0)
            {
                grdSatisAdetRapor.DataSource = dtSatisAdet;
                grdSatisAdetRapor.DataBind();
                btnYazdir.Visible = true;
            }
            else
            {
                grdSatisAdetRapor.DataSource = null;
                grdSatisAdetRapor.DataBind();
                btnYazdir.Visible = false;
            }

            dtSatisTutar = YuzdeDegerleriHesaplaTutar(dtSatisTutar);

            if (dtSatisTutar.Rows.Count > 0)
            {
                grdSatisTutarRapor.DataSource = dtSatisTutar;
                grdSatisTutarRapor.DataBind();
            }
            else
            {
                grdSatisTutarRapor.DataSource = null;
                grdSatisTutarRapor.DataBind();
            }

            this.SatisAdetListesi = dtSatisAdet;
            this.SatisTutarListesi = dtSatisTutar;
            PopupPageHelper.OpenPopUp(btnYazdir, "Print/KapiTipineGoreSatilanAdet.aspx", "", true, false, true, false, false, false, 1024, 800, true, false, "onclick");
        }

        private DataTable YuzdeDegerleriHesaplaAdet(DataTable dt)
        {
            decimal toplamAdet = Convert.ToDecimal(dt.AsEnumerable().Sum(a => Convert.ToDecimal(a.Field<string>("Yillik"))).ToString());
            decimal yuzde;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Yillik"] != DBNull.Value)
                {
                    if (toplamAdet != 0)
                    {
                        yuzde = Convert.ToDecimal((Convert.ToDecimal(row["Yillik"].ToString()) / toplamAdet));
                        row["Yuzde(%)"] = (yuzde * 100).ToString("0.00", CultureInfo.InvariantCulture);
                    }
                    else
                        row["Yuzde(%)"] = "0";
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != DBNull.Value && dt.Rows[i][j].ToString() == "0")
                        dt.Rows[i][j] = string.Empty;
                }
            }

            DataRow toplamRow = dt.NewRow();
            toplamRow[0] = "TOPLAM";
            toplamRow[1] = dt.AsEnumerable().Where(q => q.Field<string>("1") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("1"))).ToString();
            toplamRow[2] = dt.AsEnumerable().Where(q => q.Field<string>("2") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("2"))).ToString();
            toplamRow[3] = dt.AsEnumerable().Where(q => q.Field<string>("3") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("3"))).ToString();
            toplamRow[4] = dt.AsEnumerable().Where(q => q.Field<string>("4") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("4"))).ToString();
            toplamRow[5] = dt.AsEnumerable().Where(q => q.Field<string>("5") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("5"))).ToString();
            toplamRow[6] = dt.AsEnumerable().Where(q => q.Field<string>("6") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("6"))).ToString();
            toplamRow[7] = dt.AsEnumerable().Where(q => q.Field<string>("7") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("7"))).ToString();
            toplamRow[8] = dt.AsEnumerable().Where(q => q.Field<string>("8") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("8"))).ToString();
            toplamRow[9] = dt.AsEnumerable().Where(q => q.Field<string>("9") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("9"))).ToString();
            toplamRow[10] = dt.AsEnumerable().Where(q => q.Field<string>("10") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("10"))).ToString();
            toplamRow[11] = dt.AsEnumerable().Where(q => q.Field<string>("11") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("11"))).ToString();
            toplamRow[12] = dt.AsEnumerable().Where(q => q.Field<string>("12") != string.Empty).Sum(a => Convert.ToInt32(a.Field<string>("12"))).ToString();
            toplamRow["Yillik"] = toplamAdet.ToString();
            toplamRow["Yuzde(%)"] = "100";
            dt.Rows.Add(toplamRow);

            return dt;
        }

        private DataTable YuzdeDegerleriHesaplaTutar(DataTable dt)
        {
            decimal toplamAdet = dt.AsEnumerable().Sum(a => a.Field<Decimal>("Yillik"));
            decimal yuzde;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Yillik"] != DBNull.Value)
                {
                    if (toplamAdet != 0)
                    {
                        yuzde = Convert.ToDecimal((Convert.ToDecimal(row["Yillik"].ToString()) / toplamAdet));
                        row["Yuzde(%)"] = (yuzde * 100).ToString("0.00", CultureInfo.GetCultureInfo("tr-TR"));
                    }
                    else
                        row["Yuzde(%)"] = "0";
                }
            }

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        if (dt.Rows[i][j] != DBNull.Value && dt.Rows[i][j].ToString() == "0,00")
            //            dt.Rows[i][j] = string.Empty;
            //    }
            //}

            DataRow toplamRow = dt.NewRow();
            toplamRow[0] = "TOPLAM";
            toplamRow[1] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("1")).ToString("0.00", CultureInfo.GetCultureInfo("tr-TR"));
            toplamRow[2] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("2"));
            toplamRow[3] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("3"));
            toplamRow[4] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("4"));
            toplamRow[5] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("5"));
            toplamRow[6] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("6"));
            toplamRow[7] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("7"));
            toplamRow[8] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("8"));
            toplamRow[9] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("9"));
            toplamRow[10] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("10"));
            toplamRow[11] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("11"));
            toplamRow[12] = dt.AsEnumerable().Sum(a => a.Field<Decimal>("12"));
            toplamRow["Yillik"] = toplamAdet;
            toplamRow["Yuzde(%)"] = "100";
            dt.Rows.Add(toplamRow);

            return dt;
        }
    }
}
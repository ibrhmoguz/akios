using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Kobsis.Business;
using Kobsis.Util;
using Kobsis.Web.Helper;
using Telerik.Web.UI;

namespace Kobsis.Web.YonetimKonsolu
{
    public partial class FormOgeGuncelleme : KobsisBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MusteriReferanslariniGetir();
            }
        }

        private void MusteriReferanslariniGetir()
        {
            DataTable dt = ReferansDataManager.MusteriReferanslariYukle();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlReferanslar.DataSource = dt;
                ddlReferanslar.DataTextField = "RefAdi";
                ddlReferanslar.DataValueField = "RefID";
                ddlReferanslar.DataBind();
                ddlReferanslar.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            }
            else
            {
                ddlReferanslar.DataSource = null;
                ddlReferanslar.DataBind();
            }
        }

        protected void ddlReferanslar_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            if (ddlReferanslar.SelectedIndex == 0) return;

            ReferansDetaylariniYukle();
        }

        private void ReferansDetaylariniYukle()
        {
            if (SessionManager.MusteriBilgi.MusteriID == null) return;

            DataTable dt = new YonetimKonsoluBS().FormOgeDetayGetir(SessionManager.MusteriBilgi.MusteriID.Value, ddlReferanslar.SelectedValue);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvReferansDetay.DataSource = dt;
                gvReferansDetay.DataBind();
            }
            else
            {
                gvReferansDetay.DataSource = null;
                gvReferansDetay.DataBind();
            }
        }

        protected void gvReferansDetay_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int refDetayId = -1;
            for (int i = 2; i < gvReferansDetay.Rows[e.RowIndex].Cells.Count; i++)
            {
                if (gvReferansDetay.Rows[e.RowIndex].Cells[i].Controls[0] is CheckBox)
                {
                    CheckBox chcBox = (CheckBox)gvReferansDetay.Rows[e.RowIndex].Cells[i].Controls[0];
                    if (chcBox.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(chcBox.ToolTip))
                        {
                            refDetayId = Convert.ToInt32(chcBox.ToolTip);
                            break;
                        }
                    }
                }
            }

            bool islemDurum = new YonetimKonsoluBS().FormOgeDetaySil(refDetayId);
            if (islemDurum)
            {
                MessageBox.Basari(this.Page, "Öğe detayı silindi.");
            }
            else
                MessageBox.Hata(this.Page, "Öğe detayı silinmedi.");

            ReferansDetaylariniYukle();
        }

        protected void gvReferansDetay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    var chcBox = new CheckBox() { Checked = false, Enabled = false };
                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        chcBox.Checked = true;
                        chcBox.ToolTip = e.Row.Cells[i].Text;
                    }
                    e.Row.Cells[i].Controls.Add(chcBox);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    WebControl wc;
                    switch (i)
                    {
                        case 0:
                            wc = new RadButton() { Text = "Ekle", RenderMode = RenderMode.Lightweight };
                            break;
                        case 1:
                            wc = new TextBox() { Width = 250 };
                            break;
                        default:
                            wc = new CheckBox() { Checked = false, Enabled = true };
                            break;
                    }

                    e.Row.Cells[i].Controls.Add(wc);
                }
            }
        }
    }
}
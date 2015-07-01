using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
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

            if (ddlReferanslar.SelectedIndex != 0)
            {
                ReferansDetaylariniYukle();
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
                ddlReferanslar.Items.Insert(0, new DropDownListItem("Seçiniz", "0"));
            }
            else
            {
                ddlReferanslar.DataSource = null;
                ddlReferanslar.DataBind();
            }
        }

        protected void ddlReferanslar_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            if (ddlReferanslar.SelectedIndex == 0)
            {
                gvReferansDetay.Visible = false;
                return;
            }
            gvReferansDetay.Visible = true;

            ReferansDetaylariniYukle();
        }

        private void ReferansDetaylariniYukle()
        {
            if (SessionManager.MusteriBilgi.MusteriID == null) return;

            DataTable dt = new YonetimKonsoluBS().FormOgeDetayGetir(SessionManager.MusteriBilgi.MusteriID.Value, ddlReferanslar.SelectedValue);
            SessionManager.ReferansDetay = dt;

            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                }

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
                ReferansDetaylariniYukle();
                SessionManager.ReferansData = null;
                MessageBox.Basari(this.Page, "Öğe detayı silindi.");
            }
            else
                MessageBox.Hata(this.Page, "Öğe detayı silinmedi.");
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
                e.Row.BackColor = Color.FromName("#E6F0F7");
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    WebControl wc;
                    switch (i)
                    {
                        case 0:
                            var imageButton = new ImageButton() { ImageUrl = "~/App_Themes/Theme/Raster/ekle.gif" };
                            imageButton.Click += imageButton_Click;
                            wc = imageButton;
                            break;
                        case 1:
                            wc = new TextBox() { ID = "FooterTextBoxRefDetayAdi", Width = 250, BorderColor = Color.White, BackColor = Color.White };
                            break;
                        default:
                            wc = new CheckBox() { ID = "FooterRefDetaySeri" + i, Checked = false, Enabled = true };
                            break;
                    }

                    e.Row.Cells[i].Controls.Add(wc);
                }
            }
        }

        protected void imageButton_Click(object sender, ImageClickEventArgs e)
        {
            var footerRow = gvReferansDetay.FooterRow;
            var textBoxRefDetayAdi = footerRow.FindControl("FooterTextBoxRefDetayAdi") as TextBox;

            if (textBoxRefDetayAdi == null) return;
            if (string.IsNullOrEmpty(textBoxRefDetayAdi.Text))
            {
                MessageBox.Bilgi(this.Page, "Referans detay adı girmelisiniz!");
            }

            var seriIdList = new List<string>();
            for (int i = 2; i < footerRow.Cells.Count; i++)
            {
                var chcBox = footerRow.FindControl("FooterRefDetaySeri" + i) as CheckBox;
                if (chcBox == null || !chcBox.Checked)
                    continue;

                var siparisSeri = SessionManager.SiparisSeri.FirstOrDefault(q => q.SeriAdi.Equals(SessionManager.ReferansDetay.Columns[i - 1].Caption));
                if (siparisSeri != null)
                    seriIdList.Add(siparisSeri.SiparisSeriID.ToString());
            }

            bool islemDurum = new YonetimKonsoluBS().FormOgeDetayEkle(ddlReferanslar.SelectedValue, textBoxRefDetayAdi.Text, seriIdList);
            if (islemDurum)
            {
                ReferansDetaylariniYukle();
                SessionManager.ReferansData = null;
                MessageBox.Basari(this.Page, "Öğe detayı eklendi.");
            }
            else
                MessageBox.Hata(this.Page, "Öğe detayı eklenemedi.");
        }
    }
}
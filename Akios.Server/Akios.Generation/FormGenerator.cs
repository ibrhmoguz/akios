using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Akios.Business;
using Akios.DataType;
using Akios.Util;
using Telerik.Web.UI;

namespace Akios.Generation
{
    public class FormGenerator
    {
        public string SiparisSeri { get; set; }

        public FormIslemTipi IslemTipi { get; set; }

        public WebControl Generate(WebControl wc)
        {
            if (!SessionManager.KullaniciBilgi.MusteriID.HasValue) return wc;

            List<Layout> seriLayoutList = null;
            if (this.IslemTipi == FormIslemTipi.Sorgula)
            {
                if (SessionManager.SiparisSorgulaFormLayout == null)
                {
                    SessionManager.SiparisSorgulaFormLayout = new FormLayoutBS().SorgulamaFormKontrolleriniGetir(SessionManager.KullaniciBilgi.MusteriID.Value);
                }

                if (SessionManager.SiparisSorgulaFormLayout != null)
                {
                    seriLayoutList = SessionManager.SiparisSorgulaFormLayout.Where(q => q.SeriID == Convert.ToInt32(this.SiparisSeri)).ToList();
                }
            }
            else
            {
                if (SessionManager.SiparisFormLayout == null)
                {
                    SessionManager.SiparisFormLayout = new FormLayoutBS().FormKontrolleriniGetir(SessionManager.KullaniciBilgi.MusteriID.Value);
                }

                if (SessionManager.SiparisFormLayout != null)
                {
                    seriLayoutList = SessionManager.SiparisFormLayout.Where(q => q.SeriID == Convert.ToInt32(this.SiparisSeri)).ToList();
                }
            }

            if (seriLayoutList != null)
            {
                foreach (Layout layout in seriLayoutList)
                {
                    WebControl wcTemp = KontrolOlustur(layout);
                    if (wcTemp == null)
                        continue;

                    if (layout.YerlesimParentID == null)
                    {
                        wc.Controls.Add(wcTemp);
                    }
                    else
                    {
                        var parentKontrolId = ParentKontrolIdBul(seriLayoutList, layout.YerlesimParentID.Value);
                        WebControl wcParent = KontrolBul(wc, parentKontrolId);
                        if (wcParent != null)
                            wcParent.Controls.Add(wcTemp);
                    }
                }
            }
            return wc;
        }

        public object KontrolDegeriBul(Panel divPanel, string kontrolAdi, string yerlesimTabloId)
        {
            object kontrolDegeri = null;
            var control = KontrolBul(divPanel, kontrolAdi.Replace(" ", string.Empty) + yerlesimTabloId);

            var textBox = control as RadTextBox;
            if (textBox != null)
            {
                kontrolDegeri = !string.IsNullOrWhiteSpace(textBox.Text) ? textBox.Text : null;
            }
            var maskedTextBox = control as RadMaskedTextBox;
            if (maskedTextBox != null)
            {
                kontrolDegeri = !string.IsNullOrWhiteSpace(maskedTextBox.Text) ? maskedTextBox.Text : null;
            }
            else
            {
                var numericTextBox = control as RadNumericTextBox;
                if (numericTextBox != null)
                {
                    kontrolDegeri = !string.IsNullOrWhiteSpace(numericTextBox.Text) ? numericTextBox.Text : null;
                }
                else
                {
                    var checkBox = control as CheckBox;
                    if (checkBox != null)
                    {
                        kontrolDegeri = checkBox.Checked == true && checkBox.Checked;
                    }
                    else
                    {
                        var datePicker = control as RadDatePicker;
                        if (datePicker != null)
                        {
                            kontrolDegeri = datePicker.SelectedDate;
                        }
                        else
                        {
                            var dropDownList = control as RadDropDownList;
                            if (dropDownList != null)
                            {
                                kontrolDegeri = dropDownList.SelectedValue != null && dropDownList.SelectedValue != "0" ? dropDownList.SelectedValue : null;
                            }
                            else
                            {
                                var label = control as Label;
                                if (label != null)
                                {
                                    kontrolDegeri = !string.IsNullOrWhiteSpace(label.Text) ? label.Text : null;
                                }
                            }
                        }
                    }
                }
            }

            return kontrolDegeri;
        }

        public SqlDbType VeriTipiBelirle(string veriTipAdi)
        {
            var sqlType = new SqlDbType();

            switch (veriTipAdi)
            {
                case VeriTipi.BOOLEAN:
                    sqlType = SqlDbType.Bit;
                    break;
                case VeriTipi.DATETIME:
                    sqlType = SqlDbType.DateTime;
                    break;
                case VeriTipi.DATE:
                    sqlType = SqlDbType.Date;
                    break;
                case VeriTipi.DECIMAL:
                    sqlType = SqlDbType.Decimal;
                    break;
                case VeriTipi.INTEGER:
                    sqlType = SqlDbType.Int;
                    break;
                case VeriTipi.STRING:
                    sqlType = SqlDbType.VarChar;
                    break;
            }
            return sqlType;
        }

        private string ParentKontrolIdBul(List<Layout> layoutList, int parentId)
        {
            string parentKontrolId = string.Empty;
            foreach (Layout layout in layoutList)
            {
                if (layout.YerlesimID == parentId)
                {
                    parentKontrolId = KontrolIDGetir(layout);
                    break;
                }
            }
            return parentKontrolId;
        }

        private WebControl KontrolBul(WebControl rootControl, string controlId)
        {
            if (rootControl.ID == controlId) return rootControl;

            foreach (WebControl controlToSearch in rootControl.Controls)
            {
                WebControl controlToReturn = KontrolBul(controlToSearch, controlId);
                if (controlToReturn != null) return controlToReturn;
            }
            return null;
        }

        private WebControl KontrolOlustur(Layout layout)
        {
            WebControl wc = null;

            switch ((KontrolTipEnum)Enum.Parse(typeof(KontrolTipEnum), layout.KontrolTipID.ToString()))
            {
                case KontrolTipEnum.Table:
                    wc = TableOlustur(layout);
                    break;
                case KontrolTipEnum.TableRow:
                    wc = TableRowOlustur(layout);
                    break;
                case KontrolTipEnum.TableCell:
                    wc = TableCellOlustur(layout);
                    break;
                case KontrolTipEnum.TableHeaderCell:
                    wc = TableHeaderCellOlustur(layout);
                    break;
                case KontrolTipEnum.TextBox:
                    wc = (this.IslemTipi == FormIslemTipi.Goruntule || this.IslemTipi == FormIslemTipi.Print) ? LabelOlustur(layout) : TextBoxOlustur(layout);
                    break;
                case KontrolTipEnum.NumericTextBox:
                    wc = (this.IslemTipi == FormIslemTipi.Goruntule || this.IslemTipi == FormIslemTipi.Print) ? LabelOlustur(layout) : NumericTextBoxOlustur(layout);
                    break;
                case KontrolTipEnum.MaskedTextBox:
                    wc = (this.IslemTipi == FormIslemTipi.Goruntule || this.IslemTipi == FormIslemTipi.Print) ? LabelOlustur(layout) : MaskedTextBoxOlustur(layout);
                    break;
                case KontrolTipEnum.Literal:
                    wc = LiteralOlustur(layout);
                    break;
                case KontrolTipEnum.Label:
                    wc = LabelOlustur(layout);
                    break;
                case KontrolTipEnum.Image:
                    wc = ImageOlustur(layout);
                    break;
                case KontrolTipEnum.DropDownList:
                    wc = (this.IslemTipi == FormIslemTipi.Goruntule || this.IslemTipi == FormIslemTipi.Print) ? LabelOlustur(layout) : DropDownListOlustur(layout);
                    break;
                case KontrolTipEnum.DatePicker:
                    wc = (this.IslemTipi == FormIslemTipi.Goruntule || this.IslemTipi == FormIslemTipi.Print) ? LabelOlustur(layout) : DateTimePickerOlustur(layout);
                    break;
                case KontrolTipEnum.CheckBox:
                    wc = (this.IslemTipi == FormIslemTipi.Goruntule || this.IslemTipi == FormIslemTipi.Print) ? LabelOlustur(layout) : CheckBoxOlustur(layout);
                    break;
            }

            return wc;
        }

        private WebControl CheckBoxOlustur(Layout layout)
        {
            var wc = new CheckBox();
            if (layout.PostBack) wc.AutoPostBack = true;
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            if (this.IslemTipi == FormIslemTipi.Guncelle && !string.IsNullOrWhiteSpace(layout.KolonAdi))
            {
                if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                {
                    bool value;
                    if (Boolean.TryParse(SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString(), out value))
                        wc.Checked = value;
                }
            }
            return wc;
        }

        private WebControl DateTimePickerOlustur(Layout layout)
        {
            var wc = new RadDatePicker();
            if (layout.PostBack) wc.AutoPostBack = true;
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.SelectedDate = Convert.ToDateTime(layout.Text);
            KontrolOzellikAyarla(layout, wc);
            if (this.IslemTipi == FormIslemTipi.Guncelle && !string.IsNullOrWhiteSpace(layout.KolonAdi))
            {
                if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                {
                    DateTime value;
                    if (DateTime.TryParse(SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString(), out value))
                        wc.SelectedDate = value;
                }
            }
            return wc;
        }

        private WebControl DropDownListOlustur(Layout layout)
        {
            var wc = new RadDropDownList();
            wc.RenderMode = RenderMode.Lightweight;
            wc.DataValueField = "RefDetayID";
            wc.DataTextField = "RefDetayAdi";
            if (layout.PostBack) wc.AutoPostBack = true;
            KontrolOzellikAyarla(layout, wc);
            if (layout.RefID.HasValue)
            {
                wc.DataSource = new ReferansDataBS() { SiparisSeri = this.SiparisSeri, RefID = layout.RefID.Value.ToString() }.ReferansVerisiGetir();
                wc.DataBind();
                if (wc.Items != null)
                    wc.Items.Insert(0, new Telerik.Web.UI.DropDownListItem("Seçiniz", "0"));
            }
            if (this.IslemTipi == FormIslemTipi.Guncelle && !string.IsNullOrWhiteSpace(layout.KolonAdi))
            {
                if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                {
                    DropDownListItem ddli = wc.FindItemByValue(SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString());
                    if (ddli != null && ddli.Selected == false)
                        ddli.Selected = true;
                }
            }
            return wc;
        }

        private WebControl ImageOlustur(Layout layout)
        {
            var wc = new Image();
            wc.ImageUrl = "ImageForm.aspx?ImageID=" + layout.ImajID;
            KontrolOzellikAyarla(layout, wc);
            return wc;
        }

        private WebControl LabelOlustur(Layout layout)
        {
            var wc = new Label();
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            if (this.IslemTipi != FormIslemTipi.Kaydet && !string.IsNullOrWhiteSpace(layout.KolonAdi))
            {
                if (layout.RefID.HasValue)
                {
                    var dt = new ReferansDataBS() { SiparisSeri = this.SiparisSeri, RefID = layout.RefID.Value.ToString() }.ReferansVerisiGetir();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                        {
                            DataRow dr = dt.AsEnumerable().SingleOrDefault(p => p.Field<int>("RefDetayID") == Convert.ToInt32(SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString()));
                            wc.Text = (dr != null && dr["RefDetayAdi"] != DBNull.Value) ? dr["RefDetayAdi"].ToString() : string.Empty;
                        }
                    }
                }
                else if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                {
                    var kolonDegeri = SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString();
                    bool result;
                    if (Boolean.TryParse(kolonDegeri, out result))
                    {
                        wc.Text = result ? "Evet" : "Hayır";
                    }
                    else
                    {
                        wc.Text = SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString();
                    }
                }
            }
            return wc;
        }

        private WebControl LiteralOlustur(Layout layout)
        {
            if (!string.IsNullOrWhiteSpace(layout.Text))
            {
                if (this.IslemTipi == FormIslemTipi.Print && layout.Text.Equals("<br />"))
                    return null;

                return new LiteralWebControl(layout.Text) { ID = KontrolIDGetir(layout) };
            }

            return null;
        }

        private WebControl TextBoxOlustur(Layout layout)
        {
            var wc = new RadTextBox();
            wc.RenderMode = RenderMode.Lightweight;
            if (!string.IsNullOrWhiteSpace(layout.TextMode)) wc.TextMode = (InputMode)Enum.Parse(typeof(InputMode), layout.TextMode);
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            if (this.IslemTipi == FormIslemTipi.Guncelle && !string.IsNullOrWhiteSpace(layout.KolonAdi))
            {
                if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                {
                    wc.Text = SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString();
                }
            }
            return wc;
        }

        private WebControl MaskedTextBoxOlustur(Layout layout)
        {
            var wc = new RadMaskedTextBox();
            wc.RenderMode = RenderMode.Lightweight;
            if (!string.IsNullOrWhiteSpace(layout.Mask)) wc.Mask = layout.Mask;
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            if (this.IslemTipi == FormIslemTipi.Guncelle && !string.IsNullOrWhiteSpace(layout.KolonAdi))
            {
                if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                {
                    wc.Text = SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString();
                }
            }
            return wc;
        }

        private WebControl NumericTextBoxOlustur(Layout layout)
        {
            var wc = new RadNumericTextBox();
            wc.RenderMode = RenderMode.Lightweight;
            wc.CssClass = "NumericFieldClass";
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            if (this.IslemTipi == FormIslemTipi.Guncelle && !string.IsNullOrWhiteSpace(layout.KolonAdi))
            {
                if (SessionManager.SiparisBilgi != null && SessionManager.SiparisBilgi.Rows.Count > 0 && SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi] != DBNull.Value)
                {
                    wc.Text = SessionManager.SiparisBilgi.Rows[0][layout.KolonAdi].ToString();
                }
            }
            return wc;
        }

        private WebControl TableHeaderCellOlustur(Layout layout)
        {
            var wc = new TableHeaderCell();
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            return wc;
        }

        private WebControl TableCellOlustur(Layout layout)
        {
            var wc = new TableCell();
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            return wc;
        }

        private WebControl TableRowOlustur(Layout layout)
        {
            return new TableRow() { ID = KontrolIDGetir(layout) };
        }

        private WebControl TableOlustur(Layout layout)
        {
            var wc = new Table();
            KontrolOzellikAyarla(layout, wc);
            if (this.IslemTipi == FormIslemTipi.Print && !string.IsNullOrWhiteSpace(layout.PrintCssClass))
                wc.CssClass = layout.PrintCssClass;
            return wc;
        }

        private void KontrolOzellikAyarla(Layout layout, WebControl wc)
        {
            wc.ID = KontrolIDGetir(layout);
            wc.Enabled = layout.Enabled;
            if (layout.Yukseklik != null) wc.Height = new Unit(layout.Yukseklik.Value);
            if (layout.Genislik != null) wc.Width = new Unit(layout.Genislik.Value);
            if (!String.IsNullOrWhiteSpace(layout.CssClass)) wc.CssClass = layout.CssClass;
            if (!String.IsNullOrWhiteSpace(layout.Style)) wc.Attributes.Add("style", layout.Style);
            if (layout.RowSpan != null) wc.Attributes.Add("rowspan", layout.RowSpan.ToString());
            if (layout.ColSpan != null) wc.Attributes.Add("colspan", layout.ColSpan.ToString());
        }

        private string KontrolIDGetir(Layout layout)
        {
            return layout.KontrolAdi.Replace(" ", string.Empty) + layout.YerlesimTabloID.ToString();
        }
    }
}
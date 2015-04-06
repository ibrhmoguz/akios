using KobsisSiparisTakip.Business;
using KobsisSiparisTakip.Business.DataTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace KobsisSiparisTakip.Web.Util
{
    public class FormGenerator
    {
        public WebControl Generate(string kapiSeri)
        {
            string parentKontrolID = string.Empty;
            WebControl wc = new WebControl(HtmlTextWriterTag.Div);
            wc.ID = "SiparisFormKontrolleri";
            wc.CssClass = "RadGrid_Current_Theme";
            wc.Style.Add("width", "100%");

            List<Layout> layoutList = new FormLayoutBS().FormKontrolleriniGetir(SessionManager.MusteriId, kapiSeri);

            foreach (Layout layout in layoutList)
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
                    parentKontrolID = ParentKontrolIDBul(layoutList, layout.YerlesimParentID.Value);
                    WebControl wcParent = ParentKontrolBul(wc, parentKontrolID);
                    if (wcParent != null)
                        wcParent.Controls.Add(wcTemp);
                }
            }
            return wc;
        }

        private string ParentKontrolIDBul(List<Layout> layoutList, int parentID)
        {
            string parentKontrolID = string.Empty;
            foreach (Layout layout in layoutList)
            {
                if (layout.YerlesimID == parentID)
                {
                    parentKontrolID = KontrolIDGetir(layout);
                    break;
                }
            }
            return parentKontrolID;
        }
        WebControl wc1;
        private WebControl ParentKontrolBul(WebControl wc, string kontrolID)
        {
            if (wc.ID == kontrolID)
                wc1 = wc;
            else
            {
                for (int i = 0; i < wc.Controls.Count; i++)
                {
                    WebControl wcChild = (WebControl)wc.Controls[i];
                    ParentKontrolBul(wcChild, kontrolID);
                }
            }
            return wc1;
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
                    wc = TextBoxOlustur(layout);
                    break;
                case KontrolTipEnum.NumericTextBox:
                    wc = NumericTextBoxOlustur(layout);
                    break;
                case KontrolTipEnum.MaskedTextBox:
                    wc = MaskedTextBoxOlustur(layout);
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
                    wc = DropDownListOlustur(layout);
                    break;
                case KontrolTipEnum.DatePicker:
                    wc = DateTimePickerOlustur(layout);
                    break;
                case KontrolTipEnum.CheckBox:
                    wc = CheckBoxOlustur(layout);
                    break;
            }

            return wc;
        }

        private WebControl BrOlustur(Layout layout)
        {
            var wc = new LiteralWebControl("<br />");
            wc.ID = KontrolIDGetir(layout);
            return wc;
        }

        private WebControl CheckBoxOlustur(Layout layout)
        {
            var wc = new CheckBox();
            if (layout.PostBack) wc.AutoPostBack = true;
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            return wc;
        }

        private WebControl DateTimePickerOlustur(Layout layout)
        {
            var wc = new RadDateTimePicker();
            if (layout.PostBack) wc.AutoPostBack = true;
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.SelectedDate = Convert.ToDateTime(layout.Text);
            KontrolOzellikAyarla(layout, wc);
            return wc;
        }

        private WebControl DropDownListOlustur(Layout layout)
        {
            var wc = new RadDropDownList();
            if (layout.PostBack) wc.AutoPostBack = true;
            //Binding items
            KontrolOzellikAyarla(layout, wc);
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
            if (layout.KontrolAdi == "lblKapiSeri")
                wc.ID = layout.KontrolAdi;
            return wc;
        }

        private WebControl LiteralOlustur(Layout layout)
        {
            if (!string.IsNullOrWhiteSpace(layout.Text))
            {
                return new LiteralWebControl(layout.Text) { ID = KontrolIDGetir(layout) };
            }
            else
                return null;
        }

        private WebControl TextBoxOlustur(Layout layout)
        {
            var wc = new RadTextBox();
            if (!string.IsNullOrWhiteSpace(layout.TextMode)) wc.TextMode = (InputMode)Enum.Parse(typeof(InputMode), layout.TextMode);
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            return wc;
        }

        private WebControl MaskedTextBoxOlustur(Layout layout)
        {
            var wc = new RadMaskedTextBox();
            if (!string.IsNullOrWhiteSpace(layout.Mask)) wc.Mask = layout.Mask;
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
            return wc;
        }

        private WebControl NumericTextBoxOlustur(Layout layout)
        {
            var wc = new RadNumericTextBox();
            if (!string.IsNullOrWhiteSpace(layout.Text)) wc.Text = layout.Text;
            KontrolOzellikAyarla(layout, wc);
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
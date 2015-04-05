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
                    parentKontrolID = layout.KontrolAdi.Replace(" ", string.Empty) + layout.YerlesimTabloID;
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
                case KontrolTipEnum.Br:
                    wc = BrOlustur(layout);
                    break;
            }

            return wc;
        }

        private WebControl BrOlustur(Layout layout)
        {
            var wc = new BrWebControl();
            wc.ID = KontrolIDGetir(layout);
            return wc;
        }

        private WebControl CheckBoxOlustur(Layout layout)
        {
            return null;
        }

        private WebControl DateTimePickerOlustur(Layout layout)
        {
            return null;
        }

        private WebControl DropDownListOlustur(Layout layout)
        {
            return null;
        }

        private WebControl ImageOlustur(Layout layout)
        {
            return null;
        }

        private WebControl LabelOlustur(Layout layout)
        {
            return null;
        }

        private WebControl LiteralOlustur(Layout layout)
        {
            return null;
        }

        private WebControl MaskedTextBoxOlustur(Layout layout)
        {
            return null;
        }

        private WebControl NumericTextBoxOlustur(Layout layout)
        {
            return null;
        }

        private WebControl TableHeaderCellOlustur(Layout layout)
        {
            var wc = new WebControl(HtmlTextWriterTag.Th);
            wc.ID = KontrolIDGetir(layout);
            return wc;
        }

        private WebControl TableCellOlustur(Layout layout)
        {
            var wc = new WebControl(HtmlTextWriterTag.Td);
            wc.ID = KontrolIDGetir(layout);
            return wc;
        }

        private WebControl TableRowOlustur(Layout layout)
        {
            var wc = new WebControl(HtmlTextWriterTag.Tr);
            wc.ID = KontrolIDGetir(layout);
            return wc;
        }

        private WebControl TableOlustur(Layout layout)
        {
            var wc = new WebControl(HtmlTextWriterTag.Table);
            wc.ID = KontrolIDGetir(layout);
            if (layout.Yukseklik != null) wc.Height = new Unit(layout.Yukseklik.Value);
            if (layout.Genislik != null) wc.Width = new Unit(layout.Genislik.Value);
            wc.Enabled = layout.Enabled;
            wc.CssClass = layout.CssClass;
            wc.Attributes.Add("style", layout.Style);
            if (layout.RowSpan != null) wc.Attributes.Add("rowspan", layout.RowSpan.ToString());
            if (layout.ColumnSpan != null) wc.Attributes.Add("columnspan", layout.ColumnSpan.ToString());
            return wc;
        }

        private string KontrolIDGetir(Layout layout)
        {
            return layout.KontrolAdi.Replace(" ", string.Empty) + layout.YerlesimTabloID.ToString();
        }
    }
}
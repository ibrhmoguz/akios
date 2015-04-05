using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KobsisSiparisTakip.Web.Util
{
    public class BrWebControl : WebControl
    {
        public BrWebControl() : base() { }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("br");
            writer.Write(HtmlTextWriter.SelfClosingTagEnd);
        }
    }
}
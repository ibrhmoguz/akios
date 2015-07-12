using System;

//using Helpers.Utils;


namespace Akios.Web.Print
{
    
public partial class PrinterFriendly : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ImageButtonRaporAl_Click(object sender, EventArgs e)
    {
        string pageOrientation = HiddenFieldPageOrientation.Value;
        bool sayfaYatay = false;
        if (HiddenFieldPageOrientation.Value == "Landscape") sayfaYatay = true;

        //if (HiddenFieldInnerHtml.Value != "")
        //  Utils.HtmlToPdfConvert(HiddenFieldInnerHtml.Value, sayfaYatay);
    }
}

}


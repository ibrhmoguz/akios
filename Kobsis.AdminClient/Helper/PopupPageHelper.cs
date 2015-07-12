using System;
using System.Web.UI.WebControls;

namespace Kobsis.Web.Helper
{
    /// <summary>
    /// Herhangi bir kontrolün (button, linkbutton vs) bir popup sayfa açması istendiğinde, o kontrol için gerekli script kodunu otomatik olarak oluşturup sayfaya eklemek için kullanılır
    /// </summary>
    public class PopupPageHelper
    {
        public PopupPageHelper()
        {
            //
        }

        /// <summary>
        /// Herhangi bir kontrolün (button, linkbutton vs) bir popup sayfa açması istendiğinde, o kontrol için gerekli script kodunu otomatik olarak oluşturup sayfaya ekleyen bir methoddur.
        /// </summary>
        /// <param name="pOpener">Popup sayfayı açacak olan kontrol</param>
        /// <param name="pPagePath">Açılacak sayfanın dizini. Örnek: 1.sayfa bir klasör altında ise: Ornek/Ornek.aspx 2.sayfa aynı yerde ise: Ornek.aspx 3.sayfa dışdaki başka bir klasör içinde ise: ../Ornek/Ornek.aspx</param>
        /// <param name="pPageName">Açılacak sayfaya verilen ad. Sayfa adı değiştirilmeyecek ise boş geçilir</param>
        /// <param name="pShowScrollbars">true yapılırsa Scrollbar'lar görünür, false olursa görünmez</param>
        /// <param name="pShowToolbar">true yapılırsa Toolbar (araç çubuğu) görünür, false olursa görünmez</param>
        /// <param name="pShowStatus">true yapılırsa Status (durum çubuğu) görünür, false olursa görünmez</param>
        /// <param name="pShowMenubar">true yapılırsa Menubar (menü çubuğu) görünür, false olursa görünmez</param>
        /// <param name="pShowLocator">true yapılırsa url giriş alanı görünür, false yapılırsa görünmez</param>
        /// <param name="pResizable">true yapılırsa sayfa boyutu değiştirilebilir, false olursa değiştirilemez</param>
        /// <param name="pWidth">Açılacak sayfanın px tipinden genişliği</param>
        /// <param name="pHeight">Açılacak sayfanın px tipinden yüksekliği</param>
        /// <param name="pCenterScreen">true yapılırsa, sayfa ekranın ortasında, false olursa ekranın herhangi bir yerinde görüntülenir</param>
        /// <param name="pFullScreen">true yapılırsa, sayfa tüm ekranı kaplar, false yapılırsa verilen boyutlarda açılır</param>
        /// <param name="pEvent">Sayfayı açacak olan kontrolün hangi event'de bu işi yapacağı verilir. Örneğin buton kliklendiğinde sayfa açılacak ise event "onclick" olacaktır.</param>
        public static void OpenPopUp(WebControl pOpener, string pPagePath, string pPageName,
            bool pShowScrollbars,
            bool pShowToolbar,
            bool pShowStatus,
            bool pShowMenubar,
            bool pShowLocator,
            bool pResizable,
            int pWidth,
            int pHeight,
            bool pCenterScreen,
            bool pFullScreen,
            string pEvent)
        {
            try
            {
                string str = "";

                str = "window.open('";
                str = str + pPagePath + "','";
                str = str + pPageName + "','";
                str = str + "scrollbars=" + Math.Abs(Convert.ToInt16(pShowScrollbars)).ToString() + ",";
                str = str + "toolbar=" + Math.Abs(Convert.ToInt16(pShowToolbar)).ToString() + ",";
                str = str + "status=" + Math.Abs(Convert.ToInt16(pShowStatus)).ToString() + ",";
                str = str + "menubar=" + Math.Abs(Convert.ToInt16(pShowMenubar)).ToString() + ",";
                str = str + "location=" + Math.Abs(Convert.ToInt16(pShowLocator)).ToString() + ",";
                str = str + "resizable=" + Math.Abs(Convert.ToInt16(pResizable)).ToString() + ",";
                str = str + "width=" + pWidth + ",";
                str = str + "height=" + pHeight;
                if (pCenterScreen)
                {
                    str = str + "," + "left='+((screen.width-" + pWidth + ")/2)+'," + "top='+((screen.height-" + pHeight + ")/2)+'";
                }


                if (pFullScreen) str = str + "," + "fullscreen=" + pFullScreen + ",";
                str = str + "');";

                str += "return false;";
                pOpener.Attributes.Add(pEvent, str);

            }
            catch (Exception)
            {
                //
            }
        }

        /// <summary>
        /// Herhangi bir kontrolün (button, linkbutton vs) bir popup sayfa açması istendiğinde, o kontrol için gerekli script kodunu otomatik olarak oluşturup sayfaya ekleyen bir methoddur. Postback yapmaz.
        /// </summary>
        /// <param name="pOpener">Popup sayfayı açacak olan kontrol</param>
        /// <param name="pPagePath">Açılacak sayfanın dizini. Örnek: 1.sayfa bir klasör altında ise: Ornek/Ornek.aspx 2.sayfa aynı yerde ise: Ornek.aspx 3.sayfa dışdaki başka bir klasör içinde ise: ../Ornek/Ornek.aspx</param>
        /// <param name="pPageName">Açılacak sayfaya verilen ad. Sayfa adı değiştirilmeyecek ise boş geçilir</param>
        /// <param name="pShowScrollbars">true yapılırsa Scrollbar'lar görünür, false olursa görünmez</param>
        /// <param name="pShowToolbar">true yapılırsa Toolbar (araç çubuğu) görünür, false olursa görünmez</param>
        /// <param name="pShowStatus">true yapılırsa Status (durum çubuğu) görünür, false olursa görünmez</param>
        /// <param name="pShowMenubar">true yapılırsa Menubar (menü çubuğu) görünür, false olursa görünmez</param>
        /// <param name="pShowLocator">true yapılırsa url giriş alanı görünür, false yapılırsa görünmez</param>
        /// <param name="pResizable">true yapılırsa sayfa boyutu değiştirilebilir, false olursa değiştirilemez</param>
        /// <param name="pWidth">Açılacak sayfanın px tipinden genişliği</param>
        /// <param name="pHeight">Açılacak sayfanın px tipinden yüksekliği</param>
        /// <param name="pCenterScreen">true yapılırsa, sayfa ekranın ortasında, false olursa ekranın herhangi bir yerinde görüntülenir</param>
        /// <param name="pFullScreen">true yapılırsa, sayfa tüm ekranı kaplar, false yapılırsa verilen boyutlarda açılır</param>
        /// <param name="pEvent">Sayfayı açacak olan kontrolün hangi event'de bu işi yapacağı verilir. Örneğin buton kliklendiğinde sayfa açılacak ise event "onclick" olacaktır.</param>
        /// <param name="pDoPostback">Verilen event gerçekleştikten sonra eğer kontrolün postback özelliği varsa sayfanın postback olmasını veya olmamasını sağlar</param>
        public static void OpenPopUp(WebControl pOpener, string pPagePath, string pPageName,
            bool pShowScrollbars,
            bool pShowToolbar,
            bool pShowStatus,
            bool pShowMenubar,
            bool pShowLocator,
            bool pResizable,
            int pWidth,
            int pHeight,
            bool pCenterScreen,
            bool pFullScreen,
            string pEvent,
            bool pDoPostback)
        {
            try
            {
                string str = "";

                str = "window.open('";
                str = str + pPagePath + "','";
                str = str + pPageName + "','";
                str = str + "scrollbars=" + Math.Abs(Convert.ToInt16(pShowScrollbars)).ToString() + ",";
                str = str + "toolbar=" + Math.Abs(Convert.ToInt16(pShowToolbar)).ToString() + ",";
                str = str + "status=" + Math.Abs(Convert.ToInt16(pShowStatus)).ToString() + ",";
                str = str + "menubar=" + Math.Abs(Convert.ToInt16(pShowMenubar)).ToString() + ",";
                str = str + "location=" + Math.Abs(Convert.ToInt16(pShowLocator)).ToString() + ",";
                str = str + "resizable=" + Math.Abs(Convert.ToInt16(pResizable)).ToString() + ",";
                str = str + "width=" + pWidth + ",";
                str = str + "height=" + pHeight;
                if (pCenterScreen)
                {
                    str = str + "," + "left='+((screen.width-" + pWidth + ")/2)+'," + "top='+((screen.height-" + pHeight + ")/2)+'";
                }


                if (pFullScreen) str = str + "," + "fullscreen=" + pFullScreen + ",";
                str = str + "');";

                if (!pDoPostback)
                    str += "return false;";

                pOpener.Attributes.Add(pEvent, str);

            }
            catch (Exception)
            {
                //
            }
        }
    }
}

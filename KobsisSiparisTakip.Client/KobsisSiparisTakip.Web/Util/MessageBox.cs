using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

namespace KobsisSiparisTakip.Web.Util
{
    public static class MessageBox
    {
        private static void Show(Page page, string message, string title, string icon)
        {
            RadNotification messageBox;

            if (page.Master == null)
            {
                messageBox = page.FindControl("RadNotificationACKMaster") as RadNotification;
            }
            else
                messageBox = page.Master.FindControl("RadNotificationACKMaster") as RadNotification;

            messageBox.Title = title;
            messageBox.ContentIcon = icon;
            messageBox.Show(message);
        }

        public static void Bilgi(Page page, string message)
        {
            Show(page, message, "Bilgi Mesajı", "info");
        }

        public static void Basari(Page page, string message)
        {
            Show(page, message, "İşlem Mesajı", "ok");
        }

        public static void Hata(Page page, string message)
        {
            Show(page, message, "Hata Mesajı", "delete");
        }

        public static void Uyari(Page page, string message)
        {
            Show(page, message, "Uyarı Mesajı", "warning");
        }
    }
}
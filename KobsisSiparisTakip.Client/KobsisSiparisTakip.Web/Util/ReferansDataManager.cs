using KobsisSiparisTakip.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KobsisSiparisTakip.Web.Util
{
    public class ReferansDataManager
    {
        public string KapiSeri { get; set; }

        public void ReferansVerileriniYukle()
        {
            if (SessionManager.ReferansData != null)
                return;

            SessionManager.ReferansData = new ReferansDataBS().ReferansVerileriniGetir(SessionManager.MusteriId, this.KapiSeri);
        }

        public DataTable ReferansVerisiGetir(string refID)
        {
            if (SessionManager.ReferansData == null || SessionManager.ReferansData.Rows.Count == 0)
                ReferansVerileriniYukle();

            if (SessionManager.ReferansData != null)
            {
                DataRow[] rows = SessionManager.ReferansData.Select("RefID=" + refID + " AND KapiSeriID=" + this.KapiSeri);
                if (rows != null && rows.Count() > 0)
                    return rows.CopyToDataTable();
                else
                    return null;
            }
            else
                return null;
        }
    }
}
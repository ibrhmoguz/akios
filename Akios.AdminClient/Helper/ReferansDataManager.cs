using System.Data;
using System.Linq;
using Akios.Business;
using Akios.Util;

namespace Akios.AdminWebClient.Helper
{
    public class ReferansDataManager
    {
        public string SiparisSeri { get; set; }

        public string RefID { get; set; }

        public void ReferansVerileriniYukle()
        {
            SessionManager.ReferansData = new ReferansDataBS().ReferansVerileriniGetir(SessionManager.KullaniciBilgi.MusteriID.Value, this.SiparisSeri);
        }

        public DataTable ReferansVerisiGetir()
        {
            if (SessionManager.ReferansData == null || SessionManager.ReferansData.Rows.Count == 0)
                ReferansVerileriniYukle();

            if (SessionManager.ReferansData != null)
            {
                DataRow[] rows = SessionManager.ReferansData.Select("RefID=" + this.RefID + " AND SiparisSeriID=" + this.SiparisSeri);
                if (rows != null && rows.Count() > 0)
                    return rows.CopyToDataTable();
                else
                    return null;
            }
            else
                return null;
        }

        public static DataTable MusteriReferanslariYukle()
        {
            DataTable dt = SessionManager.MusteriReferansDegerleri;

            if (dt == null || dt.Rows.Count == 0)
            {
                dt = new ReferansDataBS().MusteriReferanslariniGetir(SessionManager.MusteriBilgi.MusteriID.Value);
                SessionManager.MusteriReferansDegerleri = dt;
            }

            return dt;
        }
    }
}
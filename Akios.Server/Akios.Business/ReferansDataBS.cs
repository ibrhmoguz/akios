using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Akios.DataType;
using Akios.Util;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;

namespace Akios.Business
{
    [ServiceConnectionName("AkiosConnectionString")]
    public class ReferansDataBS : BusinessBase
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
                if (rows.Any())
                    return rows.CopyToDataTable();

                return null;
            }

            return null;
        }

        public DataTable ReferansVerileriniGetir(int musteriID, string siparisSeri)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT
	                            R.MusteriID
	                            ,R.RefID
	                            ,RD.RefDetayID
	                            ,RDK.SiparisSeriID
	                            ,R.RefAdi
	                            ,RD.RefDetayAdi
                            FROM dbo.REF AS R
	                            INNER JOIN dbo.REF_DETAY AS RD ON R.RefID=RD.RefID
	                            INNER JOIN dbo.REF_DETAY_SIPARIS_SERI AS RDK ON RDK.RefDetayID=RD.RefDetayID
                            WHERE R.MusteriID=@MusteriID
                            ORDER BY R.MusteriID,R.RefID,RD.RefDetayID,RDK.SiparisSeriID";
            data.AddSqlParameter("SiparisSeriID", siparisSeri, SqlDbType.VarChar, 50);
            data.AddSqlParameter("MusteriID", musteriID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable MusteriReferanslariniGetir(int musteriId)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT
	                            RefID
	                            ,RefAdi
                            FROM dbo.REF
                            WHERE MusteriID=@MusteriID
                            ORDER BY MusteriID,RefID";
            data.AddSqlParameter("MusteriID", musteriId, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable IlleriGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT IlKod,IlAd FROM REF_IL ORDER BY 2";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable IlceleriGetir(string ilKodu)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.AddSqlParameter("IlKod", ilKodu, SqlDbType.Int, 50);

            string sqlText = @"SELECT * FROM REF_ILCE WHERE IlKod=@IlKod ORDER BY IlceAd";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable SemtleriGetir(string ilceKod)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.AddSqlParameter("IlceKod", ilceKod, SqlDbType.Int, 50);

            string sqlText = @"SELECT * FROM REF_SEMT WHERE IlceKod=@IlceKod ORDER BY SemtAd";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable KullaniciRolleriGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT RolID, RolAdi FROM KULLANICI_ROL ORDER BY 1";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public List<SiparisSeri> SeriGetirMusteriIdGore(int pMusteriId)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT * FROM SIPARIS_SERI WHERE MusteriID=@MusteriID";

            data.AddSqlParameter("MusteriID", pMusteriId, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);

            var seriList = new List<SiparisSeri>();
            foreach (DataRow row in dt.Rows)
            {
                var seri = new SiparisSeri();
                if (row["SiparisSeriID"] != DBNull.Value)
                    seri.SiparisSeriID = Convert.ToInt32(row["SiparisSeriID"]);
                seri.SeriAdi = row["SeriAdi"].ToString();
                if (row["SeriKodu"] != DBNull.Value)
                    seri.SeriKodu = row["SeriKodu"].ToString();
                seriList.Add(seri);
            }

            return seriList;
        }

        public List<MusteriRapor> MusteriRaporlariniGetir(int pMusteriId)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT
	                                R.*
                                FROM dbo.RAPOR AS R
	                                INNER JOIN dbo.MUSTERI_RAPOR AS MR ON MR.RaporID=R.RaporID
                                WHERE MR.MusteriID=@MusteriID
                                ORDER BY MR.SiraNo, R.RaporAdi";

            data.AddSqlParameter("MusteriID", pMusteriId, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);

            var raporList = new List<MusteriRapor>();
            foreach (DataRow row in dt.Rows)
            {
                var musteriRapor = new MusteriRapor();
                if (row["RaporID"] != DBNull.Value)
                    musteriRapor.RaporId = row["RaporID"].ToString();
                if (row["RaporAdi"] != DBNull.Value)
                    musteriRapor.RaporAdi = row["RaporAdi"].ToString();
                if (row["RaporMenuBaslik"] != DBNull.Value)
                    musteriRapor.RaporMenuBaslik = row["RaporMenuBaslik"].ToString();
                if (row["Dizin"] != DBNull.Value)
                    musteriRapor.Dizin = row["Dizin"].ToString();
                if (row["IkonImajID"] != DBNull.Value)
                    musteriRapor.IkonImajId = row["IkonImajID"].ToString();

                raporList.Add(musteriRapor);
            }

            return raporList;
        }
    }
}

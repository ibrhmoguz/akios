using System;
using System.Data;
using Kobsis.DataType;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;

namespace Kobsis.Business
{
    [ServiceConnectionName("KobsisConnectionString")]
    public class MusteriBS : BusinessBase
    {
        public Musteri MusteriBilgiGetirKullaniciAdinaGore(int pKullaniciID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT 
	                                M.*
                                FROM MUSTERI AS M
	                                INNER JOIN KULLANICI AS K ON K.MusteriID=M.MusteriID
                                WHERE K.KullaniciID=@KullaniciID";

            data.AddSqlParameter("KullaniciID", pKullaniciID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);

            return ConvertToMusteri(dt);
        }

        public Musteri MusteriBilgiGetirMusteriIDGore(int pMusteriID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT * FROM MUSTERI WHERE MusteriID=@MusteriID";

            data.AddSqlParameter("MusteriID", pMusteriID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);

            return ConvertToMusteri(dt);
        }

        private Musteri ConvertToMusteri(DataTable dt)
        {
            Musteri m = new Musteri();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row["MusteriID"] != DBNull.Value)
                    m.MusteriID = Convert.ToInt32(row["MusteriID"].ToString());
                if (row["Kod"] != DBNull.Value)
                    m.Kod = row["Kod"].ToString();
                if (row["YetkiliKisi"] != DBNull.Value)
                    m.YetkiliKisi = row["YetkiliKisi"].ToString();
                if (row["Adi"] != DBNull.Value)
                    m.Adi = row["Adi"].ToString();
                if (row["Adres"] != DBNull.Value)
                    m.Adres = row["Adres"].ToString();
                if (row["Tel"] != DBNull.Value)
                    m.Tel = row["Tel"].ToString();
                if (row["Mobil"] != DBNull.Value)
                    m.Mobil = row["Mobil"].ToString();
                if (row["Faks"] != DBNull.Value)
                    m.Faks = row["Faks"].ToString();
                if (row["Web"] != DBNull.Value)
                    m.Web = row["Web"].ToString();
                if (row["Mail"] != DBNull.Value)
                    m.Mail = row["Mail"].ToString();
                if (row["LogoID"] != DBNull.Value)
                    m.LogoID = Convert.ToInt32(row["LogoID"].ToString().ToString());
            }
            return m;
        }

        public DataTable MusteriListesiGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT TOP 500 * FROM MUSTERI";
            data.GetRecords(dt, sqlText);

            return dt;
        }
    }
}

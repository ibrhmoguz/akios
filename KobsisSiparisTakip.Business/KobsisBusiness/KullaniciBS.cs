using System;
using System.Collections.Generic;
using System.Data;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;
using WebFrame.DataType.Common.Logging;

namespace KobsisSiparisTakip.Business
{
    [ServiceConnectionNameAttribute("KobsisConnectionString")]
    public class KullaniciBS : BusinessBase
    {
        public DataTable KullaniciBilgisiGetir(Dictionary<string, object> prms)
        {
            return pKullaniciBilgisiGetir(prms);
        }

        private DataTable pKullaniciBilgisiGetir(Dictionary<string, object> prms)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT * FROM KULLANICIBILGI WHERE KULLANICIADI=@KULLANICIADI and SIFRE=@SIFRE";

            data.AddSqlParameter("KULLANICIADI", prms["KULLANICIADI"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("SIFRE", prms["SIFRE"], SqlDbType.VarChar, 50);
            data.GetRecords(dt, sqlText);

            return dt;
        }

        public DataTable KullanicilariGetir()
        {
            return pKullanicilariGetir();

        }

        private DataTable pKullanicilariGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT KULLANICIADI, YETKI FROM KULLANICIBILGI ORDER BY 1";
            data.GetRecords(dt, sqlText);
            return dt;
        }
        public bool KullaniciTanimla(Dictionary<string, object> prms)
        {
            return pKullaniciTanimla(prms);
        }

        private bool pKullaniciTanimla(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();

                data.AddSqlParameter("KULLANICIADI", prms["KULLANICIADI"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("YETKI", prms["YETKI"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("SIFRE", prms["SIFRE"], SqlDbType.VarChar, 50);

                string sqlKaydet = @"INSERT INTO KULLANICIBILGI (KULLANICIADI,SIFRE,YETKI) VALUES (@KULLANICIADI,@SIFRE,@YETKI)";
                data.ExecuteStatement(sqlKaydet);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "KullaniciTanimla", "", null);
                return false;
            }
        }

        public bool KullaniciSil(Dictionary<string, object> prms)
        {
            return pKullaniciSil(prms);
        }

        private bool pKullaniciSil(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();

                data.AddSqlParameter("KULLANICIADI", prms["KULLANICIADI"], SqlDbType.VarChar, 50);
                string sqlSil = @"DELETE FROM KULLANICIBILGI WHERE KULLANICIADI=@KULLANICIADI";
                data.ExecuteStatement(sqlSil);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "KullaniciSil", "", null);
                return false;
            }
        }

        public DataTable KullaniciSifreBilgisiGetir(Dictionary<string, object> prms)
        {
            return pKullaniciSifreBilgisiGetir(prms);
        }

        private DataTable pKullaniciSifreBilgisiGetir(Dictionary<string, object> prms)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.AddSqlParameter("KULLANICIADI", prms["KULLANICIADI"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("YETKI", prms["YETKI"], SqlDbType.VarChar, 50);
            string sqlText = @"SELECT KULLANICIADI, SIFRE FROM KULLANICIBILGI WHERE KULLANICIADI=@KULLANICIADI and YETKI=@YETKI";
            data.GetRecords(dt, sqlText);

            return dt;
        }

        public bool KullaniciSifreGuncelle(Dictionary<string, object> prms)
        {
            return pKullaniciSifreGuncelle(prms);
        }

        private bool pKullaniciSifreGuncelle(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();

                data.AddSqlParameter("KULLANICIADI", prms["KULLANICIADI"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("YETKI", prms["YETKI"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("SIFRE", prms["SIFRE"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("YENISIFRE", prms["YENISIFRE"], SqlDbType.VarChar, 50);
                string sqlGuncelle = @"UPDATE KULLANICIBILGI SET SIFRE=@YENISIFRE WHERE KULLANICIADI=@KULLANICIADI and SIFRE=@SIFRE and YETKI=@YETKI";

                data.ExecuteStatement(sqlGuncelle);
                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "KullaniciSifreGuncelle", "", null);
                return false;
            }
        }
    }
}

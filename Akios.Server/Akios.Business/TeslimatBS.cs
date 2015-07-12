using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Akios.Util;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;
using WebFrame.DataType.Common.Logging;

namespace Akios.Business
{
    [ServiceConnectionName("KobsisConnectionString")]
    public class TeslimatBS : BusinessBase
    {
        public DataTable TeslimatlariListele(DateTime dtBaslangic, DateTime dtBitis)
        {
            IData data = GetDataObject();
            DataTable dt = new DataTable();

            data.AddSqlParameter("BasTar", dtBaslangic, SqlDbType.DateTime, 50);
            data.AddSqlParameter("BitTar", dtBitis, SqlDbType.DateTime, 50);

            string sqlKaydet = @"SELECT
	                                M.ID
                                    , M.TeslimTarih
                                    , S.ID AS SiparisID
                                    , S.SeriID
	                                , S.SiparisNo
                                    , S.Adet
	                                , S.MusteriAd + ' ' + S.MusteriSoyad AS Musteri
	                                , RILCE.ILCEAD +'/' + RIL.ILAD AS IlIlce
	                                , S.MusteriAdres AS Adres
	                                , S.MusteriCepTel AS Tel
	                                , dbo.TESLIMAT_EKIP_LISTESI(M.ID) AS Personel
                                    , dbo.TESLIMAT_EKIP_ID_LISTESI(M.ID) AS PersonelID
                                    , M.Durum
                                FROM TESLIMAT AS M
	                                INNER JOIN SIPARIS_ABC as S ON M.SiparisID = S.ID
	                                INNER JOIN REF_IL AS RIL ON RIL.IlKod = S.MusteriIlKod
	                                INNER JOIN REF_ILCE AS RILCE ON RILCE.IlceKod = S.MusteriIlceKod
                                WHERE M.TeslimTarih >=@BasTar AND M.TeslimTarih <=@BitTar
                                ORDER BY M.TeslimTarih, S.SiparisNo";
            data.GetRecords(dt, sqlKaydet);
            return dt;
        }

        public bool TeslimatGuncelle(string teslimatId, DateTime teslimTarihi, List<string> personelListesi, string teslimatDurumu, string updatedBy, DateTime updatedTime, string musteriKod)
        {
            IData data = GetDataObject();

            try
            {
                data.BeginTransaction();

                //Teslimat tarihini guncelle
                data.AddSqlParameter("ID", teslimatId, SqlDbType.Int, 50);
                data.AddSqlParameter("TeslimTarih", teslimTarihi, SqlDbType.DateTime, 50);
                data.AddSqlParameter("Durum", teslimatDurumu, SqlDbType.VarChar, 50);
                data.AddSqlParameter("UpdatedBy", updatedBy, SqlDbType.VarChar, 50);
                data.AddSqlParameter("UpdatedTime", updatedTime, SqlDbType.DateTime, 50);

                string sqlUpdate = @"UPDATE TESLIMAT 
                                      SET TeslimTarih=@TeslimTarih
                                          ,Durum=@Durum
                                          ,UpdatedBy=@UpdatedBy
                                          ,UpdatedTime=@UpdatedTime
                                      WHERE ID=@ID";
                data.ExecuteStatement(sqlUpdate);

                int siparisDurumu = teslimatDurumu == "A" ? 2 : 3;
                data.AddSqlParameter("ID", teslimatId, SqlDbType.Int, 50);
                data.AddSqlParameter("DurumID", siparisDurumu, SqlDbType.Int, 50);
                sqlUpdate = @"UPDATE SIPARIS_{0} 
                              SET DurumID=@DurumID 
                              WHERE ID=(SELECT SiparisId FROM TESLIMAT WHERE ID=@ID)";
                data.ExecuteStatement(string.Format(sqlUpdate, musteriKod));

                if (personelListesi.Count > 0)
                {
                    //Teslimat personelini sil
                    data.AddSqlParameter("ID", teslimatId, SqlDbType.Int, 50);
                    string sqlSil = @"DELETE FROM TESLIMAT_PERSONEL WHERE TeslimatID=@ID";
                    data.ExecuteStatement(sqlSil);

                    //Teslimat personeli ekle
                    string sqlInsert = @"INSERT INTO [dbo].[TESLIMAT_PERSONEL] ([TeslimatID],[PersonelID]) VALUES ({0} ,{1}); ";
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in personelListesi)
                    {
                        sb.Append(String.Format(sqlInsert, teslimatId, item));
                    }
                    data.ExecuteStatement(sb.ToString());
                }

                data.CommitTransaction();
                return true;
            }
            catch (Exception exc)
            {
                data.RollbackTransaction();
                new LogWriter().Write(AppModules.IsTakvimi, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "TeslimatGuncelle", SessionManager.KullaniciBilgi.KullaniciAdi, null);
                return false;
            }
        }

        public DataTable TeslimatBilgisiGetir(string siparisID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.AddSqlParameter("SiparisID", siparisID, SqlDbType.VarChar, 50);

            string sqlText = @"SELECT * FROM TESLIMAT WHERE SiparisID=@SiparisID";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable TeslimatKotaListele(int pMusteriID)
        {
            IData data = GetDataObject();
            DataTable dt = new DataTable();

            string sqlKaydet = @"SELECT 
                                     ROW_NUMBER() OVER(ORDER BY TeslimatTarihi DESC) AS ID
                                      , ID AS TeslimatKotaID
                                      ,CONVERT(VARCHAR(10), TeslimatTarihi,104) AS TESLIMATTARIHI
                                      ,MaxTeslimatSayi AS MAXTESLIMATSAYI
                                      ,CASE WHEN TeslimatKabul = 0 THEN 'KAPALI' ELSE 'AÇIK' END AS TESLIMATKABUL
                                 FROM dbo.TESLIMATKOTA
                                 WHERE MusteriID=@MusteriID
                                 ORDER BY TeslimatTarihi DESC, MaxTeslimatSayi, TeslimatKabul";
            data.AddSqlParameter("MusteriID", pMusteriID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlKaydet);
            return dt;
        }

        public bool TeslimatKotaKaydet(DateTime dtTeslimat, int teslimatKota, bool teslimatKabul, int pMusteriID)
        {
            DataTable dtKotaToplam = GunlukTeslimatKotaBilgisiGetir(dtTeslimat);
            if (dtKotaToplam.Rows.Count > 0)
                return false;

            IData data = GetDataObject();

            data.AddSqlParameter("MusteriID", pMusteriID, SqlDbType.Int, 50);
            data.AddSqlParameter("TeslimatTarihi", dtTeslimat, SqlDbType.DateTime, 50);
            data.AddSqlParameter("TeslimatKota", teslimatKota, SqlDbType.Int, 50);
            data.AddSqlParameter("TeslimatKabul", teslimatKabul, SqlDbType.Bit, 50);

            string sqlInsert = @"INSERT INTO dbo.TESLIMATKOTA
                                               (MusteriID
                                               ,TeslimatTarihi
                                               ,MaxTeslimatSayi
                                               ,TeslimatKabul)
                                         VALUES(@MusteriID, @TeslimatTarihi,@TeslimatKota,@TeslimatKabul)";
            data.ExecuteStatement(sqlInsert);

            return true;
        }

        public int GunlukTeslimatSayisiniGetir(DateTime dt)
        {
            IData data = GetDataObject();
            data.AddSqlParameter("TeslimTarih", dt.ToShortDateString(), SqlDbType.DateTime, 50);

            string sqlInsert = @"SELECT COUNT(*) AS SAYI
                                  FROM dbo.TESLIMAT
                                  WHERE TeslimTarih = @TeslimTarih";

            return Convert.ToInt32(data.ExecuteScalar(sqlInsert, CommandType.Text));
        }

        public DataTable GunlukTeslimatKotaBilgisiGetir(DateTime dtTeslimatTarihi)
        {
            IData data = GetDataObject();
            DataTable dt = new DataTable();

            data.AddSqlParameter("TeslimatTarihi", dtTeslimatTarihi.ToShortDateString(), SqlDbType.DateTime, 50);
            string sqlInsert = @"SELECT 
                                  ID
                                  ,TeslimatTarihi
                                  ,MaxTeslimatSayi
                                  ,TeslimatKabul
                              FROM dbo.TESLIMATKOTA
                              WHERE TeslimatTarihi = @TeslimatTarihi";

            data.GetRecords(dt, sqlInsert);
            return dt;
        }

        public bool TeslimatKotaSil(string p)
        {
            IData data = GetDataObject();
            data.AddSqlParameter("ID", p, SqlDbType.Int, 50);

            string sqlInsert = @"DELETE FROM dbo.TESLIMATKOTA
                                 WHERE ID= @ID";

            data.ExecuteStatement(sqlInsert);
            return true;
        }
    }
}

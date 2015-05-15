using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;
using WebFrame.DataType.Common.Logging;

namespace KobsisSiparisTakip.Business
{
    [ServiceConnectionNameAttribute("KobsisConnectionString")]
    public class MontajBS : BusinessBase
    {
        public DataTable MontajlariListele(DateTime dtBaslangic, DateTime dtBitis)
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
	                                , dbo.MONTAJ_EKIP_LISTESI(M.ID) AS Personel
                                    , dbo.MONTAJ_EKIP_ID_LISTESI(M.ID) AS PersonelID
                                    , M.Durum
                                FROM MONTAJ AS M
	                                INNER JOIN SIPARIS_ABC as S ON M.SiparisID = S.ID
	                                INNER JOIN REF_IL AS RIL ON RIL.IlKod = S.MusteriIlKod
	                                INNER JOIN REF_ILCE AS RILCE ON RILCE.IlceKod = S.MusteriIlceKod
                                WHERE M.TeslimTarih >=@BasTar AND M.TeslimTarih <=@BitTar
                                ORDER BY M.TeslimTarih, S.SiparisNo";
            data.GetRecords(dt, sqlKaydet);
            return dt;
        }

        public bool MontajGuncelle(string montajID, DateTime teslimTarihi, List<string> personelListesi, string montajDurumu, string updatedBy, DateTime updatedTime)
        {
            IData data = GetDataObject();

            try
            {
                data.BeginTransaction();

                //Montaj tarihini guncelle
                data.AddSqlParameter("ID", montajID, SqlDbType.Int, 50);
                data.AddSqlParameter("TeslimTarih", teslimTarihi, SqlDbType.DateTime, 50);
                data.AddSqlParameter("Durum", montajDurumu, SqlDbType.VarChar, 50);
                data.AddSqlParameter("UpdatedBy", updatedBy, SqlDbType.VarChar, 50);
                data.AddSqlParameter("UpdatedTime", updatedTime, SqlDbType.DateTime, 50);

                string sqlUpdate = @"UPDATE MONTAJ 
                                      SET TeslimTarih=@TeslimTarih
                                          ,Durum=@Durum
                                          ,UpdatedBy=@UpdatedBy
                                          ,UpdatedTime=@UpdatedTime
                                      WHERE ID=@ID";
                data.ExecuteStatement(sqlUpdate);

                int siparisDurumu = montajDurumu == "A" ? 2 : 3;
                data.AddSqlParameter("ID", montajID, SqlDbType.Int, 50);
                data.AddSqlParameter("Durum", siparisDurumu, SqlDbType.Int, 50);
                sqlUpdate = @"UPDATE SIPARIS 
                              SET DurumID=@DurumID 
                              WHERE ID=(SELECT SIPARISID FROM MONTAJ WHERE ID=@ID)";
                data.ExecuteStatement(sqlUpdate);

                if (personelListesi.Count > 0)
                {
                    //Montaj personelini sil
                    data.AddSqlParameter("ID", montajID, SqlDbType.Int, 50);
                    string sqlSil = @"DELETE FROM MONTAJ_PERSONEL WHERE MontajID=@ID";
                    data.ExecuteStatement(sqlSil);

                    //Montaj personeli ekle
                    string sqlInsert = @"INSERT INTO [dbo].[MONTAJ_PERSONEL] ([MontajID],[PersonelID]) VALUES ({0} ,{1}); ";
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in personelListesi)
                    {
                        sb.Append(String.Format(sqlInsert, montajID, item));
                    }
                    data.ExecuteStatement(sb.ToString());
                }

                data.CommitTransaction();
                return true;
            }
            catch (Exception exc)
            {
                data.RollbackTransaction();
                new LogWriter().Write(AppModules.IsTakvimi, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "MontajGuncelle", "", null);
                return false;
            }
        }

        public DataTable MontajBilgisiGetir(string siparisID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.AddSqlParameter("SiparisID", siparisID, SqlDbType.VarChar, 50);

            string sqlText = @"SELECT * FROM MONTAJ WHERE SiparisID=@SiparisID";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable MontajKotaListele(int pMusteriID)
        {
            IData data = GetDataObject();
            DataTable dt = new DataTable();

            string sqlKaydet = @"SELECT 
                                     ROW_NUMBER() OVER(ORDER BY MontajTarihi DESC) AS ID
                                      , ID AS MontajKotaID
                                      ,CONVERT(VARCHAR(10), MontajTarihi,104) AS MontajTarihi
                                      ,MaxMontajSayi
                                      ,CASE WHEN MontajKabul = 0 THEN 'KAPALI' ELSE 'AÇIK' END AS MontajKabul
                                 FROM dbo.MONTAJKOTA
                                 WHERE MusteriID=@MusteriID
                                 ORDER BY MontajTarihi DESC, MaxMontajSayi, MontajKabul";
            data.AddSqlParameter("MusteriID", pMusteriID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlKaydet);
            return dt;
        }

        public bool MontajKotaKaydet(DateTime dtMontaj, int montajKota, bool montajKabul, int pMusteriID)
        {
            DataTable dtKotaToplam = GunlukMontajKotaBilgisiGetir(dtMontaj);
            if (dtKotaToplam.Rows.Count > 0)
                return false;

            IData data = GetDataObject();

            data.AddSqlParameter("MusteriID", pMusteriID, SqlDbType.Int, 50);
            data.AddSqlParameter("MontajTarihi", dtMontaj, SqlDbType.DateTime, 50);
            data.AddSqlParameter("MontajKota", montajKota, SqlDbType.Int, 50);
            data.AddSqlParameter("MontajKabul", montajKabul, SqlDbType.Bit, 50);

            string sqlInsert = @"INSERT INTO dbo.MONTAJKOTA
                                               (MusteriID
                                               ,MontajTarihi
                                               ,MaxMontajSayi
                                               ,MontajKabul)
                                         VALUES(@MusteriID, @MontajTarihi,@MontajKota,@MontajKabul)";
            data.ExecuteStatement(sqlInsert);

            return true;
        }

        public int GunlukMontajSayisiniGetir(DateTime dt)
        {
            IData data = GetDataObject();
            data.AddSqlParameter("TeslimTarih", dt.ToShortDateString(), SqlDbType.DateTime, 50);

            string sqlInsert = @"SELECT COUNT(*) AS SAYI
                                  FROM dbo.MONTAJ
                                  WHERE TeslimTarih = @TeslimTarih";

            return Convert.ToInt32(data.ExecuteScalar(sqlInsert, CommandType.Text));
        }

        public DataTable GunlukMontajKotaBilgisiGetir(DateTime dtMontajTarihi)
        {
            IData data = GetDataObject();
            DataTable dt = new DataTable();

            data.AddSqlParameter("MontajTarihi", dtMontajTarihi.ToShortDateString(), SqlDbType.DateTime, 50);
            string sqlInsert = @"SELECT 
                                  ID
                                  ,MontajTarihi
                                  ,MaxMontajSayi
                                  ,MontajKabul
                              FROM dbo.MONTAJKOTA
                              WHERE MontajTarihi = @MontajTarihi";

            data.GetRecords(dt, sqlInsert);
            return dt;
        }

        public bool MontajKotaSil(string p)
        {
            IData data = GetDataObject();
            data.AddSqlParameter("ID", p, SqlDbType.Int, 50);

            string sqlInsert = @"DELETE FROM dbo.MONTAJKOTA
                                 WHERE ID= @ID";

            data.ExecuteStatement(sqlInsert);
            return true;
        }
    }
}

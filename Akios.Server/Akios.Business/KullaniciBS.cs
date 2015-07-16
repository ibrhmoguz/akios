using System;
using System.Collections.Generic;
using System.Data;
using Akios.DataType;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;

namespace Akios.Business
{
    [ServiceConnectionName("AkiosConnectionString")]
    public class KullaniciBS : BusinessBase
    {
        public Kullanici KullaniciBilgisiGetir(string pKullaniciAdi, string pSifre)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT * FROM KULLANICI WHERE KullaniciAdi=@KullaniciAdi and Sifre=@Sifre";

            data.AddSqlParameter("KullaniciAdi", pKullaniciAdi, SqlDbType.VarChar, 50);
            data.AddSqlParameter("Sifre", pSifre, SqlDbType.VarChar, 50);
            data.GetRecords(dt, sqlText);

            Kullanici k = new Kullanici();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                if (row["KullaniciID"] != DBNull.Value)
                    k.KullaniciID = Convert.ToInt32(row["KullaniciID"].ToString());
                if (row["Sifre"] != DBNull.Value)
                    k.Sifre = row["Sifre"].ToString();
                if (row["MusteriID"] != DBNull.Value)
                    k.MusteriID = Convert.ToInt32(row["MusteriID"].ToString());
                if (row["KullaniciAdi"] != DBNull.Value)
                    k.KullaniciAdi = row["KullaniciAdi"].ToString();
                if (row["RolID"] != DBNull.Value)
                {
                    k.RolID = Convert.ToInt32(row["RolID"].ToString());
                    KullaniciRol kullaniciRol;
                    if (Enum.TryParse(row["RolID"].ToString(), out kullaniciRol))
                        k.Rol = kullaniciRol;
                }
            }
            return k;
        }

        public DataTable KullanicilariGetir(int musteriID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            data.AddSqlParameter("MusteriID", musteriID, SqlDbType.Int, 50);

            string sqlText = @"SELECT 
	                                K.*
	                                ,KR.RolAdi
                                    ,M.Adi
                                FROM KULLANICI AS K 
	                            INNER JOIN KULLANICI_ROL AS KR ON KR.RolID=K.RolID
                                INNER JOIN MUSTERI AS M ON M.MusteriID=K.MusteriID
                                WHERE K.MusteriID=@MusteriID
                                ORDER BY K.KullaniciID";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable TumKullanicilariGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT 
	                                K.*
	                                ,KR.RolAdi
                                    ,M.Adi
                                FROM KULLANICI AS K 
	                            INNER JOIN KULLANICI_ROL AS KR ON KR.RolID=K.RolID
                                INNER JOIN MUSTERI AS M ON M.MusteriID=K.MusteriID
                                ORDER BY K.KullaniciID";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public bool KullaniciTanimla(Dictionary<string, object> prms)
        {
            IData data = GetDataObject();

            data.AddSqlParameter("KullaniciAdi", prms["KullaniciAdi"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("MusteriID", prms["MusteriID"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("RolID", prms["RolID"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("Sifre", prms["Sifre"], SqlDbType.VarChar, 50);

            string sqlKaydet = @"INSERT INTO KULLANICI (KullaniciAdi,MusteriID,Sifre,RolID) VALUES (@KullaniciAdi,@MusteriID,@Sifre,@RolID)";
            data.ExecuteStatement(sqlKaydet);

            return true;
        }

        public bool KullaniciSil(int kullaniciID)
        {
            IData data = GetDataObject();

            data.AddSqlParameter("KullaniciID", kullaniciID, SqlDbType.Int, 50);
            string sqlSil = @"DELETE FROM KULLANICI WHERE KullaniciID=@KullaniciID";
            data.ExecuteStatement(sqlSil);

            return true;
        }

        public DataTable KullaniciSifreBilgisiGetir(string kullaniciAdi, int rolID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.AddSqlParameter("KullaniciAdi", kullaniciAdi, SqlDbType.VarChar, 50);
            data.AddSqlParameter("RolID", rolID, SqlDbType.Int, 50);
            string sqlText = @"SELECT KullaniciAdi, Sifre FROM KULLANICI WHERE KullaniciAdi=@KullaniciAdi and RolID=@RolID";
            data.GetRecords(dt, sqlText);

            return dt;
        }

        public bool KullaniciSifreGuncelle(Dictionary<string, object> prms)
        {
            IData data = GetDataObject();

            data.AddSqlParameter("KullaniciAdi", prms["KullaniciAdi"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("RolID", prms["RolID"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("Sifre", prms["Sifre"], SqlDbType.VarChar, 50);
            data.AddSqlParameter("YeniSifre", prms["YeniSifre"], SqlDbType.VarChar, 50);
            string sqlGuncelle = @"UPDATE KULLANICI SET Sifre=@YeniSifre WHERE KullaniciAdi=@KullaniciAdi and Sifre=@Sifre and RolID=@RolID";

            data.ExecuteStatement(sqlGuncelle);
            return true;
        }
    }
}

using System.Data;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;

namespace Akios.Business
{
    [ServiceConnectionName("AkiosConnectionString")]
    public class PersonelBS : BusinessBase
    {
        public DataTable PersonelListesiGetir(int musteriID)
        {
            DataTable dt = new DataTable();
            dt.TableName = "PERSONEL";
            IData data = GetDataObject();
            data.AddSqlParameter("MusteriID", musteriID, SqlDbType.Int, 50);
            string sqlText = @"SELECT P.ID
                                      , RTRIM(P.Ad)+' '+ P.Soyad AS AD
                                      , M.MusteriID
                                      , M.Adi 
                                FROM PERSONEL P 
                                     INNER JOIN MUSTERI M 
                                     ON P.MusteriID=M.MusteriID 
                                WHERE P.MusteriID=@MusteriID 
                                ORDER BY 1";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable TumPersonelListesiGetir()
        {
            DataTable dt = new DataTable();
            dt.TableName = "PERSONEL";
            IData data = GetDataObject();
            string sqlText = @"SELECT P.ID
                                      , RTRIM(P.Ad)+' '+ P.Soyad AS AD
                                      , M.MusteriID
                                      , M.Adi 
                                FROM PERSONEL P 
                                     INNER JOIN MUSTERI M 
                                     ON P.MusteriID=M.MusteriID 
                                ORDER BY 3, 1";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public bool PersonelTanimla(string musteriId, string ad, string soyad)
        {
            IData data = GetDataObject();

            data.AddSqlParameter("MusteriID", musteriId, SqlDbType.Int, 50);
            data.AddSqlParameter("Ad", ad, SqlDbType.VarChar, 50);
            data.AddSqlParameter("Soyad", soyad, SqlDbType.VarChar, 50);

            string sqlKaydet = @"INSERT INTO PERSONEL (MusteriID,Ad,Soyad) VALUES (@MusteriID,@Ad,@Soyad)";
            data.ExecuteStatement(sqlKaydet);

            return true;
        }

        public bool PersonelSil(int id)
        {
            IData data = GetDataObject();
            data.AddSqlParameter("ID", id, SqlDbType.VarChar, 50);

            string sqlSil = @"DELETE FROM PERSONEL WHERE ID=@ID";
            data.ExecuteStatement(sqlSil);

            return true;
        }
    }
}

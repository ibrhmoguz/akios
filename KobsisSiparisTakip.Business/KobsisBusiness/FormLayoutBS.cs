using KobsisSiparisTakip.Business.DataTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;

namespace KobsisSiparisTakip.Business
{
    [ServiceConnectionNameAttribute("KobsisConnectionString")]
    public class FormLayoutBS : BusinessBase
    {
        public List<Layout> FormKontrolleriniGetir(int musteriID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT
	                            Y.YerlesimTabloID
                                ,Y.YerlesimID
	                            ,Y.YerlesimParentID
	                            ,K.KontrolTipID
	                            ,K.KontrolAdi
	                            ,K.Mask
	                            ,K.TextMode
	                            ,K.Yukseklik
	                            ,K.Genislik
	                            ,K.[Enabled]
	                            ,K.PostBack
	                            ,K.CssClass
	                            ,K.Style
	                            ,K.RowSpan
	                            ,K.ColSpan
	                            ,(CASE WHEN K.[Text] IS NOT NULL THEN K.[Text]
		                               WHEN K.KontrolDegerID IS NOT NULL THEN KD.KontrolDegeri 
	                              END) AS [Text]
	                            ,K.ImajID
                                ,(CASE WHEN K.ImajID IS NOT NULL THEN I.ImajData END) AS ImajData
	                            ,K.RefID
                                ,K.ImajID
                                ,SM.KolonAdi
                                ,SM.SorgulanacakMi	                            
                                ,VT.VeriTipAdi
                                ,Y.SiparisSeriID
                            FROM dbo.YERLESIM AS Y
	                            INNER JOIN dbo.KONTROL AS K ON K.KontrolID=Y.KontrolID
	                            LEFT JOIN dbo.KONTROL_DEGER AS KD ON K.KontrolDegerID=KD.KontrolDegerID
	                            LEFT JOIN dbo.IMAJ AS I ON K.ImajID=I.ImajID
                                LEFT JOIN dbo.SIPARIS_METADATA AS SM ON SM.MetadataID=K.MetadataID
	                            LEFT JOIN dbo.VERI_TIP AS VT ON VT.VeriTipID=SM.VeriTipID
                            WHERE Y.MusteriID=@MusteriID
                            ORDER BY Y.YerlesimTabloID";
            data.AddSqlParameter("MusteriID", musteriID, SqlDbType.VarChar, 50);
            data.GetRecords(dt, sqlText);

            return GenerateLayoutListFromDataTable(dt);
        }

        public List<Layout> SorgulamaFormKontrolleriniGetir(int musteriID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"WITH SORGULANACAK_LIST AS
                            (
	                            SELECT DISTINCT
		                            Y.YerlesimTabloID
		                            ,Y.YerlesimID
		                            ,Y.YerlesimParentID
		                            ,Y.KompositKontrolID
	                            FROM dbo.YERLESIM AS Y
		                            INNER JOIN dbo.KONTROL AS K ON K.KontrolID=Y.KontrolID
		                            INNER JOIN dbo.SIPARIS_METADATA AS SM ON SM.MetadataID=K.MetadataID
	                            WHERE Y.MusteriID=@MusteriID
	                            UNION ALL 
	                            SELECT
		                            Y.YerlesimTabloID
		                            ,Y.YerlesimID
		                            ,Y.YerlesimParentID
		                            ,Y.KompositKontrolID
	                            FROM dbo.YERLESIM AS Y
		                            INNER JOIN SORGULANACAK_LIST AS SL ON SL.YerlesimParentID=Y.YerlesimID
	                            WHERE Y.MusteriID=@MusteriID
                            ) --select distinct * from SORGULANACAK_LIST order by YerlesimTabloID
                            , LIST AS
                            (
	                            SELECT DISTINCT
		                            YerlesimTabloID
		                            ,YerlesimID
		                            ,YerlesimParentID
	                            FROM SORGULANACAK_LIST
	                            UNION ALL 
	                            SELECT DISTINCT
		                            Y.YerlesimTabloID
		                            ,Y.YerlesimID
		                            ,Y.YerlesimParentID
	                            FROM dbo.YERLESIM AS Y
	                            WHERE Y.MusteriID=@MusteriID 
		                              AND Y.YerlesimID NOT IN (SELECT DISTINCT YerlesimID FROM SORGULANACAK_LIST)
		                              AND Y.KompositKontrolID IN (SELECT DISTINCT KompositKontrolID FROM SORGULANACAK_LIST)
                            ) --select * from LIST order by YerlesimTabloID
                            SELECT
                                Y.YerlesimTabloID
                                ,Y.YerlesimID
                                ,Y.YerlesimParentID
                                ,K.KontrolTipID
                                ,K.KontrolAdi
                                ,K.Mask
                                ,K.TextMode
                                ,K.Yukseklik
                                ,K.Genislik
                                ,K.[Enabled]
                                ,K.PostBack
                                ,K.CssClass
                                ,K.Style
                                ,K.RowSpan
                                ,K.ColSpan
                                ,(CASE WHEN K.[Text] IS NOT NULL THEN K.[Text]
                                       WHEN K.KontrolDegerID IS NOT NULL THEN KD.KontrolDegeri 
                                  END) AS [Text]
                                ,K.ImajID
                                ,(CASE WHEN K.ImajID IS NOT NULL THEN I.ImajData END) AS ImajData
                                ,K.RefID
                                ,K.ImajID
                                ,SM.KolonAdi
                                ,SM.SorgulanacakMi	                            
                                ,VT.VeriTipAdi
                                ,Y.SiparisSeriID
                            FROM LIST AS L
	                            INNER JOIN dbo.YERLESIM AS Y ON Y.YerlesimTabloID=L.YerlesimTabloID
                                INNER JOIN dbo.KONTROL AS K ON K.KontrolID=Y.KontrolID
                                LEFT JOIN dbo.KONTROL_DEGER AS KD ON K.KontrolDegerID=KD.KontrolDegerID
                                LEFT JOIN dbo.IMAJ AS I ON K.ImajID=I.ImajID
                                LEFT JOIN dbo.SIPARIS_METADATA AS SM ON SM.MetadataID=K.MetadataID
                                LEFT JOIN dbo.VERI_TIP AS VT ON VT.VeriTipID=SM.VeriTipID
                            ORDER BY Y.YerlesimTabloID";
            data.AddSqlParameter("MusteriID", musteriID, SqlDbType.VarChar, 50);
            data.GetRecords(dt, sqlText);

            return GenerateLayoutListFromDataTable(dt);
        }

        private static List<Layout> GenerateLayoutListFromDataTable(DataTable dt)
        {
            var layoutList = new List<Layout>();
            foreach (DataRow row in dt.Rows)
            {
                var layout = new Layout();
                layout.YerlesimTabloID = Convert.ToInt32(row["YerlesimTabloID"]);
                layout.YerlesimID = Convert.ToInt32(row["YerlesimID"]);
                layout.SeriID = Convert.ToInt32(row["SiparisSeriID"]);
                if (row["YerlesimParentID"] != DBNull.Value)
                    layout.YerlesimParentID = Convert.ToInt32(row["YerlesimParentID"]);
                layout.KontrolTipID = Convert.ToInt32(row["KontrolTipID"]);
                layout.KontrolAdi = row["KontrolAdi"].ToString();
                if (row["Mask"] != DBNull.Value)
                    layout.Mask = row["Mask"].ToString();
                if (row["TextMode"] != DBNull.Value)
                    layout.TextMode = row["TextMode"].ToString();
                if (row["Yukseklik"] != DBNull.Value)
                    layout.Yukseklik = Convert.ToInt32(row["Yukseklik"]);
                if (row["Genislik"] != DBNull.Value)
                    layout.Genislik = Convert.ToInt32(row["Genislik"]);
                if (row["Enabled"] != DBNull.Value)
                    layout.Enabled = Convert.ToBoolean(row["Enabled"]);
                if (row["PostBack"] != DBNull.Value)
                    layout.PostBack = Convert.ToBoolean(row["PostBack"]);
                if (row["CssClass"] != DBNull.Value)
                    layout.CssClass = row["CssClass"].ToString();
                if (row["Style"] != DBNull.Value)
                    layout.Style = row["Style"].ToString();
                if (row["RowSpan"] != DBNull.Value)
                    layout.RowSpan = Convert.ToInt32(row["RowSpan"]);
                if (row["ColSpan"] != DBNull.Value)
                    layout.ColSpan = Convert.ToInt32(row["ColSpan"]);
                if (row["Text"] != DBNull.Value)
                    layout.Text = row["Text"].ToString();
                if (row["ImajData"] != DBNull.Value)
                    layout.ImajData = (byte[])row["ImajData"];
                if (row["RefID"] != DBNull.Value)
                    layout.RefID = Convert.ToInt32(row["RefID"]);
                if (row["ImajID"] != DBNull.Value)
                    layout.ImajID = Convert.ToInt32(row["ImajID"]);
                if (row["KolonAdi"] != DBNull.Value)
                    layout.KolonAdi = row["KolonAdi"].ToString();
                if (row["VeriTipAdi"] != DBNull.Value)
                    layout.VeriTipi = row["VeriTipAdi"].ToString();
                if (row["SorgulanacakMi"] != DBNull.Value)
                    layout.SorgulanacakMi = Convert.ToBoolean(row["SorgulanacakMi"].ToString());

                layoutList.Add(layout);
            }

            return layoutList;
        }
    }
}

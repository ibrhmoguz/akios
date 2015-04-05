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
        public List<Layout> FormKontrolleriniGetir(string musteriID, string kapiSeri)
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
	                            ,K.ColumnSpan
	                            ,(CASE WHEN K.[Text] IS NOT NULL THEN K.[Text]
		                               WHEN K.KontrolDegerID IS NOT NULL THEN KD.KontrolDegeri 
	                              END) AS [Text]
	                            ,(CASE WHEN K.ImajID IS NOT NULL THEN I.ImajData END) AS ImajData
	                            ,K.RefID
                            FROM dbo.YERLESIM AS Y
	                            INNER JOIN dbo.KONTROL AS K ON K.KontrolID=Y.KontrolID
	                            LEFT JOIN dbo.KONTROL_DEGER AS KD ON K.KontrolDegerID=KD.KontrolDegerID
	                            LEFT JOIN dbo.IMAJ AS I ON K.ImajID=I.ImajID
                            WHERE Y.KapiSeriID=@KapiSeriID AND Y.MusteriID=@MusteriID
                            ORDER BY Y.YerlesimTabloID";
            data.AddSqlParameter("KapiSeriID", kapiSeri, SqlDbType.VarChar, 50);
            data.AddSqlParameter("MusteriID", musteriID, SqlDbType.VarChar, 50);
            data.GetRecords(dt, sqlText);

            var layoutList = new List<Layout>();
            foreach (DataRow row in dt.Rows)
            {
                var layout = new Layout();
                layout.YerlesimTabloID = Convert.ToInt32(row["YerlesimTabloID"]);
                layout.YerlesimID = Convert.ToInt32(row["YerlesimID"]);
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
                if (row["ColumnSpan"] != DBNull.Value)
                    layout.ColumnSpan = Convert.ToInt32(row["ColumnSpan"]);
                if (row["Text"] != DBNull.Value)
                    layout.Text = row["Text"].ToString();
                if (row["ImajData"] != DBNull.Value)
                    layout.ImajData = (byte[])row["ImajData"];
                if (row["RefID"] != DBNull.Value)
                    layout.RefID = Convert.ToInt32(row["RefID"]);

                layoutList.Add(layout);
            }

            return layoutList;
        }
    }
}

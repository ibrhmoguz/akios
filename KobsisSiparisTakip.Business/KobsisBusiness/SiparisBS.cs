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
using WebFrame.DataType.Common.Logging;

namespace KobsisSiparisTakip.Business
{
    [ServiceConnectionNameAttribute("KobsisConnectionString")]
    public class SiparisBS : BusinessBase
    {
        public int? SiparisKaydetGuncelle(List<DbParametre> parametreler, string musteriKodu)
        {
            IData data = GetDataObject();

            foreach (DbParametre param in parametreler)
            {
                if (param.ParametreYonu.HasValue)
                    data.AddSqlParameter(param.ParametreAdi, param.ParametreDegeri, param.VeriTipi, param.ParametreYonu.Value, param.ParametreBoyutu);
                else
                    data.AddSqlParameter(param.ParametreAdi, param.ParametreDegeri, param.VeriTipi, param.ParametreBoyutu);
            }

            Dictionary<string, object> output = data.ExecuteStatementUDI("dbo.InsertUpdateSiparis" + musteriKodu);
            if (output["ErrorMessage"] != null && output["ErrorMessage"].ToString() != string.Empty)
            {
                new LogWriter().Write(AppModules.Siparis, System.Diagnostics.EventLogEntryType.Error, new Exception(output["ErrorMessage"].ToString()), "ServerSide", "SiparisKaydet", "", null);
                return null;
            }
            else if (output["ID"] != null && output["ID"].ToString() != string.Empty)
                return Convert.ToInt32(output["ID"].ToString());
            else
                return null;
        }

        public List<SiparisMetadata> SiparisMetdataGetir(string musteriID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT 
	                            SM.KolonAdi
	                            ,SM.VeriTipID
	                            ,VT.VeriTipAdi
	                            ,K.KontrolAdi
	                            ,Y.YerlesimTabloID
                            FROM dbo.SIPARIS_METADATA AS SM
	                            INNER JOIN dbo.VERI_TIP AS VT ON SM.VeriTipID = VT.VeriTipID
	                            INNER JOIN dbo.KONTROL AS K ON K.MetadataID = SM.MetadataID
	                            INNER JOIN dbo.YERLESIM AS Y ON Y.KontrolID = K.KontrolID AND Y.MusteriID=SM.MusteriID
                            WHERE SM.MusteriID=@MusteriID";
            data.AddSqlParameter("MusteriID", musteriID, SqlDbType.VarChar, 50);
            data.GetRecords(dt, sqlText);

            var metadataList = new List<SiparisMetadata>();
            foreach (DataRow row in dt.Rows)
            {
                var metadata = new SiparisMetadata()
                {
                    KolonAdi = row["KolonAdi"] != DBNull.Value ? row["KolonAdi"].ToString() : string.Empty,
                    VeriTipAdi = row["VeriTipAdi"] != DBNull.Value ? row["VeriTipAdi"].ToString() : string.Empty,
                    VeriTipID = Convert.ToInt32(row["VeriTipID"].ToString()),
                    KontrolAdi = row["KontrolAdi"] != DBNull.Value ? row["KontrolAdi"].ToString() : string.Empty,
                    YerlesimTabloID = row["YerlesimTabloID"] != DBNull.Value ? row["YerlesimTabloID"].ToString() : string.Empty,
                };

                metadataList.Add(metadata);
            }

            return metadataList;
        }

        public DataTable SiparisGetir(string siparisID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT 
                                S.*
                                ,RIL.ILAD
                                ,RILCE.ILCEAD
                                ,SM.SEMTAD
                                ,(SELECT TeslimTarih FROM [dbo].[MONTAJ] WHERE SiparisID = S.ID) AS TESLIMTARIH
                            FROM SIPARIS_ABC AS S
	                            INNER JOIN dbo.REF_ILLER AS RIL ON S.MusteriIlKod = RIL.ILKOD
	                            INNER JOIN dbo.REF_ILCELER AS RILCE ON S.MusteriIlceKod = RILCE.ILCEKOD
	                            INNER JOIN dbo.SEMTLER AS SM ON S.MusteriSemtKod = SM.SEMTKOD
                            WHERE ID=@ID";
            data.AddSqlParameter("ID", siparisID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);
            return dt;
        }
    }
}

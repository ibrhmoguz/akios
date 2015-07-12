using System;
using System.Collections.Generic;
using System.Data;
using Kobsis.DataType;
using Kobsis.Util;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;
using WebFrame.DataType.Common.Logging;

namespace Kobsis.Business
{
    [ServiceConnectionName("KobsisConnectionString")]
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

            Dictionary<string, object> output = data.ExecuteStatementUDI("dbo.KaydetGuncelleSiparis" + musteriKodu);
            if (output["ErrorMessage"] != null && output["ErrorMessage"].ToString() != string.Empty)
            {
                new LogWriter().Write(AppModules.Siparis, System.Diagnostics.EventLogEntryType.Error, new Exception(output["ErrorMessage"].ToString()), "ServerSide", "SiparisKaydet", SessionManager.KullaniciBilgi.KullaniciAdi, null);
                return null;
            }
            
            if (output["ID"] != null && output["ID"].ToString() != string.Empty)
                return Convert.ToInt32(output["ID"].ToString());
            
            return null;
        }

        public List<SiparisMetadata> SiparisMetdataGetir(string musteriId)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT 
	                            SM.KolonAdi
	                            ,SM.VeriTipID
                                ,SM.SorgulanacakMi
	                            ,VT.VeriTipAdi
	                            ,K.KontrolAdi
	                            ,Y.YerlesimTabloID
                            FROM dbo.SIPARIS_METADATA AS SM
	                            INNER JOIN dbo.VERI_TIP AS VT ON SM.VeriTipID = VT.VeriTipID
	                            INNER JOIN dbo.KONTROL AS K ON K.MetadataID = SM.MetadataID
	                            INNER JOIN dbo.YERLESIM AS Y ON Y.KontrolID = K.KontrolID AND Y.MusteriID=SM.MusteriID
                            WHERE SM.MusteriID=@MusteriID";
            data.AddSqlParameter("MusteriID", musteriId, SqlDbType.VarChar, 50);
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
                    SorgulanacakMi = Convert.ToBoolean(row["SorgulanacakMi"].ToString())
                };

                metadataList.Add(metadata);
            }

            return metadataList;
        }

        public DataTable SiparisGetir(string siparisId)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT 
                                S.*
                                ,RIL.ILAD as MusteriIl
                                ,RILCE.ILCEAD as MusteriIlce
                                ,SM.SEMTAD as MusteriSemt
                                ,(SELECT TeslimTarih FROM [dbo].[TESLIMAT] WHERE SiparisID = S.ID) AS TESLIMTARIH
                            FROM SIPARIS_ABC AS S
	                           INNER JOIN dbo.REF_IL AS RIL ON S.MusteriIlKod = RIL.IlKod
	                           INNER JOIN dbo.REF_ILCE AS RILCE ON S.MusteriIlceKod = RILCE.IlceKod
	                           INNER JOIN dbo.REF_SEMT AS SM ON S.MusteriSemtKod = SM.SemtKod
                            WHERE ID=@ID";
            data.AddSqlParameter("ID", siparisId, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable SiparisSorgula(List<DbParametre> parametreler, string musteriKodu)
        {
            IData data = GetDataObject();

            foreach (DbParametre param in parametreler)
            {
                if (param.ParametreYonu.HasValue)
                    data.AddSqlParameter(param.ParametreAdi, param.ParametreDegeri, param.VeriTipi, param.ParametreYonu.Value, param.ParametreBoyutu);
                else
                    data.AddSqlParameter(param.ParametreAdi, param.ParametreDegeri, param.VeriTipi, param.ParametreBoyutu);
            }
            
            var ds = new DataSet();
            data.ExecuteStatement("dbo.SorgulaSiparis" + musteriKodu, ds);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];

            return null;
        }
    }
}

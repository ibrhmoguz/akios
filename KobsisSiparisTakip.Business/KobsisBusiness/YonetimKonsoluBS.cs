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
    public class YonetimKonsoluBS : BusinessBase
    {
        public DataSet RefTablolariGetir()
        {
            return pRefTablolariGetir();
        }

        private DataSet pRefTablolariGetir()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(KapiRenkGetir());
            ds.Tables.Add(KilitSistemiGetir());
            ds.Tables.Add(CitaGetir());
            ds.Tables.Add(EsikGetir());
            ds.Tables.Add(AksesuarRenkGetir());
            ds.Tables.Add(AluminyumRenkGetir());
            ds.Tables.Add(ContaRenkGetir());
            ds.Tables.Add(TacTipiGetir());
            ds.Tables.Add(PervazTipiGetir());
            ds.Tables.Add(MontajSekliGetir());
            ds.Tables.Add(TeslimSekliGetir());
            ds.Tables.Add(TumKapiModelGetir());
            ds.Tables.Add(BarelTipGetir());
            ds.Tables.Add(CekmeKoluGetir());
            ds.Tables.Add(KapiSeriGetir());
            ds.Tables.Add(PanikBarGetir());
            ds.Tables.Add(MudahaleKolGetir());
            ds.Tables.Add(YanginKasaTipGetir());
            ds.Tables.Add(YanginKapiCinsGetir());
            ds.Tables.Add(MenteseTipGetir());
            ds.Tables.Add(HidrolikKapaticiGetir());
            ds.Tables.Add(CekmeKolTakmaSekliGetir());
            ds.Tables.Add(ZirhTipiGetir());
            ds.Tables.Add(ZirhRengiGetir());
            ds.Tables.Add(BolmeCamTipiGetir());
            ds.Tables.Add(FerforjeGetir());
            ds.Tables.Add(FerforjeRenkGetir());
            ds.Tables.Add(AplikeRenkGetir());
            ds.Tables.Add(PervazRenkGetir());
            ds.Tables.Add(MetalRenkGetir());
            ds.Tables.Add(CumbaGetir());

            return ds;
        }

        private DataTable TabloGetir(string tabloAdi)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            dt.TableName = tabloAdi;
            string sqlText = @"SELECT * FROM " + tabloAdi;
            data.GetRecords(dt, sqlText);
            return dt;
        }

        private DataTable CumbaGetir()
        {
            return TabloGetir("REF_CUMBA");
        }

        private DataTable AplikeRenkGetir()
        {
            return TabloGetir("REF_APLIKE");
        }
        private DataTable PervazRenkGetir()
        {
            return TabloGetir("REF_PERVAZRENK");
        }
        private DataTable MetalRenkGetir()
        {
            return TabloGetir("REF_METALRENK");
        }

        private DataTable FerforjeRenkGetir()
        {
            return TabloGetir("REF_FERFORJERENK");
        }

        private DataTable BolmeCamTipiGetir()
        {
            return TabloGetir("REF_CAMTIP");
        }

        private DataTable FerforjeGetir()
        {
            return TabloGetir("REF_FERFORJE");
        }

        private DataTable ZirhRengiGetir()
        {
            return TabloGetir("REF_ZIRHRENK");
        }

        private DataTable ZirhTipiGetir()
        {
            return TabloGetir("REF_ZIRHTIP");
        }

        private DataTable CekmeKolTakmaSekliGetir()
        {
            return TabloGetir("REF_CEKMEKOLUTAKILMASEKLI");
        }

        private DataTable HidrolikKapaticiGetir()
        {
            return TabloGetir("REF_HIDROLIKKAPATICI");
        }

        private DataTable MenteseTipGetir()
        {
            return TabloGetir("REF_MENTESETIP");
        }

        private DataTable YanginKapiCinsGetir()
        {
            return TabloGetir("REF_KAPICINSI");
        }

        private DataTable YanginKasaTipGetir()
        {
            return TabloGetir("REF_KASATIP");
        }

        private DataTable MudahaleKolGetir()
        {
            return TabloGetir("REF_MUDAHALEKOL");
        }

        private DataTable PanikBarGetir()
        {
            return TabloGetir("REF_PANIKBAR");
        }

        private DataTable KapiRenkGetir()
        {
            return TabloGetir("REF_KAPIRENK");
        }

        private DataTable KilitSistemiGetir()
        {
            return TabloGetir("REF_KILITSISTEM");
        }

        private DataTable CitaGetir()
        {
            return TabloGetir("REF_CITA");
        }

        private DataTable BarelTipGetir()
        {
            return TabloGetir("REF_BARELTIP");
        }
        private DataTable CekmeKoluGetir()
        {
            return TabloGetir("REF_CEKMEKOLU");
        }

        private DataTable EsikGetir()
        {
            return TabloGetir("REF_ESIK");
        }

        private DataTable AksesuarRenkGetir()
        {
            return TabloGetir("REF_AKSESUARRENK");
        }

        private DataTable AluminyumRenkGetir()
        {
            return TabloGetir("REF_ALUMINYUMRENK");
        }

        private DataTable ContaRenkGetir()
        {
            return TabloGetir("REF_CONTARENK");
        }

        private DataTable TacTipiGetir()
        {
            return TabloGetir("REF_TACTIP");
        }

        private DataTable PervazTipiGetir()
        {
            return TabloGetir("REF_PERVAZTIP");
        }

        private DataTable MontajSekliGetir()
        {
            return TabloGetir("REF_MONTAJSEKLI");
        }

        private DataTable TeslimSekliGetir()
        {
            return TabloGetir("REF_TESLIMSEKLI");
        }

        public DataTable TabloAdlariGetir()
        {
            return pTabloAdlariGetir();
        }

        private DataTable pTabloAdlariGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT AD,TABLO FROM REF_TABLOLAR ORDER BY 1 ";
            data.GetRecords(dt, sqlText);

            return dt;
        }

        public bool OgeSil(Dictionary<string, object> prms)
        {
            return pOgeSil(prms);
        }

        private bool pOgeSil(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();

                data.AddSqlParameter("TABLOADI", prms["TABLOADI"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("ID", prms["ID"], SqlDbType.VarChar, 50);

                string sqlSil = @"DELETE FROM " + prms["TABLOADI"].ToString() + " WHERE ID=@ID";
                data.ExecuteStatement(sqlSil);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "OgeSil", "", null);
                return false;
            }
        }

        public bool OgeEkle(Dictionary<string, object> prms)
        {
            return pOgeEkle(prms);
        }

        private bool pOgeEkle(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();

                data.AddSqlParameter("TABLOADI", prms["TABLOADI"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("AD", prms["AD"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("NOVA", prms["NOVA"], SqlDbType.Bit, 1);
                data.AddSqlParameter("KROMA", prms["KROMA"], SqlDbType.Bit, 1);
                data.AddSqlParameter("GUARD", prms["GUARD"], SqlDbType.Bit, 1);
                data.AddSqlParameter("YANGIN", prms["YANGIN"], SqlDbType.Bit, 1);

                string sqlKaydet = @"INSERT INTO " + prms["TABLOADI"].ToString() + " (AD, NOVA, KROMA, GUARD,YANGIN) VALUES ( @AD, @NOVA, @KROMA, @GUARD, @YANGIN)";
                data.ExecuteStatement(sqlKaydet);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "OgeEkle", "", null);
                return false;
            }
        }

        public bool KapiModelEkle(Dictionary<string, object> prms)
        {
            return pKapiModelEkle(prms);
        }

        private bool pKapiModelEkle(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();

                data.AddSqlParameter("TABLOADI", prms["TABLOADI"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("KAPISERIID", prms["KAPISERIID"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("AD", prms["AD"], SqlDbType.VarChar, 50);

                string sqlKaydet = @"INSERT INTO " + prms["TABLOADI"].ToString() + " (KAPISERIID, AD) VALUES ( @KAPISERIID, @AD)";
                data.ExecuteStatement(sqlKaydet);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "KapiModelEkle", "", null);
                return false;
            }
        }

        public DataTable KapiSeriGetir()
        {
            return pKapiSeriGetir();
        }

        private DataTable pKapiSeriGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            dt.TableName = "REF_KAPISERI";

            string sqlText = @"SELECT ID,AD,VALUE FROM REF_KAPISERI ORDER BY 1 ";
            data.GetRecords(dt, sqlText);

            return dt;
        }

        public DataTable KapiModelGetir(Dictionary<string, object> prms)
        {
            return pKapiModelGetir(prms);
        }

        private DataTable pKapiModelGetir(Dictionary<string, object> prms)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.AddSqlParameter("KAPISERIID", prms["KAPISERIID"], SqlDbType.VarChar, 50);

            string sqlText = @"SELECT ID,AD FROM REF_KAPIMODEL WHERE KAPISERIID=@KAPISERIID  ORDER BY 1 ";
            data.GetRecords(dt, sqlText);

            return dt;
        }

        public DataTable TumKapiModelGetir()
        {
            return pTumKapiModelGetir();
        }

        private DataTable pTumKapiModelGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            dt.TableName = "REF_TUMKAPIMODELLERI";

            string sqlText = @"SELECT DISTINCT ID,AD FROM REF_KAPIMODEL";
            data.GetRecords(dt, sqlText);

            return dt;
        }

        public DataTable HatalariGetir()
        {
            return pHatalariGetir();
        }

        private DataTable pHatalariGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT TOP 500
                                  (CASE 
			                            WHEN MODULEID = 0 THEN 'KobsisSiparisTakip'
			                            WHEN MODULEID = 1 THEN 'Siparis'
			                            WHEN MODULEID = 2 THEN 'IsTakvimi'
			                            WHEN MODULEID = 3 THEN 'YonetimKonsolu'  
			                            WHEN MODULEID = 4 THEN 'SifreGuncelle'  
		                             END) AS MODUL
                                  ,(CASE 
			                            WHEN EVENTLOGENTRYTYPEID=1 THEN 'Error'
			                            WHEN EVENTLOGENTRYTYPEID=1 THEN 'Warning'
			                            WHEN EVENTLOGENTRYTYPEID=1 THEN 'Information'
			                            WHEN EVENTLOGENTRYTYPEID=1 THEN 'SuccessAudit'
			                            WHEN EVENTLOGENTRYTYPEID=1 THEN 'FailureAudit'
		                            END) AS LOGTYPE
                                  ,[EXCEPTION]
                                  ,[PAGEURL]
                                  ,[METHODNAME]
                                  ,[MESSAGE]
                                  ,[USERIDENTITY]
                                  ,[PCNAME]
                                  ,[USERAUTHORITY]
                                  ,[EXTENDEDPROPERTIES]
                                  ,[USERNAME]
                                  ,[DATE]
                              FROM [ACKAppDB].[dbo].[HATA]
                              ORDER BY [DATE] DESC";
            data.GetRecords(dt, sqlText);
            return dt;
        }
    }
}

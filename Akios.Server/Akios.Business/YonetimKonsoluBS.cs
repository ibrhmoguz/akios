using System;
using System.Collections.Generic;
using System.Data;
using Akios.Util;
using WebFrame.Business;
using WebFrame.DataType.Common.Attributes;
using WebFrame.DataType.Common.Logging;

namespace Akios.Business
{
    [ServiceConnectionName("KobsisConnectionString")]
    public class YonetimKonsoluBS : BusinessBase
    {
        public DataTable FormOgeDetayGetir(int musteriId, string refId)
        {
            var dt = new DataTable();
            var data = GetDataObject();

            data.AddSqlParameter("MusteriID", musteriId, SqlDbType.Int, 50);
            data.AddSqlParameter("RefID", refId, SqlDbType.Int, 50);

            const string sqlText = @"DECLARE @DynamicQuery AS NVARCHAR(MAX)
                                DECLARE @ColumnName AS NVARCHAR(MAX);
 
                                SELECT @ColumnName= COALESCE(@ColumnName + ',','') + QUOTENAME(SeriAdi)
                                FROM (SELECT SeriAdi FROM dbo.SIPARIS_SERI WHERE MusteriID=@MusteriID) AS SiparisSeri;

                                WITH REF_DETAY AS
                                (
	                                SELECT
		                                R.MusteriID
		                                ,R.RefID
		                                ,RD.RefDetayID
		                                ,RDK.SiparisSeriID
		                                ,RD.RefDetayAdi
	                                FROM dbo.REF AS R
		                                INNER JOIN dbo.REF_DETAY AS RD ON R.RefID=RD.RefID
		                                INNER JOIN dbo.REF_DETAY_SIPARIS_SERI AS RDK ON RDK.RefDetayID=RD.RefDetayID
	                                WHERE R.MusteriID=@MusteriID and R.RefID=@RefID
                                )
                                ,SIPARIS_SERI_REF_DETAY AS
                                (
	                                SELECT
		                                SS.SeriAdi
		                                ,RD.RefDetayID
		                                ,RD.RefDetayAdi AS [Adı]
	                                FROM dbo.SIPARIS_SERI AS SS
	                                INNER JOIN REF_DETAY AS RD ON SS.SiparisSeriID=RD.SiparisSeriID
                                )

                                SELECT * INTO #SERI_REF FROM SIPARIS_SERI_REF_DETAY

                                SET @DynamicQuery = N'SELECT * FROM #SERI_REF
					                                   PIVOT (Max(RefDetayID) FOR SeriAdi
					                                   IN ('+@ColumnName+')) pvt'
                                EXEC sp_executesql @DynamicQuery

                                DROP TABLE #SERI_REF;";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public bool FormOgeDetaySil(int refDetayId)
        {
            try
            {
                var data = GetDataObject();
                data.AddSqlParameter("RefDetayID", refDetayId, SqlDbType.Int, 50);

                const string sqlSil = @"BEGIN TRY
                                    BEGIN TRANSACTION
                                        DELETE FROM REF_DETAY_SIPARIS_SERI WHERE RefDetayID=@RefDetayID
                                        DELETE FROM REF_DETAY WHERE RefDetayID=@RefDetayID
                                    COMMIT TRANSACTION
	                              END TRY
	                              BEGIN CATCH
		                            IF @@TRANCOUNT > 0
			                            BEGIN 
				                            ROLLBACK TRANSACTION
                                            DECLARE @error int, @message varchar(4000)
				                            SELECT @error = ERROR_NUMBER(), @message = ERROR_MESSAGE()
				                            RAISERROR ('FormOgeDetaySil Hata: %d: %s', 16, 1, @error, @message)
			                            END
	                              END CATCH";
                data.ExecuteStatement(sqlSil);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "OgeSil", SessionManager.KullaniciBilgi.KullaniciAdi, null);
                return false;
            }
        }

        public bool FormOgeDetayEkle(string refId, string refDetayAdi, List<string> seriIdList)
        {
            var data = GetDataObject();

            try
            {
                data.BeginTransaction();

                data.AddSqlParameter("RefID", refId, SqlDbType.Int, 50);
                data.AddSqlParameter("RefDetayAdi", refDetayAdi, SqlDbType.VarChar, 150);

                var sqlText = @"INSERT INTO REF_DETAY (RefID,RefDetayAdi) VALUES (@RefID,@RefDetayAdi)
                                SELECT SCOPE_IDENTITY()";
                var refDetayId = data.ExecuteScalar(sqlText, CommandType.Text);

                foreach (var seriId in seriIdList)
                {
                    data.AddSqlParameter("SiparisSeriID", seriId, SqlDbType.Int, 50);
                    data.AddSqlParameter("RefDetayID", refDetayId, SqlDbType.Int, 50);

                    sqlText = @"INSERT INTO REF_DETAY_SIPARIS_SERI (SiparisSeriID,RefDetayID) VALUES (@SiparisSeriID,@RefDetayID)";
                    data.ExecuteStatement(sqlText);
                }

                data.CommitTransaction();
                return true;
            }
            catch (Exception exc)
            {
                data.RollbackTransaction();
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "OgeEkle", SessionManager.KullaniciBilgi.KullaniciAdi, null);
                return false;
            }
        }

        public DataTable HatalariGetir()
        {
            var dt = new DataTable();
            var data = GetDataObject();

            const string sqlText = @"SELECT TOP 500
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
                              FROM HATA
                              ORDER BY DATE DESC";
            data.GetRecords(dt, sqlText);
            return dt;
        }
    }
}

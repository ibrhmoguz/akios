using System.Collections.Generic;
using System.Data;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;
using System;
using WebFrame.DataType.Common.Logging;

namespace KobsisSiparisTakip.Business
{
    [ServiceConnectionNameAttribute("KobsisConnectionString")]
    public class PersonelBS : BusinessBase
    {
        public DataTable PersonelListesiGetir()
        {
            return pPersonelListesiGetir();
        }

        private DataTable pPersonelListesiGetir()
        {
            DataTable dt = new DataTable();
            dt.TableName = "PERSONEL";
            IData data = GetDataObject();

            string sqlText = @"SELECT ID, RTRIM(AD)+' ' +SOYAD AS AD FROM PERSONELBILGI ORDER BY 1";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public DataTable PersonelListesiGetirGenel()
        {
            return pPersonelListesiGetirGenel();
        }

        private DataTable pPersonelListesiGetirGenel()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            string sqlText = @"SELECT ID,AD,SOYAD  FROM PERSONELBILGI ORDER BY 1";
            data.GetRecords(dt, sqlText);
            return dt;
        }

        public bool PersonelTanimla(Dictionary<string, object> prms)
        {
            return pPersonelTanimla(prms);
        }

        private bool pPersonelTanimla(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();

                data.AddSqlParameter("AD", prms["AD"], SqlDbType.VarChar, 50);
                data.AddSqlParameter("SOYAD", prms["SOYAD"], SqlDbType.VarChar, 50);

                string sqlKaydet = @"INSERT INTO PERSONELBILGI (AD,SOYAD) VALUES (@AD,@SOYAD)";
                data.ExecuteStatement(sqlKaydet);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "PersonelTanimla", "", null);
                return false;
            }
        }

        public bool PersonelSil(Dictionary<string, object> prms)
        {
            return pPersonelSil(prms);
        }

        private bool pPersonelSil(Dictionary<string, object> prms)
        {
            try
            {
                IData data = GetDataObject();
                data.AddSqlParameter("ID", prms["ID"], SqlDbType.VarChar, 50);

                string sqlSil = @"DELETE FROM PERSONELBILGI WHERE ID=@ID";
                data.ExecuteStatement(sqlSil);

                return true;
            }
            catch (Exception exc)
            {
                new LogWriter().Write(AppModules.YonetimKonsolu, System.Diagnostics.EventLogEntryType.Error, exc, "ServerSide", "PersonelSil", "", null);
                return false;
            }
        }
    }
}

using System.Data;
using WebFrame.Business;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;

namespace Kobsis.Business
{
    [ServiceConnectionName("KobsisConnectionString")]
    public class ConfigBS : BusinessBase
    {
        public DataTable ConfigBilgileriniGetir()
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT * FROM CONFIG";

            data.GetRecords(dt, sqlText);

            return dt;
        }

        public bool ConfigDegerleriniKaydet(bool montajKotaKontrolu, string montajKotaVarsayilan)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"TRUNCATE TABLE dbo.CONFIG
                               INSERT INTO dbo.CONFIG VALUES('MONTAJ_KOTA_KONTROLU',@MontajKotaKontrol)
                               INSERT INTO dbo.CONFIG VALUES('MONTAJ_KOTA_VARSAYILAN',@MontajKotaVarsayilan)";

            data.AddSqlParameter("MontajKotaKontrol", montajKotaKontrolu, SqlDbType.Bit, 50);
            data.AddSqlParameter("MontajKotaVarsayilan", montajKotaVarsayilan, SqlDbType.Int, 50);
            data.ExecuteStatement(sqlText);
            return true;
        }

        public DataTable Execute(string query)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();

            data.GetRecords(dt, query);

            return dt;
        } 
    }
}

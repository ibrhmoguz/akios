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

        public bool ConfigDegerleriniKaydet(bool teslimatKotaKontrolu, string teslimatKotaVarsayilan)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"TRUNCATE TABLE dbo.CONFIG
                               INSERT INTO dbo.CONFIG VALUES('TESLIMAT_KOTA_KONTROLU',@TeslimatKotaKontrol)
                               INSERT INTO dbo.CONFIG VALUES('TESLIMAT_KOTA_VARSAYILAN',@TeslimatKotaVarsayilan)";

            data.AddSqlParameter("TeslimatKotaKontrol", teslimatKotaKontrolu, SqlDbType.Bit, 50);
            data.AddSqlParameter("TeslimatKotaVarsayilan", teslimatKotaVarsayilan, SqlDbType.Int, 50);
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

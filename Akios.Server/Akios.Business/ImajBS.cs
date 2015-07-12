using System;
using System.Data;
using Akios.DataType;
using WebFrame.Business;
using WebFrame.DataType.Common.Attributes;

namespace Akios.Business
{
    [ServiceConnectionName("KobsisConnectionString")]
    public class ImajBS : BusinessBase
    {
        public Imaj ImajGetirImajIdIle(string pImajId)
        {
            var dt = new DataTable();
            var data = GetDataObject();
            const string sqlText = @"SELECT * FROM IMAJ WHERE ImajID=@ImajID";

            data.AddSqlParameter("ImajID", pImajId, SqlDbType.VarChar, 50);
            data.GetRecords(dt, sqlText);

            var imaj = new Imaj();
            if (dt.Rows.Count <= 0)
                return imaj;

            var row = dt.Rows[0];
            if (row["ImajData"] != DBNull.Value)
                imaj.ImajData = (byte[])row["ImajData"];
            imaj.ImajId = pImajId;

            return imaj;
        }
    }
}

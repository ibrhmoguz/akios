using KobsisSiparisTakip.Business.DataTypes;
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
    public class SiparisSeriBS : BusinessBase
    {
        public DataTable SeriGetirMusteriIDGore(int pMusteriID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT * FROM KAPI_SERI WHERE MusteriID=@MusteriID";

            data.AddSqlParameter("MusteriID", pMusteriID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);

            return dt;
        }
    }
}

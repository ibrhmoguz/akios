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
        public List<SiparisSeri> SeriGetirMusteriIDGore(int pMusteriID)
        {
            DataTable dt = new DataTable();
            IData data = GetDataObject();
            string sqlText = @"SELECT * FROM SIPARIS_SERI WHERE MusteriID=@MusteriID";

            data.AddSqlParameter("MusteriID", pMusteriID, SqlDbType.Int, 50);
            data.GetRecords(dt, sqlText);

            var seriList = new List<SiparisSeri>();
            foreach (DataRow row in dt.Rows)
            {
                var seri = new SiparisSeri();
                if (row["SiparisSeriID"] != DBNull.Value)
                    seri.SiparisSeriID = Convert.ToInt32(row["SiparisSeriID"]);
                seri.SeriAdi = row["SeriAdi"].ToString();

                seriList.Add(seri);
            }

            return seriList;
        }
    }
}

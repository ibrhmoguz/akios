using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebFrame.Business;
using WebFrame.DataAccess;
using System.Data;
using Oracle.DataAccess.Client;

namespace WebFrame.Logging
{
    public class LogBS:BusinessBase
    {
        
        private IData oData;
        public void InsertBinaryLog(CustomLog log)
        {
            oData = GetDataObject();
            string sql = "INSERT INTO BINARYLOG(ID,TARIH,KULLANICI,DATA,METHOD) VALUES(BINARYLOG_SEQ.NEXTVAL,:PTARIH,:PKULLANICI,:PDATA,:PMETHOD)";

            oData.AddOracleParameter("PTARIH", DateTime.Now, OracleDbType.Date, 1000000);
            oData.AddOracleParameter("PKULLANICI", log.Kullanici, OracleDbType.Varchar2, 50);
            oData.AddOracleParameter("PDATA", new BinaryConverter().ObjectToBinary(log), OracleDbType.Blob, 1000000);
            oData.AddOracleParameter("PMETHOD", log.OperationName, OracleDbType.Varchar2, 200);
            oData.ExecuteScalar(sql, CommandType.Text);
        }

        public DataSet GetBinaryLogByParam(string TcKimlik)
        {
            oData = GetDataObject();
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select * from BINARYLOG ");
            StringBuilder sbWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(TcKimlik))
            {
                sbWhere.Append(" KULLANICI=:PKULLANICI AND");
                oData.AddOracleParameter("PKULLANICI", TcKimlik, OracleDbType.Varchar2, 50);
            }

            if (sbWhere.ToString().Length > 0)
                sb.Append(" WHERE ").Append(sbWhere.ToString().Remove(sbWhere.ToString().Length - 3));

            sb.Append(" ORDER BY TARIH DESC");
            DataSet dt = new DataSet();
            oData.GetRecords(dt, sb.ToString(), CommandType.Text);
            return dt;
        }
    }
}

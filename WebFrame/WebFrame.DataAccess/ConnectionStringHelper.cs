using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using Microsoft.Win32;
using System.Collections.Specialized;
using WebFrame.DataType.Common.Cryptography;


namespace WebFrame.DataAccess
{
    public static class ConnectionStringHelper
    {
        private static Dictionary<string, string> connectionStringCache = new Dictionary<string, string>();
        private static object lockObject = new object();

        public static string GetConnectionString(string sDBConstrName)
        {
            if (string.IsNullOrEmpty(sDBConstrName))
                throw new ArgumentNullException("sDBConstrName", "Bağlantı adı boş veya null olamaz");

            string connStr = string.Empty;

            if (!connectionStringCache.TryGetValue(sDBConstrName, out connStr))
            {
                lock (lockObject)
                {
                    if (!connectionStringCache.TryGetValue(sDBConstrName, out connStr))
                    {
                        connStr = Decrypt(ConfigurationManager.ConnectionStrings[sDBConstrName].ConnectionString);
                        connectionStringCache.Add(sDBConstrName, connStr);
                    }
                }
            }

            return connStr;
        }


        private static string Decrypt(string cipherText)
        {
            return Crytography.Decrypt(cipherText);
        }
    }
}


using System;
using System.Data;
using System.Diagnostics;

namespace WebFrame.DataType.Common.Logging
{
   public interface ILogger
   {
       void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, string userName);

       void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName,
                  string message, string[] extendedProperties, string userName);

       void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName,
                  string message, DataSet kullaniciYetkileri, string[] extendedProperties, string userName);

       void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName,
                  string message, string kullaniciSicil, string pcName, DataSet kullaniciYetkileri,
                  string[] extendedProperties, string userName);

       void WriteAudit(string tableName, string rowID, string columnName, TableOperations operation, string user,
                       string projectName, string oldValue, string newValue, string message);
   }
}

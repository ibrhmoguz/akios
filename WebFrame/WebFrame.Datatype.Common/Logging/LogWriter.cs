using System;
using System.Data.Common;
using System.Text;
using System.Diagnostics;
using System.Collections;
using WebFrame.DataType.Common.Enums;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using WebFrame.DataType.Common.Cryptography;
using System.Configuration;

namespace WebFrame.DataType.Common.Logging
{
    /// <summary>
    /// Log işlemleri için kullanılacak altyapıyı oluşturur.Loglama işlemi varsayılan olarak Oracle veritabanına yapılır. Eğer istenirse sqlserver da seçilebilir.
    /// Bu classa ait metodlar hiç bir şekilde exception fırlatmaz. Eğer veritabanına kayıt yapılırken hata olursa , oluşan hata eventlog a yazılır.
    /// </summary>
    public class LogWriter : ILogger
    {
        public string ConnectionStringName { get; set; }
        private EventLogTraceListener eventLogListener;
        private DbCommand _command;
        private DbConnection _connection;
        private static string parameterMarker;
        private static string logTableName;
        // private string auditTableName;

        internal static string sql = string.Empty;

        private DataProvider database = DataProvider.SqlServer;

        /// <summary>
        /// BU constructor kullanılarak loglama yapılırsa default olarak Oracle veritabanına kayıt yapar.
        /// </summary>
        public LogWriter()
        {

            //eventLogListener = new EventLogTraceListener("Application")
            //                     {
            //                         TraceOutputOptions =
            //                             TraceOptions.Callstack | TraceOptions.DateTime |
            //                             TraceOptions.LogicalOperationStack | TraceOptions.ProcessId |
            //                             TraceOptions.ThreadId | TraceOptions.Timestamp
            //                     };

            //LogToOracle();
            this.ConnectionStringName = "AkiosConnectionString";
            LogToSqlServer();
        }
        public LogWriter(string ConnectionStringName)
        {

            //eventLogListener = new EventLogTraceListener("Application")
            //{
            //    TraceOutputOptions =
            //        TraceOptions.Callstack | TraceOptions.DateTime |
            //        TraceOptions.LogicalOperationStack | TraceOptions.ProcessId |
            //        TraceOptions.ThreadId | TraceOptions.Timestamp
            //};
            this.ConnectionStringName = ConnectionStringName;
            //LogToOracle();
        }

        /// <summary>
        /// Bu constructor kullanılarak Oracle ve SqlServer veritabanına loglama yapılabilir.
        /// </summary>
        public LogWriter(DataProvider provider)
        {

            //eventLogListener = new EventLogTraceListener("Application")
            //{
            //    TraceOutputOptions =
            //        TraceOptions.Callstack | TraceOptions.DateTime |
            //        TraceOptions.LogicalOperationStack | TraceOptions.ProcessId |
            //        TraceOptions.ThreadId | TraceOptions.Timestamp
            //};


            switch (provider)
            {
                case DataProvider.Oracle:
                    LogToOracle();
                    break;
                case DataProvider.SqlServer:
                    LogToSqlServer();
                    break;

            }

            database = provider;
        }

        /// <summary>
        /// Oluşan hatayı veritabanına yazar.Eğer veritabanına yazılamıyorsa işletim sistemine ait eventlog tablosuna yazar.
        /// </summary>
        /// <param name="moduleId">Proje adı</param>
        /// <param name="eventType">Loga yazılacak işlemin tipi</param>
        /// <param name="ex">Oluşan hata (Exception)</param>
        /// <param name="pageUrl">Hatanın oluştuğu sayfa</param>
        /// <param name="methodName">Hatanın oluştuğu metod</param>
        /// <param name="message">hata ile ilgili mesaj</param>
        public void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, string userName)
        {
            Write(moduleId, eventType, ex, pageUrl, methodName, message, "", "", null, null, userName);
        }

        /// <summary>
        /// Oluşan hatayı veritabanına yazar.Eğer veritabanına yazılamıyorsa işletim sistemine ait eventlog tablosuna yazar.
        /// </summary>
        /// <param name="moduleId">Proje adı</param>
        /// <param name="eventType">Loga yazılacak işlemin tipi</param>
        /// <param name="ex">Oluşan hata (Exception)</param>
        /// <param name="pageUrl">Hatanın oluştuğu sayfa</param>
        /// <param name="methodName">Hatanın oluştuğu metod</param>
        /// <param name="message">hata ile ilgili mesaj</param>
        /// <param name="extendedProperties">Varsa extra parametreler</param>
        public void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, string[] extendedProperties, string userName)
        {
            Write(moduleId, eventType, ex, pageUrl, methodName, message, "", "", null, extendedProperties, userName);
        }

        /// <summary>
        /// Oluşan hatayı veritabanına yazar.Eğer veritabanına yazılamıyorsa işletim sistemine ait eventlog tablosuna yazar.
        /// </summary>
        /// <param name="moduleId">Proje adı</param>
        /// <param name="eventType">Loga yazılacak işlemin tipi</param>
        /// <param name="ex">Oluşan hata (Exception)</param>
        /// <param name="pageUrl">Hatanın oluştuğu sayfa</param>
        /// <param name="methodName">Hatanın oluştuğu metod</param>
        /// <param name="message">hata ile ilgili mesaj</param>
        /// <param name="kullaniciYetkileri">Varsa kişiye ait yetkiler</param>
        /// <param name="extendedProperties">Varsa extra parametreler</param>
        public void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, DataSet kullaniciYetkileri, string[] extendedProperties, string userName)
        {
            Write(moduleId, eventType, ex, pageUrl, methodName, message, "", "", kullaniciYetkileri, extendedProperties, userName);
        }

        /// <summary>
        /// Oluşan hatayı veritabanına yazar.Eğer veritabanına yazılamıyorsa işletim sistemine ait eventlog tablosuna yazar.
        /// </summary>
        /// <param name="moduleId">Proje adı</param>
        /// <param name="eventType">Loga yazılacak işlemin tipi</param>
        /// <param name="ex">Oluşan hata (Exception)</param>
        /// <param name="pageUrl">Hatanın oluştuğu sayfa</param>
        /// <param name="methodName">Hatanın oluştuğu metod</param>
        /// <param name="message">hata ile ilgili mesaj</param>
        /// <param name="kullaniciYetkileri">Varsa kişiye ait yetkiler</param>
        /// <param name="extendedProperties">Varsa extra parametreler</param>
        public void Write(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, string kullaniciSicil, string pcName, DataSet kullaniciYetkileri, string[] extendedProperties, string userName)
        {
            var enumName = Enum.GetName(typeof(AppModules), moduleId);
            var eventName = Enum.GetName(typeof(EventLogEntryType), eventType);
            var parsedException = "Message: " + ex.Message + " StackTrace: " + ex.StackTrace + " Source: " + ex.Source;
            if (ex.InnerException != null)
                parsedException += "Inner Exception Message: " + ex.InnerException.Message + " StackTrace: " + ex.InnerException.StackTrace + " Source: " + ex.InnerException.Source;
            var yetkiler = ParseYetkiler(kullaniciYetkileri);
            var properties = ParseProperties(extendedProperties);
            StringBuilder sb = new StringBuilder();


            if (!string.IsNullOrEmpty(this.ConnectionStringName))
            {
                LogWriter.sql = string.Format(@"INSERT INTO {0}([MODULEID]
                                                               ,[EVENTLOGENTRYTYPEID]
                                                               ,[EXCEPTION]
                                                               ,[PAGEURL]
                                                               ,[METHODNAME]
                                                               ,[MESSAGE]
                                                               ,[USERIDENTITY]
                                                               ,[PCNAME]
                                                               ,[USERAUTHORITY]
                                                               ,[EXTENDEDPROPERTIES]
                                                               ,[USERNAME]
                                                               ,[DATE]) 
                                                 VALUES({1}p1,{1}p2,{1}p3,{1}p4,{1}p5,{1}p6,{1}p7,{1}p8,{1}p9,{1}p10,{1}p11,{1}p12)", logTableName, parameterMarker);
                #region Veritabanı hazırlıkları

                _command.Parameters.Clear();

                AddParameter("p1", (int)moduleId, DbType.Int32);
                AddParameter("p2", (int)eventType, DbType.Int32);
                AddParameter("p3", parsedException, DbType.String);
                AddParameter("p4", pageUrl, DbType.String);
                AddParameter("p5", methodName, DbType.String);
                AddParameter("p6", message, DbType.String);
                AddParameter("p7", kullaniciSicil, DbType.String);
                AddParameter("p8", pcName, DbType.String);
                AddParameter("p9", yetkiler, DbType.String);
                AddParameter("p10", properties, DbType.String);
                AddParameter("p11", userName, DbType.String);
                AddParameter("p12", DateTime.Now, DbType.DateTime);

                try
                {
                    _command.CommandText = sql;
                    _connection.Open();
                    _command.ExecuteNonQuery();

                    /*
                    if (!EventLog.SourceExists(enumName))
                        EventLog.CreateEventSource(enumName, enumName);
                     * */
                }
                catch
                {
                    /*
                    //veritabanına yazdırılamazsa eventloga yazdır
                    #region Eventlog Hazırlıkları


                    sb.Append("Proje Adı :");
                    sb.Append(enumName);
                    sb.Append(Environment.NewLine);

                    sb.Append("Yapılan işlem :");
                    sb.Append(eventName);
                    sb.Append(Environment.NewLine);


                    sb.Append("PC Adı :");
                    sb.Append(pcName);
                    sb.Append(Environment.NewLine);

                    sb.Append("Kullanıcı Sicil :");
                    sb.Append(kullaniciSicil);
                    sb.Append(Environment.NewLine);

                    sb.Append("Mesaj :");
                    sb.Append(message);
                    sb.Append(Environment.NewLine);

                    sb.Append("Sayfa URL :");
                    sb.Append(pageUrl);
                    sb.Append(Environment.NewLine);


                    sb.Append(parsedException);

                    sb.Append(yetkiler);

                    sb.Append(properties);

                    #endregion

                    eventLogListener.EventLog.Source = enumName;
                    eventLogListener.EventLog.WriteEntry(sb.ToString(), eventType, 20000);
                    eventLogListener.Flush();
                    eventLogListener.Close();
                     * */
                }
                finally
                {
                    _connection.Close();
                }



                #endregion
            }
            /*else
            {
                sb.Append("Proje Adı :");
                sb.Append(enumName);
                sb.Append(Environment.NewLine);

                sb.Append("Yapılan işlem :");
                sb.Append(eventName);
                sb.Append(Environment.NewLine);


                sb.Append("PC Adı :");
                sb.Append(pcName);
                sb.Append(Environment.NewLine);

                sb.Append("Kullanıcı Sicil :");
                sb.Append(kullaniciSicil);
                sb.Append(Environment.NewLine);

                sb.Append("Mesaj :");
                sb.Append(message);
                sb.Append(Environment.NewLine);

                sb.Append("Sayfa URL :");
                sb.Append(pageUrl);
                sb.Append(Environment.NewLine);


                sb.Append(parsedException);

                sb.Append(yetkiler);

                sb.Append(properties);



                eventLogListener.EventLog.Source = enumName;
                eventLogListener.EventLog.WriteEntry(sb.ToString(), eventType, 20000);
                eventLogListener.Flush();
                eventLogListener.Close();
            }*/
        }

        private void LogToSqlServer()
        {
            _connection = new SqlConnection(ConnectionStringHelper.GetConnectionString(this.ConnectionStringName));
            _command = new SqlCommand("");
            _command.Connection = _connection;
            parameterMarker = "@";
            logTableName = "dbo.HATA";
            //auditTableName = "";
        }

        private void LogToOracle()
        {
            _connection = new OracleConnection(ConnectionStringHelper.GetConnectionString(this.ConnectionStringName));
            _command = new OracleCommand("");
            _command.Connection = _connection;

            parameterMarker = ":";
            logTableName = "TARIHCE.EXCEPTIONLOGTABLE";
            //auditTableName = "TARIHCE.TABLEAUDITS";
        }

        private int AddParameter(string parametername, object value, DbType dbType)
        {
            DbParameter parameter = _command.CreateParameter();
            parameter.ParameterName = database == DataProvider.Oracle ? parametername : parametername.Insert(0, "@");
            parameter.DbType = dbType;
            parameter.Value = value == null ? DBNull.Value : value;

            return _command.Parameters.Add(parameter);
        }

        private string ParseException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            if (ex != null)
            {

                sb.Append("Hata Mesajı : ");
                sb.Append(ex.Message);
                sb.Append(Environment.NewLine);

                sb.Append("Metod Adı : ");
                sb.Append(ex.TargetSite.ToString());
                sb.Append(Environment.NewLine);

                sb.Append("Modül Adı : ");
                sb.Append(ex.TargetSite.Module);
                sb.Append(Environment.NewLine);

                if (ex.Data.Count > 0)
                {

                    foreach (DictionaryEntry item in ex.Data)
                    {
                        sb.Append(item.Key);
                        sb.Append(" : ");
                        sb.Append(item.Value);
                        sb.Append(Environment.NewLine);
                    }

                }

                if (ex.InnerException != null)
                {

                    sb.Append("InnerException Hata Mesajı : ");
                    sb.Append(ex.InnerException.Message);
                    sb.Append(Environment.NewLine);

                    sb.Append("InnerException Metod Adı : ");
                    sb.Append(ex.InnerException.TargetSite.ToString());
                    sb.Append(Environment.NewLine);

                    sb.Append("InnerException Modül Adı : ");
                    sb.Append(ex.InnerException.TargetSite.Module);
                    sb.Append(Environment.NewLine);

                    if (ex.InnerException.Data.Count > 0)
                    {

                        foreach (DictionaryEntry item in ex.InnerException.Data)
                        {
                            sb.Append(item.Key);
                            sb.Append(" : ");
                            sb.Append(item.Value);
                            sb.Append(Environment.NewLine);
                        }

                    }

                    sb.Append("InnerException Stack trace : ");
                    sb.Append(ex.InnerException.StackTrace);
                    sb.Append(Environment.NewLine);

                }

                sb.Append("Stack trace : ");
                sb.Append(ex.StackTrace);
                sb.Append(Environment.NewLine);

            }

            return sb.ToString();

        }

        private string ParseYetkiler(DataSet kullaniciYetkileri)
        {
            StringBuilder sb = new StringBuilder();

            if (kullaniciYetkileri != null && kullaniciYetkileri.Tables[0].Rows.Count > 0)
            {
                sb.Append("Kullanıcı Yetkileri :");
                sb.Append(Environment.NewLine);

                for (int i = 0; i < kullaniciYetkileri.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < kullaniciYetkileri.Tables[0].Rows[i].ItemArray.Length; j++)
                    {
                        sb.Append(kullaniciYetkileri.Tables[0].Columns[j].ColumnName);
                        sb.Append(" : ");
                        sb.Append(kullaniciYetkileri.Tables[0].Rows[i].ItemArray[j]);
                        sb.Append(Environment.NewLine);
                    }


                }
            }

            return sb.ToString();

        }

        private string ParseProperties(params string[] extendedProperties)
        {
            StringBuilder sb = new StringBuilder();


            if (extendedProperties != null && !string.IsNullOrEmpty(extendedProperties[0]))
            {
                foreach (var item in extendedProperties)
                {
                    sb.Append(item);

                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString();

        }

        /// <summary>
        /// Tablo üzerinde yapılan işlemlerin(insert ,update ,delete) loglanması amacıyla(Auditing) hazırlanmıştır.
        /// </summary>
        /// <param name="tableName">Tablo Adı</param>
        /// <param name="rowID">Veriye ait ID</param>
        /// <param name="columnName">Sütun Adı</param>
        /// <param name="operation">Tablo üzerinde yapılan işlem</param>
        /// <param name="user">İşlemi yapan kullanıcı</param>
        /// <param name="projectName">İşlemi yapan kullanıcıya ait pc adı</param>
        /// <param name="oldValue">tablodaki eski değer</param>
        /// <param name="newValue">tablodaki yeni değer</param>
        /// <param name="message">Varsa işlem ile ilgili mesaj</param>
        public void WriteAudit(string tableName, string rowID, string columnName, TableOperations operation, string user, string projectName, string oldValue, string newValue, string message)
        {
            //LogWriter.sql = string.Format("insert into {0}(TABLENAME,CHANGEDROWID,COLUMNNAME,TABLEOPERATIONID,USERNAME,PROJECTNAME,OLDVALUE,NEWVALUE,MESSAGE) values({1}p1,{1}p2,{1}p3,{1}p4,{1}p5,{1}p6,{1}p7,{1}p8,{1}p9)", auditTableName, parameterMarker);

            //var operationName = Enum.GetName(typeof(TableOperations), operation);
            //StringBuilder sb = new StringBuilder();

            //#region Veritabanı hazırlıkları

            //_command.Parameters.Clear();

            //AddParameter("p1",tableName,DbType.String);
            //AddParameter("p2", rowID, DbType.String);
            //AddParameter("p3", columnName, DbType.String);
            //AddParameter("p4",  (int)operation, DbType.Int32);
            //AddParameter("p5", user, DbType.String);
            //AddParameter("p6", projectName, DbType.String);
            //AddParameter("p7", oldValue, DbType.String);
            //AddParameter("p8", newValue, DbType.String);
            //AddParameter("p9", message, DbType.String);

            //try
            //{
            //    _command.CommandText = sql;
            //    _connection.Open();
            //    _command.ExecuteNonQuery();


            //}
            //catch
            //{
            //    //veritabanına yazdırılamazsa eventloga yazdır
            //    #region Eventlog Hazırlıkları


            //    sb.Append("Tablo adı :");
            //    sb.Append(tableName);
            //    sb.Append(Environment.NewLine);

            //    sb.Append("Yapılan işlem :");
            //    sb.Append(operationName);
            //    sb.Append(Environment.NewLine);


            //    sb.Append("Kullanıcı :");
            //    sb.Append(user);
            //    sb.Append(Environment.NewLine);

            //    sb.Append("Proje Adı :");
            //    sb.Append(projectName);
            //    sb.Append(Environment.NewLine);

            //    sb.Append("Eski değer :");
            //    sb.Append(message);
            //    sb.Append(Environment.NewLine);

            //    sb.Append("Yeni değer :");
            //    sb.Append(message);
            //    sb.Append(Environment.NewLine);

            //    sb.Append("Mesaj :");
            //    sb.Append(message);
            //    sb.Append(Environment.NewLine);


            //    #endregion

            //    eventLogListener.EventLog.Source = "Application";
            //    eventLogListener.EventLog.WriteEntry(sb.ToString(), EventLogEntryType.FailureAudit, 20000);
            //    eventLogListener.Flush();
            //    eventLogListener.Close();
            //}
            //finally
            //{
            //    _connection.Close();
            //}



            // #endregion

        }

    }

    public static class ConnectionStringHelper
    {
        private static Dictionary<string, string> connectionStringCache = new Dictionary<string, string>();

        public static string GetConnectionString(string sDBConstrName)
        {
            if (string.IsNullOrEmpty(sDBConstrName))
                throw new ArgumentNullException("sDBConstrName", "Bağlantı adı boş veya null olamaz");

            string connStr = string.Empty;

            if (!connectionStringCache.TryGetValue(sDBConstrName, out connStr))
            {
                connStr = Decrypt(ConfigurationManager.ConnectionStrings[sDBConstrName].ConnectionString);
                connectionStringCache.Add(sDBConstrName, connStr);

            }
            return connStr;

        }

        private static string Decrypt(string cipherText)
        {

            return Crytography.Decrypt(cipherText);
        }
    }
}

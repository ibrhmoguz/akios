using System;
using System.Diagnostics;
using WebFrame.DataAccess;
using WebFrame.DataType.Common.Attributes;
using WebFrame.DataType.Common.Logging;
using WebFrame.DataType.Common.Enums;
using System.Data;
using NLog;

namespace WebFrame.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessBase
    {
       
        Logger nlogLogger;
        protected IData mDataObject { get; set; }
       // protected bool mIsRoot = false;
        /// <summary>
        /// 
        /// </summary>
        public bool mIsRoot { get; set; }
        ServiceConnectionNameAttribute serviceAttribute;


        private int nestedLevel = 0;
        /// <summary>
        /// 
        /// </summary>
        public BusinessBase()
        {

            mDataObject = null;
            mIsRoot = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool IsRoot()
        {

            if (nestedLevel > 0)
            {
                nestedLevel--;
                return false;
            }
            return mIsRoot;

        }


        /// <summary>
        /// Bu property; farklý tablespace veya farklý veritabanlarý üzerinde yapýlan iþlemleri tek bir transaction altýnda birleþtirmek amacýyla eklenmiþtir.
        /// Örnek için KullaniciBS.SifreDegisikligi metoduna bakabilirsiniz. Ýlgili metodta iki farklý Oracle tablespace ve birde sqlserver
        /// ayný transaction altýnda kullanýlmýþ ve herhangi bir hata olursa bütün veritabanlarýndaki iþlemler geri alýnabilmiþtir.
        /// </summary>
        public IData CurrentDataObject
        {
            get { return mDataObject; }
        }

        /// <summary>
        /// Log iþlemleri için kullanýlýr. Eðer kendi loglama iþleminizi yapmak istiyorsanýz <see cref="ILogger"/> interface kullanarak
        /// kendi sýnýfýnýzý oluþturabilirsiniz. Bu property kendi loglama sýnýflarýný kullanmanýza olanak saðlar.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// NLog altyapýsýný kullanarak loglama saðlar. Konfigürasyon dosyasýna istenilen ayarlar eklenerek
        /// istenilen kaynaða (dosya, veritabaný vs.) loglama yapýlabilir.
        /// </summary>
        public Logger NLogLogger
        {
            get
            {
                if(nlogLogger==null)
                    nlogLogger=LogManager.GetCurrentClassLogger();

                return nlogLogger;
            }
            set
            {
                nlogLogger=value;
            }
        }
        /// <summary>
        /// Ýlgili business classa ait baðlantý bilgilerini alan metod
        /// </summary>
        /// <returns>Baðlantý bilgilerini içeren class</returns>
        private ServiceConnectionNameAttribute GetServiceConfiguration()
        {
            if (this.GetType().IsDefined(typeof(ServiceConnectionNameAttribute), false))
            {
                serviceAttribute = (ServiceConnectionNameAttribute)this.GetType().GetCustomAttributes(typeof(ServiceConnectionNameAttribute), false)[0];
            }

            return serviceAttribute;
        }

        /// <summary>
        /// Üzerinde çalýþýlan projeye ait instance döndürür (Örn. BilgeUserBS)
        /// </summary>
        /// <param name="connectionStringName">Baðlantý adý</param>
        /// <param name="provider">Hangi veritabanýna baðlanýlacaðý</param>
        /// <returns>Oracle veya SqlServer veritabanýyla çalýþabilen projeye ait nesne (Örn. BilgeUserBS)</returns>
        protected IData GetDataObject(string connectionStringName, DataProvider provider)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                return this.GetDataObject();

            if (mDataObject == null)
            {
                mDataObject = DataFactory.GetDataObject(connectionStringName, provider);
                if (mDataObject.IsTransactional)
                    mIsRoot = true;
            }
            else
            {
                //ayni BS ten kendi iclerinde transaction olan 2 farkli metodun icice cagrildigi durumlar icin eklendi.
                if (mDataObject.IsTransactional && mIsRoot)
                {
                    nestedLevel++;

                    return mDataObject;

                }
                if (mDataObject.IsTransactional)
                {

                    nestedLevel++;

                    mIsRoot = true;
                    return mDataObject;

                }
                else if (mDataObject.IsTransactional && !mIsRoot)
                {
                    return mDataObject;
                }
                else
                {
                    mDataObject = DataFactory.GetDataObject(connectionStringName, provider);
                    if (mDataObject.IsTransactional)
                        mIsRoot = true;
                }
            }
            return mDataObject;

        }

        /// <summary>
        /// Üzerinde çalýþýlan projeye ait instance döndürür (Örn. BilgeUserBS)
        /// </summary>
        /// <param name="connectionStringName">Baðlantý adý</param>
        /// <returns>Oracle veya SqlServer veritabanýyla çalýþabilen projeye ait nesne (Örn. BilgeUserBS)</returns>
        protected IData GetDataObject(string connectionStringName)
        {

            if (string.IsNullOrEmpty(connectionStringName))
                return this.GetDataObject();

            if (mDataObject == null)
            {
                mDataObject = DataFactory.GetDataObject(connectionStringName, GetServiceConfiguration().DataProvider);
                if (mDataObject.IsTransactional)
                    mIsRoot = true;
            }
            else
            {
                //ayni BS ten kendi iclerinde transaction olan 2 farkli metodun icice cagrildigi durumlar icin eklendi.
                if (mDataObject.IsTransactional && mIsRoot)
                {
                    nestedLevel++;

                    return mDataObject;

                }
                if (mDataObject.IsTransactional)
                {

                    nestedLevel++;

                    mIsRoot = true;
                    return mDataObject;

                }
                else if (mDataObject.IsTransactional && !mIsRoot)
                {
                    return mDataObject;
                }
                else
                {
                    mDataObject = DataFactory.GetDataObject(connectionStringName, GetServiceConfiguration().DataProvider);
                    if (mDataObject.IsTransactional)
                        mIsRoot = true;
                }
            }
            return mDataObject;

        }

        /// <summary>
        /// Üzerinde çalýþýlan projeye ait instance döndürür (Örn. BilgeUserBS)
        /// </summary>
        /// <returns>Oracle veya SqlServer veritabanýyla çalýþabilen projeye ait nesne (Örn. BilgeUserBS)</returns>
        protected IData GetDataObject()
        {

            string connection = "";
            if (this.GetType().IsDefined(typeof(ServiceConnectionNameAttribute), false))
            {
                ServiceConnectionNameAttribute connectionNameAtt = (ServiceConnectionNameAttribute)this.GetType().GetCustomAttributes(typeof(ServiceConnectionNameAttribute), false)[0];
                connection = connectionNameAtt.ServiceConnectionName;
            }

            return this.GetDataObject(connection);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T CreateObject<T>()
        {
            T oBusinessObject = default(T);
            oBusinessObject = Activator.CreateInstance<T>();
            if ((mDataObject != null) && (mDataObject.IsTransactional))
            {
                BusinessBase b = oBusinessObject as BusinessBase;
                b.mDataObject = this.mDataObject;
                // b.mIsRoot = false;
            }
            return oBusinessObject;
        }

        #region "Exception Handling"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        protected void HandleSystemException(Exception ex)
        {
            HandleGumrukWebException<Exception>(ex);
          
        }

        /// <summary>
        /// Oluþan hatalarýn yakalanýp iþlenmesi için kullanýlan metod
        /// </summary>
        /// <typeparam name="T">Exception tipinden bir nesne</typeparam>
        /// <param name="ex">GumrukWebException tipinden bir nesne</param>
        protected void HandleGumrukWebException<T>(T ex) where T : Exception
        {
            //1. burada hata loglanabilir
            //2. hata bilgileri veri tabanýndan cekilip ona gore kullanýcýya bilgi gösterilebilir AYKD_TANIM
            //3. tanýma iliþkin eylem varsa aksiyon almak icin SMS atma email atma iþlemleri yapýlabilir.

            //GumrukApplicationException resultEx;
            //if (ex is GumrukApplicationException)
            //{
            //    resultEx = (GumrukApplicationException)Convert.ChangeType(ex, typeof(GumrukApplicationException));
            //}
            //else if (ex is SQLKomutCalismaHatasi)
            //{
            //    GumrukApplicationException e = new GumrukApplicationException();
            //    e.ExceptionType = EnumExceptionType.DBEXCEPTION;
            //    e.ErrorExternalMessage = ex.Message;
            //    e.ErrorInternalMessage = "Komut çalýþtýrma hatasý";
            //    e.ErrorCode = "DB";
            //    resultEx = e;
            //}
            //else if (ex is VeritabaniBaglantiHatasi)
            //{
            //    GumrukApplicationException e = new GumrukApplicationException();
            //    e.ExceptionType = EnumExceptionType.DBEXCEPTION;
            //    e.ErrorExternalMessage = ex.Message ;
            //    e.ErrorInternalMessage = "Veritabaný hatasý";
            //    e.ErrorCode = "DB";
            //    resultEx = e;

            //}
            //else if (ex is ApplicationException)
            //{
            //    GumrukApplicationException e = new GumrukApplicationException();
            //    e.ExceptionType = EnumExceptionType.DBEXCEPTION;
            //    e.ErrorExternalMessage = ex.Message;
            //    e.ErrorInternalMessage = "Sunucu uygulama hatasý";
            //    e.ErrorCode = "APPSRV";
            //    resultEx = e;

            //}
            //else
            //{
            //    GumrukApplicationException e = new GumrukApplicationException();
            //    e.ExceptionType = EnumExceptionType.SYSTEMEXCEPTION;
            //    e.ErrorExternalMessage = ex.ToString();
            //    e.ErrorInternalMessage = "Bilinmeyen hata";
            //    e.ErrorCode = "UNKNOWN";
            //    resultEx = e;

            //}

            //LogWriter logger = new LogWriter();
            //logger.Write(GumrukModules.Genel, 0, EventLogEntryType.Error, this.GetType().FullName, ex.Message);

            throw ex;

        }

        /// <summary>
        /// Oluþan hatayý veritabanýna yazar.Eðer veritabanýna yazýlamýyorsa iþletim sistemine ait eventlog tablosuna yazar.
        /// </summary>
        /// <param name="moduleId">Proje adý</param>
        /// <param name="eventType">Loga yazýlacak iþlemin tipi</param>
        /// <param name="ex">Oluþan hata (Exception)</param>
        /// <param name="pageUrl">Hatanýn oluþtuðu sayfa</param>
        /// <param name="methodName">Hatanýn oluþtuðu metod</param>
        /// <param name="message">hata ile ilgili mesaj</param>
        /// <param name="yetkiler">Varsa kiþiye ait yetkiler</param>
        /// <param name="extraParameters">Varsa extra parametreler</param>
        protected void Log(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, DataSet yetkiler, params string[] extraParameters)
        {
            Log(moduleId, eventType, ex,pageUrl, methodName, message, "", "", yetkiler, extraParameters);
        }

        /// <summary>
        /// Oluþan hatayý veritabanýna yazar.Eðer veritabanýna yazýlamýyorsa iþletim sistemine ait eventlog tablosuna yazar.
        /// </summary>
        /// <param name="moduleId">Proje adý</param>
        /// <param name="eventType">Loga yazýlacak iþlemin tipi</param>
        /// <param name="ex">Oluþan hata (Exception)</param>
        /// <param name="pageUrl">Hatanýn oluþtuðu sayfa</param>
        /// <param name="methodName">Hatanýn oluþtuðu metod</param>
        /// <param name="message">hata ile ilgili mesaj</param>
        /// <param name="extraParameters">Varsa extra parametreler</param>
        protected void Log(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, params string[] extraParameters)
        {
            Log(moduleId, eventType, ex, pageUrl, methodName, message, "", "", null, extraParameters);

        }

        /// <summary>
        /// Oluþan hatayý veritabanýna yazar.Eðer veritabanýna yazýlamýyorsa iþletim sistemine ait eventlog tablosuna yazar.
        /// </summary>
        /// <param name="moduleId">Proje adý</param>
        /// <param name="eventType">Loga yazýlacak iþlemin tipi</param>
        /// <param name="ex">Oluþan hata (Exception)</param>
        /// <param name="pageUrl">Hatanýn oluþtuðu sayfa</param>
        /// <param name="methodName">Hatanýn oluþtuðu metod</param>
        /// <param name="message">hata ile ilgili mesaj</param>
        /// <param name="kullaniciSicil">Varsa kullanýcýnýn sicili</param>
        /// <param name="pcName">Ýþlemi yapan kullanýcýya ait pc adý</param>
        /// <param name="yetkiler">Varsa kiþiye ait yetkiler</param>
        /// <param name="extraParameters">Varsa extra parametreler</param>
        protected void Log(AppModules moduleId, EventLogEntryType eventType, Exception ex, string pageUrl, string methodName, string message, string kullaniciSicil, string pcName, DataSet yetkiler, params string[] extraParameters)
        {
            if(Logger==null)
                Logger = new LogWriter(GetServiceConfiguration().DataProvider);

            Logger.Write(moduleId, eventType, ex, pageUrl, methodName, message, kullaniciSicil, pcName, yetkiler, extraParameters, "");
        }

        /// <summary>
        /// Tablo üzerinde yapýlan iþlemlerin(insert ,update ,delete) loglanmasý amacýyla hazýrlanmýþtýr.
        /// </summary>
        /// <param name="tableName">Tablo Adý</param>
        /// <param name="rowid">Veriye ait ID</param>
        /// <param name="columnName">Sütun Adý</param>
        /// <param name="operation">Tablo üzerinde yapýlan iþlem</param>
        /// <param name="user">Ýþlemi yapan kullanýcý</param>
        /// <param name="projectname">Üzerinde çalýþýlan proje adý</param>
        /// <param name="oldvalue">tablodaki eski deðer</param>
        /// <param name="newvalue">tablodaki yeni deðer</param>
        /// <param name="message">Varsa iþlem ile ilgili mesaj</param>
        protected void LogAudit(string tableName, string rowid, string columnName, TableOperations operation, string user, string projectname,string oldvalue,string newvalue,string message)
        {

            if (Logger == null)
                Logger = new LogWriter(GetServiceConfiguration().DataProvider);

            Logger.WriteAudit(tableName, rowid, columnName, operation, user, projectname, oldvalue, newvalue, message);
        }

        #endregion
    }
}
using System;
using System.Data;

using System.Collections.Generic;
using System.EnterpriseServices;
using System.Transactions;
using Oracle.DataAccess.Client;

namespace WebFrame.DataAccess
{
    /// <summary>
    /// Veritabanı işlemleri için kullanılan sınıfları temsil eder.
    /// </summary>
    public interface IData
    {
        #region Properties
        /// <summary>
        /// İşlemin transaction ile yapılıp yapılmadığını belirtir.
        /// </summary>
        bool IsTransactional { get; }
        /// <summary>
        /// Veritabanına bağlanılırken kullanılan bağlantı cümlesinin adı. 
        /// </summary>
        string ConnectionName { get; }

        #endregion


        #region methods
        /// <summary>
        /// Transaction işlemini başlatır.
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Transaction işlemini bitirir.
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// Transaction işlemini geri alır.
        /// </summary>
        void RollbackTransaction();


        /// <summary>
        /// Geriye tek bir integer değer döndüren sorgulamalar için kullanılır. Örneğin
        /// Sum, Count, Avarage gibi.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <returns>İşlem sonucu.</returns>
        int ExecuteStatement(string ssql);

        /// <summary>
        /// Geriye tek bir integer değer döndüren sorgulamalar için kullanılır. Örneğin
        /// Sum, Count, Avarage gibi.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>İşlem sonucu.</returns>
        int ExecuteStatement(string ssql, CommandType commandType);
        /// <summary>
        /// Dataseti prosedür kullanarak doldurmak amacıyla kullanılır. Bu komut sadece prosedür ile çalışır.
        /// </summary>
        /// <param name="storedprocedureName">Çalıştırılacak prosedür adı.</param>
        /// <param name="ds">Doldurulacak olan dataset</param>
        void ExecuteStatement(string storedprocedureName, DataSet ds);
        /// <summary>
        /// Gönderilen komut içerisinde kullanılan prosedür veya fonksiyona ait output parametre 
        /// değerlerini almak için kullanılır.
        /// </summary>
        /// <param name="storedProcedureOrFunctionName">Çalışıtırılacak prosedür veya fonksiyon ismi.</param>
        /// <returns>Fonsiyon veya prosedüre ait output parametrelerin adı ve dönüş değerleri</returns>
        Dictionary<string, object> ExecuteStatementUDI(string storedProcedureOrFunctionName);
        /// <summary>
        /// Geriye  tek bir değer döndüren sql komutlarını çalıştırmak için kullanılır.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="komutTipi">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>Komutun çalışması sonucu elde edilen değer</returns>
        object ExecuteScalar(string ssql, CommandType commandType);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <returns>Sorgu sonucu doldurulan dataset</returns>
        DataSet GetRecords(string ssql);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        void GetRecords(DataSet ds, string ssql);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="stable">Üzerinde çalışılan tablo adı.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        void GetRecords(DataSet ds, string ssql, string stable, CommandType commandType);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="stable">Üzerinde çalışılan tablo adı.</param>
        void GetRecords(DataSet ds, string ssql, string stable);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        void GetRecords(DataTable dt, string ssql);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        void GetRecords(DataSet ds, string ssql, CommandType commandType);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>Sorgu sonucu doldurulan dataset</returns>
        DataSet GetRecords(string ssql, CommandType commandType);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        void GetRecords(DataTable dt, string ssql, CommandType commandType);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="withSchema">Tabloya ait şema bilgisine ihtiyaç duyulur ise bu parametre kullanılır</param>
        void GetRecords(DataTable dt, string ssql, bool withSchema);
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <param name="withSchema">Tabloya ait şema bilgisine ihtiyaç duyulur ise bu parametre kullanılır</param>
        void GetRecords(DataTable dt, string ssql, CommandType commandType, bool withSchema);

        /// <summary>
        /// Parametre olarak verilen Dataset üzerinde yapılan işlemlerden sadece insert ve update 
        /// komutlarını çalıştırır.Delete komutu çalıştırılmaz.
        /// </summary>
        /// <param name="ds">Üzerinde çalışılan dataset</param>
        void InsertUpdate(DataSet ds);
        /// <summary>
        /// Dataset üzerinde yapılan insert, update, delete işlemlerini veritabanına göndermek için kullanılır.
        /// </summary>
        /// <param name="ds">Üzerinde işlem yapılan dataset</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="sTable">Üzerinde çalışılan tablo adı.</param>
        void Update(DataSet ds, string ssql, string sTable);
        /// <summary>
        /// Dataset üzerinde yapılan insert, update, delete işlemlerini veritabanına göndermek için kullanılır.
        /// </summary>
        /// <param name="ds">Üzerinde işlem yapılan dataset</param>
        void Update(DataSet ds);
        /// <summary>
        /// Datatable üzerinde yapılan insert, update, delete işlemlerini veritabanına göndermek için kullanılır.
        /// </summary>
        /// <param name="dt">Üzerinde işlem yapılan Datatable</param>
        void Update(DataTable dt);

        /// <summary>
        /// Output tipindeki SqlServer parametreleri eklemek için kullanılır.Output parametrelerde size olmak zorundadır.
        /// </summary>
        /// <param name="parameterName">Parametre adı.</param>
        /// <param name="dbType">SqlServer'a özel parametre tipi.</param>
        /// <param name="direction">Parametre yönü.</param>
        /// <param name="parameterSize">Parametre boyutu.</param>
        void AddSqlParameter(string parameterName, object parameterValue, SqlDbType dbType, ParameterDirection direction, int parameterSize);
        /// <summary>
        /// Input tipindeki SqlServer parametreleri eklemek için kullanılır.
        /// </summary>
        /// <param name="parameterName">Parametre adı.</param>
        /// <param name="parameterValue">Veritabanına eklenecek olan değer.</param>
        /// <param name="dbType">SqlServer'a özel parametre tipi.</param>
        /// <param name="parameterSize">Parametre boyutu.</param>
        void AddSqlParameter(string parameterName, object parameterValue, SqlDbType dbType, int parameterSize);

        /// <summary>
        /// Output tipindeki Oracle parametreleri eklemek için kullanılır.Output parametrelerde size olmak zorundadır.
        /// </summary>
        /// <param name="parameterName">Parametre adı.</param>
        /// <param name="dbType">Oracle'a özel parametre tipi.</param>
        /// <param name="direction">Parametre yönü.</param>
        /// <param name="parameterSize">Parametren boyutu.</param>
        void AddOracleParameter(string parameterName, OracleDbType dbType, ParameterDirection direction, int parameterSize);
        /// <summary>
        /// Input tipindeki Oracle parametreleri eklemek için kullanılır.
        /// </summary>
        /// <param name="parameterName">Parametre adı</param>
        /// <param name="parameterValue">Parametrenin değeri.</param>
        /// <param name="dbType">Oracle'a özel parametre tipi.</param>
        /// <param name="parameterSize">Parametren boyutu.</param>
        void AddOracleParameter(string parameterName, object parameterValue, OracleDbType dbType, int parameterSize);



        #endregion
    }
}

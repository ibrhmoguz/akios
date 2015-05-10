using System;
using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;
using IsolationLevel = System.Data.IsolationLevel;
using System.Data.Common;

namespace WebFrame.DataAccess
{
    /// <summary>
    /// Oracle ile ilgili işlemleri yapmak için gerekli metodları barındırır.
    /// </summary>
    class OracleData : IDisposable, IData
    {
        private OracleConnection mConnection;
        private OracleTransaction mTransaction;
        private OracleCommand mCommand;
        private OracleDataAdapter mDataAdapter;
        private bool mIsTransactional;
        Dictionary<string, object> parameterValues;
        List<OracleParameter> parameters;
        private const IsolationLevel ISOLATION_LEVEL = IsolationLevel.ReadCommitted;

        /// <summary>
        /// İşlemin transaction ile yapılıp yapılmadığını belirtir.
        /// </summary>
        public bool IsTransactional
        {
            get
            {
                return mIsTransactional;
            }
        }

        private string connectionName;
        /// <summary>
        /// Veritabanına bağlanılırken kullanılan bağlantı cümlesinin adı. 
        /// </summary>
        public string ConnectionName
        {
            get
            {
                return connectionName;
            }
        }

        private bool disposeEdildi = false;//Dispose methodu ~ tarafından 2. kez çağrılmasın diye kullanılıyor.

        /// <summary>
        /// Oracle veritabanıyla işlem yapmak için gerekli metodların bulunduğu sınıfı oluşturur.
        /// </summary>
        /// <param name="connectionStringName">Veritabanına bağlanmak için kullanılacak bağlantı adı.</param>
        public OracleData(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName", "Bağlantı adı boş olamaz.");

            mConnection = new OracleConnection(ConnectionStringHelper.GetConnectionString(connectionStringName));

            mCommand = new OracleCommand();
            mCommand.BindByName = true;
            mDataAdapter = new OracleDataAdapter(mCommand);

            mCommand.Connection = mConnection;
            parameterValues = new Dictionary<string, object>();
            parameters = new List<OracleParameter>();
            connectionName = connectionStringName;
        }

        public void Dispose()
        {
            if (!disposeEdildi)
            {
                mConnection.Dispose();
                mCommand.Dispose();
                mDataAdapter.Dispose();

                if (mTransaction != null)
                    mTransaction.Dispose();

                disposeEdildi = true;
            }
        }

        /// <summary>
        /// Veritabanı bağlantısını açar.
        /// </summary>
        private void OpenConnection()
        {
            if (mIsTransactional == false)
            {
                try
                {
                    if (mConnection.State == ConnectionState.Closed)
                        mConnection.Open();
                }
                //catch (Exception ex)
                //{
                //    ExceptionthrowHelper(ex);
                //}
                finally { }
            }
        }

        /// <summary>
        /// Veritabanı bağlantısını kapatır.
        /// </summary>
        private void CloseConnection()
        {
            if (mIsTransactional == false)
            {
                try
                {
                    if (mConnection.State == ConnectionState.Open)
                        mConnection.Close();
                }
                //catch (Exception ex)
                //{
                //    ExceptionthrowHelper(ex);
                //}
                finally { }

            }

            ClearCommandParameters();
        }

        /// <summary>
        /// Transaction işlemini başlatır.
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                if (mIsTransactional != true)
                {
                    mConnection.Open();
                    mTransaction = mConnection.BeginTransaction(ISOLATION_LEVEL);
                    mIsTransactional = true;
                }

            }
            //catch (Exception e)
            //{
            //    ExceptionthrowHelper(e);
            //}
            finally { }

        }

        /// <summary>
        /// Transaction işlemini bitirir.
        /// </summary>
        public void CommitTransaction()
        {
            if (mIsTransactional)
            {
                mTransaction.Commit();
                mConnection.Close();
                mCommand.Transaction = null;
                mIsTransactional = false;
            }

            ClearCommandParameters();
        }

        /// <summary>
        /// Transaction işlemini geri alır.
        /// </summary>
        public void RollbackTransaction()
        {
            if (mIsTransactional)
            {
                mTransaction.Rollback();
                mConnection.Close();
                mCommand.Transaction = null;
                mIsTransactional = false;

            }
            ClearCommandParameters();
        }

        /// <summary>
        /// Geriye tek bir integer değer döndüren sorgulamalar için kullanılır. Örneğin
        /// Sum, Count, Avarage gibi.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <returns>İşlem sonucu.</returns>
        public int ExecuteStatement(string ssql)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş string değer verilemez.");
            return ExecuteStatement(ssql, CommandType.Text);
        }

        /// <summary>
        /// Geriye tek bir integer değer döndüren sorgulamalar için kullanılır. Örneğin
        /// Sum, Count, Avarage gibi.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>İşlem sonucu.</returns>
        public int ExecuteStatement(string ssql, CommandType commandType)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş string değer verilemez.");

            this.OpenConnection();
            int result = 0;

            mCommand.CommandType = commandType;
            mCommand.CommandText = ssql;

            AddParameters(mCommand);

            if (mIsTransactional)
            {
                mCommand.Transaction = mTransaction;
            }
            try
            {
                result = mCommand.ExecuteNonQuery();

            }
            //catch (Exception e)
            //{
            //    ExceptionthrowHelper(e);
            //}
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        /// <summary>
        /// Dataseti prosedür kullanarak doldurmak amacıyla kullanılır. Bu komut sadece prosedür ile çalışır.
        /// </summary>
        /// <param name="storedprocedureName">Çalıştırılacak prosedür adı.</param>
        /// <param name="ds">Doldurulacak olan dataset</param>
        public void ExecuteStatement(string storedprocedureName, DataSet ds)
        {
            if (string.IsNullOrEmpty(storedprocedureName))
                throw new ArgumentNullException("storedprocedureName", "Boş string değer verilemez.");

            this.OpenConnection();

            try
            {
                mCommand.CommandText = storedprocedureName;
                mCommand.CommandType = CommandType.StoredProcedure;

                AddParameters(mCommand);

                if (mIsTransactional)
                    mCommand.Transaction = mTransaction;

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(ds);
            }
            //catch (Exception ex)
            //{
            //    ExceptionthrowHelper(ex);
            //}
            finally
            {
                this.CloseConnection();
                mCommand.Transaction = null;
            }
        }


        /// <summary>
        /// Gönderilen komut içerisinde kullanılan prosedür veya fonksiyona ait output parametre 
        /// değerlerini almak için kullanılır.
        /// </summary>
        /// <param name="storedProcedureOrFunctionName">Çalışıtırılacak prosedür veya fonksiyon ismi.</param>
        /// <returns>Fonsiyon veya prosedüre ait output parametrelerin adı ve dönüş değerleri</returns>
        public Dictionary<string, object> ExecuteStatementUDI(string storedProcedureOrFunctionName)
        {
            if (string.IsNullOrEmpty(storedProcedureOrFunctionName))
                throw new ArgumentNullException("storedProcedureOrFunctionName", "Boş string değer verilemez.");

            parameterValues.Clear();
            this.OpenConnection();
            try
            {
                mCommand.CommandText = storedProcedureOrFunctionName;
                mCommand.CommandType = CommandType.StoredProcedure;
                AddParameters(mCommand);

                if (mIsTransactional)
                    mCommand.Transaction = mTransaction;

                mCommand.ExecuteScalar();

                foreach (OracleParameter op in mCommand.Parameters)
                {
                    if (op.Direction != ParameterDirection.Input)
                        parameterValues.Add(op.ParameterName, op.Value);
                }

            }
            //catch (Exception ex)
            //{
            //    ExceptionthrowHelper(ex);
            //}
            finally
            {
                this.CloseConnection();
                mCommand.Transaction = null;
            }
            return parameterValues;
        }

        /// <summary>
        /// Geriye  tek bir değer döndüren sql komutlarını çalıştırmak için kullanılır.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="komutTipi">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>Komutun çalışması sonucu elde edilen değer</returns>
        public object ExecuteScalar(string ssql, CommandType komutTipi)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş string değer verilemez.");

            this.OpenConnection();
            object result = null;
            mCommand.CommandText = ssql;
            mCommand.CommandType = komutTipi;
            AddParameters(mCommand);

            if (mIsTransactional)
                mCommand.Transaction = mTransaction;

            try
            {
                result = mCommand.ExecuteScalar();
            }
            //catch (Exception e)
            //{
            //    ExceptionthrowHelper(e);
            //}
            finally
            {
                this.CloseConnection();
                mCommand.Transaction = null;
            }
            return result;
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        public void GetRecords(DataSet ds, string ssql)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            if (ds.Tables.Count == 0)
                ds.Tables.Add();
            GetRecords(ds.Tables[0], ssql);
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="stable">Üzerinde çalışılan tablo adı.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        public void GetRecords(DataSet ds, string ssql, string stable, CommandType commandType)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            if (string.IsNullOrEmpty(stable))
                throw new ArgumentNullException("stable", "Boş parametre verilemez.");

            if (!ds.Tables.Contains(stable))
                ds.Tables.Add(new DataTable(stable));
            GetRecords(ds.Tables[stable], ssql, commandType);
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="stable">Üzerinde çalışılan tablo adı.</param>
        public void GetRecords(DataSet ds, string ssql, string stable)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            if (string.IsNullOrEmpty(stable))
                throw new ArgumentNullException("stable", "Boş parametre verilemez.");

            ds.Tables.Add(new DataTable(stable));
            GetRecords(ds.Tables[stable], ssql);
        }
        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        public void GetRecords(DataTable dt, string ssql)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            GetRecords(dt, ssql, CommandType.Text);
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        public void GetRecords(DataSet ds, string ssql, CommandType commandType)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            if (ds.Tables.Count == 0)
                ds.Tables.Add();
            GetRecords(ds.Tables[0], ssql, commandType);
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>Sorgu sonucu doldurulan dataset</returns>
        public DataSet GetRecords(string ssql, CommandType commandType)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            GetRecords(dt, ssql, commandType);
            return ds;
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <returns>Sorgu sonucu doldurulan dataset</returns>
        public DataSet GetRecords(string ssql)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            GetRecords(dt, ssql, CommandType.Text);
            return ds;
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        public void GetRecords(DataTable dt, string ssql, CommandType commandType)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            this.OpenConnection();

            mDataAdapter.SelectCommand.CommandText = ssql;
            mDataAdapter.SelectCommand.Connection = mConnection;
            mDataAdapter.SelectCommand.CommandType = commandType;
            mDataAdapter.SelectCommand.BindByName = true;
            AddParameters(mDataAdapter.SelectCommand);

            try
            {
                if (mIsTransactional)
                    mDataAdapter.SelectCommand.Transaction = mTransaction;

                dt.Clear();
                mDataAdapter.Fill(dt);
            }
            finally
            {
                this.CloseConnection();
                mDataAdapter.SelectCommand.Transaction = null;
            }
        }

        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="withSchema">Tabloya ait şema bilgisine ihtiyaç duyulur ise bu parametre kullanılır</param>
        public void GetRecords(DataTable dt, string ssql, bool withSchema)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            GetRecords(dt, ssql, CommandType.Text, withSchema);
        }


        /// <summary>
        /// Select İşlemleri için kullanılır.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="commandType">Çalıştırılacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <param name="withSchema">Tabloya ait şema bilgisine ihtiyaç duyulur ise bu parametre kullanılır</param>
        public void GetRecords(DataTable dt, string ssql, CommandType commandType, bool withSchema)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            this.OpenConnection();

            mDataAdapter.SelectCommand.CommandText = ssql;
            mDataAdapter.SelectCommand.Connection = mConnection;
            mDataAdapter.SelectCommand.CommandType = commandType;
            mDataAdapter.SelectCommand.BindByName = true;
            AddParameters(mDataAdapter.SelectCommand);

            if (withSchema)
                mDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            try
            {
                if (mIsTransactional)
                    mDataAdapter.SelectCommand.Transaction = mTransaction;

                dt.Clear();
                mDataAdapter.Fill(dt);
            }
            finally
            {
                this.CloseConnection();
                mDataAdapter.SelectCommand.Transaction = null;
            }
        }

        /// <summary>
        /// Parametre olarak verilen Dataset üzerinde yapılan işlemlerden sadece insert ve update 
        /// komutlarını çalıştırır.Delete komutu çalıştırılmaz.
        /// </summary>
        /// <param name="ds">Üzerinde çalışılan dataset</param>
        public void InsertUpdate(DataSet ds)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            this.OpenConnection();

            mDataAdapter.SelectCommand.CommandText = "SELECT * FROM " + ds.Tables[0].TableName;
            mDataAdapter.SelectCommand.Connection = mConnection;

            if (IsTransactional)
                mDataAdapter.SelectCommand.Transaction = mTransaction;

            try
            {
                OracleCommandBuilder cb = new OracleCommandBuilder(mDataAdapter);

                mDataAdapter.InsertCommand = cb.GetInsertCommand();
                mDataAdapter.UpdateCommand = cb.GetUpdateCommand();
                mDataAdapter.DeleteCommand = null;

                if (IsTransactional)
                {
                    mDataAdapter.InsertCommand.Transaction = mTransaction;
                    mDataAdapter.UpdateCommand.Transaction = mTransaction;

                }

                mDataAdapter.Update(ds.Tables[0].Select(null, null, DataViewRowState.Added));
                mDataAdapter.Update(ds.Tables[0].Select(null, null, DataViewRowState.ModifiedCurrent));

            }
            finally
            {
                this.CloseConnection();
                mDataAdapter.SelectCommand.Transaction = null;

                if (IsTransactional)
                {
                    mDataAdapter.InsertCommand.Transaction = null;
                    mDataAdapter.UpdateCommand.Transaction = null;
                    // mDataAdapter.DeleteCommand.Transaction = null;
                }
            }
        }

        /// <summary>
        /// Dataset üzerinde yapılan insert, update, delete işlemlerini veritabanına göndermek için kullanılır.
        /// </summary>
        /// <param name="ds">Üzerinde işlem yapılan dataset</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        /// <param name="sTable">Üzerinde çalışılan tablo adı.</param>
        public void Update(DataSet ds, string ssql, string sTable)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            if (string.IsNullOrEmpty(sTable))
                throw new ArgumentNullException("stable", "Boş parametre verilemez.");

            Update(ds.Tables[sTable], ssql);
        }


        /// <summary>
        /// Dataset üzerinde yapılan insert, update, delete işlemlerini veritabanına göndermek için kullanılır.
        /// </summary>
        /// <param name="ds">Üzerinde işlem yapılan dataset</param>
        public void Update(DataSet ds)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            string ssql = "SELECT * FROM " + ds.Tables[0].TableName;
            Update(ds.Tables[0], ssql);
        }

        /// <summary>
        /// Datatable üzerinde yapılan insert, update, delete işlemlerini veritabanına göndermek için kullanılır.
        /// </summary>
        /// <param name="dt">Üzerinde işlem yapılan Datatable</param>
        public void Update(DataTable dt)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            string ssql = "SELECT * FROM " + dt.TableName;
            Update(dt, ssql);
        }

        /// <summary>
        /// Datatable üzerinde yapılan insert, update, delete işlemlerini veritabanına göndermek için kullanılır.
        /// </summary>
        /// <param name="dt">Üzerinde işlem yapılan Datatable</param>
        /// <param name="ssql">Çalıştırılacak komut.</param>
        private void Update(DataTable dt, string ssql)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boş parametre verilemez.");

            this.OpenConnection();

            mDataAdapter.SelectCommand.CommandText = ssql;
            mDataAdapter.SelectCommand.Connection = mConnection;

            AddParameters(mDataAdapter.SelectCommand);

            if (IsTransactional)
                mDataAdapter.SelectCommand.Transaction = mTransaction;

            try
            {
                OracleCommandBuilder cb = new OracleCommandBuilder(mDataAdapter);

                mDataAdapter.InsertCommand = cb.GetInsertCommand();
                mDataAdapter.UpdateCommand = cb.GetUpdateCommand();
                mDataAdapter.DeleteCommand = cb.GetDeleteCommand();

                if (IsTransactional)
                {
                    mDataAdapter.InsertCommand.Transaction = mTransaction;
                    mDataAdapter.UpdateCommand.Transaction = mTransaction;
                    mDataAdapter.DeleteCommand.Transaction = mTransaction;
                }

                mDataAdapter.Update(dt);
            }
            //catch (Exception e)
            //{

            //    ExceptionthrowHelper(e);
            //}
            finally
            {
                this.CloseConnection();
                mDataAdapter.SelectCommand.Transaction = null;

                if (IsTransactional)
                {
                    mDataAdapter.InsertCommand.Transaction = null;
                    mDataAdapter.UpdateCommand.Transaction = null;
                    mDataAdapter.DeleteCommand.Transaction = null;
                }
            }
        }

        /// <summary>
        /// Parametrelerin SqlCommand nesnesine eklemek için kullanılır.
        /// </summary>
        private void AddParameters(OracleCommand cmd)
        {

            foreach (OracleParameter p in parameters)
            {
                cmd.Parameters.Add(p);
            }

        }

        /// <summary>
        /// Output tipindeki SqlServer parametreleri eklemek için kullanılır.Output parametrelerde size olmak zorundadır.
        /// </summary>
        /// <param name="parameterName">Parametre adı.</param>
        /// <param name="dbType">SqlServer'a özel parametre tipi.</param>
        /// <param name="direction">Parametre yönü.</param>
        /// <param name="parameterSize">Parametre boyutu.</param>
        public void AddSqlParameter(string parameterName, object parameterValue, SqlDbType dbType, ParameterDirection direction, int parameterSize)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Input tipindeki SqlServer parametreleri eklemek için kullanılır.
        /// </summary>
        /// <param name="parameterName">Parametre adı.</param>
        /// <param name="parameterValue">Veritabanına eklenecek olan değer.</param>
        /// <param name="dbType">SqlServer'a özel parametre tipi.</param>
        /// <param name="parameterSize">Parametre boyutu.</param>
        public void AddSqlParameter(string parameterName, object parameterValue, SqlDbType dbType, int parameterSize)
        {

            throw new NotImplementedException();

        }

        /// <summary>
        /// Output tipindeki Oracle parametreleri eklemek için kullanılır.Output parametrelerde size olmak zorundadır.
        /// </summary>
        /// <param name="parameterName">Parametre adı.</param>
        /// <param name="dbType">Oracle'a özel parametre tipi.</param>
        /// <param name="direction">Parametre yönü.</param>
        /// <param name="parameterSize">Parametren boyutu.</param>
        public void AddOracleParameter(string parameterName, OracleDbType dbType, ParameterDirection direction, int parameterSize)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName", "Parametre adı boş olamaz.");

            if (parameterSize < 0)
                throw new ArgumentNullException("parameterSize", "parameterSize negatif değer olamaz.");

            OracleParameter p = new OracleParameter();
            p.OracleDbType = dbType;
            p.ParameterName = parameterName;
            p.Direction = direction;
            p.Size = parameterSize;
            parameters.Add(p);

        }


        /// <summary>
        /// Input tipindeki Oracle parametreleri eklemek için kullanılır.
        /// </summary>
        /// <param name="parameterName">Parametre adı</param>
        /// <param name="parameterValue">Parametrenin değeri.</param>
        /// <param name="dbType">Oracle'a özel parametre tipi.</param>
        /// <param name="parameterSize">Parametren boyutu.</param>
        public void AddOracleParameter(string parameterName, object parameterValue, OracleDbType dbType, int parameterSize)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName", "Parametre adı boş olamaz.");

            if (parameterSize < 0)
                throw new ArgumentNullException("parameterSize", "parameterSize negatif değer olamaz.");

            OracleParameter p = new OracleParameter();
            p.OracleDbType = dbType;
            p.ParameterName = parameterName;
            p.Value = parameterValue ?? DBNull.Value;
            p.Size = parameterSize;
            parameters.Add(p);
        }

        /// <summary>
        /// Veritabanı işlemleri bittikten sonra SqlCommand nesnesi içindeki parametreleriş temizler.
        /// </summary>
        private void ClearCommandParameters()
        {
            parameters.Clear();
            mCommand.Parameters.Clear();

        }

        /// <summary>
        /// Oluşan hata ile ilgli daha fazla bilgi alabilmek için hatayı düzenler.
        /// </summary>
        /// <param name="ex"></param>
        private void ExceptionthrowHelper(Exception ex)
        {

            ex.Data.Add("Hata Tipi :", ex.GetBaseException());
            ex.Data.Add("Hata Kaynağı :", ex.Source);
            ex.Data.Add("Hata Mesajı :", ex.Message);
            ex.Data.Add("Satır :", ex.StackTrace.Contains("line") ? ex.StackTrace.Substring(ex.StackTrace.IndexOf("line", System.StringComparison.Ordinal)) : ex.StackTrace.Substring(ex.StackTrace.IndexOf("konum", System.StringComparison.Ordinal)));

            ex.Data.Add("Gönderilen Sql Komutu:", mCommand.CommandText);
            ex.Data.Add("metod adı :", ex.StackTrace.Contains(" in ") ? ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(" in ", System.StringComparison.Ordinal)) : ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("içinde", System.StringComparison.Ordinal)));
            ex.Data.Add("Parametreler :", null);

            foreach (var item in parameters)
            {
                ex.Data.Add(item.ParameterName, item.Value);
            }

            ex.Data.Add("Stack :", ex.StackTrace);

            throw ex;

        }


    }
}

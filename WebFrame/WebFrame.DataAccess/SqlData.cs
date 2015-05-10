using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using IsolationLevel = System.Data.IsolationLevel;

namespace WebFrame.DataAccess
{
    /// <summary>
    /// SqlServer ile ilgili iþlemleri yapmak için gerekli metodlarý barýndýrýr.
    /// </summary>
    internal class SqlData : IDisposable, IData
    {
        private SqlConnection mConnection;
        private SqlTransaction mTransaction;
        private SqlCommand mCommand;
        private SqlDataAdapter mDataAdapter;
        private static bool mIsTransactional;
        Dictionary<string, object> parameterValues;
        List<SqlParameter> parameters;

        private const IsolationLevel ISOLATION_LEVEL = IsolationLevel.ReadCommitted;

        /// <summary>
        /// Ýþlemin transaction ile yapýlýp yapýlmadýðýný belirtir.
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
        /// Veritabanýna baðlanýlýrken kullanýlan baðlantý cümlesinin adý. 
        /// </summary>
        public string ConnectionName
        {
            get
            {
                return connectionName;
            }
        }

        private bool disposeEdildi = false;//Dispose methodu ~ tarafýndan 2. kez çaðrýlmasýn diye kullanýlýyor.
        /// <summary>
        /// SqlServer veritabanýyla iþlem yapmak için gerekli metodlarýn bulunduðu sýnýfý oluþturur.
        /// </summary>
        /// <param name="connectionStringName">Veritabanýna baðlanmak için kullanýlacak baðlantý adý.</param>
        public SqlData(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName", "Baðlantý adý boþ olamaz.");

            mConnection = new SqlConnection(ConnectionStringHelper.GetConnectionString(connectionStringName));

            mCommand = new SqlCommand();
            mDataAdapter = new SqlDataAdapter(mCommand);
            mCommand.Connection = mConnection;
            parameterValues = new Dictionary<string, object>();
            parameters = new List<SqlParameter>();
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
        /// Veritabaný baðlantýsýný açar.
        /// </summary>
        private void OpenConnection()
        {
            if (mIsTransactional == false)
            {
                try
                {
                    mConnection.Open();
                }
                //catch (Exception ex)
                //{
                //    ExceptionthrowHelper(ex);
                //}
                finally
                { }
            }
        }
        /// <summary>
        /// Veritabaný baðlantýsýný kapatýr.
        /// </summary>
        private void CloseConnection()
        {
            if (mIsTransactional == false)
            {
                try
                {
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
        /// Transaction iþlemini baþlatýr.
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
        /// Transaction iþlemini bitirir.
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
        /// Transaction iþlemini geri alýr.
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
        /// Geriye tek bir integer deðer döndüren sorgulamalar için kullanýlýr. Örneðin
        /// Sum, Count, Avarage gibi.
        /// </summary>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <returns>Ýþlem sonucu.</returns>
        public int ExecuteStatement(string ssql)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ string deðer verilemez.");

            return ExecuteStatement(ssql, CommandType.Text);
        }
        /// <summary>
        /// Geriye tek bir integer deðer döndüren sorgulamalar için kullanýlýr. Örneðin
        /// Sum, Count, Avarage gibi.
        /// </summary>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="commandType">Çalýþtýrýlacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>Ýþlem sonucu.</returns>
        public int ExecuteStatement(string ssql, CommandType commandType)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ string deðer verilemez.");

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
        /// Dataseti prosedür kullanarak doldurmak amacýyla kullanýlýr. Bu komut sadece prosedür ile çalýþýr.
        /// </summary>
        /// <param name="storedprocedureName">Çalýþtýrýlacak prosedür adý.</param>
        /// <param name="ds">Doldurulacak olan dataset</param>
        public void ExecuteStatement(string storedprocedureName, DataSet ds)
        {
            if (string.IsNullOrEmpty(storedprocedureName))
                throw new ArgumentNullException("storedprocedureName", "Boþ string deðer verilemez.");

            this.OpenConnection();

            try
            {
                mCommand.CommandText = storedprocedureName;
                mCommand.CommandType = CommandType.StoredProcedure;

                AddParameters(mCommand);

                if (mTransaction != null)
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
        /// Gönderilen komut içerisinde kullanýlan prosedür veya fonksiyona ait output parametre 
        /// deðerlerini almak için kullanýlýr.
        /// </summary>
        /// <param name="storedProcedureOrFunctionName">Çalýþýtýrýlacak prosedür veya fonksiyon ismi.</param>
        /// <returns>Fonsiyon veya prosedüre ait output parametrelerin adý ve dönüþ deðerleri</returns>
        public Dictionary<string, object> ExecuteStatementUDI(string storedProcedureOrFunctionName)
        {
            if (string.IsNullOrEmpty(storedProcedureOrFunctionName))
                throw new ArgumentNullException("storedProcedureOrFunctionName", "Boþ string deðer verilemez.");

            parameterValues.Clear();
            this.OpenConnection();
            try
            {
                mCommand.CommandText = storedProcedureOrFunctionName;
                mCommand.CommandType = CommandType.StoredProcedure;
                AddParameters(mCommand);

                if (mTransaction != null)
                    mCommand.Transaction = mTransaction;


                mCommand.ExecuteScalar();

                foreach (SqlParameter op in mCommand.Parameters)
                {
                    if ((op.Direction == ParameterDirection.InputOutput) || (op.Direction == ParameterDirection.Output) || (op.Direction == ParameterDirection.ReturnValue))
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
        /// Geriye  tek bir deðer döndüren sql komutlarýný çalýþtýrmak için kullanýlýr.
        /// </summary>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="komutTipi">Çalýþtýrýlacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>Komutun çalýþmasý sonucu elde edilen deðer</returns>
        public object ExecuteScalar(string ssql, CommandType komutTipi)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ string deðer verilemez.");

            this.OpenConnection();
            object result = null;
            mCommand.CommandText = ssql;
            mCommand.CommandType = komutTipi;
            AddParameters(mCommand);

            if (IsTransactional)
            {
                mCommand.Transaction = mTransaction;
            }
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
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        public void GetRecords(DataSet ds, string ssql)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            if (ds.Tables.Count == 0)
                ds.Tables.Add();
            GetRecords(ds.Tables[0], ssql);
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="stable">Üzerinde çalýþýlan tablo adý.</param>
        /// <param name="commandType">Çalýþtýrýlacak olan komut tipi (Örn. Text, Procedure , function)</param>
        public void GetRecords(DataSet ds, string ssql, string stable, CommandType commandType)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            if (string.IsNullOrEmpty(stable))
                throw new ArgumentNullException("stable", "Boþ parametre verilemez.");

            if (!ds.Tables.Contains(stable))
                ds.Tables.Add(new DataTable(stable));
            GetRecords(ds.Tables[stable], ssql, commandType);
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="stable">Üzerinde çalýþýlan tablo adý.</param>
        public void GetRecords(DataSet ds, string ssql, string stable)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            if (string.IsNullOrEmpty(stable))
                throw new ArgumentNullException("stable", "Boþ parametre verilemez.");

            ds.Tables.Add(new DataTable(stable));
            GetRecords(ds.Tables[stable], ssql);
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        public void GetRecords(DataTable dt, string ssql)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            GetRecords(dt, ssql, CommandType.Text);
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="ds">Doldurulacak DataSet</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="commandType">Çalýþtýrýlacak olan komut tipi (Örn. Text, Procedure , function)</param>
        public void GetRecords(DataSet ds, string ssql, CommandType commandType)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            if (ds.Tables.Count == 0)
                ds.Tables.Add();
            GetRecords(ds.Tables[0], ssql, commandType);
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="commandType">Çalýþtýrýlacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <returns>Sorgu sonucu doldurulan dataset</returns>
        public DataSet GetRecords(string ssql, CommandType commandType)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            GetRecords(dt, ssql, commandType);
            return ds;
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <returns>Sorgu sonucu doldurulan dataset</returns>
        public DataSet GetRecords(string ssql)
        {
            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            GetRecords(dt, ssql, CommandType.Text);
            return ds;
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="commandType">Çalýþtýrýlacak olan komut tipi (Örn. Text, Procedure , function)</param>
        public void GetRecords(DataTable dt, string ssql, CommandType commandType)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            this.OpenConnection();

            mDataAdapter.SelectCommand.CommandText = ssql;
            mDataAdapter.SelectCommand.Connection = mConnection;
            mDataAdapter.SelectCommand.CommandType = commandType;

            AddParameters(mDataAdapter.SelectCommand);

            try
            {
                if (mIsTransactional)
                    mDataAdapter.SelectCommand.Transaction = mTransaction;

                mDataAdapter.Fill(dt);
            }
            //catch (Exception e)
            //{
            //    ExceptionthrowHelper(e);
            //}
            finally
            {
                this.CloseConnection();
                mDataAdapter.SelectCommand.Transaction = null;
            }
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="withSchema">Tabloya ait þema bilgisine ihtiyaç duyulur ise bu parametre kullanýlýr</param>
        public void GetRecords(DataTable dt, string ssql, bool withSchema)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            GetRecords(dt, ssql, CommandType.Text, withSchema);
        }
        /// <summary>
        /// Select Ýþlemleri için kullanýlýr.
        /// </summary>
        /// <param name="dt">Doldurulacak DataTable</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="commandType">Çalýþtýrýlacak olan komut tipi (Örn. Text, Procedure , function)</param>
        /// <param name="withSchema">Tabloya ait þema bilgisine ihtiyaç duyulur ise bu parametre kullanýlýr</param>
        public void GetRecords(DataTable dt, string ssql, CommandType commandType, bool withSchema)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            this.OpenConnection();

            mDataAdapter.SelectCommand.CommandText = ssql;
            mDataAdapter.SelectCommand.Connection = mConnection;
            mDataAdapter.SelectCommand.CommandType = commandType;

            AddParameters(mDataAdapter.SelectCommand);

            if (withSchema)
                mDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            try
            {
                if (mIsTransactional)
                    mDataAdapter.SelectCommand.Transaction = mTransaction;

                mDataAdapter.Fill(dt);
            }
            //catch (Exception e)
            //{
            //    ExceptionthrowHelper(e);
            //}
            finally
            {
                this.CloseConnection();
                mDataAdapter.SelectCommand.Transaction = null;
            }
        }

        /// <summary>
        /// Parametre olarak verilen Dataset üzerinde yapýlan iþlemlerden sadece insert ve update 
        /// komutlarýný çalýþtýrýr.Delete komutu çalýþtýrýlmaz.
        /// </summary>
        /// <param name="ds">Üzerinde çalýþýlan dataset</param>
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
                SqlCommandBuilder cb = new SqlCommandBuilder(mDataAdapter);

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
        /// Dataset üzerinde yapýlan insert, update, delete iþlemlerini veritabanýna göndermek için kullanýlýr.
        /// </summary>
        /// <param name="ds">Üzerinde iþlem yapýlan dataset</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        /// <param name="sTable">Üzerinde çalýþýlan tablo adý.</param>
        public void Update(DataSet ds, string ssql, string sTable)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            if (string.IsNullOrEmpty(sTable))
                throw new ArgumentNullException("sTable", "Boþ parametre verilemez.");

            Update(ds.Tables[sTable], ssql);
        }
        /// <summary>
        /// Dataset üzerinde yapýlan insert, update, delete iþlemlerini veritabanýna göndermek için kullanýlýr.
        /// </summary>
        /// <param name="ds">Üzerinde iþlem yapýlan dataset</param>
        public void Update(DataSet ds)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Dataset null olamaz");

            string ssql = "SELECT * FROM " + ds.Tables[0].TableName;
            Update(ds.Tables[0], ssql);

        }
        /// <summary>
        /// Datatable üzerinde yapýlan insert, update, delete iþlemlerini veritabanýna göndermek için kullanýlýr.
        /// </summary>
        /// <param name="dt">Üzerinde iþlem yapýlan Datatable</param>
        public void Update(DataTable dt)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            string ssql = "SELECT * FROM " + dt.TableName;
            Update(dt, ssql);
        }
        /// <summary>
        /// Datatable üzerinde yapýlan insert, update, delete iþlemlerini veritabanýna göndermek için kullanýlýr.
        /// </summary>
        /// <param name="dt">Üzerinde iþlem yapýlan Datatable</param>
        /// <param name="ssql">Çalýþtýrýlacak komut.</param>
        private void Update(DataTable dt, string ssql)
        {
            if (dt == null)
                throw new ArgumentNullException("dt", "Datatable null olamaz");

            if (string.IsNullOrEmpty(ssql))
                throw new ArgumentNullException("ssql", "Boþ parametre verilemez.");

            this.OpenConnection();

            mDataAdapter.SelectCommand.CommandText = ssql;
            mDataAdapter.SelectCommand.Connection = mConnection;

            AddParameters(mDataAdapter.SelectCommand);

            if (IsTransactional)
                mDataAdapter.SelectCommand.Transaction = mTransaction;

            try
            {
                SqlCommandBuilder cb = new SqlCommandBuilder(mDataAdapter);

                if (IsTransactional)
                {
                    mDataAdapter.InsertCommand = cb.GetInsertCommand();
                    mDataAdapter.InsertCommand.Transaction = mTransaction;
                    mDataAdapter.UpdateCommand = cb.GetUpdateCommand();
                    mDataAdapter.UpdateCommand.Transaction = mTransaction;
                    mDataAdapter.DeleteCommand = cb.GetDeleteCommand();
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
        /// Parametrelerin SqlCommand nesnesine eklemek için kullanýlýr.
        /// </summary>
        private void AddParameters(SqlCommand cmd)
        {

            foreach (SqlParameter p in parameters)
            {
                cmd.Parameters.Add(p);
            }

        }

        /// <summary>
        /// Output tipindeki SqlServer parametreleri eklemek için kullanýlýr.Output parametrelerde size olmak zorundadýr.
        /// </summary>
        /// <param name="parameterName">Parametre adý.</param>
        /// <param name="dbType">SqlServer'a özel parametre tipi.</param>
        /// <param name="direction">Parametre yönü.</param>
        /// <param name="parameterSize">Parametre boyutu.</param>
        public void AddSqlParameter(string parameterName, object parameterValue, SqlDbType dbType, ParameterDirection direction, int parameterSize)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName", "Parametre adý boþ olamaz.");

            if (parameterSize < 0)
                throw new ArgumentNullException("parameterSize", "parameterSize negatif deðer olamaz.");

            SqlParameter p = new SqlParameter();
            p.SqlDbType = dbType;
            p.ParameterName = parameterName;
            p.Value = parameterValue ?? DBNull.Value;
            p.Direction = direction;
            p.Size = parameterSize;
            parameters.Add(p);
        }


        /// <summary>
        /// Input tipindeki SqlServer parametreleri eklemek için kullanýlýr.
        /// </summary>
        /// <param name="parameterName">Parametre adý.</param>
        /// <param name="parameterValue">Veritabanýna eklenecek olan deðer.</param>
        /// <param name="dbType">SqlServer'a özel parametre tipi.</param>
        /// <param name="parameterSize">Parametre boyutu.</param>
        public void AddSqlParameter(string parameterName, object parameterValue, SqlDbType dbType, int parameterSize)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName", "Parametre adý boþ olamaz.");

            if (parameterSize < 0)
                throw new ArgumentNullException("parameterSize", "parameterSize negatif deðer olamaz.");

            SqlParameter p = new SqlParameter();
            p.SqlDbType = dbType;
            p.ParameterName = parameterName;
            p.Value = parameterValue ?? DBNull.Value;
            p.Size = parameterSize;
            parameters.Add(p);
        }
        /// <summary>
        /// Output tipindeki Oracle parametreleri eklemek için kullanýlýr.Output parametrelerde size olmak zorundadýr.
        /// </summary>
        /// <param name="parameterName">Parametre adý.</param>
        /// <param name="dbType">Oracle'a özel parametre tipi.</param>
        /// <param name="direction">Parametre yönü.</param>
        /// <param name="parameterSize">Parametren boyutu.</param>
        public void AddOracleParameter(string parameterName, OracleDbType dbType, ParameterDirection direction, int parameterSize)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Input tipindeki Oracle parametreleri eklemek için kullanýlýr.
        /// </summary>
        /// <param name="parameterName">Parametre adý</param>
        /// <param name="parameterValue">Parametrenin deðeri.</param>
        /// <param name="dbType">Oracle'a özel parametre tipi.</param>
        /// <param name="parameterSize">Parametren boyutu.</param>
        public void AddOracleParameter(string parameterName, object parameterValue, Oracle.DataAccess.Client.OracleDbType dbType, int parameterSize)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Veritabaný iþlemleri bittikten sonra SqlCommand nesnesi içindeki parametreleriþ temizler.
        /// </summary>
        private void ClearCommandParameters()
        {
            parameters.Clear();
            mCommand.Parameters.Clear();

        }
        /// <summary>
        /// Oluþan hata ile ilgli daha fazla bilgi alabilmek için hatayý düzenler.
        /// </summary>
        /// <param name="ex"></param>
        private void ExceptionthrowHelper(Exception ex)
        {

            ex.Data.Add("Hata Tipi :", ex.GetBaseException());
            ex.Data.Add("Hata Kaynaðý :", ex.Source);
            ex.Data.Add("Hata Mesajý :", ex.Message);
            ex.Data.Add("Satýr :", ex.StackTrace.Contains("line") ? ex.StackTrace.Substring(ex.StackTrace.IndexOf("line", System.StringComparison.Ordinal)) : ex.StackTrace.Substring(ex.StackTrace.IndexOf("konum", System.StringComparison.Ordinal)));

            ex.Data.Add("Gönderilen Sql Komutu:", mCommand.CommandText);
            ex.Data.Add("metod adý :", ex.StackTrace.Contains(" in ") ? ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(" in ", System.StringComparison.Ordinal)) : ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("içinde", System.StringComparison.Ordinal)));
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
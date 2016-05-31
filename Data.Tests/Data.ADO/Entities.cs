using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.ADO
{
    public class Entities: IDisposable
    {
        #region Fields
        private SqlConnection _sqlConnection;
        #endregion

        #region Public Methods
        public SqlConnection SqlConnection
        {
            get
            {
                _sqlConnection = _sqlConnection ?? NewSqlConnection();
                

                return _sqlConnection;
            }
        }

        private static SqlConnection NewSqlConnection()
        {
            var sqlConnection = new SqlConnection("data source=(localdb)\\v11.0;initial catalog=Playground;integrated security=True");
                              //new SqlConnection("Server=(localdb)\v11.0; Database=Playground; Trusted_Connection=true;");
            
            sqlConnection.Open();
            
            return sqlConnection;
        }

        public void ExecuteNonQuery(String query, IEnumerable<SqlParameter> parameters = null, Int32 timeout = 30)
        {
            using (var sqlCommand = SqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandTimeout = timeout;

                if (parameters != null)
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                }

                sqlCommand.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteQuery(String query, IEnumerable<SqlParameter> parameters = null, Int32 timeout = 30)
        {
            using (var sqlCommand = SqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandTimeout = timeout;
                
                if (parameters != null)
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                }

                var dataAdapter = new SqlDataAdapter(sqlCommand);
                var dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                return dataTable;
            }
        }

        public T ExecuteScalar<T>(String query, IEnumerable<SqlParameter> parameters = null, Int32 timeout = 30)
        {
            using (var sqlCommand = SqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandTimeout = timeout;
                
                if (parameters != null)
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                }

                return (T)sqlCommand.ExecuteScalar();
            }
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_sqlConnection != null)
            {
                _sqlConnection.Dispose();
                _sqlConnection = null;
            }
        }
        #endregion
    }
}

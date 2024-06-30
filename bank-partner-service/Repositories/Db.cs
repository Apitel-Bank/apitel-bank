using System.Data.SqlClient;

namespace BankPartnerService.Repositories
{
    public class Db
    {
        private readonly SqlConnection conn;

        public SqlConnection Connection { get => conn; }

        public Db()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = Environment.GetEnvironmentVariable("DB_HOST"),
                UserID = Environment.GetEnvironmentVariable("DB_USER"),
                Password = Environment.GetEnvironmentVariable("DB_PASS"),
                InitialCatalog = Environment.GetEnvironmentVariable("DB_NAME"),
                TrustServerCertificate = bool.Parse(Environment.GetEnvironmentVariable("DB_TRUST_CERT"))
            };

            conn = new SqlConnection(builder.ConnectionString);
            conn.Open();

            var a = new List<string>();
        }

        public SqlDataReader ExecuteReader(SqlCommand cmd)
        {
            return cmd.ExecuteReader();
        }

        ~Db()
        {
            conn.Close();
        }

        public T WithTransaction<T>(Func<SqlTransaction, T> executable)
        {
            var transaction = Connection.BeginTransaction();

            try
            {
                var result = executable(transaction);
                transaction.Commit();
                return result;
            } catch
            {
                transaction.Rollback();   
                throw;
            }
        }

    }
}

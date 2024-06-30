using System.Data;
using System.Data.SqlClient;

namespace BankPartnerService.Repositories
{
    public class AccountTransactionStatusesRepository(Db db)
    {
        private Dictionary<string, int> statuses = [];

        public int GetStatusId(string statusDescription)
        {
            if(statuses.Count == 0)
            {
                var sql = @"SELECT AccountTransactionStatusId, Description FROM AccountTransactionStatuses";

                using var command = new SqlCommand(sql, db.Connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    statuses.Add(reader.GetString("Description"), reader.GetInt32("AccountTransactionStatusId"));
                }

                reader.Close();
                return statuses[statusDescription];
            } else
            {
                return statuses[statusDescription];
            }
        }
    }
}

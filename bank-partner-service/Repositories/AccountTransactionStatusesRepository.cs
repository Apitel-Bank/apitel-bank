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
                LoadStatuses();
                return statuses[statusDescription];
            } else
            {
                return statuses[statusDescription];
            }
        }

        public string GetStatusString(int statusId)
        {
            if (statuses.Count == 0)
            {
                LoadStatuses();
                return statuses.Where(statusEntry => statusEntry.Value == statusId).Select(statusEntry => statusEntry.Key).First();
            }
            else
            {
                return statuses.Where(statusEntry => statusEntry.Value == statusId).Select(statusEntry => statusEntry.Key).First();
            }
        }

        private void LoadStatuses()
        {
            var sql = @"SELECT AccountTransactionStatusId, Description FROM AccountTransactionStatuses";

            using var command = new SqlCommand(sql, db.Connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                statuses.Add(reader.GetString("Description"), reader.GetInt32("AccountTransactionStatusId"));
            }

            reader.Close();
        }
    }
}

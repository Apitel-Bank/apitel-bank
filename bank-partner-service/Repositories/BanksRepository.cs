using BankPartnerService.Models;
using System.Data;
using System.Data.SqlClient;

namespace BankPartnerService.Repositories
{
    public class BanksRepository(Db db)
    {
        public IEnumerable<Bank> ListAll()
        {
            var sql = @"SELECT BankId, BankName FROM Banks";
            using var command = new SqlCommand(sql, db.Connection);
            var reader = command.ExecuteReader();
            var output = new List<Bank>();
            while(reader.Read())
            {
                output.Add(new Bank(reader.GetInt32("BankId"), reader.GetString("BankName")));
            }

            return output;
        }
    }
}

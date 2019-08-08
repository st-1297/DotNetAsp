using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Transactions;
using ASPDotNetApiProxy.DbModels;

namespace ASPDotNetApiProxy.Repositories
{
    public class TestRepository
    {
        public static async Task<IEnumerable<Product>> GetAll(int id)
        {
            using (var connection = DbConnector.CreateDbConnection())
            {
                var sql = @"
select * from dbo.product
where ID = :ID
;";
                var param = id == 0 ? null : new { ID = id };
                var result =   await connection.QueryAsync<Product>(sql, param).ConfigureAwait(false);
                return result;
            }
        }
    }
}

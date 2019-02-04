using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPDotNetWebApi.DbSqls
{
    class SqlProducts
    {
        /// <summary>
        /// Productsテーブルからレコードを取得するためのSQL文を生成します
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SELECT文</returns>
        public static string CreateSql(int? id)
        {
            var query = new StringBuilder();

            query.AppendLine(" select * ");
            query.AppendLine(" from dbo.Product ");

            if (id != null)
            {
                query.AppendLine(" where id = :ID");
            }
            
            query.AppendLine(" ; ");

            return query.ToString();
        }
    }
}

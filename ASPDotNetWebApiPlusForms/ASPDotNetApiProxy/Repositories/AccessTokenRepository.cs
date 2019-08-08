using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.DataAccess;
using ASPDotNetApiProxy.DbModels;
using Dapper;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ASPDotNetApiProxy.Repositories
{
    #region

    /// <summary>
    /// アクセストークンのリポジトリとしての機能を提供します。
    /// </summary>
    public interface IAccessTokenRepository : IDisposable
    {
        /// <summary>
        /// 指定されたユーザーに対してアクセストークンを新規に発行します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>生成されたトークン</returns>
        Task<AccessToken> CreateAsync(int userId);


        /// <summary>
        /// 指定されたユーザーIDに対して発行されたトークンをすべて削除します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>削除した件数</returns>
        Task<int> DeleteAsync(int userId);


        /// <summary>
        /// 指定されたアクセストークンを削除します。
        /// </summary>
        /// <param name="token">アクセストークン</param>
        /// <returns>削除に成功した場合true</returns>
        Task<bool> DeleteAsync(string token);


        /// <summary>
        /// 有効期限が切れたトークンをすべて削除します。
        /// </summary>
        /// <returns>削除した件数</returns>
        Task<int> DeleteExpiredAsync();


        /// <summary>
        /// 指定されたトークンが有効かどうかを確認します。
        /// </summary>
        /// <param name="token">トークン</param>
        /// <returns>有効かどうか</returns>
        Task<bool> IsValidAsync(string token);


        /// <summary>
        /// 指定されたトークンを利用しているユーザーのIDを取得します。
        /// </summary>
        /// <param name="token">トークン</param>
        /// <returns>ユーザーID</returns>
        Task<int?> GetUserIdAsync(string token);
    }

    #endregion

    /// <summary>Repository        
    /// データベースで保管/管理を行うアクセストークンのリポジトリを表します。
    /// </summary>
    public class AccessTokenRepository : IAccessTokenRepository
    {
        #region IAccessTokenRepository メンバー

        /// <summary>
        /// 指定されたユーザーに対してアクセストークンを新規に発行します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>生成されたトークン</returns>
        public async Task<AccessToken> CreateAsync(int userId)
        {
            //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //    //--- レコード追加
            //    using (var connection = DbConnector.CreateDbConnection())
            //    {
            //        connection.Open();
            //        var now = DateTime.Now;
            //        var token = new T_ACCESS_TOKEN
            //        {
            //            VALUE = Guid.NewGuid().ToString(),
            //            USR_ID = userId,
            //            CREATION_TIME = now,
            //            EXPIRATION_TIME = now + AuthSettings.AccessTokenValidDuration,
            //        };
            //        var result = await connection.InsertAsync(token).ConfigureAwait(false);
            //        if (result == 1)
            //        {
            //            transaction.Complete();
            //            return token.ToApiModel();
            //        }
            //        return null;
            //    }
            //}

            //--- レコード追加
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var now = DateTime.Now;

                var token = new T_ACCESS_TOKEN
                {
                    VALUE = Guid.NewGuid().ToString(),
                    USR_ID = userId,
                    CREATION_TIME = now,
                    EXPIRATION_TIME = now + AuthSettings.AccessTokenValidDuration,
                };

                var result = await connection.InsertAsync(token).ConfigureAwait(false);
                if (result == 1) { return token.ToApiModel(); }
                return null;
            }

        }


        /// <summary>
        /// 指定されたユーザーIDに対して発行されたトークンをすべて削除します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>削除した件数</returns>
        public Task<int> DeleteAsync(int userId)
        {
            var builder = new StringBuilder();
            builder.AppendLine(PrimitiveSql.CreateDeleteAllSql<T_ACCESS_TOKEN>());
            builder.Append("where USR_ID = :userId");
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                return connection.ExecuteAsync(builder.ToString(), new { userId = userId });
            }
        }


        /// <summary>
        /// 指定されたアクセストークンを削除します。
        /// </summary>
        /// <param name="token">アクセストークン</param>
        /// <returns>削除に成功した場合true</returns>
        public async Task<bool> DeleteAsync(string token)
        {
            var sql = PrimitiveSql.CreateDeleteSql<T_ACCESS_TOKEN>();
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { VALUE = token }).ConfigureAwait(false);
                return result == 1;
            }
        }


        /// <summary>
        /// 有効期限が切れたトークンをすべて削除します。
        /// </summary>
        /// <returns>削除した件数</returns>
        public Task<int> DeleteExpiredAsync()
        {
            var builder = new StringBuilder();
            builder.AppendLine(PrimitiveSql.CreateDeleteAllSql<T_ACCESS_TOKEN>());
            builder.Append("where EXPIRATION_TIME < :now");
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                return connection.ExecuteAsync(builder.ToString(), new { now = DateTime.Now });
            }
        }


        /// <summary>
        /// 指定されたトークンが有効かどうかを確認します。
        /// </summary>
        /// <param name="token">トークン</param>
        /// <returns>有効かどうか</returns>
        public async Task<bool> IsValidAsync(string token)
        {
            var userId = await this.GetUserIdAsync(token).ConfigureAwait(false);
            return userId.HasValue;
        }


        /// <summary>
        /// 指定されたトークンを利用しているユーザーのIDを取得します。
        /// </summary>
        /// <param name="token">トークン</param>
        /// <returns>ユーザーID</returns>
        public async Task<int?> GetUserIdAsync(string token)
        {
            var sql = PrimitiveSql.CreateSelectSql<T_ACCESS_TOKEN>();
            var parameter = new T_ACCESS_TOKEN { VALUE = token };
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var record = (await connection.QueryAsync<T_ACCESS_TOKEN>(sql, parameter)
                            .ConfigureAwait(false))
                            .FirstOrDefault();
                if (record == null) return null;
                if (record.EXPIRATION_TIME < DateTime.Now) return null;
                return record.USR_ID;
            }
        }

        #endregion

        #region IDisposable メンバー

        /// <summary>
        /// 使用したリソースを解放します。
        /// </summary>
        public void Dispose()
        { }

        #endregion
    }
}
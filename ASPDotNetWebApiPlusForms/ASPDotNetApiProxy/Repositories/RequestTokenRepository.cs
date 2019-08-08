using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.DataAccess;
using ASPDotNetApiProxy.DbModels;
using Dapper;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ASPDotNetApiProxy.Repositories
{
    /// <summary>
    /// リクエストトークンのリポジトリとしての機能を提供します。
    /// </summary>
    public interface IRequestTokenRepository : IDisposable
    {
        /// <summary>
        /// 新規にトークンを発行します。
        /// </summary>
        /// <returns>生成されたリクエストトークン</returns>
        Task<RequestToken> CreateAsync();


        /// <summary>
        /// 指定されたトークンを削除します。
        /// </summary>
        /// <param name="token">トークン</param>
        /// <returns>削除に成功したかどうか</returns>
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
    }

    /// <summary>
    /// データベースで保管/管理を行うリクエストトークンのリポジトリを表します。
    /// </summary>
    public class RequestTokenRepository : IRequestTokenRepository
    {
        #region IRequestTokenRepository メンバー

        /// <summary>
        /// 新規にトークンを発行します。
        /// </summary>
        /// <returns>生成されたリクエストトークン</returns>
        public async Task<RequestToken> CreateAsync()
        {
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var now = DateTime.Now;
                var token = new T_REQUEST_TOKEN
                {
                    VALUE = Guid.NewGuid().ToString(),
                    CREATION_TIME = now,
                    EXPIRATION_TIME = now + AuthSettings.RequestTokenValidDuration,
                };
                var result = await connection.InsertAsync(token).ConfigureAwait(false);
                return result == 1 ? token.ToApiModel() : null;
            }
        }

        /// <summary>
        /// 指定されたトークンを削除します。
        /// </summary>
        /// <param name="token">トークン</param>
        /// <returns>削除に成功したかどうか</returns>
        public async Task<bool> DeleteAsync(string token)
        {
            var parameter = new T_REQUEST_TOKEN { VALUE = token };
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var result = await connection.DeleteAsync(parameter);
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
            builder.AppendLine(PrimitiveSql.CreateDeleteAllSql<T_REQUEST_TOKEN>());
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
            var sql = PrimitiveSql.CreateSelectSql<T_REQUEST_TOKEN>();
            var parameter = new T_REQUEST_TOKEN { VALUE = token };
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var record = (await connection.QueryAsync<T_REQUEST_TOKEN>(sql, parameter)
                            .ConfigureAwait(false))
                            .FirstOrDefault();
                return record == null
                    ? false
                    : DateTime.Now <= record.EXPIRATION_TIME;
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
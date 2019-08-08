using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Newtonsoft.Json;
using ASPDotNetApiProxy.DbModels;


namespace ASPDotNetApiProxy.DataAccess
{
    /// <summary>
    /// データベース操作の補助機能を提供します。
    /// </summary>
    internal static class DbOperation
    {
        #region auto id
        /// <summary>
        /// 指定された型の主キーのIDを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <returns>生成されたID</returns>
        public static int GenerateAutoId<T>(this IDbConnection connection)
        {
            var sql = PrimitiveSql.CreateNextAutoIdSql<T>();
            return connection.Query<SequenceNextValue>(sql)
                    .Single()
                    .NEXTVAL;
        }


        /// <summary>
        /// 指定された型の主キーのIDを非同期的に生成します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <returns>生成されたID</returns>
        public static async Task<int> GenerateAutoIdAsync<T>(this IDbConnection connection)
        {
            var sql = PrimitiveSql.CreateNextAutoIdSql<T>();
            return (await connection.QueryAsync<SequenceNextValue>(sql)
                    .ConfigureAwait(false))
                    .Single()
                    .NEXTVAL;
        }
        #endregion


        #region count
        /// <summary>
        /// 指定された型のレコード数を取得します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <returns>レコード数</returns>
        public static int Count<T>(this IDbConnection connection)
        {
            var sql = PrimitiveSql.CreateCountSql<T>();
            return connection.Query<RecordCount>(sql)
                    .Single()
                    .VALUE;
        }


        /// <summary>
        /// 指定された型のレコード数を非同期的に取得します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <returns>レコード数</returns>
        public static async Task<int> CountAsync<T>(this IDbConnection connection)
        {
            var sql = PrimitiveSql.CreateCountSql<T>();
            return (await connection.QueryAsync<RecordCount>(sql)
                    .ConfigureAwait(false))
                    .Single()
                    .VALUE;
        }
        #endregion


        #region insert
        /// <summary>
        /// 指定されたデータをテーブルに挿入します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">挿入するデータ</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int Insert<T>(this IDbConnection connection, T data)
        {
            var sql = PrimitiveSql.CreateInsertSql<T>();
            return connection.Execute(sql, data);
        }


        /// <summary>
        /// 指定されたデータをテーブルに非同期的に挿入します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">挿入するデータ</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> InsertAsync<T>(this IDbConnection connection, T data)
        {
            var sql = PrimitiveSql.CreateInsertSql<T>();
            return connection.ExecuteAsync(sql, data);
        }


        /// <summary>
        /// 指定されたデータをテーブルに非同期的に挿入します。※クエリ指定版
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続<</param>
        /// <param name="sql">Insert文</param>
        /// <param name="data">挿入するデータ</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> InsertAsync<T>(this IDbConnection connection, string sql, T data)
        {
            return connection.ExecuteAsync(sql, data);
        }


        /// <summary>
        /// 指定されたデータのコレクションをテーブルに挿入します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">挿入するデータのコレクション</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int Insert<T>(this IDbConnection connection, IEnumerable<T> data)
        {
            var sql = PrimitiveSql.CreateInsertSql<T>();
            return connection.Execute(sql, data);
        }


        /// <summary>
        /// 指定されたデータのコレクションをテーブルに非同期的に挿入します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">挿入するデータのコレクション</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> InsertAsync<T>(this IDbConnection connection, IEnumerable<T> data)
        {
            var sql = PrimitiveSql.CreateInsertSql<T>();
            return connection.ExecuteAsync(sql, data);
        }
        #endregion


        #region update
        /// <summary>
        /// 指定されたレコードを更新します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">更新するデータ</param>
        /// <param name="properties">更新対象の列</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int Update<T>(this IDbConnection connection, T data, params Expression<Func<T, object>>[] properties)
        {
            var sql = PrimitiveSql.CreateUpdateSql<T>(properties);
            return connection.Execute(sql, data);
        }


        /// <summary>
        /// 指定されたレコードを非同期的に更新します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">更新するデータ</param>
        /// <param name="properties">更新対象の列</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> UpdateAsync<T>(this IDbConnection connection, T data, params Expression<Func<T, object>>[] properties)
        {
            var sql = PrimitiveSql.CreateUpdateSql<T>(properties);
            return connection.ExecuteAsync(sql, data);
        }


        /// <summary>
        /// 指定されたレコードを更新します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">更新するデータのコレクション</param>
        /// <param name="properties">更新対象の列</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int Update<T>(this IDbConnection connection, IEnumerable<T> data, params Expression<Func<T, object>>[] properties)
        {
            var sql = PrimitiveSql.CreateUpdateSql<T>(properties);
            return connection.Execute(sql, data);
        }


        /// <summary>
        /// 指定されたレコードを非同期的に更新します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">更新するデータのコレクション</param>
        /// <param name="properties">更新対象の列</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> UpdateAsync<T>(this IDbConnection connection, IEnumerable<T> data, params Expression<Func<T, object>>[] properties)
        {
            var sql = PrimitiveSql.CreateUpdateSql<T>(properties);
            return connection.ExecuteAsync(sql, data);
        }


        /// <summary>
        /// 全件を指定されたデータで更新します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">更新するデータ</param>
        /// <param name="properties">更新対象の列</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int UpdateAll<T>(this IDbConnection connection, T data, params Expression<Func<T, object>>[] properties)
        {
            var sql = PrimitiveSql.CreateUpdateAllSql<T>(properties);
            return connection.Execute(sql, data);
        }


        /// <summary>
        /// 全件を指定されたデータで非同期的に更新します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">更新するデータ</param>
        /// <param name="properties">更新対象の列</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> UpdateAllAsync<T>(this IDbConnection connection, T data, params Expression<Func<T, object>>[] properties)
        {
            var sql = PrimitiveSql.CreateUpdateAllSql<T>(properties);
            return connection.ExecuteAsync(sql, data);
        }
        #endregion


        #region delete
        /// <summary>
        /// 指定されたレコードをテーブルから削除します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">削除するデータ</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int Delete<T>(this IDbConnection connection, T data)
        {
            var sql = PrimitiveSql.CreateDeleteSql<T>();
            return connection.Execute(sql, data);
        }


        /// <summary>
        /// 指定されたレコードをテーブルから非同期的に削除します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">削除するデータ</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> DeleteAsync<T>(this IDbConnection connection, T data)
        {
            var sql = PrimitiveSql.CreateDeleteSql<T>();
            return connection.ExecuteAsync(sql, data);
        }


        /// <summary>
        /// 指定されたレコードをテーブルから削除します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">削除するデータのコレクション</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int Delete<T>(this IDbConnection connection, IEnumerable<T> data)
        {
            var sql = PrimitiveSql.CreateDeleteSql<T>();
            return connection.Execute(sql, data);
        }


        /// <summary>
        /// 指定されたレコードをテーブルから非同期的に削除します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <param name="data">削除するデータのコレクション</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> DeleteAsync<T>(this IDbConnection connection, IEnumerable<T> data)
        {
            var sql = PrimitiveSql.CreateDeleteSql<T>();
            return connection.ExecuteAsync(sql, data);
        }


        /// <summary>
        /// テーブルから全レコードを削除します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <returns>影響を受けたレコード数</returns>
        public static int DeleteAll<T>(this IDbConnection connection)
        {
            var sql = PrimitiveSql.CreateDeleteAllSql<T>();
            return connection.Execute(sql);
        }


        /// <summary>
        /// テーブルから全レコードを非同期的に削除します。
        /// </summary>
        /// <typeparam name="T">テーブルにマッピングされた型</typeparam>
        /// <param name="connection">データベース接続</param>
        /// <returns>影響を受けたレコード数</returns>
        public static Task<int> DeleteAllAsync<T>(this IDbConnection connection)
        {
            var sql = PrimitiveSql.CreateDeleteAllSql<T>();
            return connection.ExecuteAsync(sql);
        }
        #endregion


        #region 削除補助
        /// <summary>
        /// 指定されたレコードを保存しつつ、削除を許可します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="action">削除処理</param>
        /// <returns>処理に成功したかどうか</returns>
        public static async Task<bool> SafeDelete(int userId, Func<IDbConnection, Task<IEnumerable<object>>> deleteAction)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = DbConnector.CreateDbConnection())
                {
                    connection.Open();

                    //--- 実処理の呼び出し
                    var targets = await deleteAction(connection).ConfigureAwait(false);
                    if (targets == null || !targets.Any())
                        return false;

                    //--- ログ記録
                    var id = await connection.GenerateAutoIdAsync<T_DELETED_RECORD>().ConfigureAwait(false);
                    var json = JsonConvert.SerializeObject(targets);
                    var record = new T_DELETED_RECORD
                    {
                        ID = id,
                        DELETE_TIME = DateTime.Now,
                        USR_ID = userId,
                        DATA = json,
                    };
                    var result = await connection.InsertAsync(record).ConfigureAwait(false);
                    if (result == 1)
                    {
                        transaction.Complete();
                        return true;
                    }
                    return false;
                }
            }
        }

        #endregion
    }
}

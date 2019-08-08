using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.DataAccess;
using ASPDotNetApiProxy.DbModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPDotNetApiProxy.Repositories
{
    /// <summary>
    /// ALL CONNECT社員のリポジトリとしての機能を提供します。
    /// </summary>
    public interface IEmployeeRepository : IDisposable
    {
        /// <summary>
        /// 全レコードを非同期的に取得します。
        /// </summary>
        /// <returns>全レコード</returns>
        Task<IEnumerable<Employee>> GetAllAsync();


        /// <summary>
        /// 指定された主キーに一致するレコードを非同期的に取得します。
        /// </summary>
        /// <param name="id">社員ID</param>
        /// <returns>レコード</returns>
        Task<Employee> GetAsync(int id);


        /// <summary>
        /// 認証処理を行います。
        /// </summary>
        /// <param name="info">ユーザー認証情報</param>
        /// <returns>認証されたかどうか</returns>
        Task<bool> AuthenticateAsync(UserAuthInfo info);
    }



    /// <summary>
    /// データベースで保管/管理を行うALL CONNECT社員のリポジトリを表します。
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        #region IEmployeeRepository メンバー

        /// <summary>
        /// 全レコードを非同期的に取得します。
        /// </summary>
        /// <returns>全レコード</returns>
        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                return connection.QueryAsync<Employee>
                        (
                            this.CreateQueryAllSql(),
                            new { ACTIVE_FLG = 1 }
                        );
            }
        }

        /// <summary>
        /// 指定された主キーに一致するレコードを非同期的に取得します。
        /// </summary>
        /// <param name="id">社員ID</param>
        /// <returns>レコード</returns>
        public async Task<Employee> GetAsync(int id)
        {
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var records = await connection.QueryAsync<Employee>
                            (
                                string.Format("{0}{1}and U.ID = :ID", this.CreateQueryAllSql(), Environment.NewLine),
                                new { ID = id, ACTIVE_FLG = 1 }
                            )
                            .ConfigureAwait(false);
                return records.SingleOrDefault();
            }
        }

        /// <summary>
        /// 認証処理を行います。
        /// </summary>
        /// <param name="info">ユーザー認証情報</param>
        /// <returns>認証されたかどうか</returns>
        public async Task<bool> AuthenticateAsync(UserAuthInfo info)
        {
            using (var connection = DbConnector.CreateDbConnection())
            {
                connection.Open();
                var sql = PrimitiveSql.CreateSelectSql<M_USER>();
                var parameter = new { ID = info.Id };
                var users = await connection.QueryAsync<M_USER>(sql, parameter).ConfigureAwait(false);
                var user = users.FirstOrDefault();
                return user != null
                    &&  Convert.ToBoolean(user.ACTIVE_FLG)
                    && user.PASSWORD == info.Password;
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


        #region SQL生成

        /// <summary>
        /// クエリ用のSQLを生成します。
        /// </summary>
        /// <returns>生成されたSQL</returns>
        private string CreateQueryAllSql()
        {
            return
@"select
    U.ID,
    U.NAME,
    U.PASSWORD
from        dbo.M_USER U
where U.ACTIVE_FLG = :ACTIVE_FLG";
        }

        /// <summary>
        /// クエリ用のSQLを生成します。
        /// </summary>
        /// <returns>生成されたSQL</returns>
        private string CreateQuerySql()
        {
            return
@"select
    U.ID,
    U.NAME,
    U.PASSWORD
from        dbo.M_USER U
where U.ACTIVE_FLG = :ACTIVE_FLG
and
U.ID = :ID";
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using This = ASPDotNetApiProxy.DataAccess.DbConnectionSetting;

namespace ASPDotNetApiProxy.DataAccess
{
    /// <summary>
    /// データベースへの接続設定を提供します。
    /// </summary>
    internal class DbConnectionSetting
    {
        #region プロパティ

        /// <summary>
        /// ユーザーIDを取得します。
        /// </summary>
        public string UserId { get; private set; }


        /// <summary>
        /// パスワードを取得します。
        /// </summary>
        public string Password { get; private set; }


        /// <summary>
        /// データソースを取得します。
        /// </summary>
        public string DataSource { get; private set; }
        
        #endregion


        #region コンストラクタ
        
        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="password">パスワード</param>
        /// <param name="dataSource">データーソース</param>
        public DbConnectionSetting(string userId, string password, string dataSource)
        {
            if (userId == null) throw new ArgumentNullException("userId");
            if (password == null) throw new ArgumentNullException("password");
            if (dataSource == null) throw new ArgumentNullException("dataSource");

            this.UserId = userId;
            this.Password = password;
            this.DataSource = dataSource;
        }

        #endregion

        #region インスタンス生成

        /// <summary>
        /// 指定された接続文字列からインスタンスを生成します。
        /// </summary>
        /// <param name="connectionString">接続文字列</param>
        /// <returns>生成されたインスタンス</returns>
        public static This From(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            var pairs = connectionString
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x =>
                        {
                            var pair = x.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                            if (pair.Length != 2)
                                throw new ArgumentException();
                            return new KeyValuePair<string, string>(pair[0], pair[1]);
                        })
                        .ToDictionary(x => x.Key, x => x.Value);
            return new This(pairs["User ID"], pairs["Password"], pairs["Data Source"]);
        }

        #endregion
    }
}

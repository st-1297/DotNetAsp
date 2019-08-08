using ASPDotNetApiProxy.DataAccess;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using This = ASPDotNetApiProxy.DbConnector;

namespace ASPDotNetApiProxy
{
    /// <summary>
    /// データベースへの接続機能を提供します。
    /// </summary>
    public static class DbConnector
    {
        #region プロパティ

        /// <summary>
        /// 接続文字列を取得または設定します。
        /// </summary>
        private static string ConnectionString { get; set; }

        /// <summary>
        /// DummyDBへの接続文字列を取得または設定します。
        /// </summary>
        private static string DummyConnectionString { get; set; }

        /// <summary>
        /// 接続設定を取得します。
        /// </summary>
        internal static DbConnectionSetting Setting { get; private set; }

        /// <summary>
        /// データベースプロバイダーを生成するインスタンスを取得します。
        /// </summary>
        internal static DbProviderFactory DbProviderFactory
        {
            get
            {
                //return This.factory ?? DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["SqlClient"]);
                return This.factory ?? DbProviderFactories.GetFactory(AppSettings.SqlClient);
            }
        }

        private static DbProviderFactory factory = null;
        
        #endregion

        #region メソッド

        /// <summary>
        /// 接続文字列を設定します。
        /// </summary>
        /// <param name="connectionString">接続文字列</param>
        public static void SetConnectionString(string connectionString)
        {
            if (This.ConnectionString != null) throw new InvalidOperationException("接続文字列はすでに設定されています。");
            if (connectionString == null) throw new ArgumentNullException("connectionString");
            This.ConnectionString = connectionString;
            This.Setting = DbConnectionSetting.From(connectionString);
        }

        /// <summary>
        /// DummyDBへの接続文字列を設定します。
        /// </summary>
        /// <param name="connectionString">接続文字列</param>
        public static void SetDummyConnectionString(string connectionString)
        {
            if (This.DummyConnectionString != null) throw new InvalidOperationException("接続文字列はすでに設定されています。");
            if (connectionString == null) throw new ArgumentNullException("connectionString");
            This.DummyConnectionString = connectionString;
        }

        /// <summary>
        /// データベース接続を生成します。
        /// </summary>
        /// <returns>データベース接続</returns>
        internal static IDbConnection CreateDbConnection()
        {
            var connection = This.DbProviderFactory.CreateConnection();
            connection.ConnectionString = This.ConnectionString;
            return connection;
        }

        /// <summary>
        /// Dummyデータベース接続を生成します。
        /// </summary>
        /// <returns>データベース接続</returns>
        internal static IDbConnection CreateDummyDbConnection()
        {
            var connection = This.DbProviderFactory.CreateConnection();
            connection.ConnectionString = This.DummyConnectionString;
            return connection;
        }

        #endregion
    }
}

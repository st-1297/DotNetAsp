using ASPDotNetApiProxy.Handlers;
using ASPDotNetApiProxy.Extensions;
using System;
using System.Net.Http;
using System.Security;
using This = ASPDotNetApiProxy.Proxies.ApiClient;

namespace ASPDotNetApiProxy.Proxies
{
    /// <summary>
    /// Web APIへのアクセス機構を提供します。
    /// </summary>
    public static class ApiClient
    {
        #region プロパティ

        /// <summary>
        /// Web APIへのルートURLを取得または設定します。
        /// </summary>
        private static Uri RootUrl { get; set; }


        /// <summary>
        /// 暗号化されたアクセストークンを取得または設定します。
        /// </summary>
        private static SecureString AccessToken { get; set; }
        
        #endregion

        #region メソッド

        /// <summary>
        /// Web APIへのルートURLを設定します。
        /// </summary>
        /// <param name="url">ルートURL</param>
        public static void SetRootUrl(Uri url)
        {
            if (This.RootUrl != null) throw new InvalidOperationException("URLはすでに設定されています。");
            if (url == null) throw new ArgumentNullException("url");
            This.RootUrl = url;
        }

        /// <summary>
        /// アクセストークンを設定します。
        /// </summary>
        /// <param name="token">アクセストークン</param>
        internal static void SetAccessToken(string token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            This.ClearAccessToken();

            This.AccessToken = token.Encrypt();

            //--- 読み取り専用にする
            This.AccessToken.MakeReadOnly();
        }

        /// <summary>
        /// アクセストークンをクリアします。
        /// </summary>
        internal static void ClearAccessToken()
        {
            if (This.AccessToken == null)
                return;

            This.AccessToken.Dispose();
            This.AccessToken = null;
        }

        /// <summary>
        /// アクセス認証を考慮したWeb APIのClientを生成します。
        /// </summary>
        /// <returns>HttpClient</returns>
        internal static HttpClient Create()
        {
            if (This.AccessToken != null)
                if (This.AccessToken.Length != 0)
                {
                    var handler = new AuthMessageHandler(This.AccessToken);
                    return new HttpClient(handler) { BaseAddress = This.RootUrl, Timeout = new TimeSpan(0, 10, 0) };
                }
            throw new InvalidOperationException("アクセストークンが設定されていません。");
        }

        /// <summary>
        /// 既定のWeb APIのClientを生成します。
        /// </summary>
        /// <returns>HttpClient</returns>
        internal static HttpClient CreateDefault()
        {
            return new HttpClient() { BaseAddress = This.RootUrl };
        }

        #endregion
    }
}

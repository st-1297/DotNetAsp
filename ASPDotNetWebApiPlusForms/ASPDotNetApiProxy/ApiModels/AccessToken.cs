using System;

namespace ASPDotNetApiProxy.ApiModels
{
    /// <summary>
    /// 認証トークンを表します。
    /// </summary>
    public abstract class AuthToken
    {
        /// <summary>
        /// トークンの値を取得または設定します。
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// トークンの生成時間を取得または設定します。
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// トークンの有効期限を取得または設定します。
        /// </summary>
        public DateTime ExpirationTime { get; set; }
    }

    /// <summary>
    /// リクエストトークンを表します。
    /// </summary>
    public sealed class RequestToken : AuthToken
    { }

    /// <summary>
    /// アクセストークンを表します。
    /// </summary>
    public sealed class AccessToken : AuthToken
    {
        /// <summary>
        /// ユーザーIDを取得または設定します。
        /// </summary>
        public int? UserId { get; set; }
    }
}
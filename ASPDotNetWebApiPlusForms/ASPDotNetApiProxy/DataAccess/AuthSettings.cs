using System;
using This = ASPDotNetApiProxy.DataAccess.AuthSettings;

namespace ASPDotNetApiProxy.DataAccess
{
    /// <summary>
    /// 認証設定を表します。
    /// </summary>
    public static class AuthSettings
    {
        #region プロパティ

        /// <summary>
        /// リクエストトークンの有効期限を取得します。
        /// </summary>
        internal static TimeSpan RequestTokenValidDuration { get; private set; }

        /// <summary>
        /// アクセストークンの有効期限を取得します。
        /// </summary>
        internal static TimeSpan AccessTokenValidDuration { get; private set; }
        
        #endregion

        #region メソッド
        
        /// <summary>
        /// リクエストトークンの有効期限を設定します。
        /// </summary>
        /// <param name="duration">有効期限</param>
        public static void SetRequestTokenValidDuration(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero) throw new ArgumentException("0以下の値は設定できません。");
            if (This.RequestTokenValidDuration > TimeSpan.Zero) throw new InvalidOperationException("すでに値が設定されています。");
            This.RequestTokenValidDuration = duration;
        }

        /// <summary>
        /// アクセストークンの有効期限を設定します。
        /// </summary>
        /// <param name="duration">有効期限</param>
        public static void SetAccessTokenValidDuration(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero) throw new ArgumentException("0以下の値は設定できません。");
            if (This.AccessTokenValidDuration > TimeSpan.Zero) throw new InvalidOperationException("すでに値が設定されています。");
            This.AccessTokenValidDuration = duration;
        }
        
        #endregion
    }
}

using System;
using System.Web;
using This = ASPDotNetWebApi.Extensions.HttpContextBaseExtensions;

namespace ASPDotNetWebApi.Extensions
{
    /// <summary>
    /// HttpRequestMessageの拡張機能を提供します。
    /// </summary>
    public static class HttpContextBaseExtensions
    {
        #region フィールド
        /// <summary>
        /// UserIDをキャッシュするためのキーを保持します。
        /// </summary>
        private static string UserIdKey = "UserId";
        #endregion


        #region UserId
        /// <summary>
        /// 現在アクセスしているユーザーIDを取得します。
        /// </summary>
        /// <param name="context">リクエスト毎のコンテキスト</param>
        /// <returns>ユーザーID</returns>
        public static bool ExistsUserId(this HttpContextBase context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            return context.Items.Contains(This.UserIdKey);
        }


        /// <summary>
        /// 現在アクセスしているユーザーIDを取得します。
        /// </summary>
        /// <param name="context">リクエスト毎のコンテキスト</param>
        /// <returns>ユーザーID</returns>
        public static int GetUserId(this HttpContextBase context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            return (int)context.Items[This.UserIdKey];
        }


        /// <summary>
        /// リクエスト毎のコンテキストにユーザーIDを設定します。
        /// </summary>
        /// <param name="context">リクエスト毎のコンテキスト</param>
        /// <param name="userId">ユーザーID</param>
        public static void SetUserId(this HttpContextBase context, int userId)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            context.Items.Add(This.UserIdKey, userId);
        }
        #endregion
    }
}
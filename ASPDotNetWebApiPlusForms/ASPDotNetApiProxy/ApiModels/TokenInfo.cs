using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ASPDotNetApiProxy.ApiModels
{
    /// <summary>
    /// トークン情報を表します。
    /// </summary>
    public class TokenInfo
    {
        #region プロパティ

        /// <summary>
        /// リクエストトークンがあるかどうかを取得します。
        /// </summary>
        public bool HasRequestToken { get { return !string.IsNullOrEmpty(this.RequestToken); } }


        /// <summary>
        /// アクセストークンがあるかどうかを取得します。
        /// </summary>
        public bool HasAccessToken { get { return !string.IsNullOrEmpty(this.AccessToken); } }


        /// <summary>
        /// リクエストトークンを取得します。
        /// </summary>
        public string RequestToken { get; private set; }


        /// <summary>
        /// アクセストークンを取得します。
        /// </summary>
        public string AccessToken { get; private set; }
        
        #endregion

        #region コンストラクタ
        
        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        private TokenInfo()
        { }
        
        #endregion

        #region メソッド
        
        /// <summary>
        /// 指定されたリクエストメッセージからインスタンスを生成します。
        /// </summary>
        /// <param name="request">リクエストメッセージ</param>
        /// <returns>インスタンス</returns>
        public static TokenInfo From(HttpRequestMessage request)
        {
            //--- ヘッダーの値を取得
            Func<string, string> getHeaderValue = key =>
            {
                IEnumerable<string> values;
                return request.Headers.TryGetValues(key, out values)
                    ? values.Single()
                    : null;
            };

            //--- インスタンス生成
            return new TokenInfo()
            {
                RequestToken = getHeaderValue("RequestToken"),
                AccessToken = getHeaderValue("AccessToken"),
            };
        }

        #endregion
    }
}
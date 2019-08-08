using ASPDotNetApiProxy.Extensions;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace ASPDotNetApiProxy.Handlers
{
    /// <summary>
    /// 認証機能を持つメッセージハンドラーを提供します。
    /// </summary>
    internal class AuthMessageHandler : DelegatingHandler
    {
        #region フィールド

        /// <summary>
        /// アクセストークンを保持します。
        /// </summary>
        private readonly SecureString accessToken = null;
        
        #endregion

        #region コンストラクタ

        /// <summary>
        /// アクセストークンを指定してインスタンスを生成します。
        /// </summary>
        /// <param name="accessToken">アクセストークン</param>
        public AuthMessageHandler(SecureString accessToken)
            : this(new HttpClientHandler(), accessToken)
        { }

        /// <summary>
        /// HttpClientの内部で利用するメッセージハンドラとアクセストークンを指定してインスタンスを生成します。
        /// </summary>
        /// <param name="innerHandler">内部で利用するメッセージハンドラ</param>
        /// <param name="accessToken">アクセストークン</param>
        public AuthMessageHandler(HttpMessageHandler innerHandler, SecureString accessToken)
            : base(innerHandler)
        {
            if (accessToken == null)
                throw new ArgumentNullException("accessToken");
            this.accessToken = accessToken;
        }

        #endregion

        #region オーバーライド

        /// <summary>
        /// HTTP通信の前後をフックし、カスタム処理を行います。
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = this.accessToken.Decrypt();
            request.Headers.Add("AccessToken", token);
            return base.SendAsync(request, cancellationToken);
        }

        #endregion
    }
}

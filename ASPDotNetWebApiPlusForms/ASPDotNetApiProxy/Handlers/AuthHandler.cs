using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.Repositories;
using ASPDotNetApiProxy.Proxies;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ASPDotNetApiProxy.Extensions;

namespace ASPDotNetWebApi.Handlers
{
    public class AuthHandler : DelegatingHandler
    {
        #region フィールド

        /// <summary>
        /// アクセストークンリポジトリを保持します。
        /// </summary>
        private readonly IAccessTokenRepository accessTokenRepository = null;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 既定の設定でインスタンスを生成します。
        /// </summary>
        public AuthHandler()
            : this(new AccessTokenRepository())
        { }


        /// <summary>
        /// 指定されたリポジトリを利用するインスタンスを生成します。
        /// </summary>
        /// <param name="access">アクセストークンリポジトリ</param>
        public AuthHandler(IAccessTokenRepository access)
        {
            this.accessTokenRepository = access;
        }

        #endregion

        #region オーバーライド

        /// <summary>
        /// 実処理前後をフックし、カスタム処理を行います。
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //--- アクセストークンが指定されているかを確認
            TokenInfo info = null;

            try
            { info = TokenInfo.From(request); }
            catch (Exception ex)
            { return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); }

            if (!info.HasAccessToken)
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "アクセストークンが指定されていません。");

            //--- アクセストークンが有効かを確認
            var userId = await this.accessTokenRepository.GetUserIdAsync(info.AccessToken).ConfigureAwait(false);

            if (!userId.HasValue)
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "指定されたアクセストークンは無効です。");

            //--- ユーザーIDをキャッシュ
            var context = request.GetHttpContext();

            context.SetUserId(userId.Value);

            //--- 実処理の呼び出し
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        #endregion

    }
}
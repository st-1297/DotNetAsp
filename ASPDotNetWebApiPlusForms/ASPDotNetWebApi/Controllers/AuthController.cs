using ASPDotNetApiProxy;
using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using This = ASPDotNetWebApi.Controllers.AuthController;

namespace ASPDotNetWebApi.Controllers
{
    /// <summary>
    /// 認証に関する要求を処理します。
    /// </summary>
    public class AuthController : ApiController
    {
        #region フィールド

        /// <summary>
        /// リクエストトークンリポジトリを保持します。
        /// </summary>
        private readonly IRequestTokenRepository requestTokenRepository = null;


        /// <summary>
        /// アクセストークンリポジトリを保持します。
        /// </summary>
        private readonly IAccessTokenRepository accessTokenRepository = null;


        /// <summary>
        /// ALL CONNECT社員のリポジトリを保持します。
        /// </summary>
        private readonly IEmployeeRepository employeeRepository = null;

        #endregion

        #region コンストラクタ
        
        /// <summary>
        /// 既定の設定でインスタンスを生成します。
        /// </summary>
        public AuthController()
            : this(new RequestTokenRepository(), new AccessTokenRepository(), new EmployeeRepository())
        { }


        /// <summary>
        /// 指定されたリポジトリを利用するインスタンスを生成します。
        /// </summary>
        /// <param name="request">リクエストトークンリポジトリ</param>
        /// <param name="access">アクセストークンリポジトリ</param>
        /// <param name="acUser">ALL CONNECT社員リポジトリ</param>
        public AuthController(IRequestTokenRepository request, IAccessTokenRepository access, IEmployeeRepository acUser)
        {
            this.requestTokenRepository = request;
            this.accessTokenRepository = access;
            this.employeeRepository = acUser;
        }

        #endregion

        #region オーバーライド
        
        /// <summary>
        /// 後処理を行います。
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this.requestTokenRepository.Dispose();
            this.accessTokenRepository.Dispose();
            this.employeeRepository.Dispose();
            base.Dispose(disposing);
        }
        
        #endregion

        #region GET

        /// <summary>
        /// リクエストトークンを発行します。
        /// </summary>
        /// <returns>リクエストトークン</returns>
        public async Task<RequestToken> Get()
        {
            var token = await this.requestTokenRepository.CreateAsync().ConfigureAwait(false);
            if (token == null)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            return token;
        }

        #endregion

        #region POST

        /// <summary>
        /// アクセストークンを発行します。
        /// </summary>
        /// <returns>アクセストークン</returns>
        public async Task<AccessToken> Post()
        {
            //--- リクエストトークンが指定されているか
            TokenInfo info = null;
            try { info = TokenInfo.From(this.Request); }
            catch { throw new HttpResponseException(HttpStatusCode.BadRequest); }
            if (!info.HasRequestToken)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //--- リクエストトークンが有効かを確認
            var isValid = await this.requestTokenRepository.IsValidAsync(info.RequestToken).ConfigureAwait(false);
            if (!isValid)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            //--- 使用したリクエストトークンを削除
            var deleted = await this.requestTokenRepository.DeleteAsync(info.RequestToken).ConfigureAwait(false);
            if (!deleted)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            //--- ユーザー認証情報が指定されているか確認
            var userInfo = await this.Request.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            if (userInfo == null || userInfo.Length == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //--- ユーザーID / パスワードを照合
            var service = new CryptoService(info.RequestToken);
            var json = service.Decrypt(userInfo);
            var authInfo = JsonConvert.DeserializeObject<UserAuthInfo>(json);
            var authorized = await this.employeeRepository.AuthenticateAsync(authInfo).ConfigureAwait(false);
            if (!authorized)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            //--- アクセストークンを発行
            return await this.accessTokenRepository.CreateAsync(authInfo.Id).ConfigureAwait(false);
        }

        #endregion

        #region DELETE

        /// <summary>
        /// 指定されたアクセストークンを削除します。
        /// </summary>
        /// <returns>レスポンス</returns>
        public async Task<HttpResponseMessage> Delete()
        {
            //--- アクセストークンが指定されているかを確認
            TokenInfo info = null;
            try { info = TokenInfo.From(this.Request); }
            catch (Exception ex) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); }
            if (!info.HasAccessToken)
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "アクセストークンが指定されていません。");

            //--- 削除実行
            var result = await this.accessTokenRepository.DeleteAsync(info.AccessToken).ConfigureAwait(false);
            return result
                ? this.Request.CreateResponse(HttpStatusCode.NoContent)
                : this.Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// 指定されたアクセストークンを削除します。
        /// 顧客のロック情報も削除します。
        /// </summary>
        /// <param name="userlock">0は顧客ロックを解除しない。1は顧客ロックを解除する。</param>
        /// <returns>レスポンス</returns>
        public async Task<HttpResponseMessage> DeleteAndUnLock(int userlock)
        {
            //--- アクセストークンが指定されているかを確認
            TokenInfo info = null;
            try { info = TokenInfo.From(this.Request); }
            catch (Exception ex) { return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); }

            if (!info.HasAccessToken)
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "アクセストークンが指定されていません。");
            
            //-- STR
            ////ユーザがロックしている顧客を解放
            //var user_id = await this.accessTokenRepository.GetUserIdAsync(info.AccessToken);
            //if (userlock == 1)
            //{
            //    if (user_id != null)
            //        await new CommonRepository().DeleteUnlockByUserId((int)user_id);

            //}
            //-- END

            //--- 削除実行
            var result = await this.accessTokenRepository.DeleteAsync(info.AccessToken).ConfigureAwait(false);

            return result
                ? this.Request.CreateResponse(HttpStatusCode.NoContent)
                : this.Request.CreateResponse(HttpStatusCode.NotFound);
        }

        #endregion
    }
}
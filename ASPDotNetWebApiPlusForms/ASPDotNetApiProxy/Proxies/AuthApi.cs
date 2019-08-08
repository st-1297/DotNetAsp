using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using This = ASPDotNetApiProxy.Proxies.AuthApi;

namespace ASPDotNetApiProxy.Proxies
{
    /// <summary>
    /// 認証APIを提供します。
    /// </summary>
    public static class AuthApi
    {
        #region 認証

        /// <summary>
        /// 指定されたユーザー情報で認証を行います。
        /// </summary>
        /// <param name="info">ユーザー認証情報</param>
        /// <returns>認証に成功したかどうか</returns>
        public static async Task<bool> AuthenticateAsync(UserAuthInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            ApiClient.ClearAccessToken();
            var requestToken = await This.GetRequestTokenAsync().ConfigureAwait(false);
            if (requestToken == null)
                return false;

            var accessToken = await This.GetAccessTokenAsync(info, requestToken).ConfigureAwait(false);
            if (accessToken == null)
                return false;

            ApiClient.SetAccessToken(accessToken.Value);
            return true;
        }

        /// <summary>
        /// 現在認証されているユーザーを認証解除します。
        /// </summary>
        /// <returns>認証解除に成功したかどうか</returns>
        public static async Task<bool> DeauthenticateAsync()
        {
            var response = await ApiClient.Create().DeleteAsync("Auth").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NotFound)
                return false;

            ApiClient.ClearAccessToken();
            return true;
        }

        /// <summary>
        /// 現在認証されているユーザーを認証解除します。
        /// </summary>
        /// <returns>認証解除に成功したかどうか</returns>
        public static async Task<bool> DeauthenticateAndUnlockAsync()
        {
            var response = await ApiClient.Create().DeleteAsync("Auth?userlock=1").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NotFound)
                return false;

            ApiClient.ClearAccessToken();
            return true;
        }

        #endregion

        #region 補助

        /// <summary>
        /// リクエストトークンを取得します。
        /// </summary>
        /// <returns>リクエストトークン</returns>
        private static async Task<RequestToken> GetRequestTokenAsync()
        {
            var response = await ApiClient.CreateDefault().GetAsync("Auth").ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return null;

            response.ThrowIfError();
            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<RequestToken>(body);
        }

        /// <summary>
        /// アクセストークンを取得します。
        /// </summary>
        /// <param name="info">ユーザー認証情報</param>
        /// <param name="requestToken">リクエストトークン</param>
        /// <returns>アクセストークン</returns>
        private static async Task<AccessToken> GetAccessTokenAsync(UserAuthInfo info, RequestToken requestToken)
        {
            var json = JsonConvert.SerializeObject(info);
            var service = new CryptoService(requestToken.Value);
            var encrypted = service.Encrypt(json);
            var content = new ByteArrayContent(encrypted);
            content.Headers.Add("RequestToken", requestToken.Value);
            var response = await ApiClient.CreateDefault().PostAsync("Auth", content).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return null;

            response.ThrowIfError();
            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<AccessToken>(body);
        }

        #endregion
    }
}

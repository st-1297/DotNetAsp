using ASPDotNetApiProxy.Proxies;
using System;
using System.Net.Http;

namespace ASPDotNetApiProxy.Extensions
{
    /// <summary>
    /// HttpResponseMessageの拡張機能を提供します。
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// 応答のステータスコードが失敗を示している場合に例外をスローします。
        /// </summary>
        /// <param name="response">HTTP通信の応答</param>
        /// <exception cref="ArgumentNullException">引数のnullの場合にスローされます。</exception>
        /// <exception cref="ApiCallException">ステータスコードが失敗を示している場合にスローされます。</exception>
        public static void ThrowIfError(this HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            if (!response.IsSuccessStatusCode)
                throw new ApiCallException(response.StatusCode, response.ReasonPhrase);
        }
    }
}

using System;
using System.Net.Http;
using System.Web;

namespace ASPDotNetApiProxy.Extensions
{
    /// <summary>
    /// HttpRequestMessageの拡張機能を提供します。
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// HttpContextを取得します。
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>HttpContext</returns>
        public static HttpContextBase GetHttpContext(this HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            object value;
            return request.Properties.TryGetValue("MS_HttpContext", out value)
                ? value as HttpContextBase
                : null;
        }
    }
}

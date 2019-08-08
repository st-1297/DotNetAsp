using ASPDotNetApiProxy.DbModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASPDotNetApiProxy
{
    public static class HttpClientProxy
    {

        #region プロパティ

        /// <summary>
        /// 
        /// </summary>
        public static HttpClient Client = new HttpClient();

        /// <summary>
        /// 認証ありのWeb APIを利用する時に利用するパラメータです。
        /// </summary>
        public static string BearerValue { get; set; } = string.Empty;

        #endregion

        #region メソッド

        /// <summary>
        /// ベースURIを設定します
        /// </summary>
        public static void SetRootUri(string uri)
        {
            Client.BaseAddress = new Uri(uri);
        }

        /// <summary>
        /// アクセストークンを考慮したクライアントを返します
        /// </summary>
        public static HttpClient Create()
        {
            // TODO
            return Client;
        }

        public static async Task<T> ExecutePostAsync<T>(string url, T contents)
        {
            //各種設定を行います。
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpClientProxy.BearerValue);

            //指定されたContentsを指定されたURLにPOSTします。
            var response = await Client.PostAsJsonAsync<T>(url, contents);

            //レスポンスのContentsをJson形式から指定されたT型のObjectのインスタンスに変換します。
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        #endregion

        #region SendAsync

        public static async Task<HttpRequestMessage> SendAsync(HttpCompletionOption httpCompletion, CancellationToken cancellationToken)
        {
            var json = new HttpRequestMessage();

            return json;
        }

        #endregion

        public static async Task<PRODUCT> GetProductByIdAsync(int id)
        {
            PRODUCT product = null;
            var url = $"products/?id={id}";

            try
            {
                HttpResponseMessage response = await Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<PRODUCT>();
                }
            }
            catch
            {

            }

            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiptId"></param>
        /// <param name="aplId"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public static async Task<PRODUCT> CreateProductAsync(PRODUCT product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var response = await Client.PostAsync("products/", content).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PRODUCT>(body);
        }

        //public static async Task<Uri> CreateProductAsync(Product product)
        //{
            
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var json = JsonConvert.SerializeObject(product);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = null;

        //    try
        //    {
        //        var url = "products/";
        //        //response = await client.PostAsync("api/products", content);
        //        response = await client.PostAsJsonAsync(url, content);
        //        response.EnsureSuccessStatusCode();
                
        //    }
        //    catch
        //    {

        //    }

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

        public static async Task<PRODUCT> UpdateProductAsync(PRODUCT product)
        {
            HttpResponseMessage response = await Client.PutAsJsonAsync(
                $"api/products/{product.ID}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<PRODUCT>();
            return product;
        }

        public static async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            HttpResponseMessage response = await Client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }

    }
}

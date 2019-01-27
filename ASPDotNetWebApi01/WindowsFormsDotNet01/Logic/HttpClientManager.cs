using ASPDotNetWebApi01.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WindowsFormsDotNet01.Logic
{
    class HttpClientManager
    {
        private static HttpClient client = new HttpClient();
        private static string rootUri = ConfigurationManager.AppSettings["RootUri"];

        /// <summary>
        /// 静的コンストラクタです。
        /// </summary>
        private HttpClientManager ()
        {
            //client.BaseAddress = new Uri("http://localhost:63991/");
            client.BaseAddress = new Uri(rootUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// 認証ありのWeb APIを利用する時に利用するパラメータです。
        /// </summary>
        public static string BearerValue { get; set; } = string.Empty;

        public static async Task<T> ExecutePostAsync<T>(string url, T contents)
        {
            //各種設定を行います。
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpClientManager.BearerValue);

            //指定されたContentsを指定されたURLにPOSTします。
            var response = await client.PostAsJsonAsync < T > (url, contents);

            //レスポンスのContentsをJson形式から指定されたT型のObjectのインスタンスに変換します。
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<IEnumerable<Product>> GetProductAsync()
        {
            List<Product> product = null;
            var param = "api/products/";
            var url = $"{rootUri}{param}";
            //var uri = rootUri + "api/products/";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<List<Product>>();
                }
            }
            catch
            {

            }
            
            return product?.ToArray();
        }

        public static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public static async Task<Product> UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }

        public static async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }

    }
}

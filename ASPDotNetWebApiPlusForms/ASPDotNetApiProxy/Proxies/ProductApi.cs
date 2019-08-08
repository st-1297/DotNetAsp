using ASPDotNetApiProxy.DbModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPDotNetApiProxy.Proxies
{
    public static class ProductApi
    {
        /// <summary>
        /// プロダクトテーブル情報を取得します。
        /// </summary>
        /// <returns>レコード</returns>
        public static async Task<IEnumerable<PRODUCT>> GetAllProductsAsync(int? id)
        {
            var url = (id == null) ? "products/" : $"products/?id={id}";
            var response = await HttpClientProxy.Create().GetAsync(url).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IEnumerable<PRODUCT>>(body);

            //return product?.ToArray();
            
        }

        

    }
}

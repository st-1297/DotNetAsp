using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPDotNetWebApiPlusForms.Models;

namespace ASPDotNetWebApiPlusForms.DbAccesses
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbAccessForProducts : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>

    }


    public class DbAccessForProducts : IDbAccessForProducts
    {

        public async Task<IEnumerable<Product>> GetInfo(int? id)
        {
            return null;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

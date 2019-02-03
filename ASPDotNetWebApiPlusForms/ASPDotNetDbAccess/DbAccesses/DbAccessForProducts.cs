using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPDotNetDbAccess.DbAccesses
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

        public async Task<IEnumerable<Product>> GetInitializeInfo(String ReceiptId)
        {

        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

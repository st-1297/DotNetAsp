using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPDotNetApiProxy.DbModels
{
    /// <summary>
    /// シーケンスの次の値を提供します。
    /// </summary>
    internal class SequenceNextValue
    {
        /// <summary>
        /// 次の値を取得または設定します。
        /// </summary>
        public int NEXTVAL { get; set; }
    }
}

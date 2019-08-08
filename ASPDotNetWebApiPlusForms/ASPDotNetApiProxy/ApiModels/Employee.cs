using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPDotNetApiProxy.ApiModels
{
    /// <summary>
    /// 社員を表します。
    /// </summary>
    public class Employee
    {
        #region プロパティ

        /// <summary>
        /// ユーザーIDを取得または設定します。
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        public string Name { get; set; }
        
        #endregion
    }
}

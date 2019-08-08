using System;

namespace ASPDotNetApiProxy.DataAccess
{
    /// <summary>
    /// シーケンス名を表す属性を表します。
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false)]
    public class SequenceAttribute : Attribute
    {
        #region プロパティ

        /// <summary>
        /// シーケンス名を取得します。
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// スキーマ名を取得または設定します。
        /// </summary>
        public string Schema { get; set; }


        /// <summary>
        /// 省略なしの名称を取得します。
        /// </summary>
        public string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Schema)
                    ? this.Name
                    : string.Format("{0}.{1}", this.Schema, this.Name);
            }
        }

        #endregion


        #region コンストラクタ

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="name">シーケンス名</param>
        public SequenceAttribute(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            this.Name = name;
        }

        #endregion
    }
}

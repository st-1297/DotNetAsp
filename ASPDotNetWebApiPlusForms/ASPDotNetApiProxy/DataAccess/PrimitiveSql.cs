using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using This = ASPDotNetApiProxy.DataAccess.PrimitiveSql;

namespace ASPDotNetApiProxy.DataAccess
{
    /// <summary>
    /// プリミティブなSQLに対する汎用自動生成機能を提供します。
    /// </summary>
    internal static class PrimitiveSql
    {

        #region enum

        internal enum DB
        {
            Oracle,
            SQLServer
        }

        #endregion

        #region auto id

        /// <summary>
        /// 指定された型情報から主キーに付属しているシーケンスのインクリメントクエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <returns>生成されたSQL</returns>
        /// <remarks>
        /// [作成例]
        /// 
        /// select * sequence.nextval from dual
        /// </remarks>
        public static string CreateNextAutoIdSql<T>()
        {
            try
            {
                var keyType = typeof(KeyAttribute);
                var autoKey = This.GetProperties<T>()
                            .Where(x => x.GetCustomAttributesData().Any(y => y.AttributeType == keyType))   //--- 主キーのみ
                            .Select(x => new
                            {
                                ColumnName = x.Name,
                                Sequence = x.GetCustomAttribute<SequenceAttribute>(false),
                            })
                            .Single(x => x.Sequence != null);
                return string.Format("select {0}.nextval from dual", autoKey.Sequence.FullName);
            }
            catch
            {
                throw new ArgumentException("主キーが1つでシーケンスが利用されている型が指定されていません。");
            }
        }
        #endregion

        #region count

        /// <summary>
        /// 指定された型情報からテーブルに含まれるレコードの件数を取得するクエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <returns>生成されたSQL</returns>
        public static string CreateCountSql<T>()
        {
            var table = This.GetTableAttribute<T>();
            return string.Format("select count(*) as value from {0}.{1}", table.Schema, table.Name);
        }

        #endregion

        #region select

        /// <summary>
        /// 指定された型情報から全レコードを取得するクエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <param name="properties">取得対象の列</param>
        /// <returns>生成されたSQL</returns>
        /// <remarks>
        /// [作成例]
        /// 
        ///  select * from ACNSM.NSM_M_COMMON
        /// </remarks>
        public static string CreateSelectAllSql<T>(params Expression<Func<T, object>>[] properties)
        {
            var table = This.GetTableAttribute<T>();
            if (properties == null || properties.Length == 0)
            {
                return string.Format("select * from {0}.{1}", table.Schema, table.Name);
            }
            else
            {
                var columnNames = properties.GetMemberNames();
                var separator = string.Format(",{0}\t", Environment.NewLine);
                var builder = new StringBuilder();
                builder.AppendLine("select");
                builder.Append("\t");
                builder.AppendLine(string.Join(separator, columnNames));
                builder.AppendFormat("from {0}.{1}", table.Schema, table.Name);
                return builder.ToString();
            }
        }


        /// <summary>
        /// 指定された型情報から主キーによるレコード検索クエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <param name="properties">取得対象の列</param>
        /// <returns>生成されたSQL</returns>
        /// <remarks>
        /// [作成例]
        /// 
        ///  select * from ACNSM.NSM_M_COMMON
        ///  where MASTER_CD_KBN = :MASTER_CD_KBN
        ///  and CD_KBN = :CD_KBN
        /// </remarks>
        public static string CreateSelectSql<T>(DB dB = DB.SQLServer, params Expression<Func<T, object>>[] properties)
        {
            IEnumerable<string> primaries = null;
            switch (dB)
            {
                case DB.SQLServer:
                    primaries = This.GetPrimaryKeyProperties<T>().Select(x => string.Format("{0} = @{0}", x));
                    break;
                case DB.Oracle:
                    primaries = This.GetPrimaryKeyProperties<T>().Select(x => string.Format("{0} = :{0}", x));
                    break;
            }
            //var primaries = This.GetPrimaryKeyProperties<T>().Select(x => string.Format("{0} = :{0}", x));
            var separator = string.Format("{0}and ", Environment.NewLine);
            var builder = new StringBuilder();
            builder.AppendLine(This.CreateSelectAllSql<T>(properties));
            builder.Append(" where ");
            builder.Append(string.Join(separator, primaries));
            return builder.ToString();
        }

        #endregion

        #region insert

        /// <summary>
        /// 指定された型情報からレコードの挿入クエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <returns>生成されたSQL</returns>
        public static string CreateInsertSql<T>(DB dB = DB.SQLServer)
        {
            //--- 列情報などのパラメーターを生成
            var properties = This.GetProperties<T>();
            var columnNames = properties.Select(x => string.Format("\t{0}", x.Name));

            IEnumerable<string> values = null;
            switch (dB)
            {
                case DB.SQLServer:
                    values = properties.Select(x => string.Format("\t@{0}", x.Name));
                    break;
                case DB.Oracle:
                    values = properties.Select(x => string.Format("\t:{0}", x.Name));
                    break;
            }
            //var values = properties.Select(x => string.Format("\t:{0}", x.Name));

            var table = This.GetTableAttribute<T>();
            var separator = string.Format(",{0}", Environment.NewLine);

            //--- SQL組み立て
            var builder = new StringBuilder();
            builder.AppendFormat("insert into {0}.{1}", table.Schema, table.Name);
            builder.AppendLine();
            builder.AppendLine("(");
            builder.AppendLine(string.Join(separator, columnNames));
            builder.AppendLine(")");
            builder.AppendLine("values");
            builder.AppendLine("(");
            builder.AppendLine(string.Join(separator, values));
            builder.Append(")");
            return builder.ToString();
        }

        #endregion


        #region update

        /// <summary>
        /// 指定された型情報から全件更新クエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <param name="properties">更新対象の列</param>
        /// <returns>生成されたSQL</returns>
        /// <remarks>
        /// [作成例]
        /// 
        ///  update TableName
        /// set
        ///     Column1 = :Column1
        ///     Column2 = :Column2
        /// </remarks>
        public static string CreateUpdateAllSql<T>(params Expression<Func<T, object>>[] properties)
        {
            //--- パラメーター
            var table = This.GetTableAttribute<T>();
            var columnNames = properties == null || properties.Length == 0
                            ? This.GetProperties<T>().Select(x => x.Name)
                            : properties.GetMemberNames();
            var setParams = columnNames.Select(x => string.Format("\t{0} = :{0}", x));
            var separator = string.Format(",{0}", Environment.NewLine);

            //--- SQL生成
            var builder = new StringBuilder();
            builder.AppendFormat("update {0}.{1}", table.Schema, table.Name);
            builder.AppendLine();
            builder.AppendLine("set");
            builder.Append(string.Join(separator, setParams));
            return builder.ToString();
        }


        /// <summary>
        /// 指定された型情報から主キーによる更新クエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <param name="properties">更新対象の列</param>
        /// <returns>生成されたSQL</returns>
        /// <remarks>
        /// [作成例]
        /// 
        ///  update TableName
        /// set
        ///     Column3 = :Column3
        ///     Column4 = :Column4
        ///  where Column1 = :Column1
        ///  and Column2 = :Column2
        /// </remarks>
        public static string CreateUpdateSql<T>(params Expression<Func<T, object>>[] properties)
        {
            var primaries = This.GetPrimaryKeyProperties<T>().Select(x => string.Format("{0} = :{0}", x));
            var separator = string.Format("{0}and ", Environment.NewLine);
            var builder = new StringBuilder();
            builder.AppendLine(This.CreateUpdateAllSql<T>(properties));
            builder.Append("where ");
            builder.Append(string.Join(separator, primaries));
            return builder.ToString();
        }

        #endregion

        #region delete
        /// <summary>
        /// 指定された型情報から全レコードを削除するクエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <returns>生成されたSQL</returns>
        /// <remarks>
        /// [作成例]
        /// 
        /// delete from ACNSM.NSM_M_COMMON
        /// </remarks>
        public static string CreateDeleteAllSql<T>()
        {
            var table = This.GetTableAttribute<T>();
            return string.Format("delete from {0}.{1}", table.Schema, table.Name);
        }


        /// <summary>
        /// 指定された型情報から全レコードを削除するクエリを生成します。
        /// </summary>
        /// <typeparam name="T">テーブルの型</typeparam>
        /// <returns>生成されたSQL</returns>
        /// <remarks>
        /// [作成例]
        /// 
        /// delete from TableName
        /// where PrimaryKey = :PrimaryKey
        /// </remarks>
        public static string CreateDeleteSql<T>(DB dB = DB.SQLServer)
        {
            IEnumerable<string> primaries = null;
            switch (dB)
            {
                case DB.SQLServer:
                    primaries = This.GetPrimaryKeyProperties<T>().Select(x => string.Format("{0} = @{0}", x));
                    break;
                case DB.Oracle:
                    primaries = This.GetPrimaryKeyProperties<T>().Select(x => string.Format("{0} = :{0}", x));
                    break;
            }

            //var primaries = This.GetPrimaryKeyProperties<T>().Select(x => string.Format("{0} = :{0}", x));
            var separator = string.Format("{0}and ", Environment.NewLine);
            var builder = new StringBuilder();
            builder.AppendLine(This.CreateDeleteAllSql<T>());
            builder.Append(" where ");
            builder.Append(string.Join(separator, primaries));
            return builder.ToString();
        }

        #endregion

        #region 補助
        
        /// <summary>
        /// 指定された型情報からテーブル属性を取得します。
        /// </summary>
        /// <typeparam name="T">対象となるレコードの型</typeparam>
        /// <returns>テーブル属性</returns>
        public static TableAttribute GetTableAttribute<T>()
        {
            var table = typeof(T).GetCustomAttribute<TableAttribute>(false);
            if (table == null)
                throw new ArgumentException("テーブルにマッピングされていない型です");
            return table;
        }

        /// <summary>
        /// 指定された型情報からプロパティ名を取得します。
        /// </summary>
        /// <typeparam name="T">対象となるレコードの型</typeparam>
        /// <returns>プロパティ情報</returns>
        private static IEnumerable<PropertyInfo> GetProperties<T>()
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
            var notMapped = typeof(NotMappedAttribute);
            return typeof(T)
                    .GetProperties(flags)
                    .Where(x => x.GetCustomAttributesData().All(y => y.AttributeType != notMapped));
        }

        /// <summary>
        /// 指定された型情報から主キー属性付与されたプロパティ名を取得します。
        /// </summary>
        /// <typeparam name="T">対象となるレコードの型</typeparam>
        /// <returns>プロパティ名</returns>
        private static IEnumerable<string> GetPrimaryKeyProperties<T>()
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
            return typeof(T)
                    .GetProperties(flags)
                    .Select(x => new
                    {
                        Name = x.Name,
                        Key = x.GetCustomAttribute<KeyAttribute>(false),
                    })
                    .Where(x => x.Key != null)
                    .Select(x => x.Name);
        }

        /// <summary>
        /// メンバーを表す式木からメンバー名を取得します。
        /// </summary>
        /// <typeparam name="T">メンバーを持つ型</typeparam>
        /// <param name="members">メンバーの式木のコレクション</param>
        /// <returns>メンバー名のコレクション</returns>
        private static IEnumerable<string> GetMemberNames<T>(this IEnumerable<Expression<Func<T, object>>> members)
        {
            if (members == null)
                throw new ArgumentNullException("members");

            return members
                    .Select(x =>
                    {
                        //--- メンバー
                        if (x.Body is MemberExpression)
                            return (MemberExpression)x.Body;

                        //--- Boxingのためにobjectへの型変換が入っている場合はそれを考慮
                        var unary = x.Body as UnaryExpression;
                        if (unary != null)
                            if (unary.NodeType == ExpressionType.Convert)
                                if (unary.Operand is MemberExpression)
                                    return (MemberExpression)unary.Operand;

                        //--- それ以外はエラー
                        throw new ArgumentException("members");
                    })
                    .Select(x => x.Member.Name);    //--- メンバー名を取得
        }

        #endregion

        #region CreateSelectInfoSql

        public static string CreateSelectInfoSql<T>(params Expression<Func<T, object>>[] properties)
        {
            var table = This.GetTableAttribute<T>();
            if (properties == null || properties.Length == 0)
            {
                return string.Format("select * from {0}.{1}", table.Schema, table.Name);
            }
            else
            {
                var columnNames = properties.GetMemberNames();
                var separator = string.Format(",{0}\t", Environment.NewLine);
                var builder = new StringBuilder();
                builder.AppendLine("select");
                builder.Append("\t");
                builder.AppendLine(string.Join(separator, columnNames));
                builder.AppendFormat("from {0}.{1}", table.Schema, table.Name);
                return builder.ToString();
            }
        }

        #endregion
    }
}

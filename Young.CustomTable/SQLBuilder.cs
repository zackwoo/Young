using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable
{
    abstract class SQLBuilder
    {
        protected StringBuilder Sql { get; private set; }
        public SQLBuilder()
        {
            PrimaryKey = "ID";
            Sql = new StringBuilder();
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TabelName { get; set; }
        /// <summary>
        /// 标识主键
        /// </summary>
        public string PrimaryKey { get; set; }

        public string[] Fields { get; set; }
        public string[] WhereFields { get; set; }
        public SQLOrder[] OrderFields { get; set; }

        /// <summary>
        /// 返回SQL语句
        /// </summary>
        /// <returns></returns>
        public virtual string GetResult()
        {
            return Sql.ToString();
        }

        public virtual void Init()
        {
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        public virtual void BuildFields() { }

        /// <summary>
        /// 创建条件
        /// </summary>
        public virtual void BuildWhere()
        {
            if (WhereFields == null || !WhereFields.Any())
            {
                return;
            }
            for (int i = 0; i < WhereFields.Length; i++)
            {
                Sql.AppendFormat(" WHERE [{0}]=@{0} ", WhereFields[i]);
                if (i < WhereFields.Length - 1)
                {
                    Sql.Append("AND");
                }
            }
        }

        /// <summary>
        /// 创建排序
        /// </summary>
        public virtual void BuildOrder() { }



    }

    internal class SQLOrder
    {
        public string Field { get; set; }

        public Sort SortInfo { get; set; }

        internal enum Sort
        {
            ASC,
            DESC
        }
    }
}

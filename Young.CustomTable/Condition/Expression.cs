﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.CustomTable.Condition
{
    public abstract class Expression
    {
        protected Expression()
        {
            this.Values = new List<object>();
            this.ParameterKeys = new List<string>();
            SetParameterKeys();
        }

        protected  void SetParameterKeys()
        {
            for (int i = 0; i < ParametersCount; i++)
            {
                ParameterKeys.Add(BuildParameterKey());
            }
        }
        /// <summary>
        /// 创建随机Key
        /// </summary>
        /// <returns></returns>
        protected string BuildParameterKey()
        {
            int min = 97, max = 123;
            var r = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                sb.Append((char) r.Next(min, max));
            }
            return sb.ToString();
        }

        public string Name { get; set; }

        protected IList<object> Values { get;private set; }

        protected IList<string> ParameterKeys { get; private set; }

        /// <summary>
        /// 参数数量，默认一个
        /// </summary>
        protected virtual int ParametersCount
        {
            get { return 1; }
        }
        
        public abstract string ToSQL();
        
        public virtual IEnumerable<SqlParameter> GetSqlParameters()
        {
            return ParameterKeys.Select((t, i) => new SqlParameter(t, Values[i]));
        }


        #region static

        public static Expression Like(string name, object parameter)
        {
            var exp = new LikeExpression
            {
                Name = name
            };
            exp.Values.Add(parameter);
            return exp;
        }
        /// <summary>
        /// 等于
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Expression EQ(string name, object parameter)
        {
            var exp = new EqualExpression
            {
                Name = name
            };
            exp.Values.Add(parameter);
            return exp;
        }
        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Expression GT(string name, object parameter)
        {
            var exp = new GreaterThanExpression
            {
                Name = name
            };
            exp.Values.Add(parameter);
            return exp;
        }
        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Expression GE(string name, object parameter)
        {
            var exp = new GreaterThanEqualToExpression
            {
                Name = name
            };
            exp.Values.Add(parameter);
            return exp;
        }

        #endregion
    }
}

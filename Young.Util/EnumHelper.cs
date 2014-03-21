using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Young.Util
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取由枚举类型的值和文本组成的数据字典
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>由枚举类型的值和文本组成的数据字典</returns>
        public static Dictionary<T,string> GetEnumKV<T>()
        {
            return Enum.GetValues(typeof (T)).Cast<T>().ToDictionary(value => value, value => GetEnumDisplayText(value));
        }
        /// <summary>
        /// 获取枚举值的说明文本，由DisplayAttribute修饰
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>枚举值的说明文本</returns>
        public static String GetEnumDisplayText(Object enumValue)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException("enumValue");
            }

            Type type = enumValue.GetType();

            FieldInfo fieldInfo = type.GetField(enumValue.ToString());

            if (fieldInfo == null)
            {
                throw new ApplicationException(String.Format(@"Can not get filed ""{0}"" in ""{1}"".", enumValue, type));
            }

            Object[] attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), true);

            if (attributes.Length > 0)
            {
                var attribute = (DisplayAttribute)attributes[0];

                return attribute.GetName();
            }

            return enumValue.ToString();
        }

    }


}

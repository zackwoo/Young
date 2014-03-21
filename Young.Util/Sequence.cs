using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Util
{
    public class Sequence
    {
        private static char[] _letter = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static int[] _num = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private static Random _random = new Random();

        /// <summary>
        /// 返回随机产生的6个字母组合新序列
        /// </summary>
        /// <param name="length">生成的序列长度</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        public static string GetNewSequence(int length = 6, string prefix = "", string suffix = "")
        {
            var sb = new StringBuilder(prefix);
            for (int i = 0; i < length; i++)
            {
                var index = _random.Next(0, _letter.Length);
                sb.Append(_letter[index]);
            }
            sb.Append(suffix);
            return sb.ToString();
        }
    }
}

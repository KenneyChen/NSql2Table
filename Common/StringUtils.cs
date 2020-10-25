using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSql2Table.Common
{
    public class StringUtils
    {
        /// <summary>
        /// guid转为Base64编码
        /// 长度为22个字符
        /// </summary>
        /// <param name="guid"></param>
        /// <example>
        /// string guid2 = GuidToBase64String(guid1);
        /// </example>
        /// <returns></returns>
        public static string GuidToBase64String()
        {
            Guid guid = Guid.NewGuid();
            return Convert.ToBase64String(guid.ToByteArray()).Replace("/", "_").Replace("+", "-").Substring(0, 22);
        }
    }
}

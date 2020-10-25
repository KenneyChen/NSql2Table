using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSql2Table.Models.OutputModels
{
    public class ReturnModel
    {
        /// <summary>
        /// http状态码
        /// </summary>
        public int code { get; set; }

        public bool success { get; set; }

        public string msg { get; set; }
    }

    public class ReturnModel<T>: ReturnModel
    {
        public T data { get; set; }
    }
}

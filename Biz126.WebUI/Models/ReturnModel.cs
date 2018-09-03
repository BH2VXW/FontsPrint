using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biz126.WebUI.Models
{
    /// <summary>
    /// 返回结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnModel<T>
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}

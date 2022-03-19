using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biz126.BlazorUI.Models
{
    /// <summary>
    /// 返回结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnModel<T>
    {
        public bool Status { get; set; }

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“Message”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        public string Message { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“Message”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        public T Data { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“Data”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
    }
}

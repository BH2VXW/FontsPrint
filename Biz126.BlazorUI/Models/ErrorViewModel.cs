using System;

namespace Biz126.BlazorUI.Models
{
    public class ErrorViewModel
    {
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“RequestId”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        public string RequestId { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“RequestId”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
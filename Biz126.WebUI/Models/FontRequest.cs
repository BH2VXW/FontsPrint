using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biz126.WebUI.Models
{
    public class FontRequest
    {
        /// <summary>
        /// 字体名称
        /// </summary>
        [Required(ErrorMessage ="请选择字体")]
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“font”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        public string font { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“font”必须包含非 null 值。请考虑将 属性 声明为可以为 null。

        /// <summary>
        /// 字号
        /// </summary>
        public float fontsize { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage ="请输入要打印的内容")]
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的 属性“body”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
        public string body { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的 属性“body”必须包含非 null 值。请考虑将 属性 声明为可以为 null。
    }
}

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
        public string font { get; set; }

        /// <summary>
        /// 字号
        /// </summary>
        public float fontsize { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage ="请输入要打印的内容")]
        public string body { get; set; }
    }
}

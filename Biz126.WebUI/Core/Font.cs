using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biz126.WebUI.Core
{
    public class Font
    {
        private string _basepath;
        private string fonts_path;
        public Font(string basepath)
        {
            _basepath = $"{PlatformServices.Default.Application.ApplicationBasePath}{basepath}";
            fonts_path = $"{_basepath}/fonts";
        }

        /// <summary>
        /// 指定字体的物理路径
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public string GetFontPath(string font)
        {
            return $"{fonts_path}/{font}";
        }

        /// <summary>
        /// 获取全部字体
        /// </summary>
        /// <returns></returns>
        public List<string> List()
        {
            //Logger.Log4Net.LogInfo($"[字体文件夹]:{fonts_path},是否存在:{Directory.Exists(fonts_path)}");

            return WebTools.FileObj.FileList(fonts_path);
        }

        /// <summary>
        /// 取配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> Config()
        {
            string conf = $"{_basepath}/setting.json";
            string json = WebTools.FileObj.ReadFile(conf, "utf-8"); //取配置文件
            if (!"不存在相应的目录".Equals(json))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            return new Dictionary<string, string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SkiaSharp;
using System.Collections;
using System.Linq;

namespace Biz126.ImageLib
{
    public class CreateImages
    {
        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="fontpath">指定的字体路径</param>
        /// <param name="text">指定的文字内容</param>
        /// <returns></returns>
        public static byte[] CreateImage(string fontpath, string text,float font_size=100)
        {
            //支持文字多行
            List<string> list = text.Split('\n').ToList();
            list.RemoveAll(x => { return string.IsNullOrEmpty(x.Trim()); });    //删除空行
            list.Reverse(); //顺序反转
            float line_height = 1.5F;   //行距
            float height = 480;
            if (list.Count * line_height*font_size >= height)
            {
                height = list.Count * line_height * font_size;
            }
            var info = new SKImageInfo(1100, (int)height);
            using (var surface = SKSurface.Create(info))
            {
                var canvas = surface.Canvas;
                // make sure the canvas is blank
                canvas.Clear(SKColors.White);
                
                var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = font_size,
                    Typeface= SkiaSharp.SKTypeface.FromFile(fontpath, 0)
                };

                int i = 0;
                list.ForEach(x =>
                {
                    var coord = new SKPoint(info.Width / 2, (info.Height + paint.TextSize * (list.Count - i) - paint.TextSize * i * 1.5F) / 2);
                    canvas.DrawText(x.Trim(), coord, paint);
                    i++;
                    
                });

                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    return data.ToArray();
                }
            }
        }
    }
}

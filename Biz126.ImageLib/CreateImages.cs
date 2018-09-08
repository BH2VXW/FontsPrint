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
            var info = new SKImageInfo(1100, 480);
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

                //支持文字多行
                string[] txt = text.Split('\n').Reverse().ToArray();
                for (int i = 0; i < txt.Length; i++)
                {
                    var coord = new SKPoint(info.Width / 2, (info.Height + paint.TextSize * (txt.Length-i) - paint.TextSize * i*1.5F) / 2);
                    canvas.DrawText(txt[i].Trim(), coord, paint);
                }

                //var coord = new SKPoint(info.Width / 2, (info.Height + paint.TextSize) / 2);
                //canvas.DrawText(text, coord, paint);
                

                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    return data.ToArray();
                }
            }
        }
    }
}

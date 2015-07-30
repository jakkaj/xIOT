using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace XIOTCore.Universal.Windows.Util
{
    public static class PixelRender
    {
        public struct PixelColor
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        public static async Task<PixelColor[,]> GetPixels(RenderTargetBitmap source)
        {

            int width = source.PixelWidth;
            int height = source.PixelHeight;

            var result = new PixelColor[width, height];

            var buffer = await source.GetPixelsAsync();
            var bytes = buffer.ToArray();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var k = (y * width + x) * 4;
                    result[x, y] = new PixelColor
                    {
                        Blue = bytes[k],
                        Green = bytes[k + 1],
                        Red = bytes[k + 2]
                    };
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Portable.Components.OLED.SSD1306;
using XIOTCore.Portable.Factory;

namespace XIOTCore_Samples_Console.OLED
{
    public class OLEDExamples_FTDI
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreFactory.Create(Platforms.FTDI_USB);

        public async Task Init()
        {
            //advanced users - configure your device for injection

            _factory.Init();

            var oled = _factory.GetComponent<IOLED_SSD1306_I2C>();

            var t = oled.Init(OLEDConstants.SSD1306_I2C_ADDRESS, OLEDDisplaySize.SSD1306_128_64);
            t.Wait();

            

            oled.Display();
            
            //return;
                    var b = new Bitmap(128, 64);

            var g = Graphics.FromImage(b);

            g.DrawString("Xamling-IOT", new Font("Consolas", 11), new SolidBrush(Color.White), 0, 0);

            g.DrawString("IOT", new Font("Consolas", 30), new SolidBrush(Color.White), 45, 15);
            g.DrawString("Y", new Font("Webdings", 30), new SolidBrush(Color.White),0, 15);

            g.Save();

            for (var i = 0; i < b.Height; i++)
            {
                for (var x = 0; x < b.Width; x++)
                {
                    var p = b.GetPixel(x, i);
                    var average = (p.R + p.G + p.G)/3;

                    if (average != 0)
                    {
                        oled.DrawPixel((ushort)x, (ushort)i, 1);
                    }
                }
            }

           oled.Display();
        }
    }
}

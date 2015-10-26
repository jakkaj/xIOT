using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Universal.Factory;
using XIOTCore.Universal.Windows.Util;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OLED_Sample_Universal_RTM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IXIOTCoreFactory _factory =
           XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);
        public MainPage()
        {
            this.InitializeComponent();
            _init();
        }

        async void _init()
        {
            await Task.Delay(500);
            
            _factory.Init();
            var oled = _factory.GetComponent<IOLED_SSD1306_I2C>();

            await oled.Init(OLEDConstants.SSD1306_I2C_ADDRESS, OLEDDisplaySize.SSD1306_128_64);

            oled.Display();

            while (true)
            {
                var r = new RenderTargetBitmap();

                await r.RenderAsync(OLEDRenderer);

                var colors = await PixelRender.GetPixels(r);

                for (var i = 0; i < r.PixelHeight; i++)
                {
                    for (var x = 0; x < r.PixelWidth; x++)
                    {
                        var pixel = colors[x, i];
                        var average = (pixel.Red + pixel.Green + pixel.Blue) / 3;
                        if (average > 0)
                        {
                            oled.DrawPixel((ushort)x, (ushort)i, 1);
                        }
                        else
                        {
                            oled.DrawPixel((ushort)x, (ushort)i, 0);
                        }
                    }
                }
                oled.Display();
                await Task.Yield();
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
using XIOTCore.Universal.RaspberryPi.Interface;
using XIOTCore.Universal.Windows.Util;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BreathoExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);

        private IExplorerHat_AnaloguePlug1 _a1;
        private IOLED_SSD1306_I2C _oled;
        public MainPage()
        {
            this.InitializeComponent();
            _init();
        }

        async void _init()
        {
            await Task.Delay(500);

            _factory.Init();
            _oled = _factory.GetComponent<IOLED_SSD1306_I2C>();
            _a1 = _factory.GetComponent<IExplorerHat_AnaloguePlug1>();
            await _a1.Init();
            await _oled.Init(OLEDConstants.SSD1306_I2C_ADDRESS, OLEDDisplaySize.SSD1306_128_64);

            _oled.Display();

            _loop();
        }

        async void _loop()
        {
            while (true)
            {
                await _read();
                await _render();
            }
        }

        async Task _read()
        {
            await Task.Yield();
            var m = await _a1.Measure();
            RenderView.SetText(m.ToString());
        }

        async Task _render()
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
                        _oled.DrawPixel((ushort)x, (ushort)i, 1);
                    }
                    else
                    {
                        _oled.DrawPixel((ushort)x, (ushort)i, 0);
                    }
                }
            }
            _oled.Display();
            await Task.Yield();
        }
    }
}

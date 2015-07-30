using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Universal.Factory;

namespace XIOTCore.Samples.Universal.OLED
{
    public class OLEDExamples_Pi
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB);

        public async Task Init()
        {
            //advanced users - configure your device for injection

            _factory.Init();

            var oled = _factory.GetComponent<IOLED_SSD1306_I2C>();

            await oled.Init(OLEDConstants.SSD1306_I2C_ADDRESS, OLEDDisplaySize.SSD1306_128_64);

            oled.Display();

            var random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));

           

            while (true)
            {
                for (var i = 0; i < 64; i++)
                {
                    for (var x = 0; x < 128; x++)
                    {

                        if (random.Next(1, 10)%2 == 1)
                        {
                            oled.DrawPixel((ushort) x, (ushort) i, 1);
                        }
                        else
                        {
                            oled.DrawPixel((ushort)x, (ushort)i, 2);
                        }
                    }
                }
                oled.Display();
                await Task.Yield();
            }

            

            
        }
    }
}

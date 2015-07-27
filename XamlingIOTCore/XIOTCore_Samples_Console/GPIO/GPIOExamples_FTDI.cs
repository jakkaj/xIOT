using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.FTDI.GPIO;
using XIOTCore.Portable.Components.OLED.SSD1306;
using XIOTCore.Portable.Factory;

namespace XIOTCore_Samples_Console.GPIO
{
    public class GPIOExamples_FTDI
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreFactory.Create(Platforms.FTDI_USB);

        public async Task Init()
        {
            _factory.Init();
            var i2c = _factory.GetComponent<IXI2CDevice>();
            await i2c.Init();

            var gpio = new GPIODevice(i2c, 0); //Pin C0

            gpio.SetDirection(XGpioDirection.Output);

            while (true)
            {
                gpio.On();
                await Task.Delay(250);
                gpio.Off();
                await Task.Delay(150);
            }
        }
    }
}

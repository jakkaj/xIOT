using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.GPIO;
using XIOTCore.FTDI.Contract;
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
            
            var gpio = _factory.GetComponent<IXGpio_0>();

            gpio.SetDirection(XGpioDirection.Input);

            while (true)
            {
                await Task.Delay(1000);
                if (gpio.State)
                {
                    Debug.WriteLine("Hight");
                }
                else
                {
                    Debug.WriteLine("Low");
                }

                //gpio.On();
                //await Task.Delay(250);
                //gpio.Off();
                //await Task.Delay(150);
            }


        }
    }
}

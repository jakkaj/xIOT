using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;
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
            _factory.Init();
            var i2c = _factory.GetComponent<IXI2CDevice>();
            var writer = new I2CIO(i2c);
            var iTask = writer.Init(OLEDConstants.SSD1306_I2C_ADDRESS);
            iTask.Wait();
            var oled = new XIOTCore.Portable.Components.OLED.SSD1306.OLED(writer, OLEDDisplaySize.SSD1306_128_64);

            oled.Init();
        }
    }
}

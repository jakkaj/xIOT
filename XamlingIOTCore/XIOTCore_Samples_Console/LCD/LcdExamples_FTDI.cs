using System.Threading.Tasks;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.Module;
using XIOTCore.Portable.Components.LCD.HD44780;
using XIOTCore.Portable.Factory;
using XIOTCore.Portable.Util.XamlingCore;

namespace XIOTCore_Samples_Console.LCD
{
    public class LcdExamples_FTDI
    {
        private readonly IXIOTCoreFactory _factory =
          XIOTCoreFactory.Create(Platforms.FTDI_USB);

        //private II2CLCD _lcd;

      
        public async Task Init()
        {
            _factory.Init();

            var i2c = _factory.GetComponent<IXI2CDevice>();

            var _lcd = new I2CLCD(i2c, 0x27, 2, 1, 0, 4, 5, 6, 7, 3, BacklightPolarity.Positive);

            await _lcd.Begin(16,2);

            for (int i = 0; i < 3; i++)
            {
                _lcd.BackLight();
                StopwatchDelay.Delay(100);
                _lcd.NoBacklight();
                StopwatchDelay.Delay(100);
            }

            _lcd.BackLight();
            _lcd.Home();
            _lcd.SetCursor(0, 0); //Start at character 4 on line 0
            _lcd.Write("FT232H, LCD, C#");
            StopwatchDelay.Delay(250);
            _lcd.SetCursor(0, 1);
            _lcd.Write("git.io/vmEdE");
        }
    }
}

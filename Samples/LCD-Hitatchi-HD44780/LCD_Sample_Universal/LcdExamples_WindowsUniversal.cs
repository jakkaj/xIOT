using System.Threading.Tasks;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Factory;
using XIOTCore.Portable.Util.XamlingCore;

namespace LCD_Sample_Universal
{
    public class LcdExamples_WindowsUniversal
    {
        private readonly IXIOTCoreFactory _factory =
          XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB);

        public async Task Init()
        {
            _factory.Init();

            var lcd = _factory.GetComponent<ILCD_Hitatchi_I2C>();

            //We got the pin config from here:
            //https://arduino-info.wikispaces.com/LCD-Blue-I2C
            //You'll have to find your own mapping for your device!

            await lcd.Init(0x27, 16, 2,
                LCDConstants.LCD_5x8DOTS,
                2, 1, 0, 4, 5, 6, 7, 3, BacklightPolarity.Positive);

            for (int i = 0; i < 3; i++)
            {
                lcd.BackLight();
                StopwatchDelay.Delay(100);
                lcd.NoBacklight();
                StopwatchDelay.Delay(100);
            }
            lcd.NoCursor();
            lcd.BackLight();
            lcd.Home();
            lcd.SetCursor(0, 0); //Start at character 4 on line 0
            lcd.Write("FT232H, LCD, C#");
            StopwatchDelay.Delay(250);
            lcd.SetCursor(0, 1);
            lcd.Write("git.io/vmEdE");
        }
    }
}

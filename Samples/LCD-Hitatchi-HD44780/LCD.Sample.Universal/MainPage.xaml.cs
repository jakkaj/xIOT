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
using Windows.UI.Xaml.Navigation;
using XIOTCore.Contract;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Portable.Factory;
using XIOTCore.Portable.Util.XamlingCore;
using XIOTCore.Universal.Factory;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LCD.Sample.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IXIOTCoreFactory _factory =
          XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB);
        public MainPage()
        {
            this.InitializeComponent();
            _init();
        }

        private async Task _init()
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
            lcd.Clear();
            lcd.SetCursor(0, 0); //Start at character 4 on line 0
            lcd.Write("FT232H, LCD, C#");
            StopwatchDelay.Delay(250);
            lcd.SetCursor(0, 1);
            lcd.Write("git.io/vmEdE");
        }
    }
}

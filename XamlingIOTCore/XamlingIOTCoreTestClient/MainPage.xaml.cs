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
using XCore.RaspberryPI.Interface;
using XIOTCore.Contract;
using XIOTCore.Contract.Interface;
using XIOTCore.Factory;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XamlingIOTCoreTestClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IXIOTCoreFactory _factory = 
            XIOTCoreFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);

        private IXLed _redLed;
        private IXLed _greenLed;

        public MainPage()
        {
            this.InitializeComponent();

            _redLed = _factory.GetComponent<IExplorerHatRedLed>();
            _greenLed = _factory.GetComponent<IExplorerHatGreenLed>();

            _cycle();
        }

        async void _cycle()
        {
            var state = true;

            while (true)
            {
                _redLed.State = state;
                _greenLed.State = !state;
                state = !state;
                await Task.Delay(500);
            }
        }
    }
}

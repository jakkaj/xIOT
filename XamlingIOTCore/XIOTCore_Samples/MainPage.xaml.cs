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
using Windows.UI.Xaml.Navigation;
using XCore.RaspberryPI.Interface;
using XIOTCore.Contract;
using XIOTCore.Contract.Interface;
using XIOTCore.Factory;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XIOTCore_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);

        private readonly IXGpioControl _redLed;
        private readonly IXGpioControl _greenLed;

        private readonly IExplorerHat_AnaloguePlug _plug1;
        private readonly IExplorerHat_AnaloguePlug _plug4;

        public MainPage()
        {
            this.InitializeComponent();

            _factory.Init();

            _redLed = _factory.GetComponent<IExplorerHat_RedLed>();
            _greenLed = _factory.GetComponent<IExplorerHat_GreenLed>();

            _plug1 = _factory.GetComponent<IExplorerHat_AnaloguePlug1>();
            _plug4 = _factory.GetComponent<IExplorerHat_AnaloguePlug4>();

            _cycle();
        }

        async void _cycle()
        {
            var init4 = await _plug4.Init();
            var init1 = await _plug1.Init();

            if (!init1 || !init4)
            {
                throw new InvalidOperationException("Could not init one of the devices");
            }

            //var state = true;

            while (true)
            {
                // _redLed.State = state;
                // _greenLed.State = !state;

                // state = !state;


                var v1 = await _plug1.MeasurePercentage();
                var v4 = await _plug4.MeasurePercentage();

                if (v1 > v4 + 5)
                {
                    _greenLed.State = false;
                    _redLed.State = true;
                }
                else if (v4 > v1 + 5)
                {
                    _greenLed.State = true;
                    _redLed.State = false;
                }
                else
                {
                    _greenLed.State = true;
                    _redLed.State = true;
                }

                //Debug.WriteLine($"Plug 1: {v1}, Plug 4: {v4}");

                _moveEllipse(v1, v4);

                await Task.Delay(5);
            }
        }

        private double _lastPos = 0;

        void _moveEllipse(double left, double right)
        {
            left = left * 8;
            right = right * 8;

            double pos = 0;
            pos = pos - left;
            pos = pos + right;

            //remove some jitter from the measurements. 
            if (_lastPos < pos + 8 && _lastPos > pos - 8)
            {
                return;
            }

            _lastPos = pos;

            ThingCompositeTransform.TranslateX = pos;
        }
    }
}

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
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.Module;
using XIOTCore.Factory;
using XIOTCore.Portable.Components.Range;
using XIOTCore.Portable.Factory;
using XIOTCore_Samples.LCD;

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

        private IXGpioControl _output1;
        private IXGpioControl _input2;

        private readonly IExplorerHat_AnaloguePlug _plug1;
        private readonly IExplorerHat_AnaloguePlug _plug4;

        private IXGpioControl _input1;

        private IHC_SR04 _echoLocationModule;

        public MainPage()
        {
            this.InitializeComponent();

            //var _lcdExamples = new LcdExamples_FTDI();

            var _lcdExamples = new LcdExamples_Rpi();

            _lcdExamples.Init();

            return;
            _factory.Init();

            _redLed = _factory.GetComponent<IExplorerHat_RedLed>();
            _greenLed = _factory.GetComponent<IExplorerHat_GreenLed>();

            _plug1 = _factory.GetComponent<IExplorerHat_AnaloguePlug1>();
            _plug4 = _factory.GetComponent<IExplorerHat_AnaloguePlug4>();

            _input1 = _factory.GetComponent<IExplorerHat_Input1>();

            _input2 = _factory.GetComponent<IExplorerHat_Input2>();
            _output1 = _factory.GetComponent<IExplorerHat_Output1>();

            _echoLocationModule = new HC_SR04(_output1, _input2);

            _cycle();
            _cycle2();
        }

        private async void _cycle()
        {
            await _echoLocationModule.Init();

            while (true)
            {
                var averageRound = await _echoLocationModule.Measure(true);

                DistanceText.Text = averageRound.ToString();
                _moveDistanceEllipse(Convert.ToInt32(averageRound));
            }
        }

        async void _cycle2()
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


                var v1 = await _plug4.MeasurePercentage();

                _moveVerticalEllipse(v1);

                var state = _input1.State;

                Debug.WriteLine($"IR State: {state}");

                MovementText.Visibility = state ? Visibility.Visible : Visibility.Collapsed;

                await Task.Delay(20);
            }
        }

        private double _lastPos = 0;
        private double _distanceLastPos = 0;
        private double _lightLastPos = 0;
        void _moveDistanceEllipse(int amount)
        {
            var screenWidth = Window.Current.Bounds.Width;

            var absoluteWidth = (1D / 1500) * amount;

            var pos = screenWidth * absoluteWidth;


            if (_distanceLastPos < amount + 10 && _distanceLastPos > amount - 10)
            {
                return;
            }

            _distanceLastPos = amount;

            DistanceCompositeTransform.TranslateX = -pos;
        }

        void _moveVerticalEllipse(double percentage)
        {
            var screenHeight = Window.Current.Bounds.Height;

            var abs = 1/percentage;

            var offsetHeight = screenHeight*abs;

            double pos = offsetHeight * 5;
            
            //remove some jitter from the measurements. 
            if (_lightLastPos < percentage + 1 && _lightLastPos > percentage - 1)
            {
                return;
            }

            _lightLastPos = percentage;

            ThingCompositeTransform.TranslateY = -pos;
        }

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

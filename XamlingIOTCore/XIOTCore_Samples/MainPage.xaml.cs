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

        private IXGpioControl _output1;
        private IXGpioControl _input2;

        private readonly IExplorerHat_AnaloguePlug _plug1;
        private readonly IExplorerHat_AnaloguePlug _plug4;

        private IXGpioControl _input1;

        public MainPage()
        {
            this.InitializeComponent();

            _factory.Init();

            _redLed = _factory.GetComponent<IExplorerHat_RedLed>();
            _greenLed = _factory.GetComponent<IExplorerHat_GreenLed>();

            _plug1 = _factory.GetComponent<IExplorerHat_AnaloguePlug1>();
            _plug4 = _factory.GetComponent<IExplorerHat_AnaloguePlug4>();

            _input1 = _factory.GetComponent<IExplorerHat_Input1>();

            _input2 = _factory.GetComponent<IExplorerHat_Input2>();
            _output1 = _factory.GetComponent<IExplorerHat_Output1>();

            _cycle();
            _cycle2();
        }

        private async void _cycle()
        {
            //var i2cinfo = _factory.GetComponent<IXI2CInfo>();
            //await i2cinfo.GetAllDevices();

            _output1.On();

            await Task.Delay(1000);

            //return;

            var averages = new List<decimal>();

            while (true)
            {
                await Task.Delay(20);

                _output1.Off();
                await Task.Delay(20);
                _output1.On();

                var sw = new Stopwatch();

                var sw2 = new Stopwatch();

                sw2.Start();
                while (!_input2.State)
                {
                    if (sw2.Elapsed.TotalSeconds > 1)
                    {
                        break;
                    }
                }

                sw.Start();

                sw2 = new Stopwatch();
                sw2.Start();
                while (_input2.State)
                {
                    if (sw2.Elapsed.TotalSeconds > 1)
                    {
                        break;
                    }
                }

                sw.Stop();

                var freq = Stopwatch.Frequency;

                var ts = sw.ElapsedTicks;

                ts = ts / 2;

                Int64 sos = 344000;

                decimal result = ((1M / freq) * ts) * sos;

                if (result > 4000)
                {
                    continue;
                }

                averages.Add(result);

                while (averages.Count > 10)
                {
                    averages.RemoveAt(1);
                }

                var averageResult = averages.Average();

                var averageRound = Math.Round(averageResult, 0);

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

                //var v4 = await _plug4.MeasurePercentage();

                //if (v1 > v4 + 5)
                //{
                //    _greenLed.State = false;
                //    _redLed.State = true;
                //}
                //else if (v4 > v1 + 5)
                //{
                //    _greenLed.State = true;
                //    _redLed.State = false;
                //}
                //else
                //{
                //    _greenLed.State = true;
                //    _redLed.State = true;
                //}

                //Debug.WriteLine($"Plug 1: {v1}, Plug 4: {v4}");

               // _moveEllipse(v1, v4);

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

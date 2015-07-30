using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Contract.Interface.GPIO;
using XIOTCore.Portable.Components.Range;
using XIOTCore.Samples.Universal.LCD;
using XIOTCore.Universal.Factory;
using XIOTCore.Universal.RaspberryPi.Interface;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XIOTCore.Samples.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);

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


            var _lcdExamples = new LcdExamples_Rpi2();

            _lcdExamples.Init();

            return;
            //_factory.Init();

            //_redLed = _factory.GetComponent<IExplorerHat_RedLed>();
            //_greenLed = _factory.GetComponent<IExplorerHat_GreenLed>();

            //_plug1 = _factory.GetComponent<IExplorerHat_AnaloguePlug1>();
            //_plug4 = _factory.GetComponent<IExplorerHat_AnaloguePlug4>();

            //_input1 = _factory.GetComponent<IExplorerHat_Input1>();

            //_input2 = _factory.GetComponent<IExplorerHat_Input2>();
            //_output1 = _factory.GetComponent<IExplorerHat_Output1>();

            //_echoLocationModule = new HC_SR04(_output1, _input2);
        }
    }
}

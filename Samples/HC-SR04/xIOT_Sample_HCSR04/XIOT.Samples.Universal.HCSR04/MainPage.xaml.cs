using Windows.UI.Xaml.Controls;
using Autofac;
using xIOT_Sample_HCSR04;
using XIOTCore.Contract;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Portable.Components.Range;
using XIOTCore.Universal.Factory;
using XIOTCore.Universal.RaspberryPi.Interface;

//For the wiring diagram see: http://fritzing.org/projects/hc-sr04-echo-range-finder-and-xiot-windows
//License and other samples / nuget package source: https://github.com/jakkaj/Xamling-IOT
//This sample doco: https://github.com/jakkaj/Xamling-IOT/tree/master/Samples/HC-SR04
//Jordan Knight @jakkaj / Xaming 2015. 


namespace XIOT.Samples.Universal.HCSR04
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IXIOTCoreFactory _factory =
            XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);

        private IHC_SR04 _echoLocationModule;

        public MainPage()
        {
            this.InitializeComponent();

            

            //Uncomment out this sample for straight, non MVVM, non dependency injection style
            /*

            _factory.Init();
            

            var input2 = _factory.GetComponent<IExplorerHat_Input2>(); //Get the digital input 2 from the Explorer HAT
            var output1 = _factory.GetComponent<IExplorerHat_Output1>(); //Get the digital output 1 from the Explorer HAT. 

            _echoLocationModule = new HC_SR04(output1, input2);

            _cycle();
            */


            //The better, MVVM/Autofac way. 

            _factory.Builder.RegisterType<MainViewModel>().AsSelf(); //register the vm for Autofac resolution

            //Do the tricky configs here in a central location, then you can inject it in to all of your classes 
            //with minimal fuss!

            _factory.Builder.Register(
                c => new HC_SR04(c.Resolve<IExplorerHat_Output1>(), c.Resolve<IExplorerHat_Input2>())).As<IHC_SR04>();

            _factory.Init();

            var vm = _factory.Container.Resolve<MainViewModel>();

            DataContext = vm;
        }

        private async void _cycle()
        {
            await _echoLocationModule.Init();

            while (true)
            {
                var averageRound = await _echoLocationModule.Measure(true);

                DistanceText.Text = averageRound.ToString();

            }
        }
    }
}

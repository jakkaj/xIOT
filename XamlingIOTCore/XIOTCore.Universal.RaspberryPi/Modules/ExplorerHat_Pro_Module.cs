using Windows.Devices.Gpio;
using Autofac;
using XIOTCore.Contract.Interface.Configs;
using XIOTCore.Universal.RaspberryPi.ExplorerHATPro;
using XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Components;
using XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Plugs;
using XIOTCore.Universal.RaspberryPi.Interface;
using XIOTCore.Universal.Windows.Gpio;

namespace XIOTCore.Universal.RaspberryPi.Modules
{
    public class ExplorerHat_Pro_Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExplorerHatConfiguration>().As<IPlatformConfiguration>();

            builder.Register(_ =>
                new ExplorerHat_GreenLed(
                    new XGpio(27,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHat_GreenLed>().SingleInstance();

            builder.Register(_ =>
                new ExplorerHat_RedLed(
                    new XGpio(5,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHat_RedLed>().SingleInstance();

            builder.Register(_ =>
                new ExplorerHat_Output1(
                    new XGpio(6,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHat_Output1>().SingleInstance();

            builder.Register(_ =>
                new ExplorerHat_Output2(
                    new XGpio(12,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHat_Output2>().SingleInstance();

            builder.Register(_ =>
               new ExplorerHat_Output3(
                   new XGpio(13,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output))).As<IExplorerHat_Output3>().SingleInstance();

            builder.Register(_ =>
               new ExplorerHat_Output4(
                   new XGpio(16,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output))).As<IExplorerHat_Output4>().SingleInstance();


            builder.Register(_ =>
               new ExplorerHat_Input1(
                   new XGpio(23,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Input))).As<IExplorerHat_Input1>().SingleInstance();

            builder.Register(_ =>
               new ExplorerHat_Input2(
                   new XGpio(22,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Input))).As<IExplorerHat_Input2>().SingleInstance();

            builder.Register(_ =>
               new ExplorerHat_Input3(
                   new XGpio(24,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Input))).As<IExplorerHat_Input3>().SingleInstance();

            builder.Register(_ =>
               new ExplorerHat_Input4(
                   new XGpio(25,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Input))).As<IExplorerHat_Input4>().SingleInstance();


            builder.RegisterType<ExplorerHat_AnaloguePlug1>().As<IExplorerHat_AnaloguePlug1>().SingleInstance();
            builder.RegisterType<ExplorerHat_AnaloguePlug2>().As<IExplorerHat_AnaloguePlug2>().SingleInstance();
            builder.RegisterType<ExplorerHat_AnaloguePlug3>().As<IExplorerHat_AnaloguePlug3>().SingleInstance();
            builder.RegisterType<ExplorerHat_AnaloguePlug4>().As<IExplorerHat_AnaloguePlug4>().SingleInstance();

            base.Load(builder);
        }
    }
}

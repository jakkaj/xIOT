using Windows.Devices.Gpio;
using Autofac;
using XCore.RaspberryPI.ExplorerHATPro;
using XCore.RaspberryPI.ExplorerHATPro.Components;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components.Gpio;
using XIOTCore.Contract.Interface;


namespace XCore.RaspberryPI.Modules
{
    public class ExplorerHat_Pro_Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ =>
                new ExplorerHat_GreenLed(
                    new XGpio(27,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHat_GreenLed>();

            builder.Register(_ =>
                new ExplorerHat_RedLed(
                    new XGpio(5,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHat_RedLed>();

            builder.RegisterType<ExplorerHat_ADS1015>().As<IExplorerHat_ADS1015>();

            base.Load(builder);
        }
    }
}

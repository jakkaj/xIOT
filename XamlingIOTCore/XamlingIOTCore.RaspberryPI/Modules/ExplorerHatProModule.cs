using Windows.Devices.Gpio;
using Autofac;
using XCore.RaspberryPI.ExplorerHATPro;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components.Gpio;
using XIOTCore.Contract.Interface;


namespace XCore.RaspberryPI.Modules
{
    public class ExplorerHatProModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ =>
                new ExplorerHatGreenLed(
                    new XGpio(27,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHatGreenLed>();

            builder.Register(_ =>
                new ExplorerHatRedLed(
                    new XGpio(5,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHatRedLed>();

            base.Load(builder);
        }
    }
}

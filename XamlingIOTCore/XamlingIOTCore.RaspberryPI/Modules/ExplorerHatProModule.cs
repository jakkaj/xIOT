using Windows.Devices.Gpio;
using Autofac;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components;
using XIOTCore.Components.Gpio;

namespace XCore.RaspberryPI.Modules
{
    public class ExplorerHatProModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ =>
                new XGpioLed(
                    new XGpio(27,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHatRedLed>();

            builder.Register(_ =>
                new XGpioLed(
                    new XGpio(5,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHatGreenLed>();

            base.Load(builder);
        }
    }
}

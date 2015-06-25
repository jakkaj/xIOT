using Windows.Devices.Gpio;
using Autofac;
using XCore.RaspberryPI.ExplorerHATPro;
using XCore.RaspberryPI.ExplorerHATPro.Components;
using XCore.RaspberryPI.ExplorerHATPro.Plugs;
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
                        GpioPinDriveMode.Output))).As<IExplorerHat_GreenLed>().SingleInstance();

            builder.Register(_ =>
                new ExplorerHat_RedLed(
                    new XGpio(5,
                        GpioController.GetDefault(),
                        GpioSharingMode.Exclusive,
                        GpioPinDriveMode.Output))).As<IExplorerHat_RedLed>().SingleInstance();

            builder.RegisterType<ExplorerHat_AnaloguePlug1>().As<IExplorerHat_AnaloguePlug1>().SingleInstance();
            builder.RegisterType<ExplorerHat_AnaloguePlug2>().As<IExplorerHat_AnaloguePlug2>().SingleInstance();
            builder.RegisterType<ExplorerHat_AnaloguePlug3>().As<IExplorerHat_AnaloguePlug3>().SingleInstance();
            builder.RegisterType<ExplorerHat_AnaloguePlug4>().As<IExplorerHat_AnaloguePlug4>().SingleInstance();

            base.Load(builder);
        }
    }
}

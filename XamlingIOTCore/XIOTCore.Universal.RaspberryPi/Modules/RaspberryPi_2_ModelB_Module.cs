using Windows.Devices.Gpio;
using Autofac;
using XIOTCore.Contract.Components.GPIO;
using XIOTCore.Contract.Interface.GPIO;
using XIOTCore.Universal.Windows.Gpio;

namespace XIOTCore.Universal.RaspberryPi.Modules
{
    public class RaspberryPi_2_ModelB_Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(0,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_0>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(1,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_1>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(2,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_2>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(3,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_3>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(4,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_4>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(5,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_6>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(6,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_7>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(7,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_7>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(8,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_9>()
                       .SingleInstance();

            builder.Register(_ =>
               new XGpioControl(
                   new XGpio(10,
                       GpioController.GetDefault(),
                       GpioSharingMode.Exclusive,
                       GpioPinDriveMode.Output)))
                       .As<IXGpio_10>()
                       .SingleInstance();




            base.Load(builder);
        }
    }
}

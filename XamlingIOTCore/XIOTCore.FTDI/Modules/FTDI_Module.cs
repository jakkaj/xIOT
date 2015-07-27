using Autofac;
using XIOTCore.Contract.Components.GPIO;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.GPIO;
using XIOTCore.FTDI.Contract;
using XIOTCore.FTDI.GPIO;
using XIOTCore.FTDI.I2C;

namespace XIOTCore.FTDI.Modules
{
    public class FTDI_Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<I2CDevice_FTDI>().As<IXI2CDevice_FTDI>();
            builder.RegisterType<I2CDevice_FTDI>().As<IXI2CDevice>();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 0))).As<IXGpio_0>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 1))).As<IXGpio_1>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 2))).As<IXGpio_2>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 3))).As<IXGpio_3>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 4))).As<IXGpio_4>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 5))).As<IXGpio_5>();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 6))).As<IXGpio_6>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 7))).As<IXGpio_7>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 8))).As<IXGpio_8>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 9))).As<IXGpio_9>()
                    .SingleInstance();

            builder.Register(_ =>
                new XGpioControl(
                    new GPIODevice(_.Resolve<IXI2CDevice_FTDI>(), 10))).As<IXGpio_10>()
                    .SingleInstance();


            base.Load(builder);
        }
    }
}

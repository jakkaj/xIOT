using System;
using Windows.Devices.Gpio;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.GPIO;

namespace XIOTCore.Universal.Windows.Gpio
{
    public class XGpio : IXGpio
    {
        private readonly int _pinNumber;
        private readonly GpioController _gpio;
        private readonly GpioSharingMode _sharingMode;

        private GpioPinDriveMode _driveMode;

        private GpioPin _pin;

        public XGpio(int pinNumber, GpioController gpio, GpioSharingMode sharingMode, GpioPinDriveMode driveMode)
        {
            _pinNumber = pinNumber;
            _gpio = gpio;
            _sharingMode = sharingMode;
            _driveMode = driveMode;

            _init();
        }

        void _init()
        {
            _pin = _gpio.OpenPin(_pinNumber, _sharingMode);

            if (!_pin.IsDriveModeSupported(_driveMode))
            {
                throw new NotSupportedException($"Drive mode {_driveMode} not supported on pin {_pinNumber}");    
            }

            _pin.SetDriveMode(_driveMode);
        }

        public void SetDirection(XGpioDirection direction)
        {
            GpioPinDriveMode mode;

            if (direction == XGpioDirection.Input)
            {
                mode = GpioPinDriveMode.Input;
            }
            else
            {
                mode = GpioPinDriveMode.Output;
            }

            if (!_pin.IsDriveModeSupported(mode))
            {
                throw new NotSupportedException($"Drive mode {mode} not supported on pin {_pinNumber}");
            }

            _pin.SetDriveMode(mode);

            _driveMode = mode;
        }

        public void SetLow()
        {
            _pin.Write(GpioPinValue.Low);
        }

        public void SetHigh()
        {
            _pin.Write(GpioPinValue.High);
        }

        public XPinValue GetValue()
        {
            var r=  _pin.Read();
            return r == GpioPinValue.Low ? XPinValue.Low : XPinValue.High;
        }
    }
}

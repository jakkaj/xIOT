using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.FTDI.Contract;
using XIOTCore.FTDI.Exceptions;
using XIOTCore.FTDI.LibMPSSE;
using XIOTCore.FTDI.Types;

namespace XIOTCore.FTDI.I2C
{
    public class I2CDevice_FTDI : IXI2CDevice_FTDI
    {
        private static IntPtr _handle = IntPtr.Zero;
        private static FtChannelConfig _currentGlobalConfig;

        private FtChannelConfig _cfg;
        private int _deviceAddress;

        private bool _isDisposed;
        private I2CConfiguration _i2cConfig;

        private const int ConnectionSpeed = (int)I2CModes.I2C_CLOCK_FAST_MODE; // Hz
        private const int LatencyTimer = 255; // Hz

        private byte _direction;
        private byte _gpo;
        private object _lock = new object();

        public async Task<bool> Init()
        {
            var config = new FtChannelConfig
            {
                ClockRate = ConnectionSpeed,
                LatencyTimer = LatencyTimer
            };

            _i2cConfig = _i2cConfig ?? I2CConfiguration.ChannelZeroConfiguration;
            _cfg = config;
           
            InitLibAndHandle();

            return true;
        }

        public async Task<bool> Init(int deviceAddress)
        {
            _deviceAddress = deviceAddress;
            return await Init();
        }

        void InitLibAndHandle()
        {
            FtResult result;

            if (_handle != IntPtr.Zero) 
                return;

            LibMpsse.Init();

            var num_channels = 0;

            var channels = LibMpsseI2C.I2C_GetNumChannels(out num_channels);

            CheckResult(channels);

            //if (num_channels > 0)
            //{
            //    for (var i = 0; i < num_channels; i++)
            //    {
            //        FtDeviceInfo cInfo;
            //        var channelInfoStatus = LibMpsseI2C.I2C_GetChannelInfo(i, out cInfo);
            //        CheckResult(channelInfoStatus);
            //        Debug.WriteLine($"Flags: {cInfo.Flags}");
            //        Debug.WriteLine($"Type: {cInfo.Type}");
            //        Debug.WriteLine($"ID: {cInfo.ID}");
            //        Debug.WriteLine($"LocId: {cInfo.LocId}");
            //        Debug.WriteLine($"SerialNumber: {cInfo.SerialNumber}");
            //        Debug.WriteLine($"Description: {cInfo.Description}");
            //        Debug.WriteLine($"ftHandle: {cInfo.ftHandle}");
            //    }
            //}

            result = LibMpsseI2C.I2C_OpenChannel(_i2cConfig.ChannelIndex, out _handle);

            CheckResult(result);

            if (_handle == IntPtr.Zero)
                throw new I2CChannelNotConnectedException(FtResult.InvalidHandle);

            result = LibMpsseI2C.I2C_InitChannel(_handle, ref _cfg);

            CheckResult(result);
            _currentGlobalConfig = _cfg;

        }

        public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            throw new NotImplementedException();
        }

        public bool Write(byte[] array)
        {
            int writtenAmount;

            var result = Write(array, array.Length, out writtenAmount, FtI2CTransferOptions.StartBit | FtI2CTransferOptions.StopBit);

            return result == FtResult.Ok;
        }

        public bool Write(int value)
        {
            var array = new byte[1];
            array[0] = Convert.ToByte(value);
            //var array = BitConverter.GetBytes(Convert.ToUInt32(value));
            int writtenAmount;

            var result = Write(array, array.Length, out writtenAmount, FtI2CTransferOptions.StartBit | FtI2CTransferOptions.StopBit);

            return result == FtResult.Ok;
        }

        public FtResult Write(byte[] buffer, int sizeToTransfer, out int sizeTransfered, FtI2CTransferOptions options)
        {
            return LibMpsseI2C.I2C_DeviceWrite(_handle, _deviceAddress, sizeToTransfer, buffer, out sizeTransfered, options);
        }

        public FtResult Read(byte[] buffer, int sizeToTransfer, out int sizeTransfered, FtI2CTransferOptions options)
        {
            //EnforceRightConfiguration();
            return LibMpsseI2C.I2C_DeviceRead(_handle, _deviceAddress, sizeToTransfer, buffer, out sizeTransfered, options);
        }

        public void Read(byte[] buffer)
        {
            int sizeTransfered = 0;
            var result = LibMpsseI2C.I2C_DeviceRead(
                _handle, _deviceAddress,
                buffer.Length, buffer, out sizeTransfered, FtI2CTransferOptions.StartBit);

            CheckResult(result);
        }

        protected static void CheckResult(FtResult result)
        {
            if (result != FtResult.Ok)
                throw new I2CChannelNotConnectedException(result);
        }

        //Parts of this code are from http://www.chd.at/sites/default/files/files/FTDI.cs
        //pin = pin number; dir = 0:=input; 1:=output
        public bool SetGPIODirection(byte pin, byte dir)
        {
            if (_handle == IntPtr.Zero)
            {
                return false;
            }
            if (dir == 1)
            {
                _direction |= (byte)(1 << pin);
            }
            else
            {
                _direction &= ((byte)~(1 << pin));
            }
            _gpo &= ((byte)~(1 << pin));

            lock (_lock)
            {
                var status = LibMpsseI2C.FT_WriteGPIO(_handle, _direction, _gpo);
                CheckResult(status);
            }
            return true;
        }

        public bool SetGPIOOn(byte pin)
        {
            if (_handle == IntPtr.Zero)
            {
                return false;
            }
            _gpo = (byte)(_gpo | (byte)(1 << pin));

            lock (_lock)
            {
                var status = LibMpsseI2C.FT_WriteGPIO(_handle, _direction, _gpo);
                CheckResult(status);
            }
            return true;
        }

        public bool SetGPIOOff(byte pin)
        {
            if (_handle == IntPtr.Zero)
            {
                return false;
            }

            _gpo &= ((byte)~(1 << pin));

            lock (_lock)
            {
               var status = LibMpsseI2C.FT_WriteGPIO(_handle, _direction, _gpo);
                CheckResult(status);
            }
            return true;
        }

        public bool ReadGPIO(byte pin, out bool value)
        {
            if (_handle == IntPtr.Zero)
            {
                value = false;
                return false;
            }

            lock (_lock)
            {
                int valTest;

                var status = LibMpsseI2C.FT_ReadGPIO(_handle, out valTest);

                var valShift = (valTest >> pin) & 1;

                value = valShift == 1;

                CheckResult(status);
            }

            return true;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

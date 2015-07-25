namespace XIOTCore.FTDI.Types
{
    public enum FtResult
    {
        Ok = 0,
        InvalidHandle,
        DeviceNotFound,
        DeviceNotOpened,
        IoError,
        InsufficientResources,
        InvalidParameter,
        InvalidBaudRate,
    }
}

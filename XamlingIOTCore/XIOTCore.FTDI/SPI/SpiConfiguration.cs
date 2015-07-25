namespace XIOTCore.FTDI.SPI
{
    public class SpiConfiguration
    {
        public static readonly SpiConfiguration ChannelZeroConfiguration = new SpiConfiguration(0);

        public int ChannelIndex { get; private set; }

        public SpiConfiguration(int channelIndex)
        {
            ChannelIndex = channelIndex;
        }

    }
}

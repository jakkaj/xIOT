using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIOTCore.Contract.Enum
{
    public enum OLEDDisplaySize
    {
        SSD1306_128_64,
        SSD1306_128_32,
        SSD1306_96_16
    }

    public static class OLEDConstants
    {
        public const int BLACK = 0;
        public const int WHITE = 1;
        public const int INVERSE = 2;

        public const int SSD1306_I2C_ADDRESS = 0x3C;

        public const int SSD1306_SETCONTRAST = 0x81;
        public const int SSD1306_DISPLAYALLON_RESUME = 0xA4;
        public const int SSD1306_DISPLAYALLON = 0xA5;
        public const int SSD1306_NORMALDISPLAY = 0xA6;
        public const int SSD1306_INVERTDISPLAY = 0xA7;
        public const int SSD1306_DISPLAYOFF = 0xAE;
        public const int SSD1306_DISPLAYON = 0xAF;

        public const int SSD1306_SETDISPLAYOFFSET = 0xD3;
        public const int SSD1306_SETCOMPINS = 0xDA;

        public const int SSD1306_SETVCOMDETECT = 0xDB;

        public const int SSD1306_SETDISPLAYCLOCKDIV = 0xD5;
        public const int SSD1306_SETPRECHARGE = 0xD9;

        public const int SSD1306_SETMULTIPLEX = 0xA8;

        public const int SSD1306_SETLOWCOLUMN = 0x00;
        public const int SSD1306_SETHIGHCOLUMN = 0x10;

        public const int SSD1306_SETSTARTLINE = 0x40;

        public const int SSD1306_MEMORYMODE = 0x20;
        public const int SSD1306_COLUMNADDR = 0x21;
        public const int SSD1306_PAGEADDR = 0x22;

        public const int SSD1306_COMSCANINC = 0xC0;
        public const int SSD1306_COMSCANDEC = 0xC8;

        public const int SSD1306_SEGREMAP = 0xA0;

        public const int SSD1306_CHARGEPUMP = 0x8D;

        public const int SSD1306_EXTERNALVCC = 0x1;
        public const int SSD1306_SWITCHCAPVCC = 0x2;

        // Scrolling #defines
        public const int SSD1306_ACTIVATE_SCROLL = 0x2F;
        public const int SSD1306_DEACTIVATE_SCROLL = 0x2E;
        public const int SSD1306_SET_VERTICAL_SCROLL_AREA = 0xA3;
        public const int SSD1306_RIGHT_HORIZONTAL_SCROLL = 0x26;
        public const int SSD1306_LEFT_HORIZONTAL_SCROLL = 0x27;
        public const int SSD1306_VERTICAL_AND_RIGHT_HORIZONTAL_SCROLL = 0x29;
        public const int SSD1306_VERTICAL_AND_LEFT_HORIZONTAL_SCROLL = 0x2A;
    }
}

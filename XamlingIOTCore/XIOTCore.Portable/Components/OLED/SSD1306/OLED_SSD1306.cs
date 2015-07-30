//a partial port of https://github.com/adafruit/Adafruit_SSD1306

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.Devices;

namespace XIOTCore.Portable.Components.OLED.SSD1306
{
    public class OLDED_SSD1306_I2C : OLED_SSD1306, IOLED_SSD1306_I2C
    {
        private readonly IXI2CDevice _device;

        public OLDED_SSD1306_I2C(IXI2CDevice device)
        {
            _device = device;
        }

        public async Task<IOLED_SSD1306_I2C> Init(int address, OLEDDisplaySize displaySize)
        {
            var writer = new OLED_SSD1306_I2CIO(_device);
            await writer.Init(address);
            _writer = writer;
            Init(displaySize);
            return this;
        }
    }

    public class OLED_SSD1306 : IOLED_SSD1306
    {
        protected ISimpleWriter _writer;
        private OLEDDisplaySize _displaySize;

        private int _width;
        private int _height;
        private int _pages;

        private byte[] _buffer;
        

        public void Init(OLEDDisplaySize displaySize)
        {
            _displaySize = displaySize;
            if (_displaySize == OLEDDisplaySize.SSD1306_128_32)
            {
                _width = 128;
                _height = 32;
                // Init sequence for 128x32 OLED module
                Command(OLEDConstants.SSD1306_DISPLAYOFF);                    // 0xAE
                Command(OLEDConstants.SSD1306_SETDISPLAYCLOCKDIV);            // 0xD5
                Command(0x80);                                  // the suggested ratio 0x80
                Command(OLEDConstants.SSD1306_SETMULTIPLEX);                  // 0xA8
                Command(0x1F);
                Command(OLEDConstants.SSD1306_SETDISPLAYOFFSET);              // 0xD3
                Command(0x0);                                   // no offset
                Command(OLEDConstants.SSD1306_SETSTARTLINE | 0x0);            // line #0
                Command(OLEDConstants.SSD1306_CHARGEPUMP);                    // 0x8D
                                                                              //if (vccstate == SSD1306_EXTERNALVCC)
                                                                              //{ Command(0x10); }
                                                                              //else
                                                                              //{
                Command(0x14);
                //}
                Command(OLEDConstants.SSD1306_MEMORYMODE);                    // 0x20
                Command(0x00);                                  // 0x0 act like ks0108
                Command(OLEDConstants.SSD1306_SEGREMAP | 0x1);
                Command(OLEDConstants.SSD1306_COMSCANDEC);
                Command(OLEDConstants.SSD1306_SETCOMPINS);                    // 0xDA
                Command(0x02);
                Command(OLEDConstants.SSD1306_SETCONTRAST);                   // 0x81
                Command(0x8F);
                Command(OLEDConstants.SSD1306_SETPRECHARGE);                  // 0xd9
                                                                              //if (vccstate == SSD1306_EXTERNALVCC)
                                                                              //{
                                                                              //    Command(0x22);
                                                                              //}
                                                                              //else
                                                                              //{
                Command(0xF1);
                //}
                Command(OLEDConstants.SSD1306_SETVCOMDETECT);                 // 0xDB
                Command(0x40);
                Command(OLEDConstants.SSD1306_DISPLAYALLON_RESUME);           // 0xA4
                Command(OLEDConstants.SSD1306_NORMALDISPLAY);                 // 0xA6
            }

            if (_displaySize == OLEDDisplaySize.SSD1306_128_64)
            {
                _width = 128;
                _height = 64;
                // Init sequence for 128x64 OLED module
                Command(OLEDConstants.SSD1306_DISPLAYOFF); // 0xAE
                Command(OLEDConstants.SSD1306_SETDISPLAYCLOCKDIV); // 0xD5
                Command(0x80); // the suggested ratio 0x80
                Command(OLEDConstants.SSD1306_SETMULTIPLEX); // 0xA8
                Command(0x3F);
                Command(OLEDConstants.SSD1306_SETDISPLAYOFFSET); // 0xD3
                Command(0x0); // no offset
                Command(OLEDConstants.SSD1306_SETSTARTLINE | 0x0); // line #0
                Command(OLEDConstants.SSD1306_CHARGEPUMP); // 0x8D
                                                           //if (vccstate == SSD1306_EXTERNALVCC)
                                                           //{
                                                           //    Command(0x10);
                                                           //}
                                                           //else
                                                           //{
                Command(0x14);
                //}
                Command(OLEDConstants.SSD1306_MEMORYMODE); // 0x20
                Command(0x00); // 0x0 act like ks0108
                Command(OLEDConstants.SSD1306_SEGREMAP | 0x1);
                Command(OLEDConstants.SSD1306_COMSCANDEC);
                Command(OLEDConstants.SSD1306_SETCOMPINS); // 0xDA
                Command(0x12);
                Command(OLEDConstants.SSD1306_SETCONTRAST); // 0x81
                                                            //if (vccstate == SSD1306_EXTERNALVCC)
                                                            //{
                                                            //    Command(0x9F);
                                                            //}
                                                            //else
                                                            //{
                Command(0xCF);
                // }
                Command(OLEDConstants.SSD1306_SETPRECHARGE); // 0xd9
                                                             //if (vccstate == SSD1306_EXTERNALVCC)
                                                             //{
                                                             //    Command(0x22);
                                                             //}
                                                             //else
                                                             //{
                Command(0xF1);
                // }
                Command(OLEDConstants.SSD1306_SETVCOMDETECT); // 0xDB
                Command(0x40);
                Command(OLEDConstants.SSD1306_DISPLAYALLON_RESUME); // 0xA4
                Command(OLEDConstants.SSD1306_NORMALDISPLAY);
                // 0xA6
            }

            if (_displaySize == OLEDDisplaySize.SSD1306_96_16)
            {
                _width = 96;
                _height = 16;

                // Init sequence for 96x16 OLED module
                Command(OLEDConstants.SSD1306_DISPLAYOFF); // 0xAE
                Command(OLEDConstants.SSD1306_SETDISPLAYCLOCKDIV); // 0xD5
                Command(0x80); // the suggested ratio 0x80
                Command(OLEDConstants.SSD1306_SETMULTIPLEX); // 0xA8
                Command(0x0F);
                Command(OLEDConstants.SSD1306_SETDISPLAYOFFSET); // 0xD3
                Command(0x00); // no offset
                Command(OLEDConstants.SSD1306_SETSTARTLINE | 0x0); // line #0
                Command(OLEDConstants.SSD1306_CHARGEPUMP); // 0x8D
                                                           //if (vccstate == SSD1306_EXTERNALVCC)
                                                           //{
                                                           //    Command(0x10);
                                                           //}
                                                           //else
                                                           //{
                Command(0x14);
                // }
                Command(OLEDConstants.SSD1306_MEMORYMODE); // 0x20
                Command(0x00); // 0x0 act like ks0108
                Command(OLEDConstants.SSD1306_SEGREMAP | 0x1);
                Command(OLEDConstants.SSD1306_COMSCANDEC);
                Command(OLEDConstants.SSD1306_SETCOMPINS); // 0xDA
                Command(0x2); //ada x12
                Command(OLEDConstants.SSD1306_SETCONTRAST); // 0x81
                                                            //if (vccstate == SSD1306_EXTERNALVCC)
                                                            //{
                                                            //    Command(0x10);
                                                            //}
                                                            //else
                                                            //{
                Command(0xAF);
                //}
                Command(OLEDConstants.SSD1306_SETPRECHARGE); // 0xd9
                                                             //if (vccstate == SSD1306_EXTERNALVCC)
                                                             //{
                                                             //    Command(0x22);
                                                             //}
                                                             //else
                                                             //{
                Command(0xF1);
                // }
                Command(OLEDConstants.SSD1306_SETVCOMDETECT); // 0xDB
                Command(0x40);
                Command(OLEDConstants.SSD1306_DISPLAYALLON_RESUME); // 0xA4
                Command(OLEDConstants.SSD1306_NORMALDISPLAY); // 0xA6
            }

            Command(OLEDConstants.SSD1306_DISPLAYON);//--turn on oled panel
            _pages = _height / 8;
            _buffer = new byte[_height * _width / 8];
        }
        public void DrawPixel(int x, int y, int color)
        {
            if ((x < 0) || (x >= _width) || (y < 0) || (y >= _height))
                return;

            // check rotation, move pixel around if necessary
            switch (0)
            {
                case 1:
                    var xtemp = y;
                    y = x;
                    x = xtemp;

                    x = _width - x - 1;
                    break;
                case 2:
                    x = _width - x - 1;
                    y = _width - y - 1;
                    break;
                case 3:
                    var xtemp2 = y;
                    y = x;
                    x = xtemp2;
                    y = _height - y - 1;
                    break;
            }

            // x is which column
            switch (color)
            {
                case OLEDConstants.WHITE:  _buffer[x + (y / 8) * _width] |= (byte)(1 << (y & 7)); break; 
                case OLEDConstants.BLACK: _buffer[x + (y / 8) * _width] &= (byte)~(1 << (y & 7)); break;
                case OLEDConstants.INVERSE: _buffer[x + (y / 8) * _width] ^= (byte)(1 << (y & 7)); break;
            }

        }

        public void Display()
        {
            Command(OLEDConstants.SSD1306_COLUMNADDR);
            Command(0x00);   // Column start address (0 = reset)
            Command(Convert.ToByte(_width - 1)); // Column end address (127 = reset)

            Command(OLEDConstants.SSD1306_PAGEADDR);
            Command(0x00); // Page start address (0 = reset)
            Command(Convert.ToByte(_pages - 1));


            for (int i = 0; i < (_width * _height / 8); i++)
            {
                // send a bunch of data in one xmission

                var sendBuffer = new List<byte>();
                sendBuffer.Add(0x40);

                for (int x = 0; x < 16; x++)
                {
                    sendBuffer.Add(_buffer[i]);
                    i++;
                }
                i--;
                _writer.Write(sendBuffer.ToArray());
            }
        }


        public void Command(byte c)
        {
            _writer.Write(0x00, c);
        }

        public void Data(byte c)
        {
            _writer.Write(0x40, c);
        }
    }
}

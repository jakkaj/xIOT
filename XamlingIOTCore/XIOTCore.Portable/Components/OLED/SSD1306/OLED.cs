using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;

namespace XIOTCore.Portable.Components.OLED.SSD1306
{
    public class OLED
    {
        private readonly ISimpleWriter _writer;
        private readonly OLEDDisplaySize _displaySize;

        public OLED(ISimpleWriter writer, OLEDDisplaySize displaySize)
        {
            _writer = writer;
            _displaySize = displaySize;
        }

        public void Init()
        {
            if (_displaySize == OLEDDisplaySize.SSD1306_128_32)
            {
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
        }

        public void DrawPixel(UInt16 x, UInt16 y, UInt16 color)
        {

        }

        public void Command(int c)
        {
            _writer.Write(c);
        }

        public void Data(UInt16 c)
        {

        }
    }
}

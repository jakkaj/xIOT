### OLED Display in .NET (SSD_1306 based - I2C)

To jump straight in install the Xamling-IOT Nuget package. 

    PM> Install-Package XamlingIOT
  
Update: You can render to a supported display (OLED over I2C for example) from [XAML](https://github.com/jakkaj/xIOT/blob/master/Samples/OLED_SSD1306/OLED_Sample_Universal_RTM/MainPage.xaml.cs) in UWP on Raspeberry Pi!

Video of XAML rendering in action on [YouTube](https://www.youtube.com/watch?v=YOPAqjO0HVU).


Using the Xamling-IOT library you can easily drive an OLED display in a variety of resolutions. 

The best part about using .NET is you can use System.Drawing to draw advanced imagery on the display without the need for 
a large custom library. You can use all Windows and custom fonts, shapes and the rest!

<img src="https://raw.githubusercontent.com/jakkaj/Xamling-IOT/master/Samples/OLED_SSD1306/OLED_Image.jpg" width="300" height="300"/>

With special thanks to the guys at Adafruit for their Arduino code - some of which we ported here. [https://github.com/adafruit/Adafruit_SSD1306](https://github.com/adafruit/Adafruit_SSD1306)

###What you'll need

* Visual Studio 2015
* An FT232H USB breakout or similar (for PC version)
* A Rasberry Pi 2 with Windows 10 IOT Core (for RPi2 version)
* Two resistors (~4.5k). (for PC version)

XIOT supports FTDI based devices, such as [FT232H breakout] (https://learn.adafruit.com/adafruit-ft232h-breakout/overview) from Adafruit. This device can plug straight in to your PC, meaning you can very quickly develop your code and design your circuits before taking them to the PI (and possible others later) for completion. 

###FT232H Wiring

This circuit connects the I2C based OLED display to the FT232H.

![Writing Diagram](https://raw.githubusercontent.com/jakkaj/Xamling-IOT/master/Samples/OLED_SSD1306/FT242H%20to%20I2C%20OLED_FTDI.png "Wiring Diagram")

SCL and SDA both connect to the board in the same row as the SDA connection from the FTDI board. D0 on the FT232H will be the SCL, and SDA will map to both D1 and D2 (one for receive and one for send). The software driver handles this for you. This is due to the FT232H being a very general purpose device. 

You'll need a couple of 5V pull up resistors. I2C requires these but they are not built in to the FT232H - again becasue it's a very generalised device. On a PI or Arduino you don't need to worry about this. 

Once you have all this conneced, you can connect the USB connection to your PC.

###Code
Note: For the PC version you will need to install the [FTDI D2XX drivers](http://www.ftdichip.com/Drivers/D2XX.htm) before any of that stuff work will :) If you're starting from scratch, or you're working in your own project, remember to include the [libMPSSE.dll](http://www.ftdichip.com/Support/SoftwareExamples/MPSSE/LibMPSSE-I2C.htm) file and have it copy to the output dir in its properties. 

Add the XamlingIOT nuget package. 

    PM> Install-Package XamlingIOT
  
Set up the factory. You will have to pass in your platform type to the constructor. This is the only code that changes between PC and Pi2!

```C#
private readonly IXIOTCoreFactory _factory =
          XIOTCoreFactory.Create(Platforms.FTDI_USB);
```

or for running on the PI2 ->

```C#
private readonly IXIOTCoreFactory _factory =
          XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB);
```

Note: On Windows you need to call the XIOTCoreWindowsFactory. We're considering how to make this the same call as the .NET version - but for now this is what we have.

Before you can use the factory you need to call Init() on it. 

```C#
_factory.Init();
```

```C#
var oled = _factory.GetComponent<IOLED_SSD1306_I2C>();

var t = oled.Init(OLEDConstants.SSD1306_I2C_ADDRESS, OLEDDisplaySize.SSD1306_128_64);
t.Wait();

oled.Display();
```

Now you can start writing to the display. 

The idea is you make all the changes you want using DrawPixel() then send it all to the display using Display().

In this lib, we rely on System.Drawing to place some pixels for us, then we copy then to the display buffer, then on to the device. 


First, create a new Bitmap that is the same size as your display. Grab a graphics object that targets that Bitmap. 
```C#
var b = new Bitmap(128, 64);
var g = Graphics.FromImage(b);
```

Now you can start using your mad skills to draw some bits on to the object. I do not have mad skills, so I just drew some text and a love heart :)

```C#
g.DrawString("Xamling-IOT", new Font("Consolas", 11), new SolidBrush(Color.White), 0, 0);

g.DrawString("IOT", new Font("Consolas", 30), new SolidBrush(Color.White), 45, 15);
g.DrawString("Y", new Font("Webdings", 30), new SolidBrush(Color.White), 0, 15);

g.Save();

```

Once you're ready to display your art you need to loop through the pixels row by row and update the display buffer.

```C#
 for (var i = 0; i < b.Height; i++)
{
    for (var x = 0; x < b.Width; x++)
    {
        var p = b.GetPixel(x, i);
        var average = (p.R + p.G + p.G) / 3;

        if (average != 0)
        {
            oled.DrawPixel((ushort)x, (ushort)i, 1);
        }
    }
}

oled.Display();
```

Once you buffer is ready you can call Display and it will pop it on the wire to the device. 

###About

At present the FTDI parts of the framework supports I2C, SPI and GPIO.

We started with DVDPT's [libMPSSEWrapper](https://github.com/DVDPT/libMPSSE-.Net-Wrapper) project and extended from there to add I2C support using FTDI's libMPSSE (which is required). Our fork of that project is [here](https://github.com/jakkaj/libMPSSE-.Net-Wrapper) but the code in XIOT is now separate (as we've made it injectable etc). 

We ported the LCD library from [here](https://bitbucket.org/fmalpartida/new-liquidcrystal/wiki/Home) (thanks fmalpartida for a great lib) - itself a port from the in built Arduino LCD library. 

We've only implemnted the I2C version of the OLED library. Using FTDI it will run from USB - or you can run directly from your PC.

###Further Reading

[FT232H on Adafruit](https://learn.adafruit.com/adafruit-ft232h-breakout/overview) - or skip to the [I2C section](https://learn.adafruit.com/adafruit-ft232h-breakout/i2c)

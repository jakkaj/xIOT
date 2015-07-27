#Driving an I2C LCD in .NET (Hitatchi HD44780 based)

This example demonstrates how to use the Xamling-IOT library to drive an LCD display. 

You can use this sample code, or you can start from scratch! Just install the Xamling-IOT Nuget package. 

    PM> Install-Package XamlingIOT
  

###What you'll need

* Visual Studio 2015
* An FT232H USB breakout or similar (for PC version)
* A Rasberry Pi 2 with Windows 10 IOT Core (for RPi2 version)
* Two resistors (~4.5k). (for PC version)

XIOT supports FTDI based devices, such as [FT232H breakout] (https://learn.adafruit.com/adafruit-ft232h-breakout/overview) from Adafruit. This device can plug straight in to your PC, meaning you can very quickly develop your code and design your circuits before taking them to the PI (and possible others later) for completion. 

###FT232H Wiring

This circuit connects the I2C based LCD display to the FT212H.

![Writing Diagram](https://raw.githubusercontent.com/jakkaj/Xamling-IOT/master/Samples/LCD-Hitatchi-HD44780/FT242H%20to%20I2C%20LCD_bb.png "Wiring Diagram")

From left to right on the LCD are SCL (I2C Clock), SDA (I2C Data), VCC (+5v) and GND. 

SCL and SDA both connect to the board in the same row as the appropriate connection from the FTDI board. D0 on the FT232H will be the SCL, and SDA will map to both D1 and D2 (one for receive and one for send). The software driver handles this for you. This is due to the FT232H being a very general purpose device. 

You'll need a couple of 5V pull up resistors. I2C requires these but they are not built in to the FT232H - again becasue it's a very generalised device. On a PI or Arduino you don't need to worry about this. 

Once you have all this conneced, you can connect the USB connection to your PC. The LCD should illuminiate immediately. 

###Raspberry Pi 2 Wiring

Unlike the FT232H, you do not need any pull up resistors, you can connect the LCD to the pins on the Pi directly. 

![Writing Diagram](https://raw.githubusercontent.com/jakkaj/Xamling-IOT/master/Samples/LCD-Hitatchi-HD44780/FT242H%20to%20I2C%20LCD_PI2.png "Wiring Diagram")

From left to right on the LCD are SCL (I2C Clock), SDA (I2C Data), VCC (+5v) and GND. 

That's it... Super simple!

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

The actual code is faily simple. In this example we turn on the backlight then write out some text. 

```C#
 var i2c = _factory.GetComponent<IXI2CDevice>(); //Grab an I2C device

//Create an I2C LCD, pass in the I2C device. 
//You could set up the factory to grab this using Autofac injection (see advanced examples - TODO :P). 
//This allows you to change the I2C address of the LCD as well as the pin maps to suit displays that may differ. 
var _lcd = new I2CLCD(i2c, 0x27, 2, 1, 0, 4, 5, 6, 7, 3, BacklightPolarity.Positive);

await _lcd.Begin(16,2); //fire it up as a 16x2 LCD

_lcd.BackLight(); //Turn on the backlight. .NoBacklight() will disable it
_lcd.Home(); 
_lcd.SetCursor(0, 0); //Start at character 4 on line 0
_lcd.Write("RPi 2, LCD, C#");
StopwatchDelay.Delay(250);
_lcd.SetCursor(0, 1);
_lcd.Write("git.io/vmEdE");
```       

Cool eh?!

Check out the source code in the main Xamling-IOT library here: [LCD Bits](https://github.com/jakkaj/Xamling-IOT/tree/master/XamlingIOTCore/XIOTCore.Portable/Components/LCD/HD44780).

###About

At present the FTDI parts of the framework supports I2C, SPI and GPIO.

We started with DVDPT's [libMPSSEWrapper](https://github.com/DVDPT/libMPSSE-.Net-Wrapper) project and extended from there to add I2C support using FTDI's libMPSSE (which is required). Our fork of that project is [here](https://github.com/jakkaj/libMPSSE-.Net-Wrapper) but the code in XIOT is now separate (as we've made it injectable etc). 

We ported the LCD library from [here](https://bitbucket.org/fmalpartida/new-liquidcrystal/wiki/Home) (thanks fmalpartida for a great lib) - itself a port from the in built Arduino LCD library. 

We've only implemnted the I2C version of the LCD library. Using FTDI it will run from USB - or you can run directly from your PC.

###Further Reading

Information on the LCD display, wiring and Arduino samples [https://arduino-info.wikispaces.com/LCD-Blue-I2C](https://arduino-info.wikispaces.com/LCD-Blue-I2C). 

[FT232H on Adafruit](https://learn.adafruit.com/adafruit-ft232h-breakout/overview) - or skip to the [I2C section](https://learn.adafruit.com/adafruit-ft232h-breakout/i2c)

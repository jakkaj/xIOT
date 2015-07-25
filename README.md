# XIOTCore

An injectable framework for GPIO, I2C, SPI and other bits on Windows 10 IOT core for Rasberry Pi 2 and FTDI based USB devices. 

    PM> Install-Package XamlingIOT
  
Note: You will need [FTDI D2XX drivers](http://www.ftdichip.com/Drivers/D2XX.htm) if you're using an FTDI based break out (not required for Rasberry Pi 2 deployments). 

XIOTCore is a framework to assist you to develop IOT applications on Windows 10 IOT Core. It allows you to get your projects up and running very quickly so you can concentrate on your thing rather than being bogged down with boilerplate code. 

Note: At the moment the full version of VS2015 RTM does not support Windows 10 apps - it's coming on July 29th! Use RC version for now!

###Getting Started

Once you've installed the Nuget package you can get started by configuring the factory and requesting objects from it. You can request I2C devices, or GPIO devices for example. By configuring the factory you can tell the framework which platform you'd like to operate on, like Raspberry Pi2 or FTDI. 

The framework can also supports HATs (Hardware Attached on Top - like an Arduino shield) and other extensions. At the moment it supports features from the [Explorer HAT Pro](http://shop.pimoroni.com/products/explorer-hat).  

Configure the factor for base Raspberry PI 2 operation. 

```C#
private readonly IXIOTCoreFactory _factory = 
    XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB);
```

Configure for Raspberry PI 2 with an Explorer HAT Pro on top.

```C#
private readonly IXIOTCoreFactory _factory = 
    XIOTCoreWindowsFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);
```

Configure for an FTDI based USB thingo - we use a FT232H

```C#
private readonly IXIOTCoreFactory _factory =
    XIOTCoreWindowsFactory.Create(Platforms.FTDI_USB);
````

Important: Before you can start requesting things, you need to Init() the factory!

Note: You can use the FTDI bits in normal .NET (non Windows) projects, like console apps. The FTDI parts are in a PCL. Use XIOTCoreFactory as opposed to XIOTCoreWindowsFactory.

```C#
_factory.Init();
```

Now that the factory is configured you can start asking for things!

Get the Red Led from the Explorer HAT Pro and turn it off and on

```C#
var _redLed = _factory.GetComponent<IExplorerHat_RedLed>();
_redLed.On();
await Task.Delay(2000);
_redLed.Off();
```

IEXplorerHat_RedLed is based on the IXGpioControl which you can use to do straight GPIO style contorl on any pin. The interface which looks like this.

```C#
public interface IXGpioControl
{
    void On();
    void Off();
    bool State { get; set; }
}
```

What about somethign a little more advanced, like controlling a LCD connected via I2C. Note, change the factory creation settings and this exact same code will work on an FT232H or a Rasberry PI 2! 

Note: The framework supports the common Hitachi HD44780 based displays on I2C only. 

```C#
 var i2c = _factory.GetComponent<IXI2CDevice>(); //Grab an I2C device

//Create an I2C LCD, pass in the I2C device. 
//You could set up the factory to grab this using Autofac injection (see advanced examples - TODO :P). 
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

Cool eh?! For wiring and examples see the LCD samples section (TODO :P). 

###Flexible Framework
XIOT is based around [Autofac](http://autofac.org/) dependency injection, so you can build simple apps right up to the most advanced applications without having to add any complexity. Just inject your device and start using it!

Becasue of XIOT's design around [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection) it can support multiple different platforms wihtout you having to change your code (much!). At this time the framework supports FTDI and Raspberry PI2. 

###Rapid development
Sometimes building for IOT devices is slow and time consuming. That's due in part to slow build, deployment and debug cycles.

XIOT supports FTDI based devices, such as [FT232H breakout] (https://learn.adafruit.com/adafruit-ft232h-breakout/overview) from Adafruit. This device can plug straight in to your PC, meaning you can very quickly develop your code and design your circuits before taking them to the PI (and possible others later) for completion. 

At present the framework supports I2C, SPI. We're working on GPIO too very soon. 

We started with DVDPT's [libMPSSEWrapper](https://github.com/DVDPT/libMPSSE-.Net-Wrapper) project and extended from there to add I2C support using FTDI's libMPSSE (which is required). Our fork of that project is [here](https://github.com/jakkaj/libMPSSE-.Net-Wrapper) but the code in XIOT is now separate (as we've made it injectable etc). 

You will need to install the [FTDI D2XX drivers](http://www.ftdichip.com/Drivers/D2XX.htm) before any of that stuff work will :)

Please our the FTDI examples for more information on how to get it up and running!

###Runs on PI2 and your PC
XIOT is built around Windows 10 Universal Apps. The framework works for UI based apps and headless background apps. It will run on your PC or Surface (using FTDI based USB breakout) or your PI2. Write once, run anywhere (okay, run on some things anyway).

###Support for special components
As time goes on we'll add support for lots of components that you can inject in to your code and start using straight away. For now we've added some things to get started. 

Right now we support the [HC-SR04](http://www.micropik.com/PDF/HCSR04.pdf) sonic distance sensor ([sample](https://github.com/jakkaj/Xamling-IOT/tree/master/Samples/HC-SR04)  and Hitachi (HD44780) based LCD displays. 

We ported the LCD library from [here](https://bitbucket.org/fmalpartida/new-liquidcrystal/wiki/Home) (thanks fmalpartida for a great lib) - itself a port from the in built Arduino LCD library. 

We've only implemnted the I2C version of the LCD library. Using FTDI it will run from USB - or you can run directly from your PI by changing aroudn the factory config. 


* Support for Raspberry PI2 running Windows 10 IOT Core
* Support for FTDI (FT232H) USB for rapid development of circuits and logic from your PC
..* I2C, SPI (GPIO coming soon)
..* Soon this will work from console apps too, so you could create Unit Test for circuits - cool eh. We'll do an example on how to unit test circuits soon!
* Supports the built in Windows 10 IOT classes like I2C, GPIO etc
* Supports Explorer HAT Pro for Raspberry Pi 2
* Supports HC-SR04 ultrasonic distance sensor
* Supports Hitatchi HD44780 based displays using I2C
* More coming very soon...

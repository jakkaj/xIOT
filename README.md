# XIOTCore


    PM> Install-Package XamlingIOT
  
XIOTCore is a framework to assist you to develop IOT applications on Windows 10 IOT Core. It allows you to get your projects up and running very quickly so you can concentrate on your thing rather than being bogged down with boilerplate code. 

###Flexible Framework
XIOT is based around [Autofac](http://autofac.org/) dependency injection, so you can build simple apps right up to the most advanced applications without having to add any complexity. Just inject your device and start using it!

Becasue of XIOT's design around [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection) it can support multiple different platforms wihtout you having to change your code (much!). At this time the framework supports FTDI and Raspberry PI2. 

###Rapid development
Sometimes building for IOT devices is slow and time consuming. That's due in part to slow build, deployment and debug cycles.

XIOT supports FTDI based devices, such as [FT232H breakout] (https://learn.adafruit.com/adafruit-ft232h-breakout/overview) from Adafruit. This device can plug straight in to your PC, meaning you can very quickly develop your code and design your circuits before taking them to the PI (and possible others later) for completion. 

At present the framework supports I2C, SPI. We're working on GPIO too very soon. 

Please see the FTDI examples for more information on how to get it up and running!

###Runs on PI2
XIOT is built around Windows 10 Universal Apps. The framework works for UI based apps and headless background apps. 

###Support for special components
As time goes on we'll add support for lots of components that you can inject in to your code and start using straight away. 

Right now we support the [HC-SR04](http://www.micropik.com/PDF/HCSR04.pdf) sonic distance sensor ([sample](https://github.com/jakkaj/Xamling-IOT/tree/master/Samples/HC-SR04)  and Hitachi (HD44780) based LCD displays. 

We ported the LCD library from [here](https://bitbucket.org/fmalpartida/new-liquidcrystal/wiki/Home) (thanks fmalpartida for a great lib) - itself a port from the in built Arduino LCD library. 

We've only implemnted the I2C version of the library. Using FTDI it will run from USB - or you can run directly from your PI by changing aroudn the factory config. 



* 
* Support for Raspberry PI2 running Windows 10 IOT Core
* Support for FTDI (FT232H) USB for rapid development of circuits and logic from your PC
..* I2C, SPI (GPIO coming soon)
* Supports the built in Windows 10 IOT classes like I2C, GPIO etc

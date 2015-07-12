#Distance measurement using a HC-SR04 Module

This example demonstrates how to use the xIOT library to measure distances using the HC-SR04 echo location module. 

You can use this sample code, or you can start from scratch! Just install the Xamling-IOT Nuget package. 

###What you'll need

* Windows 10 IOT Core running on a Raspberry Pi 2
* An [Explorer HAT Pro] (http://shop.pimoroni.com/products/explorer-hat). You can do this without the Explorer, but this example uses it)
* A PNP transistor. I used a BC328-25 from Jaycar. A 2N2906 will do the trick too. 
* Some resistors (~500, ~1k, ~4k). 

###Wiring

The basic idea of the circuit is that we tell the module to do a ping by running voltage to the tigger lead. We then time how long it takes for the module to signal to rise on the input pin. 

This is all handled by the library - if you want to check out what is going on inside, including the speed of sound measurements, check out the source code in the main Xamling-IOT library here: [HC-SR04.cs](https://github.com/jakkaj/Xamling-IOT/blob/master/XamlingIOTCore/XIOTCore.Components/Modules/Range/HC-SR04.cs).


![Writing Diagram](https://raw.githubusercontent.com/jakkaj/Xamling-IOT/master/Samples/HC-SR04/HC-SR04%20on%20Explorer%20HAT%20Pro_bb.png "Wiring Diagram")

**Please note:** The transitor here is shown is EBC from the left.

We use a transistor because the Explorer HAT outputs are a bit different than you might expect. When you turn on an output pin on the Explorer, it does not pop some voltage to it, it instead links it to ground. We connect the base pin on the PNP to the output, which will open to ground when we set the output pin on, allowing voltage to flow from E to C on the PNP. 

###Code

Create a new Windows Universal App (WUP) for Windows 10 or use the sample included here. If you do create a new App youself, make sure you include the Xamling-IOT Nuget package and add a reference ot the Windows IOT Extension SDK. 

First you'll start by initialising the framework. The framework will support more boards and platforms, so it need to be told what it's going to be running on. 

```C#
private readonly IXIOTCoreFactory _factory =
  XIOTCoreFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);
```

Then you have to initialise the library. 

```C#
private IHC_SR04 _echoLocationModule;

public MainPage()
{
  this.InitializeComponent();
  _factory.Init();

```

Now there are a couple of ways you can proceed. You can instanciate the objects in a manual fashion, or you can configure the IOC container to instantiate your objects and inject them for you. 

####Method 1 - Manual instantiation

First, choose your pins. In this example we are using Output 1 and Input 2. 

```C#
var input2 = _factory.GetComponent<IExplorerHat_Input2>(); //Get the digital input 2 from the Explorer HAT
var output1 = _factory.GetComponent<IExplorerHat_Output1>(); //Get the digital output 1 from the Explorer HAT. 
_echoLocationModule = new HC_SR04(output1, input2);
```

All that is left is to create a loop and continuously asks the module to ping. 

```C#
 private async void _cycle()
{
    await _echoLocationModule.Init();

    while (true)
    {
        var averageRound = await _echoLocationModule.Measure(true);

        DistanceText.Text = averageRound.ToString();
       
    }
}
```

Rememver to call ```_cycle``` from your constructor **after** you call Init on the factory!

Also remember to create a TextBlock in your XAML code called DistanceText!

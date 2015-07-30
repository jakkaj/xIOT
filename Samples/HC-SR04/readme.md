#Distance measurement using a HC-SR04 Module

    PM> Install-Package xIOT

This example demonstrates how to use the Xamling-IOT library to measure distances using the HC-SR04 echo location module. 

You can use this sample code, or you can start from scratch! Just install the Xamling-IOT Nuget package. 

###What you'll need

* Visual Studio 2015
* Windows 10 IOT Core running on a Raspberry Pi 2
* Windows 10 IOT Core SDK
* An [Explorer HAT Pro] (http://shop.pimoroni.com/products/explorer-hat). You can do this without the Explorer, but this example uses it.
* A PNP transistor. I used a BC328-25 from Jaycar. A 2N2906 will do the trick too. 
* Some resistors (~500, ~1k, ~4k). 

###Wiring

The basic idea of the circuit is that we tell the module to do a ping by running voltage to the tigger lead. We then time how long it takes for the module signal to rise on the input pin. 

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

####Method 2 - MVVM, IOC, Autofac

First up, you'll need to configure the system to inject your own view model. This is done **before** you call ```Init()``` on the factory. 

```C#
_factory.Builder.Register(
  c => new HC_SR04(c.Resolve<IExplorerHat_Output1>(), c.Resolve<IExplorerHat_Input2>())).As<IHC_SR04>();
_factory.Init();
```

You'll note here that you can change the input and outputs to suit your wiring. 

The good thing about this is now you can ask for an IHC_SR04 anywhere in your code without having to configure it!

Create a new view model. Ask the system to inject a IHC_SR04 in to your constructor. Most the code from there on is the same as the first example. Trigger the module to do a ping and get the response. 

```C#
public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private IHC_SR04 _echoLocationModule;

    private string _distance;

    public MainViewModel(IHC_SR04 echoLocationModule)
    {
        _echoLocationModule = echoLocationModule;
        _init();
    }

    async void _init()
    {
        await _echoLocationModule.Init();
        _loop();
    }

    private async void _loop()
    {
        while (true)
        {
            var averageRound = await _echoLocationModule.Measure(true);
            Distance = averageRound.ToString();
            await Task.Yield(); //make sure other stuff can do things. 
        }
    }

    public string Distance
    {
        get { return _distance; }
        set
        {
            _distance = value;
            OnPropertyChanged();
        }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Back in the MainPage.xaml.cs instanciate the view model and set it to the DataContext. 

```C#
var vm = _factory.Container.Resolve<MainViewModel>();

DataContext = vm;
```

Don't forget to add DataBindings on to your TextBlock in the XAML!

```XAML
 <TextBlock Text="{Binding Distance}" />
```

##Special note about Autofac. 

If you are using your own Autofac instance - you can easily add the required modules to that, rather than using ```Init()``` on the Factory. 

Add ```ExplorerHat_Pro_Module``` and ```RaspberryPi_2_ModelB_Module```. 


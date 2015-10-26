using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OLED_Sample_Universal_RTM.View
{
    public sealed partial class OLEDRenderView : UserControl
    {
        public OLEDRenderView()
        {
            this.InitializeComponent();
            _wait2();
            _wait();
        }

        async void _wait2()
        {
            while (true)
            {
                VisualStateManager.GoToState(this, "Upped", true);
                await Task.Delay(4000);
                VisualStateManager.GoToState(this, "Downed", true);
                await Task.Delay(4000);
            }
        }

        async void _wait()
        {
            while (true)
            {
                await Task.Delay(1000);
                TheText.Text = DateTime.Now.ToString("HH:mm:ss tt");
            }
        }
    
}
}

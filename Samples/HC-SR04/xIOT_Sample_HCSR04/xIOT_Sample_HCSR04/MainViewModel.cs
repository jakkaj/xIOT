﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using xIOT_Sample_HCSR04.Annotations;
using XCore.RaspberryPI.Interface;
using XIOTCore.Contract.Interface.Module;

namespace xIOT_Sample_HCSR04
{
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
}

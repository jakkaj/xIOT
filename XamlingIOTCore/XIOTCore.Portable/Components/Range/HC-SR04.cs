using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Contract.Interface.GPIO;

namespace XIOTCore.Portable.Components.Range
{
    public class HC_SR04 : IHC_SR04
    {
        private readonly IXGpioControl _triggerOutput;
        private readonly IXGpioControl _input;

        List<decimal> _averages = new List<decimal>();

        public HC_SR04(IXGpioControl triggerOutput, IXGpioControl input)
        {
            _triggerOutput = triggerOutput;
            _input = input;
        }

        public async Task Init()
        {
            _triggerOutput.On();
            await Task.Delay(1000);
            _triggerOutput.Off();
        }

        //Meansures the range from the echo device in millimeters

        public async Task<decimal> Measure(bool averageOutResult = false)
        {
            await Task.Delay(20);

            _triggerOutput.On();
            await Task.Delay(20);
            _triggerOutput.Off();

            var sw = new Stopwatch();

            var sw2 = new Stopwatch();

            sw2.Start();
            while (!_input.State)
            {
                if (sw2.Elapsed.TotalSeconds > 1)
                {
                    break;
                }
            }

            sw.Start();

            sw2 = new Stopwatch();
            sw2.Start();

            while (_input.State)
            {
                if (sw2.Elapsed.TotalSeconds > 1)
                {
                    break;
                }
            }

            sw.Stop();

            var freq = Stopwatch.Frequency;

            var ts = sw.ElapsedTicks;

            ts = ts/2;

            Int64 sos = 344000;

            decimal result = ((1M/freq)*ts)*sos;

            if (result > 6000)
            {
                return _getAverages();
            }

            _averages.Add(result);

            while (_averages.Count > 5)
            {
                _averages.RemoveAt(1);
            }

            if (averageOutResult)
            {
                return _getAverages();
            }

            return result;
        }

        decimal _getAverages()
        {
            if (_averages.Count == 0)
            {
                return -1;
            }

            var averageResult = _averages.Average();

            var averageRound = Math.Round(averageResult, 0);

            return averageRound;
        }
    }
}

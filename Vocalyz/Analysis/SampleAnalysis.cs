using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocalyz.Analysis
{
    public struct SampleAnalysis
    {
        public double Frequency
        {
            get;
            set;
        }
        public double Amplitude
        {
            get;
            set;
        }

        public double CalculateIntensity()
        {
            return Physics.GetIntensity(Amplitude);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocalyz.DesignPattern;

namespace Vocalyz.Analysis.Amplitude
{
    public static class AWeigth
    {
        public static double[] AIWeights = new double[50000];

        [InDevelopment(InDevelopmentState.HAS_BUG, "not working")]
        public static void Initialize()
        {
            for (int i = 0; i < AIWeights.Length; i++)
            {
                double a = Physics.GetAWeigth(i);

                double intensity = 1d * Math.Pow(10, a / 10);

                var amplitudeInAir = Physics.GetAmplitude(intensity);

                AIWeights[i] = amplitudeInAir / 10d;
            }
        }

        [InDevelopment(InDevelopmentState.HAS_BUG, "not working")]
        public static double GetAmplitude(double amplitude, double frequency)
        {
            var r = amplitude * AIWeights[(int)frequency];

            if (double.IsNaN(r) || double.IsNegativeInfinity(r) || double.IsPositiveInfinity(r))
            {
                return 0;
            }
            return r;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocalyz.Analysis;

namespace Vocalyz.Graphics
{
    /// <summary>
    /// Q Factor ? => todo?
    /// </summary>
    public class RainbowSpectrogram : Spectrogram
    {
        private double SignificantAmplitude
        {
            get;
            set;
        }
        public RainbowSpectrogram(ShortTimeFourierTransform stft, int maxFrequency, double significantAmplitude = 80) : base(stft, maxFrequency)
        {
            this.SignificantAmplitude = significantAmplitude;
        }
        protected override void Fill(DirectBitmap spectrogramBmp)
        {
            double maxDb = 0;

            foreach (var v in TimedAnalyse)
            {
                foreach (var d in v)
                {
                    if (d.Amplitude > maxDb)
                        maxDb = d.Amplitude;
                }
            }

            int X = 0;

            double max = 0;
            double f = 0;

            foreach (var samplesAnalysis in TimedAnalyse)
            {
                foreach (var a in samplesAnalysis)
                {
                    if (a.Amplitude > max)
                    {
                        max = a.Amplitude;
                        f = a.Frequency;
                    }
                }
            }
            Console.WriteLine("Max frequency is " + f);


            foreach (var samplesAnalysis in TimedAnalyse)
            {
                int Y = 0;
                foreach (var sampleAnalyse in samplesAnalysis)
                {
                    Color pixelColor = ColorUtils.MapRainbowColor(maxDb, 0, maxDb);

                    if (!double.IsPositiveInfinity(sampleAnalyse.Amplitude) && !double.IsNegativeInfinity(sampleAnalyse.Amplitude) && sampleAnalyse.Amplitude > SignificantAmplitude)
                    {
                        pixelColor = ColorUtils.MapRainbowColor(sampleAnalyse.Amplitude, maxDb, SignificantAmplitude);
                    }
                    spectrogramBmp.SetPixel(X, Y, pixelColor);
                    Y++;

                }
                X++;
            }
        }




    }

}

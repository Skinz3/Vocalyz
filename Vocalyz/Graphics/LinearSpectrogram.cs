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
    public class LinearSpectrogram : Spectrogram
    {

        private SpectrogramColor[] Colors
        {
            get;
            set;
        }

        public LinearSpectrogram(ShortTimeFourierTransform stft, int maxFrequency) : base(stft, maxFrequency)
        {
            this.Colors = GetDefaultColors();
        }
        protected override void Fill(DirectBitmap spectrogramBmp)
        {
            int X = 0;

            var defaultColor = Colors.First(x => x.MinimumAmplitude == Colors.Min(y => y.MinimumAmplitude)).Color;

            foreach (var samplesAnalysis in TimedAnalyse)
            {
                int Y = 0;
                foreach (var sampleAnalyse in samplesAnalysis)
                {
                    Color pixelColor = defaultColor;

                    foreach (var pair in Colors)
                    {
                        if (sampleAnalyse.Amplitude > pair.MinimumAmplitude)
                        {
                            pixelColor = pair.Color;
                        }
                    }
                    spectrogramBmp.SetPixel(X, Y, pixelColor);
                    Y++;

                }
                X++;
            }
        }
        public void SetColors(SpectrogramColor[] colors)
        {
            this.Colors = colors;
        }
        public static SpectrogramColor[] GetDefaultColors()
        {
            return new SpectrogramColor[]
            {
                new SpectrogramColor(0,Color.DarkBlue),
                new SpectrogramColor(80,Color.Blue),
                new SpectrogramColor(90,Color.Orange),
                new SpectrogramColor(100,Color.Red),
            };
        }
    }
    public struct SpectrogramColor
    {
        public SpectrogramColor(double minimumAmplitude, Color color)
        {
            this.MinimumAmplitude = minimumAmplitude;
            this.Color = color;
        }

        public double MinimumAmplitude
        {
            get;
            private set;
        }
        public Color Color
        {
            get;
            private set;
        }
    }
}

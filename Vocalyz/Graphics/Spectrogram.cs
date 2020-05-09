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
    public abstract class Spectrogram
    {
        public const int FREQUENCY_TEXT_GAP = 50;
        public const int TIME_TEXT_GAP = 50;

        private int MaxFrequency
        {
            get;
            set;
        }
        protected List<SampleAnalysis[]> TimedAnalyse
        {
            get;
            private set;
        }
        private ShortTimeFourierTransform Analyser
        {
            get;
            set;
        }

        public Spectrogram(ShortTimeFourierTransform stft, int maxFrequency)
        {
            this.MaxFrequency = maxFrequency;
            this.Analyser = stft;
            this.TimedAnalyse = new List<SampleAnalysis[]>();
        }

        protected abstract void Fill(DirectBitmap spectrogramBmp);

        public double GetFrequency(Point point)
        {
            int height = Physics.GetScaledFFTLength(MaxFrequency, Analyser.FFTLength, Analyser.WaveFormat.SampleRate);
            var gap = Physics.GetFrequencyGap(Analyser.WaveFormat.SampleRate, Analyser.FFTLength); 
            var f = (point.Y) * gap;
            return f;
        }
        public double GetTime(Point point)
        {
            var time = point.X * Analyser.TimeResolution;
            return time;
        }

        public DirectBitmap Generate()
        {
            Analyser.Processed += Processed;
            Analyser.Start();
            Analyser.Processed -= Processed;

            int width = (int)(Analyser.TotalTime / Analyser.TimeResolution);
            int height = Physics.GetScaledFFTLength(MaxFrequency, Analyser.FFTLength, Analyser.WaveFormat.SampleRate);

            DirectBitmap spectrogramBmp = new DirectBitmap(width, height);

            Fill(spectrogramBmp);

            TimedAnalyse.Clear();
            spectrogramBmp.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

            return spectrogramBmp;
        }
        private void Processed(SampleAnalysis[] obj)
        {
            TimedAnalyse.Add(obj.FilterByFrequency(MaxFrequency));
        }
        private void AddText(Bitmap bmp, string text, int x, int y)
        {
            var font = new Font("Arial", 8);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            SizeF stringSize = g.MeasureString(text, font);
            RectangleF rectf = new RectangleF(x, y - stringSize.Height / 2, stringSize.Width, stringSize.Height);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(text, font, Brushes.White, rectf);
            g.Flush();
        }

    }
}

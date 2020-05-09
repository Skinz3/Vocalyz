using Microsoft.Xna.Framework;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocalyz.Analysis;
using Vocalyz.Analysis.RealTime;
using Vocalyz.Graphics;
using Vocalyz.Music;
using Vocalyz.RealtimeSpectrum.Graphics;

namespace Vocalyz.RealtimeSpectrum
{
    public class DrawableSpectrum
    {
        public static int FFT_LENGTH = 4096;// 4096;//4096;//4096;//44100;//1024;// (int) Math.Pow(2, 16);// 44100;

        public const int HEIGHT = 600;

        public const double MAX_FREQUENCY = 2000;

        public const int PIXEL_GAP = 5;

        public const double SignificantAmplitude = 80;

        private Rectangle Rectangle
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        private Point Position
        {
            get
            {
                return new Point((int)(SpectrumView.WIDOW_WIDTH / 2d - Width / 2d), SpectrumView.WINDOW_HEIGHT - HEIGHT);
            }
        }
        private DrawableLine[] Lines
        {
            get;
            set;
        }
        private ShortTimeFourierTransform Analyser
        {
            get;
            set;
        }
        private AudioPlayback Player
        {
            get;
            set;
        }
        public DrawableSpectrum(string filePath)
        {

            this.Player = new AudioPlayback();
            this.Analyser = new ShortTimeFourierTransform(filePath, FFT_LENGTH, 0.01);
            this.Player.Load(filePath, FFT_LENGTH, Analyser);

            this.Analyser.Processed += Analyser_Processed;

            int gap = Analyser.WaveFormat.SampleRate / Analyser.FFTLength;

            if (gap == 0)
                gap = 1;

            int linesCount = Physics.GetScaledFFTLength(MAX_FREQUENCY, Analyser.FFTLength, Analyser.WaveFormat.SampleRate);

            this.Lines = new DrawableLine[linesCount];

            for (int i = 0; i < Lines.Length; i++)
            {
                Lines[i] = new DrawableLine(new Vector2(), new Vector2(), Color.White);
            }
            Width = linesCount * PIXEL_GAP;
            this.Rectangle = new Rectangle(Position.X, Position.Y, Width, HEIGHT);


        }

        private void Analyser_Processed(SampleAnalysis[] obj)
        {
            List<SampleAnalysis> results = new List<SampleAnalysis>();


            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i].Frequency <= MAX_FREQUENCY)
                {
                    results.Add(obj[i]);
                }
            }


            for (int i = 0; i < Lines.Length; i++)
            {
                Lines[i].Color = Color.White;
                Lines[i].Text = null;
            }

            double maxDb = 0;

            foreach (var d in obj)
            {
                if (d.Amplitude > maxDb)
                    maxDb = d.Amplitude;
            }

            int x = 0;

            var res = results.OrderByDescending(r => r.Amplitude).Take(2);
            foreach (var r in res)
            {
                if (r.Amplitude > 90)
                {
                    var note = NotesManager.FindNote(r.Frequency);

                    if (note != null)
                        Lines[results.IndexOf(r)].Text = note.ToString();
                }
            }
        

         

            for (int i = 0; i < results.Count; i++)
            {
                int relativeIntensity = (int)(100 + (results[i].CalculateIntensity() * 3000)); // change me this

                Lines[i].Start = new Vector2(Position.X + x, Position.Y + HEIGHT);
                Lines[i].End = new Vector2(Position.X + x, Position.Y + HEIGHT - relativeIntensity);
                x += PIXEL_GAP;



                /*   if (maxDb > 0)
                   {
                       var pixelColor = ColorUtils.MapRainbowColor(maxDb, 0, maxDb);

                       if (results[i].Amplitude > SignificantAmplitude+20)
                       {
                           Lines[i].Text = results[i].Frequency.ToString();
                       }

                       if (!double.IsPositiveInfinity(results[i].Amplitude) && !double.IsNegativeInfinity(results[i].Amplitude) && results[i].Amplitude > SignificantAmplitude)
                       {
                           pixelColor = ColorUtils.MapRainbowColor(results[i].Amplitude, maxDb, SignificantAmplitude);
                       }

                       Lines[i].Color = new Color(pixelColor.R, pixelColor.G, pixelColor.B, pixelColor.A);
                   } */


            }
        }

        public void Pause()
        {
            Player?.Pause();
        }

        public void Dispose()
        {
            Player?.Dispose();
            Player = null;
        }





        public void Start()
        {
            this.Analyser.Reset();
            this.Player.Play();
        }
        public void Draw()
        {
            Debug.DrawRectangle(Rectangle, Color.White, 1);

            foreach (var line in Lines)
            {
                line.Draw();
            }

            Debug.DrawText("FFT Length : " + Analyser.FFTLength, new Vector2(), Color.White);
            Debug.DrawText("Sample Rate : " + Analyser.WaveFormat.SampleRate, new Vector2(0, 20), Color.White);
            Debug.DrawText("Max Frequency:" + MAX_FREQUENCY, new Vector2(0, 40), Color.White);

        }
    }
}

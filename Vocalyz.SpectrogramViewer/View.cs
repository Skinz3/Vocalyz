using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocalyz.Analysis;
using Vocalyz.Analysis.Amplitude;
using Vocalyz.Graphics;
using Vocalyz.Music;

namespace Vocalyz.SpectrogramViewer
{
    public partial class View : Form
    {
        ShortTimeFourierTransform analyser;

        Spectrogram spectrogram;

        WaveOut Player;

        DirectBitmap OriginalBitmap;

        public View()
        {
            InitializeComponent();
            AWeigth.Initialize();
        }
        private void ResetSpectrogramBitmap()
        {
            pictureBox1.Invoke(new Action(() => pictureBox1.Image = (Bitmap)OriginalBitmap.Bitmap.Clone()));
        }
        private void OnEnded(double obj)
        {
            pictureBox1.Refresh();
            Debug(Path.GetFileName(analyser.FilePath) + " processed in " + (int)obj + "s");
        }
        private void Debug(string text)
        {
            richTextBox1.AppendText(Environment.NewLine + text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Player?.Pause();
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var r = openFileDialog1.ShowDialog();
            if (r == DialogResult.OK)
                DisplaySpectrogram(openFileDialog1.FileName);



        }
        public void Play()
        {
            if (Player != null && Player.PlaybackState == PlaybackState.Playing)
            {
                return;
            }
            if (Player != null && Player.PlaybackState == PlaybackState.Paused)
            {
                Player.Play();
                return;
            }

            new Thread(new ThreadStart(new Action(() =>
            {
                var pair = analyser.CreateWaveOut();
                pair.Key.Play();
                Player = pair.Key;
                var g = System.Drawing.Graphics.FromImage(pictureBox1.Image);

                while (Player != null && Player.PlaybackState != PlaybackState.Stopped)
                {
                    if (Player.PlaybackState == PlaybackState.Paused)
                        continue;
                    g.Clear(Color.White);
                    g.DrawImage(OriginalBitmap.Bitmap, new PointF());

                    int x = (int)((OriginalBitmap.Width) * ((double)pair.Value.Position / pair.Value.Length));
                    g.DrawLine(new Pen(Brushes.Black, 2f), new Point(x, 1), new Point(x, pictureBox1.Height));
                    pictureBox1.Invoke(new Action(() => pictureBox1.Refresh()));

                }
                Player?.Dispose();
                Player = null;
                pair.Value.Dispose();
                ResetSpectrogramBitmap();


            }))).Start();
        }
        public void DisplaySpectrogram(string filePath)
        {
            // analyser = new ShortTimeFourierTransform(filePath, double.Parse(fResolutionTxt.Text), double.Parse(timeStepTxt.Text));
            analyser = new ShortTimeFourierTransform(filePath, int.Parse(fftSizeTxt.Text), double.Parse(timeStepTxt.Text));
            spectrogram = new RainbowSpectrogram(analyser, int.Parse(textBox1.Text));
            analyser.Ended += OnEnded;
            OriginalBitmap = spectrogram.Generate();
            pictureBox1.Image = (Bitmap)OriginalBitmap.Bitmap.Clone();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                DialogResult result = saveFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Player?.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (analyser != null)
                DisplaySpectrogram(analyser.FilePath);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            int minSpace = 10;

            var px = OriginalBitmap.GetPixels();
            List<int> pxx = new List<int>();

            int spacing = 0;

            for (int i = 0; i < OriginalBitmap.Width; i++)
            {

                int h = OriginalBitmap.Height;
                int totalR = 0;
                int totalB = 0;
                int totalG = 0;

                Color average = Color.Transparent;

                for (int j = 0; j < h; j++)
                {
                    var color = OriginalBitmap.GetPixel(i, j);

                    totalR += color.R;
                    totalG += color.G;
                    totalB += color.B;

                }

                var oldAverage = average;

                average = Color.FromArgb(totalR / h, totalG / h, totalB / h);
                spacing++;


                if (average.G > 100 && spacing >= minSpace)
                {
                    pxx.Add(i);
                    spacing = 0;
                }
            }

            var a = OriginalBitmap.Bitmap;

            foreach (var x in pxx)
            {
                for (int i = 0; i < OriginalBitmap.Height; i++)
                {
                    a.SetPixel(x, i, Color.Violet);
                }
            }

            pictureBox1.Image = a;
            var p = px.Where(x => x.Value.R == 255).ToArray();

            var z = spectrogram.GetFrequency(p[1].Key);

            // (pictureBox1.Image as Bitmap).SetPixel(p.Key.X, p.Key.Y, Color.Violet);



        }
    }
}

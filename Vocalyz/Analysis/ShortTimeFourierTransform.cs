using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vocalyz.Analysis.Amplitude;
using Vocalyz.DesignPattern;

namespace Vocalyz.Analysis
{
    public class ShortTimeFourierTransform
    {
        public int FFTLength
        {
            get;
            private set;
        }

        public event Action<SampleAnalysis[]> Processed;

        /// <summary>
        /// double = elapsed time, in seconds
        /// </summary>
        public event Action<double> Ended;

        private AudioFileReader AudioReader
        {
            get;
            set;
        }
        /// <summary>
        /// En secondes
        /// </summary>
        public double TimeResolution
        {
            get;
            private set;
        }
        private NotifyingSampleProvider SampleProvider
        {
            get;
            set;
        }
        private int SampleCount
        {
            get;
            set;
        }
        public WaveFormat WaveFormat
        {
            get
            {
                return AudioReader.WaveFormat;
            }
        }
        public int SampleGap
        {
            get
            {
                return (int)(WaveFormat.SampleRate * TimeResolution);
            }
        }
        public double ProcessingCurrentTime
        {
            get
            {
                return SampleProcessed / (double)AudioReader.WaveFormat.SampleRate;
            }
        }
        public double TotalTime
        {
            get
            {
                return AudioReader.TotalTime.TotalSeconds;
            }
        }

        private Queue<float> Samples
        {
            get;
            set;
        }
        private int SampleProcessed
        {
            get;
            set;
        }
        private int CurrentGap
        {
            get;
            set;
        }
        public string FilePath
        {
            get;
            private set;
        }
        public double ProgressPercent
        {
            get
            {
                return (double)SampleProcessed / (SampleCount / WaveFormat.Channels) * 100;
            }
        }

        private int m;

        public ShortTimeFourierTransform(string filePath, int fftLength = 1024, double timeResolution = 0.010)
        {
            FilePath = filePath;
            FFTLength = fftLength;
            TimeResolution = timeResolution;
            m = (int)Math.Log(FFTLength, 2.0);
            Reset();
        }
        /*
         * Yet im not able to calculate FFT with non power of 2 fftsize. !!! check it out? !!! it is important? !!!
         * -------> WIP <---------------
        public ShortTimeFourierTransform(string filePath, double frequencyGap, double timeResolution = 0.010)
        {
            FilePath = filePath;
            TimeResolution = timeResolution;
            Reset();
            FFTLength = (int)(AudioReader.WaveFormat.SampleRate / frequencyGap);
            m = (int)Math.Log(FFTLength, 2.0);
        } */
        public void Start()
        {
            Reset();
            float[] samples = new float[SampleCount];
            Stopwatch stopwatch = Stopwatch.StartNew();
            SampleProvider.Read(new float[SampleCount], 0, SampleCount);
            SampleProvider.Sample -= OnSample;
            stopwatch.Stop();
            AudioReader.Dispose();
            Ended?.Invoke(stopwatch.Elapsed.TotalSeconds);
        }
        public KeyValuePair<WaveOut, AudioFileReader> CreateWaveOut()
        {
            var reader = new AudioFileReader(FilePath);
            var waveOut = new WaveOut();
            waveOut.Init(reader);
            return new KeyValuePair<WaveOut, AudioFileReader>(waveOut, reader);
        }
        public void Reset()
        {
            AudioReader = new AudioFileReader(FilePath);
            SampleProvider = new NotifyingSampleProvider(AudioReader);
            Samples = new Queue<float>();
            SampleCount = AudioReader.GetSampleCount();
            SampleProvider.Sample += OnSample;
            CurrentGap = 0;
            SampleProcessed = 0;
        }
        private void Enqueue(float sample)
        {
            Samples.Enqueue(sample);

            if (Samples.Count > FFTLength)
                Samples.Dequeue();
        }
        [InDevelopment(InDevelopmentState.TODO, "Set maximum frequency here and not in the spectrogram class, btw FastFourierTransform.FFT(forward = false) give interessant results... think about it")]
        public void AddSample(float sample)
        {
            SampleProcessed++;

            if (Samples.Count < FFTLength)
            {
                Enqueue(sample);
            }
            else
            {
                Enqueue(sample);

                CurrentGap++;

                if (CurrentGap < SampleGap && SampleProcessed != SampleCount)
                {
                    return;
                }

                CurrentGap = 0;

                if (Samples.Count == FFTLength)
                {
                    Complex[] fft = new Complex[FFTLength];

                    int i = 0;

                    foreach (var s in Samples)
                    {
                        fft[i].X = (float)(s * FastFourierTransform.HammingWindow(i, FFTLength));
                        fft[i].Y = 0;
                        i++;
                    }

                    FastFourierTransform.FFT(true, m, fft);


                    SampleAnalysis[] result = new SampleAnalysis[FFTLength];


                    for (int n = 0; n < fft.Length; n++)
                    {
                        result[n].Frequency = Physics.GetFrequency(n, AudioReader.WaveFormat.SampleRate, FFTLength);
                        result[n].Amplitude = Physics.GetAmplitude(fft[n]);
                    }

                    Processed?.Invoke(result);

                }
            }


        }


        private void OnSample(object sender, SampleEventArgs e)
        {
            AddSample(e.Left);
        }
    }
}

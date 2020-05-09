using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocalyz.Analysis
{
    /// <summary>
    /// if (FFTPosition >= FFTBuffer.Length) wtf, it seems useless?
    /// deprecated?
    /// </summary>
    [Obsolete]
    public class WeirdFastFourierTransform
    {
        public int FFTLength
        {
            get;
            private set;
        }
        private AudioFileReader AudioReader
        {
            get;
            set;
        }
        private NotifyingSampleProvider SampleProvider
        {
            get;
            set;
        }
        private Complex[] FFTBuffer
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
        private int FFTPosition
        {
            get;
            set;
        }
        private int SampleCount
        {
            get;
            set;
        }
        private string FilePath
        {
            get;
            set;
        }
        public event Action<SampleAnalysis[]> Processed;

        public WeirdFastFourierTransform(string filePath, int fftLength = 1024)
        {
            FilePath = filePath;
            FFTLength = fftLength;
            FFTBuffer = new Complex[FFTLength];
        }
        public void Start()
        {
            Init();
            SampleCount = AudioReader.GetSampleCount();
            float[] samples = new float[SampleCount];
            SampleProvider.Read(new float[SampleCount], 0, SampleCount);
            SampleProvider.Sample -= OnSample;
        }
        private void Init()
        {
            AudioReader = new AudioFileReader(FilePath);
            SampleProvider = new NotifyingSampleProvider(AudioReader);
            SampleCount = AudioReader.GetSampleCount();
            SampleProvider.Sample += OnSample;
        }
        private void OnSample(object sender, SampleEventArgs e)
        {
            FFTBuffer[FFTPosition].X = (float)(e.Right * FastFourierTransform.HammingWindow(FFTPosition, FFTLength)); // e.Right ? seems equal to e.Left in this case (mono?)
            FFTBuffer[FFTPosition].Y = 0;
            FFTPosition++;
            if (FFTPosition >= FFTBuffer.Length)
            {
                FastFourierTransform.FFT(true, (int)Math.Log(FFTLength, 2.0), FFTBuffer);
                FFTPosition = 0;

                SampleAnalysis[] result = new SampleAnalysis[FFTLength];

                for (int n = 0; n < FFTBuffer.Length; n++)
                {
                    result[n].Frequency = Physics.GetFrequency(n, AudioReader.WaveFormat.SampleRate, FFTLength);
                    result[n].Amplitude = Physics.GetAmplitude(FFTBuffer[n]);
                }

                Processed?.Invoke(result);
            }
        }

    }
}

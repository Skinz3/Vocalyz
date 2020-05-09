using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocalyz.DesignPattern;

namespace Vocalyz
{
    public static class Physics
    {
        /// <summary>
        /// monsieur Gaudichon, terminale S 
        /// </summary>
        [InDevelopment(InDevelopmentState.THINK_ABOUT_IT, "A good idea to use this?")]
        private const double REFERENCE_SOUND_INTENSITY_IN_AIR = 1E-12d;

        private const double REFERENCE_SOUND_NONE = 1d;

        public static double CURRENT_REFERENCE = REFERENCE_SOUND_INTENSITY_IN_AIR;

        /// <summary>
        /// SampleCount must be multiple of BlockAlign. By experience, we round to the next integer to prevent from error. (what is BlockAlign?)
        /// </summary>
        /// <param name="waveStream"></param>
        /// <returns></returns>
        public static int GetSampleCount(this WaveStream waveStream)
        {
            return (int)Math.Ceiling(waveStream.TotalTime.TotalSeconds * waveStream.WaveFormat.SampleRate * waveStream.WaveFormat.Channels);
        }
        public static double GetIntensity(Complex c)
        {
            return Math.Sqrt(c.X * c.X + c.Y * c.Y);
        }
        public static double GetIntensity(double amplitude)
        {
            return CURRENT_REFERENCE * Math.Pow(10, amplitude / 10);
        }
        /// <summary>
        /// 20 log (p2/p1) dB  = 10 log (p2²/p1²) dB  = 10 log (P2/P1) dB   
        /// </summary>
        public static double GetAmplitude(Complex c)
        {
            return 10 * Math.Log10(GetIntensity(c) / CURRENT_REFERENCE);
        }
        public static double GetAmplitude(double intensity)
        {
            return 10 * Math.Log10(intensity / CURRENT_REFERENCE);
        }
        [InDevelopment(InDevelopmentState.TODO, "https://en.wikipedia.org/wiki/A-weighting")]
        public static double GetAWeigth(double frequency)
        {
            double fS = Math.Pow(frequency, 2);

            double num = Math.Pow(12194, 2) * Math.Pow(frequency, 4);

            double num2 = (fS + Math.Pow(20.6, 2)) * Math.Sqrt((fS + Math.Pow(107.7, 2)) * (fS + Math.Pow(739.9, 2))) * (fS * Math.Pow(12194, 2));

            double ra = num / num2;

            return 20 * Math.Log10(ra) + 2.0;
        }
        public static double GetFrequency(int n, int sampleRate, int fftLength)
        {
            return n * (sampleRate / (double)fftLength);
        }
        /// <summary>
        /// https://en.wikipedia.org/wiki/Piano_key_frequencies
        /// </summary>
        /// <param name="n">number of the key [1-88]</param>
        /// <returns></returns>
        public static double GetNoteFrequency(int n)
        {
            return Math.Pow(2d, ((n - 49d) / 12d)) * 440d;
        }
        public static double GetNoteNumber(int frequency)
        {
            return 12 * Math.Log(frequency / 440d) + 49;
        }
        public static double GetFrequencyGap(int sampleRate, int fftSize)
        {
            return (double)sampleRate / fftSize;
        }
        /// <summary>
        /// Y-a t'il un moyen de ne pas redimensionner la FFT sans perdre en précision?
        /// </summary>
        public static int GetScaledFFTLength(double maxFrequency, int fftSize, int sampleRate)
        {
            return (int)(maxFrequency * fftSize / sampleRate) + 1;

        }
    }
}

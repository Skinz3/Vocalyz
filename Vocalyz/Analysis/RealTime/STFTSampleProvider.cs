using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocalyz.Analysis;

namespace Vocalyz.Analysis.RealTime
{
    public class STFTSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider source;

        private ShortTimeFourierTransform ShortTimeFourierTransform
        {
            get;
            set;
        }
        public STFTSampleProvider(ISampleProvider source, ShortTimeFourierTransform shortTimeFourierTransform, int fftLength = 1024)
        {
            if (!IsPowerOfTwo(fftLength))
            {
                throw new ArgumentException("FFT Length must be a power of two");
            }
            this.ShortTimeFourierTransform = shortTimeFourierTransform;
            this.source = source;
        }

        static bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }

        public WaveFormat WaveFormat => source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            var samplesRead = source.Read(buffer, offset, count);

            for (int n = 0; n < samplesRead; n += source.WaveFormat.Channels)
            {
                ShortTimeFourierTransform.AddSample(buffer[n + offset]);
            }
            return samplesRead;
        }
    }
}

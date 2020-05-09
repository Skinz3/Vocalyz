using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocalyz.Analysis;

namespace Vocalyz.Analysis.RealTime
{
    public class AudioPlayback : IDisposable
    {
        private IWavePlayer PlaybackDevice
        {
            get;
            set;
        }
        private WaveStream FileStream
        {
            get;
            set;
        }
        private ShortTimeFourierTransform ShortTimeFourierTransform
        {
            get;
            set;
        }
        public void Load(string fileName, int fftLength, ShortTimeFourierTransform stft)
        {
            this.ShortTimeFourierTransform = stft;
            Stop();
            CloseFile();
            EnsureDeviceCreated();
            OpenFile(fileName, fftLength);

        }

        private void CloseFile()
        {
            FileStream?.Dispose();
            FileStream = null;
        }

        private void OpenFile(string fileName, int fftLength)
        {
            try
            {
                var inputStream = new AudioFileReader(fileName);
                FileStream = inputStream;
                var aggregator = new STFTSampleProvider(inputStream, ShortTimeFourierTransform, fftLength);
                PlaybackDevice.Init(aggregator);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CloseFile();
            }
        }
        public WaveStream GetInputStream()
        {
            return FileStream;
        }
        private void EnsureDeviceCreated()
        {
            if (PlaybackDevice == null)
            {
                CreateDevice();
            }
        }

        private void CreateDevice()
        {
            PlaybackDevice = new WaveOut { DesiredLatency = 200 }; //200 


        }


        public void Play()
        {
            if (PlaybackDevice != null && FileStream != null && PlaybackDevice.PlaybackState != PlaybackState.Playing)
            {
                PlaybackDevice.Play();
            }
        }

        public void Pause()
        {
            PlaybackDevice?.Pause();
        }

        public void Stop()
        {
            PlaybackDevice?.Stop();
            if (FileStream != null)
            {
                FileStream.Position = 0;
            }
        }

        public void Dispose()
        {
            Stop();
            CloseFile();
            PlaybackDevice?.Dispose();
            PlaybackDevice = null;
        }
    }
}

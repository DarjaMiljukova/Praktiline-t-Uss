//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Uss2
//{
//    public class music
//    {
//        public async Task Tagaplaanis_Mangida(string Path)
//        {
//            await Task.Run(() =>
//            {
//                using (AudioFileReader audioFileReader = new AudioFileReader(Path))
//                using (IWavePlayer waveOutDevice = new WaveOutEvent { DesiredLatency = 200 })
//                {
//                    waveOutDevice.Init(audioFileReader);
//                    waveOutDevice.Play();
//                    while (waveOutDevice.PlaybackState == PlaybackState.Playing)
//                    {
//                        Thread.Sleep(1000);
//                    }
//                }
//            });
//        }


//        public async Task Natuke_Mangida(string Path)
//        {
//            await Task.Run(() =>
//            {
//                using (AudioFileReader audioFileReader = new AudioFileReader(Path))
//                using (IWavePlayer waveOutDevice = new WaveOutEvent())
//                {
//                    waveOutDevice.Init(audioFileReader);
//                    waveOutDevice.Play();
//                    while (waveOutDevice.PlaybackState == PlaybackState.Playing)
//                    {
//                        Thread.Sleep(50);
//                    }
//                }
//            });
//        }

//    }
//}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace Uss2
{
    class Sound
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private float outputVolume;

        public Sound(string fail)
        {
            audioFile = new AudioFileReader(fail);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputVolume = 1.0f;
        }
        public void SetVolume(float volume)
        {
            outputVolume = volume;
            outputDevice.Volume = outputVolume;
        }
        public void Play()
        {
            outputDevice.Play();
        }

        public void Stop()
        {
            outputDevice.Stop();
            audioFile.Position = 0;
        }
        public bool IsPlaying()
        {
            return outputDevice.PlaybackState == PlaybackState.Playing;
        }
    }
}

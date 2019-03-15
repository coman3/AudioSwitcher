using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeTracker
{
    class Program : IObserver<DeviceVolumeChangedArgs>
    {
        static void Main(string[] args)
        {
            new Program().Start();

        }

        void IObserver<DeviceVolumeChangedArgs>.OnCompleted()
        {
            Console.WriteLine("Completed");
        }

        void IObserver<DeviceVolumeChangedArgs>.OnError(Exception error)
        {
            Console.WriteLine(error);
        }

        void IObserver<DeviceVolumeChangedArgs>.OnNext(DeviceVolumeChangedArgs value)
        {
            Console.WriteLine(value.Volume);
        }

        private void Start()
        {

            var controller = new CoreAudioController(true, false, false, false);
            CoreAudioDevice defaultPlaybackDevice = controller.DefaultPlaybackDevice;
            
            defaultPlaybackDevice.ReloadAudioEndpointVolume();
            defaultPlaybackDevice.VolumeChanged.Subscribe(this);
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}

using UnityEngine;

namespace Audio
{
    public class AudioSystem : IAudioSystem
    {
        public void Play(SoundData data, AudioSource audioSource)
        {
            audioSource.pitch = Random.Range(data.MinPitch, data.MaxPitch);
            audioSource.PlayOneShot(data.Clip, data.Volume);
        }

        public void Stop(AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}
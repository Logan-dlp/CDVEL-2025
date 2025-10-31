using UnityEngine;

namespace Audio
{
    public class AudioSystem : IAudioSystem
    {
        public void Play(SoundData data, AudioSource audioSource)
        {
            audioSource.pitch = Random.Range(data.MinPitch, data.MaxPitch);

            switch (data.AudioType)
            {
                case AudioType.SFX:
                    audioSource.PlayOneShot(data.Clip, data.Volume);
                    break;

                case AudioType.Environment:
                    audioSource.clip = data.Clip;
                    audioSource.volume = data.Volume;
                    audioSource.loop = true;
                    audioSource.Play();
                    break;
            }
        }

        public void Stop(AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}
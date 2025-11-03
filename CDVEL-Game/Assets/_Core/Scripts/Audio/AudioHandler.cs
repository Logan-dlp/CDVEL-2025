using Singletons;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandler : MonoSingleton<AudioHandler>
    {
        private AudioSource _audioSource;

        private IAudioSystem _audioSystem;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();

            _audioSystem = new AudioSystem();
        }

        public void PlaySound(SoundData data)
        {
            _audioSystem.Play(data, _audioSource);
        }

        public void StopSound()
        {
            _audioSystem.Stop(_audioSource);
        }
    }
}
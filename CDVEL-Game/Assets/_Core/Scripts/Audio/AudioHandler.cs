using Singletons;
using System.Net.Http.Headers;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandler : MonoSingleton<AudioHandler>
    {
        private AudioSource _audioSource;

        private IAudioSystem _audioSystem;
        private AudioHandler _instance;

        public AudioHandler HandlerInstance => _instance;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();

            _audioSystem = new AudioSystem();
            _instance = this;
        }

        public void PlaySound(SoundData data)
        {
            if(_audioSystem == null)
            {
                Debug.LogWarning("AudioSystem is null so can't play the sound");
            }

            if(data == null)
            {
                Debug.LogWarning("SounData is null");
            }

            _audioSystem.Play(data, _audioSource);
        }

        public void StopSound()
        {
            if (_audioSystem == null) return;
            _audioSystem.Stop(_audioSource);
        }
    }
}
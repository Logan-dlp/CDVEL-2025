using Singletons;
using UnityEngine;

namespace Audio
{
    public class AudioHandler : MonoSingleton<AudioHandler>
    {
        [SerializeField] private AudioSource _environmentSource;
        [SerializeField] private AudioSource _sfxSource;

        private IAudioSystem _audioSystem;

        protected override void Awake()
        {
            base.Awake();
            _audioSystem = new AudioSystem();
        }

        public void PlaySound(SoundData data)
        {
            if(data.AudioType == AudioType.SFX)
            {
                _audioSystem.Play(data, _sfxSource);
            }
            else if(data.AudioType == AudioType.Environment)
            {
                _audioSystem.Play(data, _environmentSource);
            }                
        }

        public void StopSound()
        {
            _audioSystem.Stop(_environmentSource);
        }
    }
}
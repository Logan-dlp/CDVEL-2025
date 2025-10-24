using UnityEngine;

namespace Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private IAudioSystem _audioSystem;

        private void Awake()
        {
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
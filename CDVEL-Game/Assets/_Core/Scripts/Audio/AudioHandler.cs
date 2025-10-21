using System;
using UnityEngine;

namespace Audio
{
    public enum SfxType
    {
        HeartBreak,
        HeartPickup,
        MalusPickup,
        Bumper,
        PlayerCollision,
        Victory,
        Tramway,
        Portal
    }

    public enum MusicType
    {
        GameMusic
    }

    [ExecuteInEditMode]
    public class AudioHandler : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private MusicList[] _musicList;

        [Header("SFX")]
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private SfxList[] _sfxList;

        private static AudioHandler _instance;
        private IAudioSystem _audioSystem;

        public static AudioHandler Instance => _instance;

        private void Awake()
        {
            _audioSystem = new AudioSystem();

            _instance = this;

            if (_audioSystem is AudioSystem sys)
            {
                sys.OnMusicPlayRequested += HandlePlayMusicRequested;
                sys.OnMusicStopRequested += HandleStopMusicRequested;
                sys.OnSfxPlayRequested += HandlePlaySfxRequested;
            }
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            string[] sfxs = Enum.GetNames(typeof(SfxType));
            Array.Resize(ref _sfxList, sfxs.Length);
            for (int i = 0; i < _sfxList.Length; i++){
                _sfxList[i]._name = sfxs[i];
            }

            string[] musics = Enum.GetNames(typeof(MusicType));
            Array.Resize(ref _musicList, musics.Length);
            for (int i = 0; i < _musicList.Length; i++){
                _musicList[i]._name = musics[i];
            }
        }
            
        #endif
        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            if (_audioSystem is AudioSystem sys)
            {
                sys.OnMusicPlayRequested -= HandlePlayMusicRequested;
                sys.OnMusicStopRequested -= HandleStopMusicRequested;
                sys.OnSfxPlayRequested -= HandlePlaySfxRequested;
            }

            if (_instance == this) _instance = null;
        }


        public void PlayMusic(MusicType music, float volume = 1)
        {
            _audioSystem.RequestPlayMusic(music, volume);
        }

        public void StopMusic()
        {
            _audioSystem.RequestStopMusic();
        }

        public void PlaySfx(SfxType sfx, float volume = 1)
        {
            _audioSystem.RequestPlaySfx(sfx, volume);
        }

        private void HandlePlayMusicRequested(MusicType music, float volume)
        {
            var clips = _musicList[(int)music].Musics;
            if (clips == null || clips.Length == 0) return;
            var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            if (_musicSource != null) _musicSource.PlayOneShot(clip, volume);
        }

        private void HandleStopMusicRequested()
        {
            if (_musicSource != null) _musicSource.Stop();
        }

        private void HandlePlaySfxRequested(SfxType sfx, float volume)
        {
            var clips = _sfx_list_safe((int)sfx);
            if (clips == null || clips.Length == 0) return;
            var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            if (_sfxSource != null) _sfxSource.PlayOneShot(clip, volume);
        }

        private AudioClip[] _sfx_list_safe(int idx)
        {
            if (_sfxList == null || idx < 0 || idx >= _sfxList.Length) return null;
            return _sfxList[idx].Sfxs;
        }
    }

    [Serializable]
    public struct SfxList
    {
        public AudioClip[] Sfxs { get => _sfxs; }
        [HideInInspector] public string _name;
        [SerializeField] private AudioClip[] _sfxs;
    }

    [Serializable]
    public struct MusicList
    {
        public AudioClip[] Musics { get => _musics; }
        [HideInInspector] public string _name;
        [SerializeField] private AudioClip[] _musics;
    }
}

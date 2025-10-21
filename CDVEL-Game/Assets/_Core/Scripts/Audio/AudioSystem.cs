using System;

namespace Audio
{
    public class AudioSystem : IAudioSystem
    {
        public event Action<MusicType, float> OnMusicPlayRequested;
        public event Action OnMusicStopRequested;
        public event Action<SfxType, float> OnSfxPlayRequested;

        private MusicType? _currentMusic;
        public MusicType? CurrentMusic => _currentMusic;

        public void RequestPlayMusic(MusicType music, float volume = 1f)
        {
            _currentMusic = music;
            OnMusicPlayRequested?.Invoke(music, volume);
        }

        public void RequestStopMusic()
        {
            _currentMusic = null;
            OnMusicStopRequested?.Invoke();
        }

        public void RequestPlaySfx(SfxType sfx, float volume = 1f)
        {
            OnSfxPlayRequested?.Invoke(sfx, volume);
        }
    }
}

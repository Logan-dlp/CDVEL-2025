using System;

namespace Audio
{
    public interface IAudioSystem
    {
        public event Action<MusicType, float> OnMusicPlayRequested;
        public event Action OnMusicStopRequested;
        public event Action<SfxType, float> OnSfxPlayRequested;

        public MusicType? CurrentMusic { get; }
        public void RequestPlayMusic(MusicType music, float volume = 1f);
        public void RequestStopMusic();
        public void RequestPlaySfx(SfxType sfx, float volume = 1f);
    }
}

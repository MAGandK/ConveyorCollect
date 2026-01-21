using Audio.Types;

namespace Audio
{
    public interface IAudioManager
    {
        bool IsSoundMuted { get; }
        bool IsMusicMuted { get; }

        void Play(SoundType soundType, float volume = 1, float pitch = 1, bool loop = false);
        void Play(MusicType musicType, float volume = 1, float pitch = 1);
        void StopAllSound();

        void SetMuteSound(bool isMuted);
        void SetMuteMusic(bool isActiveState);
    }
}
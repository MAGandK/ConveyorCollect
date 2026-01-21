using Audio.Settings;
using UnityEngine.Audio;

namespace Audio
{
    public interface IAudioSettings
    {
        AudioPreset[] AudioPresets { get; }
        PooledAudio PooledAudioPrefab { get; }
        AudioMixer AudioMixer { get; }
    }
}
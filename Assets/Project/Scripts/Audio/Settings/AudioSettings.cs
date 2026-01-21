using UnityEngine;
using UnityEngine.Audio;

namespace Audio.Settings
{
    [CreateAssetMenu(menuName = "Sound/Create AudioSettings", fileName = "AudioSettings", order = 0)]
    public class AudioSettings : ScriptableObject, IAudioSettings
    {
        [SerializeField] private AudioPreset[] _audioPresets;
        [SerializeField] private PooledAudio _pooledAudioPrefab;
        [SerializeField] private AudioMixer _audioMixer;
        
        public AudioPreset[] AudioPresets => _audioPresets;
        public PooledAudio PooledAudioPrefab => _pooledAudioPrefab;
        public AudioMixer AudioMixer => _audioMixer;
    }
}
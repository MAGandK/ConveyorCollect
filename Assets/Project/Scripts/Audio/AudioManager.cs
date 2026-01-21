using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio.Settings;
using Audio.Types;
using Constants;
using Pool;
using Storage;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Audio
{
    public class AudioManager : IAudioManager, IInitializable
    {
        private readonly IPool _pool;
        private readonly AudioStorageData _audioStorageData;
        private readonly IAudioSettings _audioSettings;
        private readonly MonoBehaviour _monoBehaviour;

        private AudioMixerGroup _soundGroup;
        private AudioMixerGroup _musicGroup;

        private Dictionary<SoundType, SoundPreset> _soundMap;
        private Dictionary<MusicType, MusicPreset> _musicMap;


        private readonly Dictionary<SoundType, List<PooledAudio>> _pooledSoundMap = new();
        private readonly List<Coroutine> _stopSoundCoroutines = new();
        private KeyValuePair<MusicType, PooledAudio> _currentMusic;

        public bool IsSoundMuted => _audioStorageData.IsSoundMuted;
        public bool IsMusicMuted => _audioStorageData.IsMusicMuted;

        public AudioManager(IPool pool, IAudioSettings audioSettings, IStorageService storageService, MonoBehaviour monoBehaviour)
        {
            _audioSettings = audioSettings;
            _pool = pool;
            _monoBehaviour = monoBehaviour;
            _audioStorageData = storageService.GetData<AudioStorageData>(StorageDataNames.AUDIO_DATA_KEY);
        }

        public void Initialize()
        {
            _soundGroup = _audioSettings.AudioMixer.FindMatchingGroups("Master/Sound")[0];
            _musicGroup = _audioSettings.AudioMixer.FindMatchingGroups("Master/Music")[0];

            _soundMap = _audioSettings.AudioPresets.OfType<SoundPreset>().ToArray()
                .ToDictionary(x => x.SoundType, x => x);

            _musicMap = _audioSettings.AudioPresets.OfType<MusicPreset>().ToArray()
                .ToDictionary(x => x.MusicType, x => x);

            SetMuteMusic(_audioStorageData.IsMusicMuted);
            SetMuteSound(_audioStorageData.IsSoundMuted);
        }

        public void Play(SoundType soundType, float volume = 1, float pitch = 1, bool loop = false)
        {
            if (_audioStorageData.IsSoundMuted || !_soundMap.TryGetValue(soundType, out var soundPreset))
            {
                return;
            }

            var audioClip = soundPreset.AudioClip;

            var pooledAudio = Play(audioClip, _soundGroup, soundPreset.Volume == 0 ? volume : soundPreset.Volume, pitch);
            pooledAudio.SetIsLoop(loop);

            if (!_pooledSoundMap.TryAdd(soundType, new List<PooledAudio>() { pooledAudio }))
            {
                _pooledSoundMap[soundType].Add(pooledAudio);
            }

            if (loop)
            {
                var coroutine =
                    _monoBehaviour.StartCoroutine(ReturnToPoolCor(soundType, pooledAudio, audioClip.length));

                _stopSoundCoroutines.Add(coroutine);
            }
        }

        public void Play(MusicType musicType, float volume = 1, float pitch = 1)
        {
            if (!_musicMap.TryGetValue(musicType, out var musicPreset))
            {
                return;
            }

            var hasMusic = _currentMusic.Value != null;

            if (_currentMusic.Key == musicType && hasMusic)
            {
                return;
            }

            if (hasMusic)
            {
                _currentMusic.Value.SetIsFree(true);
                _currentMusic.Value.gameObject.SetActive(false);
            }

            var pooledAudio = Play(musicPreset.AudioClip, _musicGroup, musicPreset.Volume == 0 ? volume : musicPreset.Volume, pitch);
            pooledAudio.SetIsLoop(true);

            _currentMusic = new KeyValuePair<MusicType, PooledAudio>(musicType, pooledAudio);
        }

        public void StopAllSound()
        {
            foreach (var (key, pooledAudios) in _pooledSoundMap)
            {
                foreach (var pooledAudio in pooledAudios)
                {
                    pooledAudio.SetIsFree(true);
                    pooledAudio.gameObject.SetActive(false);
                }
            }

            _pooledSoundMap.Clear();

            foreach (var coroutine in _stopSoundCoroutines)
            {
                _monoBehaviour.StopCoroutine(coroutine);
            }

            _stopSoundCoroutines.Clear();
        }

        public void SetMuteSound(bool isMuted)
        {
            _audioStorageData.SetIsSoundMute(isMuted);

            _audioSettings.AudioMixer.SetFloat("SoundVolume", Mathf.Log10(isMuted ? 0.000001f : 1) * 20);
        }

        public void SetMuteMusic(bool isMuted)
        {
            _audioStorageData.SetIsMusicMute(isMuted);

            _audioSettings.AudioMixer.SetFloat("MusicVolume", Mathf.Log10(isMuted ? 0.000001f : 1) * 20);
        }

        private PooledAudio Play(
            AudioClip audioClip,
            AudioMixerGroup soundGroup,
            float volume = 1,
            float pitch = 1)
        {
            var poolData = new PoolData(_audioSettings.PooledAudioPrefab, "PooledAudio");
            var pooledAudio = _pool.Get<PooledAudio>(poolData);
            pooledAudio.gameObject.SetActive(true);
            pooledAudio.SetupAndPlay(audioClip, volume, pitch, soundGroup);

            return pooledAudio;
        }

        private IEnumerator ReturnToPoolCor(SoundType key, PooledAudio pooledAudio, float audioClipLength)
        {
            yield return new WaitForSeconds(audioClipLength);

            pooledAudio.SetIsFree(true);
            pooledAudio.gameObject.SetActive(false);
            _pooledSoundMap[key].Remove(pooledAudio);
        }
    }
}

using Pool;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class PooledAudio : MonoBehaviour, IPoolObject
    {
        [SerializeField] private AudioSource _source;

        private bool _isFree;
        public bool IsFree => _isFree;

        public void SetIsFree(bool isFree)
        {
            _isFree = isFree;
        }

        public void SetupAndPlay(AudioClip audioClip,
            float volume,
            float pitch,
            AudioMixerGroup mixerGroup)
        {
            _source.clip = audioClip;
            _source.volume = volume;
            _source.pitch = pitch;
            _source.outputAudioMixerGroup = mixerGroup;
            SetIsFree(false);
            
            _source.Play();
        }

        public void SetIsLoop(bool isLoop)
        {
            _source.loop = isLoop;
        }
    }
}
using Pool;
using UnityEngine;

namespace Audio
{
    public class Audio : MonoBehaviour, IPoolObject
    {
        [SerializeField] private AudioSource _audioSource;
        public bool _isFree;
        public bool IsFree => _isFree;
        public void SetIsFree(bool isFree)
        {
            _isFree = isFree;
            gameObject.SetActive(!_isFree);
        }

        public void Setup(AudioClip audioClip, float volume, float pitch)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;
        }
    }
}
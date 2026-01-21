using System;
using UnityEngine;

namespace Audio.Settings
{
    [Serializable]
    public class AudioPreset : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] [Range(0, 1)] private float _volume = 1;

        public AudioClip AudioClip => _audioClip;
        public float Volume => _volume;
    }
}
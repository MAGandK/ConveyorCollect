using UnityEngine;

namespace Particles
{
    [CreateAssetMenu(menuName = "PooledParticleSystem/Create ParticleSettings", fileName = "ParticleSettings", order = 0)]
    public class ParticleSettings : ScriptableObject, IParticleSettings
    {
        [SerializeField] private ParticlePreset[] _particlePresets;

        public ParticlePreset[] ParticlePresets => _particlePresets;

    }
}

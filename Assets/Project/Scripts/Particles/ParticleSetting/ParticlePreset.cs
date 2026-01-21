using UnityEngine;

namespace Particles
{
    [CreateAssetMenu(menuName = "PooledParticleSystem/Preset/Create ParticlePreset", fileName = "ParticlePreset", order = 0)]
    public class ParticlePreset : ScriptableObject
    {
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private PooledParticle _pooledParticle;
        [SerializeField] private float _duration = 5f;
        [SerializeField] private Vector3 _scale = Vector3.one;

        public PooledParticle PooledParticle => _pooledParticle;
        public ParticleType ParticleType => _particleType;
        public float Duration => _duration;
        public Vector3 Scale => _scale;
    }
}

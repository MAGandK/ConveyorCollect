using Pool;
using UnityEngine;

namespace Particles
{
    public class PooledParticle : MonoBehaviour, IPoolObject
    {
        [SerializeField] private ParticleSystem _particleSystem;
        public ParticleSystem ParticleSystem => _particleSystem;
        public bool IsFree => _isFree;

        private bool _isFree;

        public void SetIsFree(bool isFree)
        {
            _isFree = isFree;
        }

        public void SetupAndPlay(Vector3 position, Vector3 scale)
        {
            transform.position = position;
            transform.localScale = scale;

            _particleSystem.Play();
            SetIsFree(false);
        }

        public void Stop()
        {
            _particleSystem.Stop();
            SetIsFree(true);
        }
    }
}

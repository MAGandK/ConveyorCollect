using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pool;
using UnityEngine;
using Zenject;

namespace Particles
{
    public class ParticleManager : IParticleManager, IInitializable
    {
        private readonly IPool _pool;
        private readonly IParticleSettings _particleSettings;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly List<PooledParticle> _activeParticles = new();

        public ParticleManager(IPool pool, IParticleSettings particleSettings, MonoBehaviour monoBehaviour)
        {
            _pool = pool;
            _particleSettings = particleSettings;
            _monoBehaviour = monoBehaviour;
        }

        public void Initialize()
        {
        }

        public void Play(ParticleType particleType, Vector3 position)
        {
            var particlePreset = _particleSettings.ParticlePresets.FirstOrDefault(x => x.ParticleType == particleType);

            if (particlePreset == null)
            {
                return;
            }

            var poolData = new PoolData(particlePreset.PooledParticle, "PooledParticle");
            var pooledParticle = _pool.Get<PooledParticle>(poolData);

            pooledParticle.transform.position = position;
            pooledParticle.gameObject.SetActive(true);
            pooledParticle.SetupAndPlay(position, particlePreset.Scale);

            _activeParticles.Add(pooledParticle);

            _monoBehaviour.StartCoroutine(ReturnToPoolCor(pooledParticle, particlePreset.Duration));
        }

        public void ReturnAllParticle()
        {
            foreach (var particle in _activeParticles)
            {
                particle.Stop();
                particle.gameObject.SetActive(false);
                particle.SetIsFree(true);
            }

            _activeParticles.Clear();
        }

        private IEnumerator ReturnToPoolCor(PooledParticle pooledParticle, float particlePresetDuration)
        {
            var particleSystem = pooledParticle.ParticleSystem;

            if (particlePresetDuration > 0)
            {
                yield return new WaitForSeconds(particlePresetDuration);
            }
            else
            {
                yield return new WaitForSeconds(particleSystem.main.duration +
                                                particleSystem.main.startLifetime.constant);
            }

            pooledParticle.Stop();
            pooledParticle.gameObject.SetActive(false);
            pooledParticle.SetIsFree(true);

            _activeParticles.Remove(pooledParticle);
        }
    }
}
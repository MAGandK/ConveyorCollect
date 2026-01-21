using UnityEngine;

namespace Particles
{
    public interface IParticleManager
    {
        void Play(ParticleType particleType, Vector3 position);
        void ReturnAllParticle();
    }
}
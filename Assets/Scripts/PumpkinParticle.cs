using UnityEngine;

public class PumpkinParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem PumpkinParticles;

    public void StartLeafExplosion()
    {
        PumpkinParticles.Play();
    }
}

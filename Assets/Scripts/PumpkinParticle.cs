using UnityEngine;

public class PumpkinParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem PumpkinParticles;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (animator != null)
        {
            
        }
    }

    public void StartLeafExplosion()
    {
        PumpkinParticles.Play();
    }
}

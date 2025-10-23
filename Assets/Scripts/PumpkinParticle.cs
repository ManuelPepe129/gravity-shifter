using UnityEngine;

public class PumpkinParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem PumpkinParticles;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (animator != null)
        {
            animator.SetBool("Squash", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (animator != null)
        {
            animator.SetBool("Squash", false);
        }
    }

    public void StartLeafExplosion()
    {
        PumpkinParticles.Play();
    }
}

using UnityEngine;

public class PumpkinParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem PumpkinParticles;

    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (animator != null)
        {
            animator.SetBool("Squash", true);
            audioSource.Play();
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

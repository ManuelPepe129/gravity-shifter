using System.Collections;
using UnityEngine;

public class LeverActivation : MonoBehaviour
{
    [SerializeField] GameObject particles;

    SceneManager sceneManager;
    Animator animator;

    private void Awake()
    {
        sceneManager = FindAnyObjectByType<SceneManager>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (sceneManager != null)
        {
            animator.SetTrigger("Activate");
            particles.SetActive(false);
            StartCoroutine(WaitForAnimation("LeverMovement"));
        }
    }

    IEnumerator WaitForAnimation(string stateName)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        while (!info.IsName(stateName))
        {
            yield return null;
            info = animator.GetCurrentAnimatorStateInfo(0);
        }

        // attende che finisca
        while (info.normalizedTime < 1.0f)
        {
            yield return null;
            info = animator.GetCurrentAnimatorStateInfo(0);
        }

        Debug.Log($"{stateName} completata!");
        sceneManager.OnLeverActivated();
    }

}

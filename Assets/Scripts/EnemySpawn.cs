using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject ghostRoot;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speedMultiplier = 1.0f;

    GameObject ghost;
    GameObject particles;

    private void Awake()
    {
        ghost = ghostRoot.GetComponentInChildren<MeshRenderer>(true).gameObject;
        particles = ghostRoot.GetComponentInChildren<ParticleSystem>(true).gameObject;
    }

    /// <summary>
    /// When the player collides with the trigger
    /// collider of the grave, the ghost is spawned
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TriggerSequence());
        }
    }

    /// <summary>
    /// 1. Activate particles
    /// 2. Activate ghost
    /// 3. Start moving
    /// </summary>
    /// <returns></returns>
    private IEnumerator TriggerSequence()
    {
        particles.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        ghost.SetActive(true);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(MoveEnemy(startingPoint, endPoint));
    }

    private IEnumerator MoveEnemy(Transform from, Transform to)
    {
        float pathLength = (from.position - to.position).magnitude;
        float movementDuration = (pathLength / 2.0f) / speedMultiplier;
        float elapsedTime = 0f;
        float alpha = 0f;

        while (elapsedTime < movementDuration)
        {
            Vector3 currentPosition = Vector3.Lerp(from.position, to.position, alpha);
            ghostRoot.transform.position = currentPosition;
            alpha += Time.deltaTime / movementDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Final destination + switch rotation
        ghostRoot.transform.position = to.position;
        ghostRoot.transform.Rotate(new Vector3(0, 1, 0), 180);

        // Invert from and to
        from = (from == startingPoint) ? endPoint : startingPoint;
        to = (to == startingPoint) ? endPoint : startingPoint;
        StartCoroutine(MoveEnemy(from, to));
    }
}

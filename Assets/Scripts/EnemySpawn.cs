using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject ghost;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speedMultiplier = 1.0f;

    /// <summary>
    /// When the player collides with the trigger
    /// collider of the grave, the ghost is spawned
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ghost.SetActive(true);
            StartCoroutine(MoveEnemy(startingPoint, endPoint));
        }
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
            ghost.transform.position = currentPosition;
            alpha += Time.deltaTime / movementDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Final destination + switch rotation
        ghost.transform.position = to.position;
        ghost.transform.Rotate(new Vector3(0, 1, 0), 180);

        // Invert from and to
        from = (from == startingPoint) ? endPoint : startingPoint;
        to = (to == startingPoint) ? endPoint : startingPoint;
        StartCoroutine(MoveEnemy(from, to));
    }
}

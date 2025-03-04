using System.Collections;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{
    public float damage = 25f;
    public float activationDelay = 0f;
    public float activeTime = 1.0f;
    public float resetTime = 2.0f;

    private bool isActive = false;
    private Vector3 originalPosition;
    private Vector3 activatedPosition;

    public Transform spikesTransform;

    private void Start()
    {
        if (spikesTransform == null) spikesTransform = transform;
        originalPosition = spikesTransform.localPosition;
        activatedPosition = originalPosition + new Vector3(0, 0.9f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.CompareTag("Player"))
        {
            StartCoroutine(ActivateTrap(other));
        }
    }

    private IEnumerator ActivateTrap(Collider target)
    {
        isActive = true;
        yield return new WaitForSeconds(activationDelay);

        spikesTransform.localPosition = activatedPosition;

        PlayerStatus targetHealth = target.GetComponent<PlayerStatus>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage((int)damage);
        }

        yield return new WaitForSeconds(activeTime);

        spikesTransform.localPosition = originalPosition;

        yield return new WaitForSeconds(resetTime);
        isActive = false;
    }
}

using System.Collections;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{
    public int damage = 25;
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


    private IEnumerator ActivateTrap()
    {
        isActive = true;
        yield return new WaitForSeconds(activationDelay);

        spikesTransform.localPosition = activatedPosition;

        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, 0.5f, LayerMask.GetMask("Player"));
        foreach (Collider player in hitPlayers)
        {
            Player targetHealth = player.GetComponent<Player>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(activeTime);

        spikesTransform.localPosition = originalPosition;

        yield return new WaitForSeconds(resetTime);
        isActive = false;
    }


    public void TriggerTrap()
    {
        if (!isActive)
        {
            Debug.Log("Kích hoạt bẫy!");
            StartCoroutine(ActivateTrap());
        }
    }
}

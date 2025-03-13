using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFunction : MonoBehaviour
{
    [Header("Looking")]
    public float interactionRange = 3f;
    public LayerMask interactableLayer;
    private IInteractable currentInteractable;


    // Start is called before the first frame update

    public GameObject skillAoeFx;
    public LayerMask minionsLayer;
    private float attackRange = 5f;
    private float attackPlayer;
    private Player player;
    private Animator animator;
    private int isAttacking;
    public event Action<Player, GameObject, float> OnAttack;

    private void Start()
    {
        if (player == null)
        {
            player = GetComponent<Player>();
        }
        attackPlayer = player.attackDamage;
        animator = GetComponent<Animator>();
        isAttacking = Animator.StringToHash("isAttacking");
    }


    // Update is called once per frame
    void Update()
    {
        CameraLooking();
        if (Input.GetMouseButtonDown(0))
        {
            IsAttack();
        }
    }

    void CameraLooking()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 rayOrigin = transform.position + Vector3.up;
            Vector3 rayDirection = transform.forward;
            Ray ray = new Ray(rayOrigin, rayDirection);

            RaycastHit[] hits = Physics.RaycastAll(ray, interactionRange, interactableLayer);
            Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red, 2f);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Player")) continue; // Bỏ qua Player

                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    Debug.Log("Tìm thấy vật thể có thể tương tác: " + hit.collider.name);
                    currentInteractable = interactable;
                    currentInteractable.Interact();
                    return;
                }
            }

            Debug.LogWarning("Không tìm thấy vật thể nào có thể tương tác!");
            currentInteractable = null;
        }
    }

        void IsAttack()
        {
        animator.SetTrigger(isAttacking);
        Collider[] hitMinions = Physics.OverlapSphere(transform.position, attackRange, minionsLayer);

        if (hitMinions.Length > 0)
            {
            foreach (Collider minion in hitMinions)
            {
                Minions minions = minion.GetComponent<Minions>();
                if (minions != null)
                {
                    OnAttack?.Invoke(player, minion.gameObject, attackPlayer);
                    Debug.Log("Hit " + minion.name + " with " + attackPlayer + " damage.");
                }
            }
                }
        else
        {
            Debug.Log("No minions in range, attack missed.");
            }
        }
}

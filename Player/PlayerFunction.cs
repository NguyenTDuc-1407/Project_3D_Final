using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFunction : MonoBehaviour
{
    [Header("Looking")]
    public float lookDistance = 3f;
    public LayerMask interactableLayer;


    // Start is called before the first frame update
   
    public GameObject skillAoeFx;
    public LayerMask minionsLayer;
    private float attackRange = 2f;
    public float attackPlayer;
    private Player player;
    private Animator animator;
    private int isAttacking;
    public event Action<Player, GameObject, float> OnAttack;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
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
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray look = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(look, out hit, lookDistance, interactableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Debug.Log("Tuong Tac" + hit.collider.gameObject.name);
            }
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
                OnAttack?.Invoke(player, minion.gameObject, attackPlayer);
                Debug.Log("Hit " + minion.name + " with " + attackPlayer + " damage.");
            }
                }
        else
        {
            Debug.Log("No minions in range, attack missed.");
            }
    }

}

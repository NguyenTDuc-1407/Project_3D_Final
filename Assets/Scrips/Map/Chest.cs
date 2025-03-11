using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Chest : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public bool isOpen = false;
    private Animator animator;
    public ItemConfig[] possibleItems;
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator chưa được gán trên " + gameObject.name);
        }
    }

    public void Interact()
    {
        if (!isOpen)
        {
            animator.SetTrigger("chestOpen");
            isOpen = true;
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (possibleItems.Length == 0)
        {
            return;
        }
        ItemConfig randomDrop = possibleItems[Random.Range(0, possibleItems.Length)];
    }
}

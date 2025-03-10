using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    private Animator animator;

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
            animator.SetTrigger("doorOpen");
            isOpen = true;
            Debug.Log("Cửa đã mở!");
        }
        else
        {
            Debug.Log("Cửa đã mở rồi!");
        }
    }
}


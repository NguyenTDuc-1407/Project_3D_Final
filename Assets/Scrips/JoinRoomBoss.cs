using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class JoinRoomBoss : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene("BossMage");
    }
}

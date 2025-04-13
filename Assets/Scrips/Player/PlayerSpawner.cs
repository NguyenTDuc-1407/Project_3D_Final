using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");

        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Player hoặc PlayerSpawnPoint!");
        }
    }
}

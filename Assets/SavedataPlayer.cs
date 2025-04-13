using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedataPlayer : MonoBehaviour
{
    public static SavedataPlayer instance;
    public Player player;
    [Header("Các scene KHÔNG cần Player")]
    public string[] excludedScenes;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (player == null)
        {
            player = gameObject.AddComponent<Player>();
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += CheckIfShouldDestroy;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckIfShouldDestroy;
    }

    void CheckIfShouldDestroy(Scene scene, LoadSceneMode mode)
    {
        foreach (string excluded in excludedScenes)
        {
            if (scene.name == excluded)
            {
                Destroy(gameObject);
                instance = null;
                break;
            }
        }
    }
}

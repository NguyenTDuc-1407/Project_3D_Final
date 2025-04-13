using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObjectsManager : MonoBehaviour
{
    public List<GameObject> objectsToKeep;
    public List<string> excludedScenes;
    void Awake()
    {
        foreach (var obj in objectsToKeep)
        {
            DontDestroyOnLoad(obj);
        }

        DontDestroyOnLoad(gameObject);
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
                
                break;
            }
        }
    }
}

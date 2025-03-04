using UnityEngine;
using UnityEngine.UIElements;
public class PlayerFunction : MonoBehaviour
{
    [Header("Looking")]
    public float lookDistance = 3f;
    public LayerMask interactableLayer;


    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        CameraLooking();
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
                
            }
        }

    }
  
}
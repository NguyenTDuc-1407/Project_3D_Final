using UnityEngine;
using UnityEngine.EventSystems;

public class CursorLockManager : MonoBehaviour
{
    void Update()
    {
        if (IsPointerOverUIObject())
        {
            Cursor.lockState = CursorLockMode.None; // Không khóa chuột
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Khóa chuột
            Cursor.visible = false;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}

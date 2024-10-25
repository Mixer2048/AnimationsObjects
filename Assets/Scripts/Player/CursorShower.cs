using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    [SerializeField] private bool cursorLock = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = !cursorLock;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

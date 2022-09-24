using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private bool isOpened = false;
    public GameObject UIPanel;
    void Start()
    {
        UIPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isOpened = !isOpened;
            bool state = isOpened;
            UIPanel.SetActive(state);
            CursorChangeState(state);
            CharacterMoving.isMove = !state;
            CameraController.isMove = !state;
        }

    }
    private void CursorChangeState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
}

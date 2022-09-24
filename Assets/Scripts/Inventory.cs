using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
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
            UIPanel.SetActive(isOpened);
            CursorChangeState(isOpened);

            isOpened = !isOpened;
        }
        void CursorChangeState(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
        }
    }
}

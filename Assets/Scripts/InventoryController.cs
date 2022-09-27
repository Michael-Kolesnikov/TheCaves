using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryController : MonoBehaviour
{
    private bool isOpened = false;
    public GameObject UIPanel;
    public List<InventorySlot> itemList = new List<InventorySlot>();
    Camera mainCamera;
    float reachDistance = 3f;


    public GameObject spawn;
    void Start()
    {
        mainCamera = Camera.main;
        UIPanel.SetActive(false);
    }

    void Update()
    {
        Cursor.visible = true;
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpened = !isOpened;
            bool state = isOpened;
            UIPanel.SetActive(state);
            CursorChangeState(state);
            CharacterMoving.isMove = !state;
            CameraController.isMove = !state;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


        // уничтожение подобранных предметов
        RaycastHit hit;
        if (Input.GetKey(KeyCode.E) )
        {
            Physics.Raycast(ray, out hit, reachDistance);
            if (hit.collider?.gameObject.GetComponent<Item>() != null)
                Destroy(hit.collider.gameObject);
        }

    }
    private void CursorChangeState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }
}

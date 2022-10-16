using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public Inventory inventory { get; private set; }
    Camera mainCamera;
    float reachDistance = 3f;
    public GameObject UIInventoryPanel;
    public Transform InventoryPanel;
    public Transform canvas;
    private bool isOpened = false;
    private List<UIInventorySlot> uiInventorySlot;
    private void Awake()
    {
        
    }
    private void Start()
    {
        inventory = new Inventory(24);
        inventory.OnInventoryStateChangeEvent += OnInventoryStateChange;
        uiInventorySlot = new List<UIInventorySlot>();

        mainCamera = Camera.main;
        UIInventoryPanel.SetActive(false);
        for(var i = 0; i < InventoryPanel.childCount;i++)
        {
            uiInventorySlot.Add(InventoryPanel.GetChild(i).GetComponent<UIInventorySlot>());
        }
        SetupInventoryUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpened = !isOpened;
            bool state = isOpened;
            SetActiveHudElements(state);
            CharacterMoving.isMove = !state;
            CameraController.isMove = !state;
        }
        /// Pick up item 
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider?.gameObject.GetComponent<InventoryItem>() != null)
                {
                    var item = hit.collider.gameObject.GetComponent<InventoryItem>();
                    inventory.TryToAddItem(item);
                    //RefreshHotBatUI();
                    Destroy(hit.collider.gameObject);
                    //
                }
            }
        }
    }
    private void SetActiveHudElements(bool state)
    {
        for (var i = 0; i < canvas.childCount; i++)
        {
            if (canvas.GetChild(i).gameObject != UIInventoryPanel.gameObject)
            {
                if (canvas.GetChild(i).GetComponent<Image>() != null)
                    canvas.GetChild(i).GetComponent<Image>()?.gameObject.SetActive(!state);
            }
            else
                canvas.GetChild(i).GetComponent<Image>()?.gameObject.SetActive(state);
        }
        CursorChangeState(state);
    }
    private void CursorChangeState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
    private void OnInventoryStateChange()
    {
        foreach (var slot in uiInventorySlot)
            slot.Refresh();
    }
    private void SetupInventoryUI()
    {
        for(var i = 0; i< inventory.capacity; i++)
        {
            var slot = inventory.slots[i];
            var uiSlot = uiInventorySlot[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }
}

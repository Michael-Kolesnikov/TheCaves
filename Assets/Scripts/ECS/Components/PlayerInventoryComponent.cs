using System;
using UnityEngine;

[Serializable]
public struct PlayerInventoryComponent
{
    public Transform playerInventoryUIPanel;
    [HideInInspector]
    public Inventory inventory;
    [HideInInspector]
    public bool isInventoryOppened;
    [HideInInspector]
    public UIInventory UIInventory;
}

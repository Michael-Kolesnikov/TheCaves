using UnityEngine;
using System;

[Serializable]
public struct PlayerInventoryComponent
{
    // Canvas savign
    public Transform playerInventoryUIPanel;
    [HideInInspector]
    public Inventory inventory;
    [HideInInspector]
    public bool isInventoryOppened;
}

using UnityEngine;
using System;

[Serializable]
public struct PlayerInventoryComponent
{
    public Transform playerInventoryUIPanel;
    [HideInInspector]
    public Inventory inventory;
    [HideInInspector]
    public bool isInventoryOppened;
}

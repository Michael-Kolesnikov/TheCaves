using System;
using UnityEngine;

[Serializable]
public struct PlayerHotBarComponent
{
    /// <summary>
    /// Have a child component as HotBarSlot
    /// </summary>
    public Transform hudBarCanvas;
    public int activeSlotIndex;
}

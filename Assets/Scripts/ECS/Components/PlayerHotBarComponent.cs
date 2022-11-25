using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct PlayerHotBarComponent
{
    /// <summary>
    /// Have a child component as HotBarSlot
    /// </summary>
    public Transform hudBarCanvas;
    public int activeSlotIndex;
}

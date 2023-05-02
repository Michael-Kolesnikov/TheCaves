using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvenManager : MonoBehaviour
{

    public static Action OnInventoryStateChange;

    public static void RefreshHotBar()
    {
        OnInventoryStateChange?.Invoke();
    }
}

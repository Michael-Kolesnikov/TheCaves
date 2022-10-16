using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    public Image imageIcon;
    public TMP_Text textAmount;

    InventoryItem inventoryItem;
    
    public void Refresh()
    {

    }
    public void Clean()
    {
        imageIcon = default;
        textAmount.text = default;
    }
}

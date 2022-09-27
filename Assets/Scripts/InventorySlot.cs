
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isEmpty = true;
    public ItemScriptableObject currentItem;
    public int amount;
    public Sprite icon;
    public Sprite defaultIcon;
    public TMP_Text itemAmountText;
    private void Start()
    {

        GetComponent<Image>().sprite = defaultIcon;
    }
    public void SetIcon(Sprite spriteIcon)
    {
        icon = spriteIcon;
        GetComponent<Image>().sprite = spriteIcon;
    }
     

}

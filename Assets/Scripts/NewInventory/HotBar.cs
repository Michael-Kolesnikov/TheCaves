using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    public Transform activeSlot;
    public Transform character;
    private Inventory inventory;
    private UIInventory uiInventory;
    public Sprite activeHotBarIcon;
    public Sprite hotbarIcon;
    public Transform hand;

    private int activeSlotIndex = 0;
    private void Start()
    {
        inventory = character.GetComponent<PickUpItems>().inventory;
        uiInventory = character.GetComponent<PickUpItems>().uiInventory;
        activeSlot = this.transform.GetChild(activeSlotIndex);
        GlobalEvenManager.OnInventoryStateChange += UpdateHotbar;
        GlobalEvenManager.OnInventoryStateChange += ChangeActiveItem;
        for (var i = 0; i < this.transform.childCount; i++)
        {
            var itemImage = this.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            itemImage.enabled = false;
        }
    }
    private void Update()
    {
        var isChange = false;
        activeSlot.GetComponent<Image>().sprite = hotbarIcon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            activeSlotIndex++;
            isChange = true;
        }
        //Mouse ScroolWheel Down
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            activeSlotIndex--;
            isChange = true;
        }

        //constantly scroll through
        if (activeSlotIndex >= this.transform.childCount)
            activeSlotIndex = 0;
        else if (activeSlotIndex < 0)
            activeSlotIndex = this.transform.childCount - 1;

        activeSlot = this.transform.GetChild(activeSlotIndex);
        activeSlot.GetComponent<Image>().sprite = activeHotBarIcon;
        if (isChange)
        {
            ChangeActiveItem();
        }
    }
    private void UpdateHotbar()
    {
        for (var i = 0; i < this.transform.childCount; i++)
        {
            var slotItem = this.transform.GetChild(i);
            var itemImage = slotItem.GetChild(0).GetComponent<Image>();
            if (inventory.slots[i].item == null)
            {
                itemImage.enabled = false;
                itemImage.sprite = null;
                slotItem.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
            }
            else
            {
                itemImage.enabled = true;
                itemImage.sprite = inventory.slots[i].item.spriteIcon;

                if (inventory.slots[i].amount != 1)
                {
                    slotItem.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = inventory.slots[i].amount.ToString();
                }
                else
                {
                    slotItem.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = string.Empty;

                }
            }
        }

    }
    private void ChangeActiveItem()
    {
        if (hand.transform.childCount > 0)
        {
            Destroy(hand.transform.GetChild(0).gameObject);
        }
        if (inventory.slots[activeSlotIndex].item != null)
        {
            var createdThing = Instantiate(inventory.slots[activeSlotIndex].item.prefab, hand.position, Quaternion.identity, hand);
            Destroy(createdThing.GetComponent<InventoryItem>());
            Destroy(createdThing.GetComponent<Rigidbody>());

        }
    }
}

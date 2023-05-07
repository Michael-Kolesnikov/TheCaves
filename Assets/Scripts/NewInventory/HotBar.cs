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
    bool isPlaceableItem = false;
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


        if (isPlaceableItem)
        {

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
                Debug.Log("Update hotbar");
                itemImage.enabled = true;
                itemImage.sprite = inventory.slots[i].item.spriteIcon;
                if (itemImage.GetComponent<InventoryItem>() == null)
                {
                    Debug.Log("Instanti invent item component");
                    itemImage.gameObject.AddComponent<InventoryItem>().itemScriptableObject = inventory.slots[i].item;
                    itemImage.gameObject.GetComponent<InventoryItem>().amount = inventory.slots[i].amount;
                }
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
        isPlaceableItem = false;

    }
    private void ChangeActiveItem()
    {
        if (hand.transform.childCount > 0)
        {
            for(var i = 0; i < hand.childCount; i++)
            {
                Destroy(hand.transform.GetChild(i).gameObject);
            }
        }
        var slot = inventory.slots[activeSlotIndex];
        if (slot.item != null && slot.item.prefab != null)
        {
            var createdThing = Instantiate(slot.item.prefab, hand.position, Quaternion.identity, hand);
            createdThing.transform.localEulerAngles = slot.item.prefab.transform.localEulerAngles;
            createdThing.transform.localPosition = slot.item.previewPosition;

            if (createdThing.GetComponent<InventoryItem>().itemScriptableObject.itemType == ItemType.Construction)
            {
                isPlaceableItem = true;
                Instantiate(((ConstructionItem)createdThing.GetComponent<InventoryItem>().itemScriptableObject).previewPrefab);
            }
            Destroy(createdThing.GetComponent<InventoryItem>());
            Destroy(createdThing.GetComponent<Rigidbody>());

        }
    }
}

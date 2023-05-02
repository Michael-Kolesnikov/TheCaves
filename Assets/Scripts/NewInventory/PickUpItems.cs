using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public Inventory inventory { get; private set; }
    public UIInventory uiInventory { get; private set; }
    public Transform inventoryPanel;
    public Transform invItemPrefab;
    private void Start()
    {
        inventory = new Inventory(28);
        uiInventory = new UIInventory(inventory, inventoryPanel, invItemPrefab);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {

                if (hit.collider.gameObject.GetComponent<InventoryItem>() != null)
                {
                    var added = inventory.TryToAddItem(hit.collider.gameObject.GetComponent<InventoryItem>());
                    if (added)
                    {
                        uiInventory.Refresh();
                        Destroy(hit.collider.gameObject);
                    }
                    Debug.Log(added);
                }
            }
        }
    }
}

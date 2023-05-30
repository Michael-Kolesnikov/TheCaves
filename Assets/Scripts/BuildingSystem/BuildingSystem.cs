using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject currentObject;
    public Transform hotbarPanel;
    public Transform activeSlot;
    public LayerMask layer;
    public RaycastHit hit;
    public bool isBuild = true;
    public Transform character;
    private Vector3 currentPos;
    private float gridSize = 1.0f;
    private float offset = 1.0f;
    private HotBar hotbar;

    public bool isBuildeable;

    private void Start()
    {
        GlobalEvenManager.OnHotbarItemChange += InstantiatePreviewObject;
        hotbar = hotbarPanel.GetComponent<HotBar>();
    }
    private void Update()
    {

        if (isBuild)
        {
            if (hotbar.transform.GetChild(hotbar.activeSlotIndex).GetChild(0).GetComponent<InventoryItem>() == null)
            {
                Destroy(currentObject);
                return;
            }
            if (currentObject != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentObject.transform.Rotate(new Vector3(0, 90, 0));
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    currentObject.transform.Rotate(new Vector3(0, -90, 0));
                }
            }
            ShowPreview();
            if (Input.GetMouseButtonDown(1) && currentObject != null && currentObject.GetComponent<PreviewObject>().isBuildeable)
            {
                var prefab = hotbar.activeSlot.GetChild(0).GetComponent<InventoryItem>()?.itemScriptableObject.prefab;
                Instantiate(prefab, currentObject.transform.position, Quaternion.Euler(currentObject.transform.eulerAngles));
                character.GetComponent<PickUpItems>().inventory.TryToRemove(new InventoryItem(hotbar.activeSlot.GetChild(0).GetComponent<InventoryItem>()?.itemScriptableObject), 1);
                InstantiatePreviewObject();
            }
        }
    }

    private void ShowPreview()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10, layer))
        {
            if (hit.transform != this.transform)
            {
                currentPos = hit.point;
                currentPos -= Vector3.one * offset;
                currentPos /= gridSize;
                currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
                currentPos *= gridSize;
                currentPos += Vector3.one * offset;
                currentPos.y = hit.point.y;
                if (currentObject != null)
                {
                    currentObject.transform.position = currentPos;
                }
            }
        }
    }

    private void InstantiatePreviewObject()
    {
        Destroy(currentObject);
        var inventoryItem = hotbar.transform.GetChild(hotbar.activeSlotIndex).GetChild(0).GetComponent<InventoryItem>();
        if (inventoryItem == null || inventoryItem.itemScriptableObject.itemType != ItemType.Construction)
        {
            return;
        }
        var prefab = ((ConstructionItem)hotbar.transform.GetChild(hotbar.activeSlotIndex).GetChild(0).GetComponent<InventoryItem>()?.itemScriptableObject).previewPrefab;
        currentObject = Instantiate(prefab, currentPos, Quaternion.Euler(prefab.transform.eulerAngles));
    }
}

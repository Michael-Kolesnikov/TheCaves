using System;
using System.Linq.Expressions;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public Transform hotbar;
    public Transform activeSlot;
    public Transform curPreviewObject;
    private void Start()
    {

    }
    bool flag = false;
    private void Update()
    {
        if (!flag)
        {
            activeSlot = hotbar.GetComponent<HotBar>().activeSlot;
            var item = activeSlot.GetChild(0).gameObject.GetComponent<InventoryItem>();
            if (item != null && item.itemScriptableObject.itemType == ItemType.Construction)
            {
                Debug.Log("Spawned");
                var previewObject = ((ConstructionItem)activeSlot.GetChild(0).GetComponent<InventoryItem>().itemScriptableObject).previewPrefab;
                GameObject curPreviewObj = Instantiate(previewObject, Vector3.zero, Quaternion.identity) as GameObject;
                curPreviewObject = curPreviewObj.transform;
                StartPreview();
            }
        }

    }
    public LayerMask layer;
    public void StartPreview()
    {
        flag = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit, 100, layer))
        {
            if(hit.transform != this.transform)
            {
                ShowPreview(hit);
            }
        }
    }

    private void ShowPreview(RaycastHit hit)
    {
        var currentPos = hit.point;
        currentPos = new Vector3(Mathf.Round(currentPos.x),Mathf.Round(currentPos.y),Mathf.Round(currentPos.z));
        curPreviewObject.position = currentPos;
    }
}

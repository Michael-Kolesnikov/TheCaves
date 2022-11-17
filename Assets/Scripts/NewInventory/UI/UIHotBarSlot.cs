using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHotBarSlot : MonoBehaviour
{
    public int index { get; private set; }
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _textAmount;
    [SerializeField] private Transform _activeBorder;
    public InventorySlot inventorySlot;
    GameObject createdObject;
    public GameObject currentItemInHand;
    public void SetInventorySlot(InventorySlot inventorySlot)
    {
        this.inventorySlot = inventorySlot;
    }
    public void Refresh()
    {
        _imageIcon.sprite = inventorySlot?.item?.spriteIcon;
        _textAmount.text = inventorySlot.amount == 0 ? "" : inventorySlot.amount.ToString();
    }
    public void SelectSlot()
    {
        _activeBorder.gameObject.SetActive(true);
    }
    public void UnSelect()
    {
        _activeBorder.gameObject.SetActive(false);
        DestroyPrefab();
    }
    public void SpawnPrefab(Transform player)
    {
        if (inventorySlot?.item?.prefab != null)
        {
            currentItemInHand = inventorySlot.item.prefab.gameObject;
            currentItemInHand.GetComponent<Rigidbody>().isKinematic = true;
            currentItemInHand.transform.parent = player;
            currentItemInHand.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    public void DestroyPrefab()
    {
        Destroy(createdObject);
    }
    private void Start()
    {
        var player = GetComponentInParent<Player>();
    }
}

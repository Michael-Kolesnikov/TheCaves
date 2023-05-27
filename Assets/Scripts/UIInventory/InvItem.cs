using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Image image;
    [HideInInspector] public Transform parentAfterDrag; 
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    } 
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Transform originParent;
    public Transform OriginParent => originParent;
    private Slot slot;
    private GameObject targetParent;
    private RectTransform rect;
    void Start()
    {
        slot = transform.parent.GetComponent<Slot>();
        rect = transform.GetComponent<RectTransform>();
        targetParent = GameObject.FindWithTag("UiCanvas");
        originParent = transform.parent;
    }


    public void OnDrag(PointerEventData _eventData)
    {
        if(slot.Data != null)
        {
            Vector2 screenPoint = _eventData.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetParent.transform as RectTransform, screenPoint, targetParent.GetComponent<Canvas>().worldCamera, out Vector2 localPoint);

            // 변환된 좌표를 RectTransform에 할당
            rect.anchoredPosition = localPoint;
        }

    }

    public void OnBeginDrag(PointerEventData _eventData)
    {

        transform.SetParent(targetParent.transform);
        GetComponent<Image>().raycastTarget = false;
    }
    public void OnEndDrag(PointerEventData _eventData)
    {
        
        transform.SetParent(originParent);
        transform.localPosition = Vector3.zero;
        GetComponent<Image>().raycastTarget = true;
    }

    public void OnDrop(PointerEventData _eventData)
    {
        
        Slot _slot = _eventData.pointerDrag.transform.GetComponent<ItemDragHandler>().OriginParent.GetComponent<Slot>();
        if(_slot != null)
        {
            if (slot.Data != null)
            {
                ItemData _data = slot.Data;
                slot.Data = _slot.Data;
                _slot.Data = _data;
            }
            else
            {

                slot.Data = _slot.Data;
                _slot.Data = null;

            }
        }

    }
}

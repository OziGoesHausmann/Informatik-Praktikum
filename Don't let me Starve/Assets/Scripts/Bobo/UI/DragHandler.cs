using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IDragHandler, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler //,IPointerClickHandler
{
    public GameObject cursorSlotPrefab;
    GameObject cursorSlotInstance;
    CursorSlot cursorSlot;
    public InventorySlot inventorySlot;

    Button button;

    Canvas canvas;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        button = GetComponent<Button>();
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (!button.interactable || inventorySlot.isNotUsable)
        {
            return;
        }

        cursorSlotInstance = Instantiate(cursorSlotPrefab);

        cursorSlotInstance.transform.SetParent(canvas.transform, false);
        cursorSlotInstance.transform.SetAsLastSibling();

        cursorSlot = cursorSlotInstance.GetComponent<CursorSlot>();
        cursorSlot.FillSlot(inventorySlot);

        cursorSlotInstance.transform.position = pointerEventData.pointerEnter.transform.position;
    }

    public void OnInitializePotentialDrag(PointerEventData pointerEventData)
    {
        pointerEventData.useDragThreshold = false;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        cursorSlotInstance.GetComponent<RectTransform>().anchoredPosition += pointerEventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        Destroy(cursorSlotInstance);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoldieDropHandler : MonoBehaviour, IDropHandler
{
    public MoldieInteraction moldieInteraction;

    public void OnDrop(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag != null)
        {
            Debug.Log("Slot dragged on!");

            Ingredient ingredient = pointerEventData.pointerDrag.GetComponent<InventorySlot>().slottedIngredient;

            moldieInteraction.FeedMoldie(ingredient);

            Destroy(FindObjectOfType<CursorSlot>().gameObject);
        }
    }
}

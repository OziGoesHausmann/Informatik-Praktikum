using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreserveTest : MonoBehaviour, IDropHandler
{
    public string station;

    public PreserveManager preserveManager;

    public void OnDrop(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag != null)
        {

            Debug.Log("Slot dragged on!");

            Ingredient ingredient = pointerEventData.pointerDrag.GetComponent<InventorySlot>().slottedIngredient;

            if (!ingredient.preserved)
            {
                preserveManager.Preserve(station, ingredient);
            }

            Destroy(FindObjectOfType<CursorSlot>().gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSlot : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public Ingredient slottedIngredient;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void FillSlot(InventorySlot draggedSlot)
    {
        slottedIngredient = draggedSlot.slottedIngredient;
    }
}
